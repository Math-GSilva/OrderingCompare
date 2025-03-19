using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;
using Serilog;

namespace OrderingCompare.Sorting
{
    public class CountingSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int comparisons = 0;
            int swapCount = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            if (array.Length == 0)
                return;

            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                comparisons++;
                if (array[i] > max)
                    max = array[i];
            }

            int[] countArray = new int[max + 1];

            foreach (int num in array)
            {
                countArray[num]++;
            }

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
            Log.Information("Ordenação concluída pelo algoritmo {Algoritmo} em {TempoExecucao} microssegundos, Comparações: {Comparacoes}, Trocas: {Trocas}, Tamanho do Array: {TamanhoArray}",
                "Counting", stopwatch.Elapsed.Microseconds, comparisons, swapCount, array.Length);
        }
    }
}
