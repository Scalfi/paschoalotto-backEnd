using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paschoalotto.Models.Database;
using Paschoalotto.Repositories;

namespace Paschoalotto.Controllers
{
    [Route("api/[controller]")]

    public class BoletoController : BaseController
    {
        private readonly BoletoRepository _boletoRepository;
        public BoletoController(BoletoRepository boletoRepository)
        {
            _boletoRepository = boletoRepository;
        }

        [HttpPost]
        [Route("simulaboleto")]
        [Authorize(Roles = "colaborador")]
        public async Task<IActionResult> SimulaGeracaoBoleto([FromBody] GeraBoleto boleto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var simulacao = await _boletoRepository.SimulaGeracaoBoletoAsync(boleto);

                    if (simulacao != null)
                    {
                        return Ok(simulacao);
                    }

                    return InternalServerError();

                }
                catch
                {
                    return InternalServerError();

                }
            }


            return BadRequest(ModelState.IsValid);
        }

        [HttpPost]
        [Authorize(Roles = "colaborador")]
        public async Task<IActionResult> GeraBoleto([FromBody] List<Boleto> boletos)
        {
            if (ModelState.IsValid)
            {

                var novosBoletos = await _boletoRepository.GeracaoBoletoAsync(boletos);

                if (boletos != null)
                {
                    return Ok(novosBoletos);
                }

                return InternalServerError();
            }

            return BadRequest(ModelState.IsValid);

        }


        [HttpGet]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> PegaBoleto(string documento)
        {
            if (documento.Trim().Length > 0) 
            {
                var boletos = await _boletoRepository.PegaBoletosAsync(documento);
                if (boletos != null)
                {
                    return Ok(boletos.ToList());
                }

                return InternalServerError();
            }

            return BadRequest("É necessário enviar um documento.");
        }

        [HttpPut]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> PagaBoleto([FromBody] Boleto boleto)
        {
    
            var boletos = await _boletoRepository.PagaBoletosAsync(boleto);

            if (boletos != null)
            {
                return Ok(boletos);
            }

            return InternalServerError();
            

        }
    }
}