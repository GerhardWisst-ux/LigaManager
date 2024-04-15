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

        public async Task<Verein> AddVerein(Verein verein)
        {
            int bPokal;
            int bBundesliga;

            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Vereine] (VereinNr,Vereinsname1,Vereinsname2,Stadion,Fassungsvermoegen,Erfolge,Gegruendet,Pokal,Bundesliga)" +
                    " VALUES(@VereinNr,@Vereinsname1,@Vereinsname2,@Stadion,@Fassungsvermoegen,@Erfolge,@Gegruendet,@Pokal,@Bundesliga)";

                if (verein.Pokal == false)
                    bPokal = 0;
                else
                    bPokal = 1;

                if (verein.Bundesliga == false)
                    bBundesliga = 0;
                else
                    bBundesliga = 1;

                cmd.Parameters.AddWithValue("@VereinNr", verein.VereinNr);
                cmd.Parameters.AddWithValue("@Vereinsname1", verein.Vereinsname1);
                cmd.Parameters.AddWithValue("@Vereinsname2", verein.Vereinsname2);
                cmd.Parameters.AddWithValue("@Stadion", verein.Stadion);
                cmd.Parameters.AddWithValue("@Fassungsvermoegen", verein.Fassungsvermoegen);
                cmd.Parameters.AddWithValue("@Erfolge", verein.Erfolge);
                cmd.Parameters.AddWithValue("@Gegruendet", verein.Gegruendet);
                cmd.Parameters.AddWithValue("@Pokal", bPokal);
                cmd.Parameters.AddWithValue("@Bundesliga", bBundesliga);

                cmd.ExecuteNonQuery();

                conn.Close();

                return verein;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            //var result = await appDbContext.Vereine.AddAsync(Verein);
            //await appDbContext.SaveChangesAsync();
            //return result.Entity;
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

        public async Task<Verein> DeleteVerein(int vereinId)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Vereine] Where Id= @Id";

            cmd.Parameters.AddWithValue("@Id", vereinId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;

            //var result = await appDbContext.Vereine
            //   .FirstOrDefaultAsync(e => e.Id == VereinId);
            //if (result != null)
            //{
            //    appDbContext.Vereine.Remove(result);
            //    await appDbContext.SaveChangesAsync();
            //    return result;
            //}

            //return null;
        }

        public async Task<Verein> GetVerein(int vereinnr)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Vereine] Where VereinNr =" + vereinnr, conn);
                Verein verein = null;
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new Verein();
                                                
                        verein.Id = int.Parse(reader["Id"].ToString());
                        verein.VereinNr = int.Parse(reader["VereinNr"].ToString());
                        verein.Vereinsname1 = reader["Vereinsname1"].ToString();
                        verein.Vereinsname2 = reader["Vereinsname2"].ToString();
                        verein.Fassungsvermoegen = reader["Fassungsvermoegen"].ToString();
                        verein.Erfolge = reader["Erfolge"].ToString();
                        verein.Stadion = reader["Stadion"].ToString();
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Pokal = bool.Parse(reader["Pokal"].ToString());
                        verein.Bundesliga = bool.Parse(reader["Bundesliga"].ToString());
                    }
                }
                conn.Close();
                return verein;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }
            //return await appDbContext.Vereine
            //    .FirstOrDefaultAsync(d => d.VereinNr == VereinId);
        }

        public async Task<IEnumerable<Verein>> GetVereine()
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Vereine]", conn);
                Verein verein = null;
                List<Verein> vereinelist = new List<Verein>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new Verein();

                        verein.Id = int.Parse(reader["Id"].ToString());
                        verein.VereinNr = int.Parse(reader["VereinNr"].ToString());
                        verein.Vereinsname1 = reader["Vereinsname1"].ToString();
                        verein.Vereinsname2 = reader["Vereinsname2"].ToString();
                        verein.Fassungsvermoegen = reader["Fassungsvermoegen"].ToString();
                        verein.Erfolge = reader["Erfolge"].ToString();
                        verein.Stadion = reader["Stadion"].ToString();
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Pokal = bool.Parse(reader["Pokal"].ToString());
                        verein.Bundesliga = bool.Parse(reader["Bundesliga"].ToString());

                        vereinelist.Add(verein);
                    }
                }
                conn.Close();
                return vereinelist;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }

            //return await appDbContext.Vereine.ToListAsync();
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

        public async Task<Verein> UpdateVerein(Verein verein)
        {
            int bPokal;
            int bBundesliga;

            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                // cmd.CommandText = "UPDATE Vereine(VereinNr,Vereinsname1,Vereinsname2,Stadion,Fassungsvermoegen,Erfolge,Gegruendet,Pokal,Bundesliga)" +
                //" VALUES(@VereinNr,@Vereinsname1, @Vereinsname2,@Stadion,@Fassungsvermoegen,@Erfolge,@Gegruendet,@Pokal,@Bundesliga)";

                // cmd.Parameters.AddWithValue("@VereinNr", verein.VereinNr);
                // cmd.Parameters.AddWithValue("@Vereinsname1", verein.Vereinsname1);
                // cmd.Parameters.AddWithValue("@Vereinsname2", verein.Vereinsname2);
                // cmd.Parameters.AddWithValue("@Stadion", verein.Stadion);
                // cmd.Parameters.AddWithValue("@Fassungsvermoegen", verein.Fassungsvermoegen);
                // cmd.Parameters.AddWithValue("@Erfolge", verein.Erfolge);
                // cmd.Parameters.AddWithValue("@Gegruendet", verein.Gegruendet);
                // cmd.Parameters.AddWithValue("@Pokal", verein.Pokal);
                // cmd.Parameters.AddWithValue("@Bundesliga", verein.Bundesliga);

                if (verein.Pokal == false)
                    bPokal = 0;
                else
                    bPokal = 1;

                if (verein.Bundesliga == false)
                    bBundesliga = 0;
                else
                    bBundesliga = 1;


                cmd.CommandText = "UPDATE [dbo].[Vereine] SET " +                        
                        "[VereinNr] = '" + verein.VereinNr + "'" +
                        ",[Vereinsname1] = '" + verein.Vereinsname1 + "'" +
                        ",[Vereinsname2] = '" + verein.Vereinsname2 + "'" +                                               
                        ",[Stadion] = '" + verein.Stadion + "'" +
                        ",[Fassungsvermoegen] = '" + verein.Fassungsvermoegen + "'" +
                        ",[Erfolge] = '" + verein.Erfolge + "'" +
                        ",[Gegruendet] =" + verein.Gegruendet +
                        ",[Pokal] =" + bPokal +
                        ",[Bundesliga] =" + bBundesliga +
                        " WHERE  [VereinNr] = " + verein.VereinNr;

                cmd.ExecuteNonQuery();

                conn.Close();

                return verein;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            //try
            //{
            //    var result = await appDbContext.Vereine
            //    .FirstOrDefaultAsync(e => e.Id == Verein.Id);

            //    if (result != null)
            //    {
            //        result.VereinNr = Verein.VereinNr;
            //        result.Vereinsname1 = Verein.Vereinsname1;
            //        result.Vereinsname2 = Verein.Vereinsname2;
            //        result.Stadion = Verein.Stadion;
            //        result.Erfolge = Verein.Erfolge;

            //        await appDbContext.SaveChangesAsync();

            //        return result;
            //    }
            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    Debug.Print(ex.StackTrace);
            //    return null;
            //}      

        }

      
    }
}

