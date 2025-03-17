namespace OrderingCompare.Domain.Interfaces
{
    public interface INumbersGeneratorService
    {
        void GerarArquivo(int quantidade, string tipoArquivo);
        int[] LerArquivo();
        int[] OrdenarNumeros(int[] numeros, ISortingStrategy sortingStrategy);
    }
}
