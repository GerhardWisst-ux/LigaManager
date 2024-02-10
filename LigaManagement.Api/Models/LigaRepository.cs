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
    public class LigaRepository : ILigaRepository
    {
        private readonly AppDbContext appDbContext;

        public LigaRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Liga> AddLiga(Liga Liga)
        {
            var result = await appDbContext.Ligen.AddAsync(Liga);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Liga> DeleteLiga(int LigaId)
        {
            var result = await appDbContext.Ligen
               .FirstOrDefaultAsync(e => e.Id == LigaId);
            if (result != null)
            {
                appDbContext.Ligen.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Liga> GetLiga(int LigaId)
        {
            return await appDbContext.Ligen
                .FirstOrDefaultAsync(d => d.Id == LigaId);
        }

        public async Task<IEnumerable<Liga>> GetLigen()
        {
            return await appDbContext.Ligen.ToListAsync();
        }

        public async Task<Liga> UpdateLiga(Liga Liga)
        {
            try
            {
                var result = await appDbContext.Ligen
                .FirstOrDefaultAsync(e => e.Id == Liga.Id);

                if (result != null)
                {
                    result.Liganame = Liga.Liganame;
                    result.Verband = Liga.Verband;
                    result.Absteiger = Liga.Absteiger;
                    result.Aktiv = Liga.Aktiv;
                    result.Erstaustragung = Liga.Erstaustragung;

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

