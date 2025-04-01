using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class CountingSortParallel : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int comparisons = 0;
            int swapCount = 0;
            Stopwatch stopwatch = new Stopwatch();

            var threadIdsUsed = new System.Collections.Concurrent.ConcurrentDictionary<int, bool>();
            int threadsUsed = 0;

            stopwatch.Start();

            if (array.Length == 0)
                return;

            int max = array[0];
            Parallel.For(1, array.Length, i =>
            {
                int localMax = array[i];
                if (localMax > max)
                {
                    lock (array)
                    {
                        if (localMax > max)
                        {
                            max = localMax;
                        }
                    }
                }

                threadIdsUsed.TryAdd(Thread.CurrentThread.ManagedThreadId, true);
            });

            int[] countArray = new int[max + 1];
            Parallel.ForEach(array, num =>
            {
                System.Threading.Interlocked.Increment(ref countArray[num]);

                threadIdsUsed.TryAdd(Thread.CurrentThread.ManagedThreadId, true);
            });

            int index = 0;
            for (int i = 0; i < countArray.Length; i++)
            {
                while (countArray[i] > 0)
                {
                    comparisons++;
                    array[index++] = i;
                    countArray[i]--;
                    swapCount++;
                }
            }

            stopwatch.Stop();

            threadsUsed = threadIdsUsed.Count;

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}, Threads Usadas: {ThreadsUsadas}",
                "CountingParallel", stopwatch.Elapsed.Microseconds, comparisons, swapCount, array.Length, threadsUsed);
        }
    }
}
