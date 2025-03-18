using System.Diagnostics;
using OrderingCompare.Domain.Interfaces;

namespace OrderingCompare.Sorting
{
    public class InsertionSortStrategy : ISortingStrategy
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            int comparisons = 0;
            int switches = 0;
            Stopwatch time = new Stopwatch();
            
            time.Start();
            for (int i = 1; i < n; ++i)
            {
                int key = array[i];
                int j = i - 1;
                
                while (j >= 0)
                {
                    comparisons++;
                    if (array[j] > key)
                    {
                        array[j + 1] = array[j];
                        switches++;
                        j = j - 1;
                    }
                    else
                    {
                        break;
                    }
                }
                array[j + 1] = key;
            }
            time.Stop();
            
            Console.WriteLine($"Tempo de execução: {time.ElapsedMilliseconds} ms");
            Console.WriteLine($"Quantidade de comparações: {comparisons}");
            Console.WriteLine($"Quantidade de trocas: {switches}");
        }
    }
}