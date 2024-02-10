using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagerManagement.Api.Models
{
    public class TabelleRepository : ITabelleRepository
    {
        private readonly AppDbContext appDbContext;

        public TabelleRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Tabelle> AddTabelle(Tabelle Tabelle)
        {
            var result = await appDbContext.Tabellen.AddAsync(Tabelle);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Tabelle> DeleteTabelle(int TabelleId)
        {
            var result = await appDbContext.Tabellen
               .FirstOrDefaultAsync(e => e.Id == TabelleId);
            if (result != null)
            {
                appDbContext.Tabellen.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Tabelle> GetTabelle(int TabelleId)
        {
            return await appDbContext.Tabellen
                .Include(e => e.Verein)
                .FirstOrDefaultAsync(d => d.Id == TabelleId);
        }

        public async Task<IEnumerable<Tabelle>> GetTabellen()
        {
            return await appDbContext.Tabellen
                .Include(e => e.Verein).ToListAsync();
        }

        public async Task<Tabelle> UpdateTabelle(Tabelle Tabelle)
        {
            var result = await appDbContext.Tabellen.AddAsync(Tabelle);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}

