using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MariGlobals.Utils;
using Microsoft.EntityFrameworkCore;
using Paschoalotto.Models;
using Paschoalotto.Models.Database;

namespace Paschoalotto.Repositories
{
    public class ContratoRepository
    {
        private readonly DatabaseContext db;
        public ContratoRepository(DatabaseContext context)
        {
            db = context;
        }

        public async Task<List<Contrato>> PegaContratosAtivosAsync(string documento)
        {
            return await db.Contrato
                    .Where(c => c.Documento.Equals(documento) && !c.DividaFinalizada)
                    .ToListAsync();
        }


        public async Task<Contrato> PegaContratoAsync(Guid idContrato)
        {

            return await db.Contrato
                .Where(c => c.IdContrato.Equals(idContrato))
                .FirstOrDefaultAsync();
        }

        public async Task FinalizaContratoAsync(Guid idContrato)
        {

            var contrato = await db.Contrato
                    .Where(c => c.IdContrato.Equals(idContrato))
                    .FirstOrDefaultAsync();
            if (contrato.HasContent())
            {
                contrato.DividaFinalizada = true;

                await db.SaveChangesAsync();

            }

        }
    }
}