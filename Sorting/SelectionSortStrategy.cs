using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class SelectionSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            int comparisons = 0;
            int switches = 0;
            Stopwatch time = new Stopwatch();
            
            time.Start();
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    comparisons++;
                    if (array[j] < array[minIdx])
                        minIdx = j;
                }
                
                if (minIdx != i)
                {
                    int temp = array[minIdx];
                    array[minIdx] = array[i];
                    array[i] = temp;
                    switches++;
                }
            }
            time.Stop();

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}",
                "Selection", time.Elapsed.Microseconds, comparisons, switches, n);
        }
    }
}