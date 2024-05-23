using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagerManagement.Api.Models
{
    public class TabelleRepository : ITabelleRepository
    {
      

        public async Task<Tabelle> AddTabelle(Tabelle Tabelle)
        {
            //var result = await appDbContext.Tabellen.AddAsync(Tabelle);
            //await appDbContext.SaveChangesAsync();
            // return result.Entity;
            return null;
        }

        public async Task<Tabelle> DeleteTabelle(int TabelleId)
        {
            //var result = await appDbContext.Tabellen
            //   .FirstOrDefaultAsync(e => e.Id == TabelleId);
            //if (result != null)
            //{
            //    appDbContext.Tabellen.Remove(result);
            //    await appDbContext.SaveChangesAsync();
            //    return result;
            //}

            return null;
        }

        public async Task<Tabelle> GetTabelle(int TabelleId)
        {
            //return await appDbContext.Tabellen
            //    .Include(e => e.Verein)
            //    .FirstOrDefaultAsync(d => d.Id == TabelleId);

            return null;
        }

        public async Task<IEnumerable<Tabelle>> GetTabellen()
        {
            //return await appDbContext.Tabellen
            //    .Include(e => e.Verein).ToListAsync();

            return null;
        }

        public async Task<Tabelle> UpdateTabelle(Tabelle Tabelle)
        {
            //var result = await appDbContext.Tabellen.AddAsync(Tabelle);
            //await appDbContext.SaveChangesAsync();
            //return result.Entity;

            return null;
        }
    }
}

