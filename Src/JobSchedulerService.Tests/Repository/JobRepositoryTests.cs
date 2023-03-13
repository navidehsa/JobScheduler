
using JobSchedulerService.Models;
using JobSchedulerService.Repository;

namespace JobSchedulerService.Tests.Repository
{
    public class JobRepositoryTests
    {
        private readonly IJobRepository _jobRepository;

        public JobRepositoryTests()
        {
            _jobRepository = new JobRepository();
        }

        [Fact]
        public void CreateOrUpdate_AddsJobToResourceAndReturnsId()
        {
            // Arrange
            var job = new Job { Id = "job-1", InputNumbers = new int[] { 1, 8, 5, 7, 9 }, Status = JobStatus.Pending.ToString() };

            // Act
            var result = _jobRepository.CreateOrUpdate(job);

            // Assert
            Assert.Equal("job-1", result);
            Assert.Single(_jobRepository.GetAll());
            Assert.Equal(job, _jobRepository.Get("job-1"));
        }

        [Fact]
        public void Get_ReturnsNullIfJobNotFound()
        {
            // Act
            var result = _jobRepository.Get("job-1");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAll_ReturnsAllJobs()
        {
            // Arrange
            var job1 = new Job { Id = "job-1", InputNumbers = new int[] { 1, 8, 5, 7, 9 }, Status = JobStatus.Completed.ToString() };
            var job2 = new Job { Id = "job-2", InputNumbers = new int[] { 5, 8, 3, 7, 9 }, Status = JobStatus.Pending.ToString() };
            _jobRepository.CreateOrUpdate(job1);
            _jobRepository.CreateOrUpdate(job2);

            // Act
            var result = _jobRepository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(job1, result);
            Assert.Contains(job2, result);
        }
    }
}
