namespace JobSchedulerService.Models
{
    public enum JobStatus
    {
        /// <summary>
        /// The status of a job when execution is not complete yet
        /// </summary>
        Pending,
        /// <summary>
        /// The status of a job when execution is completed succefully
        /// </summary>
        Completed,
    }
}
