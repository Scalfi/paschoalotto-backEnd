using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Paschoalotto.Models.Database;
using System.Security.Cryptography;
using System;
using Paschoalotto.Models;
using MariGlobals.Utils;

namespace Paschoalotto.Repositories
{
    public sealed class UsuarioRepository
    {
        private readonly DatabaseContext db;
        public UsuarioRepository(DatabaseContext context)
        {
            db = context;
        }
        public async Task<UsuarioLogin> CriarUsuarioAsync(UsuarioLogin usuario)
        {

            var novoUsuario = new UsuarioLogin
            {
                Usuario = usuario.Usuario,
                Senha = usuario.Senha.ToSha256(),
                Email = usuario.Email,
                Role =  "cliente",
                Nome = usuario.Nome,
                Documento = usuario.Documento
            };

            await db.AddAsync(novoUsuario);
            try
            {
                await db.SaveChangesAsync();
                return novoUsuario;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);

                Console.WriteLine(e.Message);
                return null;
            }



        }

        public async Task<UsuarioLogin> existeUsuarioAsync(UsuarioLogin usuario)
        {

            return await db.UsuarioLogin
                    .Where(u => u.Documento.Equals(usuario.Documento)
                    || u.Usuario.Equals(usuario.Usuario)
                    || u.Email.Equals(usuario.Email)
                    )
                    .FirstOrDefaultAsync();


        }
        public async Task<UsuarioLogin> usuarioLoginAsync(UsuarioLogin usuario)
        {

            return await db.UsuarioLogin
                .Where(u => u.Usuario.Equals(usuario.Usuario)
                && u.Senha.Equals(usuario.Senha)
                )
                .FirstOrDefaultAsync();

        }
    }
}