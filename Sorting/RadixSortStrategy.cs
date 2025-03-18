using System;
using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class RadixSortStrategy : ISortingStrategy
    {
        public int[] Sort(int[] array)
        {
            int comparisons = 0; 
            int swapCount = 0;   
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            int[] sortedArray = RadixSort(array, ref swapCount);
            stopwatch.Stop();

            Console.WriteLine($"Tempo de execução: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Quantidade de comparações: {comparisons} (Radix Sort não faz comparações diretas)");
            Console.WriteLine($"Quantidade de trocas/redistribuições: {swapCount}");

            return sortedArray;
        }

        private int[] RadixSort(int[] array, ref int swapCount)
        {
            if (array.Length == 0)
                return array;

            int max = GetMax(array);
            int exp = 1; 
            int[] output = new int[array.Length];

            while (max / exp > 0)
            {
                CountSortByDigit(array, exp, ref swapCount);  
                exp *= 10; 
            }

            return array;
        }

        private void CountSortByDigit(int[] array, int exp, ref int swapCount)
        {
            int[] output = new int[array.Length];
            int[] count = new int[10];

            foreach (int num in array)
            {
                int digit = (num / exp) % 10;
                count[digit]++;
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
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
        }

        private int GetMax(int[] array)
        {
            int max = array[0];
            foreach (int num in array)
            {
                if (num > max)
                {
                    max = num;
                }
            }
            return max;
        }
    }
}