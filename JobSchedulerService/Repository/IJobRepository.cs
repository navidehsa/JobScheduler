using JobSchedulerService.Models;

namespace JobSchedulerService.Repository
{
    public interface IJobRepository
    {
        string CreateOrUpdate(Job job);
        Job? Get(string id);
        IEnumerable<Job> GetAll();
    }
}
