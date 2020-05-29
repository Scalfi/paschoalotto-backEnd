using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Paschoalotto.Middleware
{
    public class AutenticacaoMiddleware
    {
        private readonly RequestDelegate _next;

        public AutenticacaoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var keyValue = context.Request.Query["key"];

     
            await _next(context);

        }
    }
}