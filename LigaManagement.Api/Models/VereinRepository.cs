using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;


namespace LigaManagerManagement.Api.Models
{
    public class VereinRepository : IVereinRepository
    {

        public async Task<Verein> AddVerein(Verein verein)
        {
            int bPokal;
            int bBundesliga;

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
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
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public Task<List<VereineSaison>> AddVereineSaison(List<VereineSaison> vereineSaison)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
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
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Vereine] Where Id= @Id";

            cmd.Parameters.AddWithValue("@Id", vereinId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;


        }

        public async Task<Verein> GetVerein(int vereinnr)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
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
                        verein.Fassungsvermoegen = int.Parse(reader["Fassungsvermoegen"].ToString());
                        verein.Erfolge = reader["Erfolge"].ToString();
                        verein.Stadion = reader["Stadion"].ToString();
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Bundesliga = bool.Parse(reader["Bundesliga"].ToString());
                        verein.Pokal = bool.Parse(reader["Pokal"].ToString());
                        verein.Hyperlink = reader["Hyperlink"].ToString();
                    }
                }
                conn.Close();
                return verein;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Verein> GetVereinCL(int Id)
        {
            try
            {
                int i = 1;
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT Distinct Verein1_Nr,Verein1,SaisonID from SpieltageCL Where Verein1_Nr = " + Id, conn);
                VereinAktSaison verein = null;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinAktSaison();
                        verein.Id = i;
                        verein.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        verein.VereinNr = int.Parse(reader["Verein1_Nr"].ToString());
                        verein.Vereinsname1 = reader["Verein1"].ToString();
                        verein.Vereinsname2 = reader["Verein1"].ToString();
                        //verein.Hyperlink = reader["Hyperlink"].ToString();                        
                        verein.Fassungsvermoegen = 0;
                        verein.Erfolge = "";
                        verein.Stadion = "";
                        verein.Gegruendet = 0;
                        verein.Pokal = true;
                        verein.Bundesliga = true;


                        i++;
                    }
                }
                conn.Close();
                return verein;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Verein>> GetVereine()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
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
                        verein.Fassungsvermoegen = int.Parse(reader["Fassungsvermoegen"].ToString());
                        verein.Erfolge = reader["Erfolge"].ToString();
                        verein.Stadion = reader["Stadion"].ToString();
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Bundesliga = bool.Parse(reader["Bundesliga"].ToString());
                        verein.Pokal = bool.Parse(reader["Pokal"].ToString());
                        verein.Hyperlink = reader["Hyperlink"].ToString();

                        vereinelist.Add(verein);
                    }
                }
                conn.Close();
                return vereinelist;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<VereinAktSaison>> GetVereineCL()
        {

            int i = 1;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT Distinct Verein1_Nr,Verein1,SaisonID from SpieltageCL", conn);
                VereinAktSaison verein = null;
                List<VereinAktSaison> vereinelist = new List<VereinAktSaison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinAktSaison();
                        verein.Id = i;
                        verein.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        verein.VereinNr = int.Parse(reader["Verein1_Nr"].ToString());
                        verein.Vereinsname1 = reader["Verein1"].ToString();
                        verein.Vereinsname2 = reader["Verein1"].ToString();
                        verein.Hyperlink = reader["Hyperlink"].ToString();
                        verein.Fassungsvermoegen = 0;
                        verein.Erfolge = "";
                        verein.Stadion = "";
                        verein.Gegruendet = 0;
                        verein.Pokal = true;
                        verein.Bundesliga = true;

                        vereinelist.Add(verein);

                        i++;
                    }
                }
                conn.Close();
                return vereinelist;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<VereinAktSaison>> GetVereineEMWM()
        {

            int i = 1;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT DISTINCT [Verein1_Nr],[Verein1] FROM [dbo].[SpieltageEMWM] where groupID = 1 SELECT DISTINCT [Verein2_Nr],[Verein2] FROM [dbo].[SpieltageEMWM]", conn);
                VereinAktSaison verein = null;
                List<VereinAktSaison> vereinelist = new List<VereinAktSaison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinAktSaison();
                        verein.Id = i;
                        //verein.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        verein.VereinNr = int.Parse(reader["MannschaftNr"].ToString());
                        verein.Vereinsname1 = reader["MannschaftName1"].ToString();
                        verein.Vereinsname2 = reader["MannschaftName1"].ToString();
                        verein.Hyperlink = reader["Hyperlink"].ToString();
                        verein.Fassungsvermoegen = 0;
                        verein.Erfolge = "";
                        verein.Stadion = "";
                        verein.Gegruendet = 0;
                        verein.Pokal = true;
                        verein.Bundesliga = true;

                        vereinelist.Add(verein);

                        i++;
                    }
                }
                conn.Close();
                return vereinelist;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }


        public async Task<IEnumerable<VereinAktSaison>> GetVereineL3()
        {
            int i = 1;
            bool bFound = false;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                
                SqlCommand command = new SqlCommand("SELECT Vereine.Vereinsname1,VereineSaison.[VereinNr],[SaisonID],[LigaID] FROM [dbo].[VereineSaison] inner join vereine on Vereine.VereinNr = VereineSaison.VereinNr order by Vereine.Vereinsname1", conn);
                VereinAktSaison verein = null;
                List<VereinAktSaison> vereinelist = new List<VereinAktSaison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinAktSaison();
                        verein.Id = i;                        
                        verein.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        verein.VereinNr = int.Parse(reader["VereinNr"].ToString());
                        verein.Vereinsname1 = reader["Vereinsname1"].ToString();
                        verein.Vereinsname2 = reader["Vereinsname1"].ToString();

                        
                        //verein.Hyperlink = reader["Hyperlink"].ToString();
                        verein.Fassungsvermoegen = 0;
                        verein.Erfolge = "";
                        verein.Stadion = "";
                        verein.Gegruendet = 0;
                        verein.Pokal = true;
                        verein.Bundesliga = true;

                        vereinelist.Add(verein);

                        i++;
                        bFound = true;
                    }
                }

                if (bFound == false)
                {
                    
                    verein = null;
                    vereinelist = new List<VereinAktSaison>();
                    command = new SqlCommand("SELECT Distinct Verein1_Nr,Verein1,SaisonID from SpieltageL3", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            verein = new VereinAktSaison();
                            verein.Id = i;
                            verein.VereinNr = int.Parse(reader["Verein1_Nr"].ToString());
                            verein.Vereinsname1 = reader["Verein1"].ToString();
                            verein.Vereinsname2 = reader["Verein1"].ToString();
                            // verein.Hyperlink = "";
                            verein.Fassungsvermoegen = 0;
                            verein.Erfolge = "";
                            verein.Stadion = "";
                            verein.Gegruendet = 0;
                            verein.Pokal = true;
                            verein.Bundesliga = true;

                            vereinelist.Add(verein);
                            i++;

                        }
                    }
                }
                
                conn.Close();
                return vereinelist;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Verein> GetVereinEMWM(int Id)
        {
            int i = 1;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT DISTINCT [Verein1_Nr],[Verein1] FROM [dbo].[SpieltageEMWM] where groupID = 1 SELECT DISTINCT [Verein2_Nr],[Verein2] FROM [dbo].[SpieltageEMWM] where id =" + Id, conn);
                VereinAktSaison verein = null;
                List<VereinAktSaison> vereinelist = new List<VereinAktSaison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinAktSaison();
                        verein.Id = i;
                        //verein.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        verein.VereinNr = int.Parse(reader["MannschaftNr"].ToString());
                        verein.Vereinsname1 = reader["MannschaftName1"].ToString();
                        verein.Vereinsname2 = reader["MannschaftName1"].ToString();
                        verein.Hyperlink = reader["Hyperlink"].ToString();
                        verein.Fassungsvermoegen = 0;
                        verein.Erfolge = "";
                        verein.Stadion = "";
                        verein.Gegruendet = 0;
                        verein.Pokal = true;
                        verein.Bundesliga = true;                      

                        i++;
                    }
                }
                conn.Close();
                return verein;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<VereinAktSaison>> GetVereineSaison()
        {
            List<VereinAktSaison> vereineSaison = new List<VereinAktSaison>();

            SqlConnection conn = new SqlConnection(Globals.connstring);
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
                    verein.Hyperlink = reader["Hyperlink"].ToString();
                    vereineSaison.Add(verein);
                }
            }

            conn.Close();
            return vereineSaison;
        }

        public async Task<Verein> GetVereinL3(int intvereinnr)
        {

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT SaisonID, Vereinsname1, Vereinsname2, Stadion, VereineSaison.VereinNr FROM VereineSaison inner Join Vereine on Vereine.VereinNr = VereineSaison.VereinNr Where VereineSaison.VereinNr=" + intvereinnr, conn);
                Verein verein = null;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new Verein();
                        verein.VereinNr = int.Parse(reader["VereinNr"].ToString());
                        verein.Vereinsname1 = reader["Vereinsname1"].ToString();
                        verein.Vereinsname2 = reader["Vereinsname1"].ToString();
                        //verein.Hyperlink = reader["Hyperlink"].ToString();
                        verein.Fassungsvermoegen = 0;
                        verein.Erfolge = "";
                        verein.Stadion = "";
                        verein.Gegruendet = 0;
                        verein.Pokal = true;
                        verein.Bundesliga = true;


                    }
                }
                conn.Close();
                return verein;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }


        public async Task<Verein> UpdateVerein(Verein verein)
        {
            int bPokal;
            int bBundesliga;

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

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
                        ",[Hyperlink] = '" + verein.Hyperlink + "'" +
                        ",[Fassungsvermoegen] = " + verein.Fassungsvermoegen +
                        ",[Erfolge] = '" + verein.Erfolge + "'" +
                        ",[Gegruendet] =" + verein.Gegruendet +
                        ",[Pokal] =" + bPokal +
                        ",[Bundesliga] =" + bBundesliga +
                        " WHERE  [VereinNr] = " + verein.VereinNr;

                cmd.ExecuteNonQuery();

                conn.Close();

                return verein;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
       
    }
}


