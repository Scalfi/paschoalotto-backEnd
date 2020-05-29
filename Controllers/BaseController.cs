using Microsoft.AspNetCore.Mvc;

namespace Paschoalotto.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController()
        {

        }

        protected ObjectResult InternalServerError()
        {
            return new ObjectResult("Ocorreu um erro, tente novamente mais tarde ou se persistir contate o desenvolvimento do sistema.")
            {
                StatusCode = 500,
            };
        }

    }
}