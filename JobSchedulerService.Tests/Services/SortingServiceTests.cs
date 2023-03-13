using JobSchedulerService.Services;

namespace JobSchedulerService.Tests.Services
{
    public class SortingServiceTests
    {
        private readonly ISortingService _sortingService;

        public SortingServiceTests()
        {
            _sortingService = new SortingService();
        }

        [Fact]
        public void Sort_ShouldReturnSortedArray_WhenInputIsUnsorted()
        {
            // Arrange
            int[] input = new int[] { 3, 2, 1 };

            // Act
            int[] output = _sortingService.Sort(input);

            // Assert
            Assert.Equal(new int[] { 1, 2, 3 }, output);
        }
    }
}
