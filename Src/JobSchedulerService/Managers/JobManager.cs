using Hangfire;
using JobSchedulerService.Models;
using JobSchedulerService.Repository;
using JobSchedulerService.Services;

namespace JobSchedulerService.Managers
{
    public class JobManager : IJobManager
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IJobRepository _jobRepository;
        private readonly ISortingService _sortingService;
        private readonly ILogger<JobManager> _logger;

        public JobManager(IBackgroundJobClient backgroundJobClient, IJobRepository jobRepository, ISortingService sortingService, ILogger<JobManager> logger)
        {
            _backgroundJobClient = backgroundJobClient;
            _jobRepository = jobRepository;
            _sortingService= sortingService;
            _logger = logger;
        }

        public string CreateJob(int[] inputs)
        {
            var job = new Job()
            {
                Id = Guid.NewGuid().ToString("N"),
                InputNumbers = inputs,
                Status = JobStatus.Pending.ToString(),
                StartTimeStamp = DateTime.UtcNow,
            };
            _jobRepository.CreateOrUpdate(job);
            _logger.LogInformation("enqueu job invoking, a new job is created");

            _backgroundJobClient.Enqueue(() => ProcessAsync(job));

            return job.Id;

        }

        public IEnumerable<Job> GetAllJobs()
        {
            _logger.LogInformation("tring to retrive all jobs");
            return _jobRepository.GetAll();
        }

        public Job GetJob(string id)
        {
            _logger.LogInformation($"retrive job with id:{id}");
            var job= _jobRepository.Get(id);
            return job == null ? throw new KeyNotFoundException(id) : job;
        }

        public async Task ProcessAsync(Job job)
        {
            //TODO : lets wait a bit to test better, we can remove this later
            await Task.Delay(5000);
            var inputArray = new int[job.InputNumbers.Length];
            inputArray=job.InputNumbers.Select(x=>x).ToArray();

            var sortedArray = _sortingService.Sort(job.InputNumbers);
            _logger.LogInformation("create job invoking");
            try
            {
                var updatedJob = new Job()
                {
                    Id = job.Id,
                    InputNumbers = inputArray,
                    StartTimeStamp = job.StartTimeStamp,
                    OutPutNumbers = sortedArray,
                    Status = JobStatus.Completed.ToString(),
                    ExecuteDuration = DateTime.UtcNow.Subtract(job.StartTimeStamp)
                };

                _jobRepository.CreateOrUpdate(updatedJob);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error during processing job {job.Id}");
            }

        }
    }
}
