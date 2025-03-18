using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class MergeSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int comparisons = 0;
            int switchs = 0;
            Stopwatch time = new Stopwatch();

            time.Start();
            MergeSort(array, 0, array.Length - 1, ref comparisons, ref switchs);
            time.Stop();

            Console.WriteLine($"Tempo de execução: {time.ElapsedMilliseconds} ms");
            Console.WriteLine($"Quantidade de comparações: {comparisons}");
            Console.WriteLine($"Quantidade de trocas: {switchs}");
        }

        private void MergeSort(int[] array, int left, int right, ref int comparisons, ref int switchs)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;

                MergeSort(array, left, mid, ref comparisons, ref switchs);
                MergeSort(array, mid + 1, right, ref comparisons, ref switchs);

                Merge(array, left, mid, right, ref comparisons, ref switchs);
            }
        }

        private void Merge(int[] array, int left, int mid, int right, ref int comparisons, ref int switchs)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            Array.Copy(array, left, leftArray, 0, n1);
            Array.Copy(array, mid + 1, rightArray, 0, n2);

            int i = 0, j = 0, k = left;
            while (i < n1 && j < n2)
            {
                comparisons++;
                if (leftArray[i] <= rightArray[j])
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                    switchs++;
                }
                k++;
            }

            while (i < n1)
            {
                array[k] = leftArray[i];
                i++;
                k++;
                switchs++;
            }

            while (j < n2)
            {
                array[k] = rightArray[j];
                j++;
                k++;
                switchs++;
            }
        }
    }
}
