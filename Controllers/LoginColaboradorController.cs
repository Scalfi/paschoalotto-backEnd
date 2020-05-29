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

    public class LoginColaboradorController : BaseController
    {
        private readonly ColaboradorRepository _colaboradorRepository;
        public LoginColaboradorController(ColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] ColaboradorLogin login)
        {
            try
            {

                var usuario = await _colaboradorRepository.ColaboradorLoginAsync(login);

                if (usuario == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken(usuario);

                return Ok(new
                {
                    usuario.Role,
                    usuario.Usuario,
                    token
                });
            }
            catch (Exception e)
            {

                return InternalServerError();
            }
        }

    }
}