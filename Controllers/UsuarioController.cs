using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paschoalotto.Models;
using Paschoalotto.Models.Database;
using Paschoalotto.Repositories;
using Paschoalotto.Services;

namespace Paschoalotto.Controllers
{
    [Route("api/[controller]")]

    public class UsuarioController : BaseController
    {
        private readonly UsuarioRepository _usuarioRepository;
        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> CadastroAsync([FromBody] UsuarioLogin newUsuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existeUsuario = await _usuarioRepository.existeUsuarioAsync(newUsuario);

                    if (existeUsuario == null)
                    {
                        var usuario = await _usuarioRepository.CriarUsuarioAsync(newUsuario);

                        if (usuario != null)
                        {
                            usuario.Senha = "";
                            return Created($"{HttpContext.Request.Host}{HttpContext.Request.Path}/{usuario.Id}", usuario);
                        }

                    }

                    return BadRequest("Usúario já cadastrado!");


                }

                return BadRequest(ModelState.IsValid);

            }
            catch (Exception e)
            {
                return InternalServerError();
            }

        }


    }
}