using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class SelectionSortParallel : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            int comparisons = 0;
            int switches = 0;
            int threadsUsed = 0;
            Stopwatch time = new Stopwatch();

            var threadIdsUsed = new System.Collections.Concurrent.ConcurrentDictionary<int, bool>();

            time.Start();
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;

                int localComparisons = 0;

                Parallel.For(i + 1, n, j =>
                {
                    threadIdsUsed.TryAdd(Thread.CurrentThread.ManagedThreadId, true);

                    if (array[j] < array[minIdx])
                    {
                        lock (array)
                        {
                            if (array[j] < array[minIdx])
                            {
                                minIdx = j;
                            }
                        }
                    }
                    localComparisons++;
                });

                comparisons += localComparisons;

                if (minIdx != i)
                {
                    (array[i], array[minIdx]) = (array[minIdx], array[i]);
                    switches++;
                }
            }
            time.Stop();

            threadsUsed = threadIdsUsed.Count;

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}, Threads Usadas: {ThreadsUsadas}",
                "SelectionParallalel", time.Elapsed.Microseconds, comparisons, switches, n, threadsUsed);
        }
    }
}
