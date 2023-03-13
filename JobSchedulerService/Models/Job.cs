namespace JobSchedulerService.Models
{
    /// <summary>
    /// Represent a job which proceed in backgroung, a job takes an unsorted array of numbers as input, sorts the input
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Represent a unique identifier assigned by the application to identify each job
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Represent an unsorted array of numbers provided as input to the job
        /// </summary>
        public int[] InputNumbers { get; set; }

        /// <summary>
        /// Represent an sorted array of numbers provided as input to the job
        /// </summary>
        public int[] OutPutNumbers { get; set; }

        /// <summary>
        /// Represent the timestamp when the job started executing
        /// </summary>
        public DateTime StartTimeStamp { get; set; }

        /// <summary>
        /// Represent the amount of time that take to execute the job
        /// </summary>
        public TimeSpan? ExecuteDuration { get; set; }

        /// <summary>
        /// Represent the status of the job
        /// </summary>
        public string Status { get; set; } = JobStatus.Pending.ToString();
    }
}
