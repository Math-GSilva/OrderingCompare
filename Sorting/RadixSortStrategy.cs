using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class RadixSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int comparisons = 0;
            int swapCount = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            if (array.Length > 0)
            {
                int max = array[0];
                foreach (int num in array)
                {
                    comparisons++;
                    if (num > max)
                    {
                        max = num;
                    }
                }

                int exp = 1;
                int[] output = new int[array.Length];

                while (max / exp > 0)
                {
                    int[] count = new int[10];

                    foreach (int num in array)
                    {
                        int digit = (num / exp) % 10;
                        count[digit]++;
                        comparisons++;
                    }

                    for (int i = 1; i < 10; i++)
                    {
                        count[i] += count[i - 1];
                        comparisons++;
                    }

                    for (int i = array.Length - 1; i >= 0; i--)
                    {
                        int num = array[i];
                        int digit = (num / exp) % 10;
                        output[count[digit] - 1] = num;
                        count[digit]--;
                        swapCount++;
                    }

                    Array.Copy(output, array, array.Length);
                    exp *= 10;
                }
            }

            stopwatch.Stop();

            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}",
                "RadixSort", stopwatch.Elapsed.Microseconds, comparisons, swapCount, array.Length);
        }
    }
}
