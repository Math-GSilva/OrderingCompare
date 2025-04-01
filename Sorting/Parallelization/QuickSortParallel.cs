using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class QuickSortParallel : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int comparisons = 0;
            int swapCount = 0;
            Stopwatch stopwatch = new Stopwatch();

            var threadIdsUsed = new ConcurrentDictionary<int, bool>();
            int threadsUsed = 0;

            stopwatch.Start();

            QuickSort(array, 0, array.Length - 1, out int localComparisons, out int localSwaps, threadIdsUsed);
            comparisons += localComparisons;
            swapCount += localSwaps;

            stopwatch.Stop();

            threadsUsed = threadIdsUsed.Count;

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}, Threads Usadas: {ThreadsUsadas}",
                "QuickSortParallel", stopwatch.Elapsed.Microseconds, comparisons, swapCount, array.Length, threadsUsed);
        }

        private void QuickSort(int[] array, int left, int right, out int comparisons, out int swapCount, ConcurrentDictionary<int, bool> threadIdsUsed)
        {
            comparisons = 0;
            swapCount = 0;

            if (left < right)
            {
                int pivotIndex = Partition(array, left, right, out int localComparisons, out int localSwaps);
                comparisons += localComparisons;
                swapCount += localSwaps;

                int leftComparisons = 0, leftSwaps = 0;
                int rightComparisons = 0, rightSwaps = 0;

                threadIdsUsed.TryAdd(Thread.CurrentThread.ManagedThreadId, true);

                Parallel.Invoke(
                    () => QuickSort(array, left, pivotIndex - 1, out leftComparisons, out leftSwaps, threadIdsUsed),
                    () => QuickSort(array, pivotIndex + 1, right, out rightComparisons, out rightSwaps, threadIdsUsed)
                );

                comparisons += leftComparisons + rightComparisons;
                swapCount += leftSwaps + rightSwaps;
            }
        }

        private int Partition(int[] array, int left, int right, out int comparisons, out int swapCount)
        {
            comparisons = 0;
            swapCount = 0;

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
