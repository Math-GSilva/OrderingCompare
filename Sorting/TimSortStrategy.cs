using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class TimSortStrategy : ISortingStrategy
    {
        private const int RUN = 32;

        public void Sort(int[] array)
        {
            int comparisons = 0;
            int swapCount = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            TimSort(array, array.Length, ref comparisons, ref swapCount);
            stopwatch.Stop();

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao}ms, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}",
                "TimSort", stopwatch.Elapsed.Microseconds, comparisons, swapCount, array.Length);
        }

        private void TimSort(int[] array, int n, ref int comparisons, ref int swapCount)
        {
            for (int i = 0; i < n; i += RUN)
                InsertionSort(array, i, Math.Min((i + RUN - 1), (n - 1)), ref comparisons, ref swapCount);

            for (int size = RUN; size < n; size *= 2)
            {
                for (int left = 0; left < n; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min((left + 2 * size - 1), (n - 1));

                    if (mid < right)
                        Merge(array, left, mid, right, ref comparisons, ref swapCount);
                }
            }
        }

        private void InsertionSort(int[] array, int left, int right, ref int comparisons, ref int swapCount)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = array[i];
                int j = i - 1;

                while (j >= left && array[j] > temp)
                {
                    comparisons++;
                    array[j + 1] = array[j];
                    j--;
                    swapCount++;
                }
                array[j + 1] = temp;
                swapCount++;
            }
        }

        private void Merge(int[] array, int left, int mid, int right, ref int comparisons, ref int swapCount)
        {
            int len1 = mid - left + 1, len2 = right - mid;

            Span<int> leftArray = stackalloc int[len1];
            Span<int> rightArray = stackalloc int[len2];

            array.AsSpan(left, len1).CopyTo(leftArray);
            array.AsSpan(mid + 1, len2).CopyTo(rightArray);

            int i = 0, j = 0, k = left;

            while (i < len1 && j < len2)
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
                }
                k++;
                swapCount++;
            }

            while (i < len1)
            {
                array[k++] = leftArray[i++];
                swapCount++;
            }

            while (j < len2)
            {
                array[k++] = rightArray[j++];
                swapCount++;
            }
        }
    }
}
