using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Paschoalotto.Models.Database;
using System.Security.Cryptography;
using System;
using Paschoalotto.Models;

namespace Paschoalotto.Repositories
{
    public sealed class ColaboradorRepository
    {
        private readonly DatabaseContext db;
        public ColaboradorRepository(DatabaseContext context)
        {
            db = context;
        }
        public async Task<ColaboradorLogin> ColaboradorLoginAsync(ColaboradorLogin usuario)
        {

            return await db.ColaboradorLogin
                .Where(u => u.Usuario.Equals(usuario.Usuario)
                && u.Senha.Equals(usuario.Senha))
                .FirstOrDefaultAsync();

        }
    }
}