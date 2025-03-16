using OrderingCompare.Domain.Interfaces;
using System.Text;

namespace OrderingCompare.Domain.Services
{
    public class NumbersGeneratorService : INumbersGeneratorService
    {
        private const string CaminhoArquivo = "dados_numericos";

        public void GerarArquivo(int quantidade, string tipoArquivo)
        {
            var numeros = Enumerable.Range(0, quantidade).Select(_ => new Random().Next(1, 1000));
            string caminhoCompleto = $"{CaminhoArquivo}.{tipoArquivo}";

            using (var writer = new StreamWriter(caminhoCompleto, false, Encoding.UTF8))
            {
                writer.WriteLine(string.Join(tipoArquivo == "csv" ? "," : "\n", numeros));
            }
        }

        public string LerArquivo()
        {
            if (!File.Exists($"{CaminhoArquivo}.csv")) return "Arquivo não encontrado.";
            return File.ReadAllText($"{CaminhoArquivo}.csv");
        }
    }
}
