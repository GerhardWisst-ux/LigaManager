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
                .FirstOrDefaultAsync(d => d.VereinNr == VereinId);
        }

        public async Task<IEnumerable<Verein>> GetVereine()
        {
            return await appDbContext.Vereine.ToListAsync();
        }

        public async Task<IEnumerable<Verein>> GetVereineSaison()
        {
            List<Verein> vereineSaison = new List<Verein>();

            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT Vereinsname1, Vereinsname2, Stadion, VereineSaison.VereinNr FROM VereineSaison inner Join Vereine on Vereine.VereinNr = VereineSaison.VereinNr", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Verein verein = new Verein();
                    verein.VereinNr = (int)reader["VereinNr"];
                    verein.Vereinsname1 = (string)reader["Vereinsname1"];
                    verein.Vereinsname2 = (string)reader["Vereinsname2"];
                    verein.Stadion = "Stadion";

                    vereineSaison.Add(verein);
                }
            }

            conn.Close();
            return vereineSaison;
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

