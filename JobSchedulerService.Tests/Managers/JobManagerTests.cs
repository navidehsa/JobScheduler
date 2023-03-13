using Hangfire;
using Hangfire.States;
using JobSchedulerService.Managers;
using JobSchedulerService.Models;
using JobSchedulerService.Repository;
using JobSchedulerService.Services;
using Microsoft.Extensions.Logging;
using Moq;


namespace JobSchedulerService.Tests.Managers
{
    public class JobManagerTests
    {
        private readonly Mock<IBackgroundJobClient> _backgroundJobClientMock;
        private readonly Mock<IJobRepository> _jobRepositoryMock;
        private readonly Mock<ISortingService> _sortingServiceMock;
        private readonly Mock<ILogger<JobManager>> _loggerMock;
        private readonly JobManager _jobManager;

        public JobManagerTests()
        {
            _backgroundJobClientMock = new Mock<IBackgroundJobClient>();
            _jobRepositoryMock = new Mock<IJobRepository>();
            _sortingServiceMock = new Mock<ISortingService>();
            _loggerMock = new Mock<ILogger<JobManager>>();

            _jobManager = new JobManager(
                _backgroundJobClientMock.Object,
                _jobRepositoryMock.Object,
                _sortingServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public void CreateJob_ShouldCreateNewJob()
        {
            // Arrange
            var inputs = new int[] { 3, 1, 4, 2 };
            var expectedJobId = "123";
            var expectedJobStatus = JobStatus.Pending.ToString();

            _jobRepositoryMock
                .Setup(x => x.CreateOrUpdate(It.IsAny<Job>()))
                .Callback<Job>(job => job.Id = expectedJobId);

            // Act
            var result = _jobManager.CreateJob(inputs);

            // Assert
            Assert.Equal(expectedJobId, result);

            _jobRepositoryMock.Verify(x => x.CreateOrUpdate(It.IsAny<Job>()), Times.Once);

            _backgroundJobClientMock.Verify(x => x.Create(
               It.Is<Hangfire.Common.Job>(job => job.Method.Name == "ProcessAsync"), It.IsAny<EnqueuedState>()),Times.Once);

            _jobRepositoryMock.Verify(x =>
                x.CreateOrUpdate(It.Is<Job>(job => job.Status == expectedJobStatus)),
                Times.Once);
        }

        [Fact]
        public void GetAllJobs_ShouldReturnAllJobs()
        {
            // Arrange
            var expectedJobs = new Job[]
            {
            new Job { Id = "1", Status = JobStatus.Pending.ToString() },
            new Job { Id = "2", Status = JobStatus.Pending.ToString() },
            new Job { Id = "3", Status = JobStatus.Completed.ToString() }
            };

            _jobRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(expectedJobs);

            // Act
            var result = _jobManager.GetAllJobs();

            // Assert
            Assert.Equal(expectedJobs, result);
        }

        [Fact]
        public void GetJob_WithValidId_ShouldReturnJob()
        {
            // Arrange
            var jobId = "123";
            var expectedJob = new Job { Id = jobId };

            _jobRepositoryMock
                .Setup(x => x.Get(jobId))
                .Returns(expectedJob);

            // Act
            var result = _jobManager.GetJob(jobId);

            // Assert
            Assert.Equal(expectedJob, result);
        }

        [Fact]
        public void GetJob_WithInvalidId_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var jobId = "123";

            _jobRepositoryMock
                .Setup(x => x.Get(jobId))
                .Returns((Job)null);

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => _jobManager.GetJob(jobId));
            Assert.Equal(jobId, exception.Message);
        }

    }
}
