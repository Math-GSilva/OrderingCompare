using Microsoft.AspNetCore.Mvc;
using OrderingCompare.Domain.Interfaces;
using System.Text;

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
    }
}
