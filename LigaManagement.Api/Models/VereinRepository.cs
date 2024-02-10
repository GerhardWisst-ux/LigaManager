using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


namespace LigaManagerManagement.Api.Models
{
    public class VereinRepository : IVereinRepository
    {
        private readonly AppDbContext appDbContext;

        public VereinRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Verein> AddVerein(Verein Verein)
        {
            var result = await appDbContext.Vereine.AddAsync(Verein);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Verein> DeleteVerein(int VereinId)
        {
            var result = await appDbContext.Vereine
               .FirstOrDefaultAsync(e => e.Id == VereinId);
            if (result != null)
            {
                appDbContext.Vereine.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Verein> GetVerein(int VereinId)
        {
            return await appDbContext.Vereine
                .FirstOrDefaultAsync(d => d.Id == VereinId);
        }

        public async Task<IEnumerable<Verein>> GetVereine()
        {
            return await appDbContext.Vereine.ToListAsync();
        }

        public async Task<Verein> UpdateVerein(Verein Verein)
        {
            try
            {
                var result = await appDbContext.Vereine
                .FirstOrDefaultAsync(e => e.Id == Verein.Id);

                if (result != null)
                {
                    result.VereinNr = Verein.VereinNr;
                    result.Vereinsname1 = Verein.Vereinsname1;
                    result.Vereinsname2 = Verein.Vereinsname2;
                    result.Stadion = Verein.Stadion;
                    result.Erfolge = Verein.Erfolge;

                    await appDbContext.SaveChangesAsync();

                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return null;
            }      
        
        }
    }
}

