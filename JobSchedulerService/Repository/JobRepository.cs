using JobSchedulerService.Models;
using System.Collections.Concurrent;

namespace JobSchedulerService.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly ConcurrentDictionary<string, Job> _jobResource = new();
        public string CreateOrUpdate(Job job)
        {
            _jobResource[job.Id] = job;
            return job.Id;
        }

        public Job? Get(string id)
        {
            if (_jobResource.TryGetValue(id, out var job))
                return job;
            return null;
        }

        public IEnumerable<Job> GetAll()
        {
            var jobs = new List<Job>();
            foreach (var item in _jobResource)
            {
                jobs.Add(item.Value);
            }
            return jobs;
        }
    }
}
