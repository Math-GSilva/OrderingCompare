using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class QuickSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int comparisons = 0;
            int swapCount = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            int left = 0;
            int right = array.Length - 1;

            if (left < right)
            {
                int pivotIndex = Partition(array, left, right, ref comparisons, ref swapCount);
                QuickSort(array, left, pivotIndex - 1, ref comparisons, ref swapCount);
                QuickSort(array, pivotIndex + 1, right, ref comparisons, ref swapCount);
            }

            stopwatch.Stop();

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}",
                "QuickSort", stopwatch.Elapsed.Microseconds, comparisons, swapCount, array.Length);
        }

        private void QuickSort(int[] array, int left, int right, ref int comparisons, ref int swapCount)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right, ref comparisons, ref swapCount);
                QuickSort(array, left, pivotIndex - 1, ref comparisons, ref swapCount);
                QuickSort(array, pivotIndex + 1, right, ref comparisons, ref swapCount);
            }
        }

        private int Partition(int[] array, int left, int right, ref int comparisons, ref int swapCount)
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
                    swapCount++;
                }
            }

            (array[i + 1], array[right]) = (array[right], array[i + 1]);
            swapCount++;

            return i + 1;
        }
    }
}
