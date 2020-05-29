using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paschoalotto.Models.Database;
using Paschoalotto.Repositories;

namespace Paschoalotto.Controllers
{
    [Route("api/[controller]")]
    public class ContratoController : BaseController
    {
        private readonly UsuarioRepository _usuarioRepository;

        private readonly ContratoRepository _contratoRepository;
        public ContratoController(ContratoRepository contratoRepository, UsuarioRepository usuarioRepository)
        {
            _contratoRepository = contratoRepository;
            _usuarioRepository = usuarioRepository;
        }
       
        [HttpGet]
        [Authorize(Roles = "colaborador")]
        public async Task<IActionResult> PegaContratoAsync(UsuarioLogin usuario)
        {
            try
            {

           
                var cliente = await _usuarioRepository.existeUsuarioAsync(usuario);

                if (cliente == null)
                    return NotFound("Cliente não existe");

                var consulta = await _contratoRepository.PegaContratosAtivosAsync(cliente.Documento);
                            
                return Ok(consulta);
            }
            catch(Exception e)
            {
                return InternalServerError();

            }
        }
    }
}