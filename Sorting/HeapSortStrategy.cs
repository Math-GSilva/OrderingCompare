using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class HeapSortStrategy : ISortingStrategy
    {
        public int[] Sort(int[] array)
        {
            int comparisons = 0;
            int swapCount = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            HeapSort(array, ref comparisons, ref swapCount);
            stopwatch.Stop();

            Console.WriteLine($"Tempo de execução: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Quantidade de comparações: {comparisons}");
            Console.WriteLine($"Quantidade de trocas: {swapCount}");

            return array;
        }

        private void HeapSort(int[] array, ref int comparisons, ref int swapCount)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i, ref comparisons, ref swapCount);

            for (int i = n - 1; i > 0; i--)
            {
                Swap(ref array[0], ref array[i]);
                swapCount++;

                Heapify(array, i, 0, ref comparisons, ref swapCount);
            }
        }

        private void Heapify(int[] array, int n, int i, ref int comparisons, ref int swapCount)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n)
            {
                comparisons++;
                if (array[left] > array[largest])
                    largest = left;
            }

            if (right < n)
            {
                comparisons++;
                if (array[right] > array[largest])
                    largest = right;
            }

            if (largest != i)
            {
                Swap(ref array[i], ref array[largest]);
                swapCount++;

                Heapify(array, n, largest, ref comparisons, ref swapCount);
            }
        }

        private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}