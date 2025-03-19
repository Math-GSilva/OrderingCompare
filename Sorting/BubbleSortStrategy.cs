using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class BubbleSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            int comparisons = 0;
            int switchs = 0;
            Stopwatch time = new Stopwatch();
            
            time.Start();
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    comparisons++;
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        switchs++;
                    }
                }
            }
            time.Stop();

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}",
                "BubbleSort", time.Elapsed.Microseconds, comparisons, switchs, n);
        }
    }
}