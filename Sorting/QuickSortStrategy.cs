using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class QuickSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int comparisons = 0;
            int switchs = 0;
            Stopwatch time = new Stopwatch();

            time.Start();
            QuickSort(array, 0, array.Length - 1, ref comparisons, ref switchs);
            time.Stop();

            Console.WriteLine($"Tempo de execução: {time.ElapsedMilliseconds} ms");
            Console.WriteLine($"Quantidade de comparações: {comparisons}");
            Console.WriteLine($"Quantidade de trocas: {switchs}");
        }

        private void QuickSort(int[] array, int left, int right, ref int comparisons, ref int switchs)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right, ref comparisons, ref switchs);
                QuickSort(array, left, pivotIndex - 1, ref comparisons, ref switchs);
                QuickSort(array, pivotIndex + 1, right, ref comparisons, ref switchs);
            }
        }

        private int Partition(int[] array, int left, int right, ref int comparisons, ref int switchs)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                comparisons++;
                if (array[j] < pivot)
                {
                    i++;
                    (array[i], array[j]) = (array[j], array[i]);
                    switchs++;
                }
            }
            (array[i + 1], array[right]) = (array[right], array[i + 1]);
            switchs++;
            return i + 1;
        }
    }
}