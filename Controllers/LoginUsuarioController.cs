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

    public class LoginUsuarioController : BaseController
    {
        private readonly UsuarioRepository _usuarioRepository;
        public LoginUsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UsuarioLogin login)
        {
            try
            {

                var usuario = await _usuarioRepository.usuarioLoginAsync(login);

                if (usuario == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken(usuario);

                return Ok(new
                {
                    usuario.Role,
                    usuario.Usuario,
                    usuario.Documento,
                    usuario.Email,
                    usuario.Nome,
                    token
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);

                return InternalServerError();
            }
        }

    }
}