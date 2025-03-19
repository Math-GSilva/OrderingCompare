using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class MergeSortStrategy : ISortingStrategy
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
                int mid = left + (right - left) / 2;

                if (left < mid)
                {
                    MergeSort(array, left, mid, ref comparisons, ref swapCount);
                }
                if (mid + 1 < right)
                {
                    MergeSort(array, mid + 1, right, ref comparisons, ref swapCount);
                }

                Merge(array, left, mid, right, ref comparisons, ref swapCount);
            }

            stopwatch.Stop();

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}",
                "MergeSort", stopwatch.Elapsed.Microseconds, comparisons, swapCount, array.Length);
        }

        private void MergeSort(int[] array, int left, int right, ref int comparisons, ref int swapCount)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;

                MergeSort(array, left, mid, ref comparisons, ref swapCount);
                MergeSort(array, mid + 1, right, ref comparisons, ref swapCount);

                Merge(array, left, mid, right, ref comparisons, ref swapCount);
            }
        }

        private void Merge(int[] array, int left, int mid, int right, ref int comparisons, ref int swapCount)
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
                    swapCount++; // Contabilizando as trocas
                }
                k++;
            }

            // Copiando os elementos restantes
            while (i < n1)
            {
                array[k] = leftArray[i];
                i++;
                k++;
                swapCount++;
            }

            while (j < n2)
            {
                array[k] = rightArray[j];
                j++;
                k++;
                swapCount++;
            }
        }
    }
}
