﻿using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;


namespace LigaManagerManagement.Api.Models
{
    public class VereinNLRepository : IVereineNLRepository
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
                cmd.CommandText = "INSERT INTO [VereineNL] (VereinNr,Vereinsname1,Vereinsname2,Stadion,Fassungsvermoegen,Erfolge,Gegruendet,Pokal,Liga1,Liga2,Hyperlink)" +
                    " VALUES(@VereinNr,@Vereinsname1,@Vereinsname2,@Stadion,@Fassungsvermoegen,@Erfolge,@Gegruendet,@Pokal,@Liga1,@Liga2,@Hyperlink)";

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
                cmd.Parameters.AddWithValue("@Liga2", false);
                cmd.Parameters.AddWithValue("@Hyperlink", "");

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
                cmd.CommandText = "INSERT VereineSaisonNL (VereinNr, SaisonID, LigaID)" +

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
            cmd.CommandText = "DELETE FROM [dbo].[VereineNL] Where Id= @Id";

            cmd.Parameters.AddWithValue("@Id", vereinId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;

        }

        public async Task<VereinAUS> GetVerein(int vereinnr)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [VereineNL] Where VereinNr =" + vereinnr, conn);
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
                        verein.Hyperlink = reader["Hyperlink"].ToString();
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Pokal = bool.Parse(reader["Pokal"].ToString());
                        verein.Liga1 = bool.Parse(reader["Liga1"].ToString());
                        verein.Liga2 = bool.Parse(reader["Liga2"].ToString());

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

        public async Task<IEnumerable<VereinAUS>> GetVereine()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [VereineNL]", conn);
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
                        verein.Hyperlink = reader["Hyperlink"].ToString();
                        verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                        verein.Pokal = bool.Parse(reader["Pokal"].ToString());
                        verein.Liga1 = true; //bool.Parse(reader["Liga1"].ToString());
                        verein.Liga2 = false; // bool.Parse(reader["Liga2"].ToString());

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
            int bLiga2;

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

                if (verein.Liga1 == false)
                    bLiga1 = 0;
                else
                    bLiga1 = 1;

                if (verein.Liga2 == false)
                    bLiga2 = 0;
                else
                    bLiga2 = 1;



                cmd.CommandText = "UPDATE [dbo].[VereineNL] SET " +
                        "[VereinNr] = " + verein.VereinNr +
                        ",[Vereinsname1] = '" + verein.Vereinsname1 + "'" +
                        ",[Vereinsname2] = '" + verein.Vereinsname2 + "'" +
                        ",[Stadion] = '" + verein.Stadion + "'" +
                        ",[Fassungsvermoegen] = '" + verein.Fassungsvermoegen + "'" +
                        ",[Erfolge] = '" + verein.Erfolge + "'" +
                        ",[Gegruendet] =" + verein.Gegruendet +
                        ",[Pokal] =" + bPokal +
                        ",[Liga1] =" + bLiga1 +
                        ",[Liga2] =" + bLiga2 +
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

