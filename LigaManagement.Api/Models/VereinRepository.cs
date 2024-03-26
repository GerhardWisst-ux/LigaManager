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

        public Task<List<VereineSaison>> AddVereineSaison(List<VereineSaison> vereineSaison)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            for (int i = 0; i < vereineSaison.Count; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT VereineSaison (VereinNr, SaisonID, LigaID)" +
                    " VALUES(@VereinNr,@SaisonID,@LigaID)";

                cmd.Parameters.AddWithValue("@VereinNr", vereineSaison[i].VereinNr);
                cmd.Parameters.AddWithValue("@SaisonID", vereineSaison[i].SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", vereineSaison[i].LigaID);
                cmd.ExecuteNonQuery();
            }         
           
            conn.Close();
                        
            return null;
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

        public async Task<IEnumerable<VereinAktSaison>> GetVereineSaison()
        {
            List<VereinAktSaison> vereineSaison = new List<VereinAktSaison>();

            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT SaisonID, Vereinsname1, Vereinsname2, Stadion, VereineSaison.VereinNr FROM VereineSaison inner Join Vereine on Vereine.VereinNr = VereineSaison.VereinNr", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    VereinAktSaison verein = new VereinAktSaison();
                    verein.VereinNr = (int)reader["VereinNr"];
                    verein.Vereinsname1 = (string)reader["Vereinsname1"];
                    verein.Vereinsname2 = (string)reader["Vereinsname2"];
                    verein.Stadion = (string)reader["Stadion"];
                    verein.SaisonID = (int)reader["SaisonID"]; //Gegruendet wird zweckentfremdet für SaisonID verwendet

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

