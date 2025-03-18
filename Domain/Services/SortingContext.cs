using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Domain.Services
{
    public class SortingContext
    {
        private readonly ISortingStrategy _sortingStrategy;

        public SortingContext(ISortingStrategy sortingStrategy)
        {
            _sortingStrategy = sortingStrategy;
        }

        public void Sort(int[] array)
        {
            _sortingStrategy.Sort(array);
        }
    }
}