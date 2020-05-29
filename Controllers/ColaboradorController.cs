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

    public class ColaboradorController : BaseController
    {
        private readonly ColaboradorRepository _colaboradoRepository;
        public ColaboradorController(ColaboradorRepository colaboradoRepository)
        {
            _colaboradoRepository = colaboradoRepository;
        }
        
       

    }
}