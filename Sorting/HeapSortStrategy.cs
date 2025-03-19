using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class HeapSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int comparisons = 0;
            int swapCount = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(array, n, i, ref comparisons, ref swapCount);
            }

            for (int i = n - 1; i > 0; i--)
            {
                Swap(ref array[0], ref array[i]);
                swapCount++;

                Heapify(array, i, 0, ref comparisons, ref swapCount);
            }

            stopwatch.Stop();

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}",
                "HeapSort", stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000), comparisons, swapCount, array.Length);
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
