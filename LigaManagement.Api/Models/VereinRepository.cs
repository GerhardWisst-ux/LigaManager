﻿using LigaManagement.Models;
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

                SqlCommand command = new SqlCommand("SELECT Distinct Verein1_Nr,Verein1 from SpieltageCL", conn);
                VereinAktSaison verein = null;
                List<VereinAktSaison> vereinelist = new List<VereinAktSaison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinAktSaison();
                        verein.Id = i;
                        //verein.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        verein.VereinNr = int.Parse(reader["Verein1_Nr"].ToString());
                        verein.Vereinsname1 = reader["Verein1"].ToString();
                        verein.Vereinsname2 = reader["Verein1"].ToString();
                        //verein.Hyperlink = reader["Hyperlink"].ToString();
                        //verein.Fassungsvermoegen = 0;
                        //verein.Erfolge = "";
                        //verein.Stadion = "";
                        //verein.Gegruendet = 0;
                        //verein.Pokal = true;
                        //verein.Bundesliga = true;

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

                SqlCommand command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[Erfolge],[Gegruendet],[Hyperlink],[LandID],[GroupID2024]," +
                    "[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004],[GroupID2002],[GroupID2000]," +
                    "[GroupID1998],[GroupID1996],[GroupID1994],[GroupID1992],[GroupID1990],[GroupID1988], [GroupID1986],[GroupID1984],[GroupID1982],[GroupID1980],[GroupID1978],[GroupID1974],[GroupID1970], [GroupID1966], " +
                    "[GroupID1962],[GroupID1958],[GroupID1954],[GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [LigaDB].[dbo].[MannschaftEMWM]", conn);
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
                        verein.Vereinsname2 = reader["MannschaftName2"].ToString();
                        verein.Hyperlink = reader["Hyperlink"].ToString();
                        verein.Fassungsvermoegen = 0;
                        verein.Erfolge = reader["Erfolge"].ToString();
                        verein.Stadion = "";
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Pokal = false;
                        verein.Bundesliga = false;
                        verein.GroupID2024 = int.Parse(reader["GroupID2024"].ToString());
                        verein.GroupID2022 = int.Parse(reader["GroupID2022"].ToString());
                        verein.GroupID2020 = int.Parse(reader["GroupID2020"].ToString());
                        verein.GroupID2018 = int.Parse(reader["GroupID2018"].ToString());
                        verein.GroupID2016 = int.Parse(reader["GroupID2016"].ToString());
                        verein.GroupID2014 = int.Parse(reader["GroupID2014"].ToString());
                        verein.GroupID2012 = int.Parse(reader["GroupID2012"].ToString());
                        verein.GroupID2010 = int.Parse(reader["GroupID2010"].ToString());
                        verein.GroupID2008 = int.Parse(reader["GroupID2008"].ToString());
                        verein.GroupID2006 = int.Parse(reader["GroupID2006"].ToString());
                        verein.GroupID2004 = int.Parse(reader["GroupID2004"].ToString());
                        verein.GroupID2002 = int.Parse(reader["GroupID2002"].ToString());
                        verein.GroupID2000 = int.Parse(reader["GroupID2000"].ToString());
                        verein.GroupID1998 = int.Parse(reader["GroupID1998"].ToString());
                        verein.GroupID1996 = int.Parse(reader["GroupID1996"].ToString());
                        verein.GroupID1994 = int.Parse(reader["GroupID1994"].ToString());
                        verein.GroupID1992 = int.Parse(reader["GroupID1992"].ToString());
                        verein.GroupID1990 = int.Parse(reader["GroupID1990"].ToString());
                        verein.GroupID1988 = int.Parse(reader["GroupID1988"].ToString());
                        verein.GroupID1986 = int.Parse(reader["GroupID1986"].ToString());
                        verein.GroupID1984 = int.Parse(reader["GroupID1984"].ToString());
                        verein.GroupID1982 = int.Parse(reader["GroupID1982"].ToString());
                        verein.GroupID1980 = int.Parse(reader["GroupID1980"].ToString());
                        verein.GroupID1978 = int.Parse(reader["GroupID1978"].ToString());
                        verein.GroupID1974 = int.Parse(reader["GroupID1974"].ToString());
                        verein.GroupID1970 = int.Parse(reader["GroupID1970"].ToString());
                        verein.GroupID1966 = int.Parse(reader["GroupID1966"].ToString());
                        verein.GroupID1962 = int.Parse(reader["GroupID1962"].ToString());
                        verein.GroupID1958 = int.Parse(reader["GroupID1958"].ToString());
                        verein.GroupID1954 = int.Parse(reader["GroupID1954"].ToString());
                        verein.GroupID1950 = int.Parse(reader["GroupID1950"].ToString());
                        verein.GroupID1938 = int.Parse(reader["GroupID1938"].ToString());
                        verein.GroupID1934 = int.Parse(reader["GroupID1934"].ToString());
                        verein.GroupID1930 = int.Parse(reader["GroupID1930"].ToString());

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

                SqlCommand command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[Erfolge],[Gegruendet],[Hyperlink],[LandID],[GroupID2024]," +
                  "[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004],[GroupID2002],[GroupID2000]," +
                  "[GroupID1998],[GroupID1996],[GroupID1994],[GroupID1992],[GroupID1990],[GroupID1988], [GroupID1986], [GroupID1984], [GroupID1984],[GroupID1980] FROM [LigaDB].[dbo].[MannschaftEMWM] Where MannschaftNr = " + Id, conn);

                
                VereinAktSaison verein = null;
                List<VereinAktSaison> vereinelist = new List<VereinAktSaison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinAktSaison();
                        verein.Id = i;
                        verein.VereinNr = int.Parse(reader["MannschaftNr"].ToString());
                        verein.Vereinsname1 = reader["MannschaftName1"].ToString();
                        verein.Vereinsname2 = reader["MannschaftName1"].ToString();
                        verein.Hyperlink = reader["Hyperlink"].ToString();
                        verein.Fassungsvermoegen = 0;
                        verein.Erfolge = reader["Erfolge"].ToString();
                        verein.Stadion = "";
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Pokal = false;
                        verein.Bundesliga = false;
                        verein.GroupID2024 = int.Parse(reader["GroupID2024"].ToString());
                        verein.GroupID2022 = int.Parse(reader["GroupID2022"].ToString());
                        verein.GroupID2020 = int.Parse(reader["GroupID2020"].ToString());
                        verein.GroupID2018 = int.Parse(reader["GroupID2018"].ToString());
                        verein.GroupID2016 = int.Parse(reader["GroupID2016"].ToString());
                        verein.GroupID2014 = int.Parse(reader["GroupID2014"].ToString());
                        verein.GroupID2012 = int.Parse(reader["GroupID2012"].ToString());
                        verein.GroupID2010 = int.Parse(reader["GroupID2010"].ToString());
                        verein.GroupID2008 = int.Parse(reader["GroupID2008"].ToString());
                        verein.GroupID2006 = int.Parse(reader["GroupID2006"].ToString());
                        verein.GroupID2004 = int.Parse(reader["GroupID2004"].ToString());
                        verein.GroupID2000 = int.Parse(reader["GroupID2000"].ToString());
                        verein.GroupID1996 = int.Parse(reader["GroupID1996"].ToString());
                        verein.GroupID1994 = int.Parse(reader["GroupID1994"].ToString());
                        verein.GroupID1992 = int.Parse(reader["GroupID1992"].ToString());                        
                        verein.GroupID1990 = int.Parse(reader["GroupID1990"].ToString());
                        verein.GroupID1988 = int.Parse(reader["GroupID1988"].ToString());
                        verein.GroupID1986 = int.Parse(reader["GroupID1986"].ToString());
                        verein.GroupID1984 = int.Parse(reader["GroupID1984"].ToString());
                        verein.GroupID1980 = int.Parse(reader["GroupID1980"].ToString());

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
                        verein.Stadion = (string)reader["Stadion"];
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


