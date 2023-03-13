using JobSchedulerService.Models;

namespace JobSchedulerService.Managers
{
    public interface IJobManager
    {
        string CreateJob(int[] inputs);
        IEnumerable<Job> GetAllJobs();
        Job GetJob(string id);
    }
}
