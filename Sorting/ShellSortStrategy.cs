using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class ShellSortStrategy : ISortingStrategy
    {
        public int[] Sort(int[] array)
        {
            int comparisons = 0;  
            int swapCount = 0;   
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            int[] sortedArray = ShellSort(array, ref comparisons, ref swapCount);
            stopwatch.Stop();

            Console.WriteLine($"Tempo de execução: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Quantidade de comparações: {comparisons}");
            Console.WriteLine($"Quantidade de trocas: {swapCount}");

            return sortedArray;
        }

        private int[] ShellSort(int[] array, ref int comparisons, ref int swapCount)
        {
            int n = array.Length;
            int gap = n / 2; 

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = array[i];
                    int j = i;

                    while (j >= gap && array[j - gap] > temp)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                        comparisons++;  
                        swapCount++;   
                    }
                    array[j] = temp;
                }

                gap /= 2; 
            }

            return array;
        }
    }
}
