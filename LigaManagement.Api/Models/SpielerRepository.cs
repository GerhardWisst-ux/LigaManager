using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


namespace LigaManagerManagement.Api.Models
{
    public class SpielerRepository : ISpielerRepository
    {
        private readonly AppDbContext appDbContext;

        public SpielerRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Spieler> AddSpieler(Spieler Spieler)
        {
            var result = await appDbContext.AllSpieler.AddAsync(Spieler);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Spieler> DeleteSpieler(int SpielerId)
        {
            var result = await appDbContext.AllSpieler
               .FirstOrDefaultAsync(e => e.Id == SpielerId);
            if (result != null)
            {
                appDbContext.AllSpieler.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Spieler> GetSpieler(int SpielerId)
        {
            return await appDbContext.AllSpieler
                .FirstOrDefaultAsync(d => d.Id == SpielerId);
        }

        public async Task<IEnumerable<Spieler>> GetAllSpieler()
        {
            return await appDbContext.AllSpieler.ToListAsync();
        }

        public async Task<Spieler> UpdateSpieler(Spieler Spieler)
        {
            try
            {
                var result = await appDbContext.AllSpieler
                .FirstOrDefaultAsync(e => e.Id == Spieler.Id);

                if (result != null)
                {
                    result.Aktiv = Spieler.Aktiv;
                    result.Name = Spieler.Name;
                    result.Vorname = Spieler.Vorname;
                    result.Geburtsdatum = Spieler.Geburtsdatum;
                    result.VereinID = Spieler.VereinID;
                    result.ImVereinSeit = Spieler.ImVereinSeit;
                    result.LigaID = Spieler.LigaID;

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

