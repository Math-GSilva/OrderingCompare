using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class SelectionSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int n = array.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                    if (array[j] < array[minIdx])
                        minIdx = j;

                int temp = array[minIdx];
                array[minIdx] = array[i];
                array[i] = temp;
            }
        }
    }
}