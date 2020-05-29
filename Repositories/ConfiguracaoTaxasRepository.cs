using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Paschoalotto.Models.Database;

namespace Paschoalotto.Repositories
{
    public class ConfiguracaoTaxasRepository
    {
        private readonly DatabaseContext db;
        public ConfiguracaoTaxasRepository(DatabaseContext context)
        {
            db = context;
        }

        public async Task<ConfiguracaoTaxas> GetConfigSimpleAsync()
        {
            return await db.ConfiguracaoTaxas
                    .Where(j => j.JurosSimple)
                    .FirstOrDefaultAsync();
        }


        public async Task<ConfiguracaoTaxas> GetConfigCompostoAsync()
        {
            return await db.ConfiguracaoTaxas
                    .Where(j => j.JurosComposto)
                    .FirstOrDefaultAsync();
        }
    }
}