using JobSchedulerService.Managers;
using JobSchedulerService.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobManager _jobManager;

        public JobsController(IJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        [HttpPost]
        [SwaggerOperation(
        Summary = "Enqueue and create a new job, by providing an unsorted array of numbers as input.",
        OperationId = "CreateJob"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(string))]
        public IActionResult CreateJob(int[] input)
        {
            var jobId = _jobManager.CreateJob(input);
            return Ok(jobId);
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Fetches all enqueued job.",
        OperationId = "GetAllJobs"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IEnumerable<Job>))]
        public IActionResult GetAllJobs()
        {
            var jobs = _jobManager.GetAllJobs();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
        Summary = "Fetches information about a specific job and returns the job status and metadata.",
        OperationId = "GetJob"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(Job))]
        public IActionResult GetJob(string id)
        {
            var job = _jobManager.GetJob(id);
            return Ok(job);
        }
    }
}
