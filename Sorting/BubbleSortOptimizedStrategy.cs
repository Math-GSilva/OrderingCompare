using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class BubbleSortOptimizedStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }
    }
}