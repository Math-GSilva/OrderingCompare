using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class CountingSortStrategy : ISortingStrategy
    {
        public int[] Sort(int[] array)
        {
            int comparisons = 0;  
            int swapCount = 0;    
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            int[] sortedArray = CountingSort(array, ref swapCount);
            stopwatch.Stop();

            Console.WriteLine($"Tempo de execução: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Quantidade de comparações: {comparisons} (Counting Sort não faz comparações diretas)");
            Console.WriteLine($"Quantidade de trocas/redistribuições: {swapCount}");

            return sortedArray;
        }

        private int[] CountingSort(int[] array, ref int swapCount)
        {
            if (array.Length == 0)
                return array;

            int max = array[0];
            foreach (int num in array)
            {
                if (num > max)
                    max = num;
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
                    array[index++] = i;
                    countArray[i]--;
                    swapCount++; 
                }
            }

            return array;
        }
    }
}
