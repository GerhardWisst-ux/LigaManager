using LigaManagement.Api.Models;
using LigaManagement.Models;
using Ligamanager.Components;
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
    public class VereinTURepository : IVereineTURepository
    {       

        public async Task<VereinAUS> AddVerein(VereinAUS verein)
        {
            int bPokal;
            int bLiga1;

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [VereineTU] (VereinNr,Vereinsname1,Vereinsname2,Stadion,Fassungsvermoegen,Erfolge,Gegruendet,Pokal,Liga1)" +
                    " VALUES(@VereinNr,@Vereinsname1,@Vereinsname2,@Stadion,@Fassungsvermoegen,@Erfolge,@Gegruendet,@Pokal,@Liga1)";

                if (verein.Pokal == false)
                    bPokal = 0;
                else
                    bPokal = 1;

                if (verein.Liga1 == false)
                    bLiga1 = 0;
                else
                    bLiga1 = 1;

                cmd.Parameters.AddWithValue("@VereinNr", verein.VereinNr);
                cmd.Parameters.AddWithValue("@Vereinsname1", verein.Vereinsname1);
                cmd.Parameters.AddWithValue("@Vereinsname2", verein.Vereinsname2);
                cmd.Parameters.AddWithValue("@Stadion", verein.Stadion);
                cmd.Parameters.AddWithValue("@Fassungsvermoegen", verein.Fassungsvermoegen);
                cmd.Parameters.AddWithValue("@Erfolge", verein.Erfolge);
                cmd.Parameters.AddWithValue("@Gegruendet", verein.Gegruendet);
                cmd.Parameters.AddWithValue("@Pokal", bPokal);
                cmd.Parameters.AddWithValue("@Liga1", bLiga1);

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
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            for (int i = 0; i < vereineSaison.Count; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT VereineSaisonPL (VereinNr, SaisonID, LigaID)" +
                    " VALUES(@VereinNr,@SaisonID,@LigaID)";

                cmd.Parameters.AddWithValue("@VereinNr", vereineSaison[i].VereinNr);
                cmd.Parameters.AddWithValue("@SaisonID", vereineSaison[i].SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", vereineSaison[i].LigaID);
                cmd.ExecuteNonQuery();
            }         
           
            conn.Close();
                        
            return null;
        }

        public async Task<VereinAUS> DeleteVerein(int vereinId)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[VereineTU] Where Id= @Id";

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

        public async Task<VereinAUS> GetVerein(int vereinnr)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [VereineTU] Where VereinNr =" + vereinnr, conn);
                VereinAUS verein = null;
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinAUS();

                        verein.Id = int.Parse(reader["Id"].ToString());
                        verein.VereinNr = int.Parse(reader["VereinNr"].ToString());
                        verein.Vereinsname1 = reader["Vereinsname1"].ToString();
                        verein.Vereinsname2 = reader["Vereinsname2"].ToString();
                        verein.Fassungsvermoegen = reader["Fassungsvermoegen"].ToString();
                        verein.Erfolge = reader["Erfolge"].ToString();
                        verein.Stadion = reader["Stadion"].ToString();
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Pokal = bool.Parse(reader["Pokal"].ToString());
                        verein.Liga1 = bool.Parse(reader["Liga1"].ToString());
                        verein.Liga2 = bool.Parse(reader["Liga2"].ToString());

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

        public async Task<IEnumerable<VereinAUS>> GetVereine()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [VereineTU]", conn);
                VereinAUS verein = null;
                List<VereinAUS> vereinelist = new List<VereinAUS>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinAUS();

                        verein.Id = int.Parse(reader["Id"].ToString());
                        verein.VereinNr = int.Parse(reader["VereinNr"].ToString());
                        verein.Vereinsname1 = reader["Vereinsname1"].ToString();
                        verein.Vereinsname2 = reader["Vereinsname2"].ToString();
                        verein.Fassungsvermoegen = reader["Fassungsvermoegen"].ToString();
                        verein.Erfolge = reader["Erfolge"].ToString();
                        verein.Stadion = reader["Stadion"].ToString();
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Pokal = bool.Parse(reader["Pokal"].ToString());
                        verein.Liga1 = bool.Parse(reader["Liga1"].ToString());
                        verein.Liga2 = bool.Parse(reader["Liga2"].ToString());

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

        public async Task<IEnumerable<VereinAktSaisonAUS>> GetVereineSaison()
        {
            List<VereinAktSaisonAUS> vereineSaison = new List<VereinAktSaisonAUS>();

            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT SaisonID, Vereinsname1, Vereinsname2, Stadion, VereineSaison.VereinNr FROM VereineSaison inner Join Vereine on Vereine.VereinNr = VereineSaison.VereinNr", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    VereinAktSaisonAUS verein = new VereinAktSaisonAUS();
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

        public async Task<VereinAUS> UpdateVerein(VereinAUS verein)
        {
            int bPokal;
            int bLiga1;

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
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

                if (verein.Liga1 == false)
                    bLiga1 = 0;
                else
                    bLiga1 = 1;


                cmd.CommandText = "UPDATE [dbo].[VereineTU] SET " +                        
                        "[VereinNr] = '" + verein.VereinNr + "'" +
                        ",[Vereinsname1] = '" + verein.Vereinsname1 + "'" +
                        ",[Vereinsname2] = '" + verein.Vereinsname2 + "'" +                                               
                        ",[Stadion] = '" + verein.Stadion + "'" +
                        ",[Fassungsvermoegen] = '" + verein.Fassungsvermoegen + "'" +
                        ",[Erfolge] = '" + verein.Erfolge + "'" +
                        ",[Gegruendet] =" + verein.Gegruendet +
                        ",[Pokal] =" + bPokal +
                        ",[Bundesliga] =" + bLiga1 +
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

