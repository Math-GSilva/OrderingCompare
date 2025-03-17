using Microsoft.AspNetCore.Mvc;
using OrderingCompare.Domain.Interfaces;
using OrderingCompare.Sorting;

namespace OrderingCompare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersGeneratorController : ControllerBase
    {
        private readonly INumbersGeneratorService _numeroService;

        public NumbersGeneratorController(INumbersGeneratorService numeroService)
        {
            _numeroService = numeroService;
        }

        [HttpPost("gerar")]
        public IActionResult GerarNumeros([FromQuery] int quantidade, [FromQuery] string tipoArquivo = "csv")
        {
            _numeroService.GerarArquivo(quantidade, tipoArquivo);
            return Ok("Arquivo gerado com sucesso.");
        }

        [HttpGet("ler")]
        public IActionResult LerNumeros()
        {
            var conteudo = _numeroService.LerArquivo();
            return Ok(conteudo);
        }

        [HttpPost("ordenar")]
        public IActionResult OrdenarNumeros([FromQuery] string algoritmo)
        {
            var numeros = _numeroService.LerArquivo();
            ISortingStrategy sortingStrategy = algoritmo.ToLower() switch
            {
                "bubble" => new BubbleSortStrategy(),
                "bubble-optimized" => new BubbleSortOptimizedStrategy(),
                "insertion" => new InsertionSortStrategy(),
                "selection" => new SelectionSortStrategy(),
                _ => throw new ArgumentException("Algoritmo de ordenação desconhecido")
            };

            var numerosOrdenados = _numeroService.OrdenarNumeros(numeros, sortingStrategy);
            return Ok(numerosOrdenados);
        }
    }
}