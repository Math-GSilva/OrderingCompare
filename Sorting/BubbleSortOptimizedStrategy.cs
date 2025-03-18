using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class BubbleSortOptimizedStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            bool switched;
            int comparisons = 0;
            int switches = 0;
            Stopwatch time = new Stopwatch();

            time.Start();
            for (int i = 0; i < n - 1; i++)
            {
                switched = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    comparisons++;
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        switches++;
                        switched = true;
                    }
                }
                if (!switched)
                    break;
            }
            time.Stop();

            Console.WriteLine($"Tempo de execução: {time.ElapsedMilliseconds} ms");
            Console.WriteLine($"Quantidade de comparações: {comparisons}");
            Console.WriteLine($"Quantidade de trocas: {switches}");
        }
    }
}