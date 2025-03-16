namespace OrderingCompare.Domain.Interfaces
{
    public interface INumbersGeneratorService
    {
        void GerarArquivo(int quantidade, string tipoArquivo);
        string LerArquivo();
    }
}
