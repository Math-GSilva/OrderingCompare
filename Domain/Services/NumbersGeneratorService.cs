using OrderingCompare.Domain.Interfaces;
using System.Text;

namespace OrderingCompare.Domain.Services
{
    public class NumbersGeneratorService : INumbersGeneratorService
    {
        private readonly string _filePath = "numeros.txt";

        public void GerarArquivo(int quantidade, string tipoArquivo)
        {
            var random = new Random();
            var numeros = new int[quantidade];
            for (int i = 0; i < quantidade; i++)
            {
                numeros[i] = random.Next(0, 10000);
            }

            if (tipoArquivo == "csv")
            {
                File.WriteAllText(_filePath, string.Join(",", numeros));
            }
            else
            {
                using (var writer = new BinaryWriter(File.Open(_filePath, FileMode.Create)))
                {
                    foreach (var numero in numeros)
                    {
                        writer.Write(numero);
                    }
                }
            }
        }

        public int[] LerArquivo()
        {
            var conteudo = File.ReadAllText(_filePath);
            var numeros = Array.ConvertAll(conteudo.Split(','), int.Parse);
            return numeros;
        }

        public int[] OrdenarNumeros(int[] numeros, ISortingStrategy sortingStrategy)
        {
            var sortingContext = new SortingContext(sortingStrategy);
            sortingContext.Sort(numeros);
            return numeros;
        }
    }
}
