using LigaManagement.Api.Migrations;
using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LigaManagerManagement.Api.Models
{
    public class SpieltagRepository : ISpieltagRepository
    {
        private readonly AppDbContext appDbContext;

        public SpieltagRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Spieltag> AddSpieltag(Spieltag spieltag)
        {
            var result = await appDbContext.Spieltage.AddAsync(spieltag);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Spieltag> DeleteSpieltag(int SpieltagId)
        {
            var result = await appDbContext.Spieltage
               .FirstOrDefaultAsync(e => e.SpieltagId == SpieltagId);
            if (result != null)
            {
                appDbContext.Spieltage.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public Task<Spieltag> GetAktSpieltag()
        {
            throw new NotImplementedException();
        }

        public async Task<Spieltag> GetSpieltag(int SpieltagId)
        {
            return await appDbContext.Spieltage               
                .FirstOrDefaultAsync(d => d.SpieltagId == SpieltagId);
        }

        public async Task<IEnumerable<Spieltag>> GetSpieltage()
        {
            return await appDbContext.Spieltage                
                .ToListAsync();
        }

        public int MaxSpieltag(int SaisonID)
        {
            int iMaxSpieltag = 0;
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM[LigaDB].[dbo].[Spieltage] WHERE Datum<GETDATE() and Saison = '" +  SaisonID +  "2023/24", conn);
            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                }
            }
            conn.Close();
            return iMaxSpieltag;            
        }

        public async Task<Spieltag> UpdateSpieltag(Spieltag spieltag)
        {
            try
            {
                var result = await appDbContext.Spieltage
                .FirstOrDefaultAsync(e => e.SpieltagId == spieltag.SpieltagId);

                if (result != null)
                {
                    result.Verein1 = spieltag.Verein1;
                    result.Verein2 = spieltag.Verein2;
                    result.Tore1_Nr = spieltag.Tore1_Nr;
                    result.Tore2_Nr = spieltag.Tore2_Nr;
                    result.Verein1_Nr = spieltag.Verein1_Nr;
                    result.Verein2_Nr = spieltag.Verein2_Nr;
                    result.Ort = spieltag.Ort;
                    result.Datum = spieltag.Datum;
                    result.Abgeschlossen = true;
                    result.SpieltagNr = spieltag.SpieltagNr;
                    result.LigaID = spieltag.LigaID;
                    result.SaisonID = spieltag.SaisonID;
                    result.Schiedrichter = spieltag.Schiedrichter;
                    result.Zuschauer = spieltag.Zuschauer;

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

