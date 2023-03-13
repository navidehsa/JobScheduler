namespace JobSchedulerService.Services
{
    public class SortingService : ISortingService
    {
        public int[] Sort(int[] input)
        {
            Array.Sort(input);
            return input;
        }
    }
}
