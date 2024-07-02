using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;


namespace LigaManagement.Api.Models
{
    public class SaisonenRepository : ISaisonenRepository
    {       
        public async Task<Saison> AddSaison(Saison saison)
        {
            int bAktuell;
            int bAbgeschlossen;

            try
            {
                if (saison.Aktuell == false)
                    bAktuell = 0;
                else
                    bAktuell = 1;

                if (saison.Abgeschlossen == false)
                    bAbgeschlossen = 0;
                else
                    bAbgeschlossen = 1;

                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Saisonen] (LigaID, LandID, Saisonname,Liganame,Aktuell,Abgeschlossen,AnzahlVereine)" +
                    " VALUES(@LigaID,@LandID, @Saisonname,@Liganame,@Aktuell,@Abgeschlossen,@AnzahlVereine)";
                                
                cmd.Parameters.AddWithValue("@LigaID", saison.LigaID);
                cmd.Parameters.AddWithValue("@LandID", saison.LandID);
                cmd.Parameters.AddWithValue("@Saisonname", saison.Saisonname);
                cmd.Parameters.AddWithValue("@Liganame", saison.Liganame);
                cmd.Parameters.AddWithValue("@Aktuell", bAktuell);
                cmd.Parameters.AddWithValue("@Abgeschlossen", bAbgeschlossen);
                cmd.Parameters.AddWithValue("@AnzahlVereine", saison.AnzahlVereine);
                
                cmd.ExecuteNonQuery();

                conn.Close();

                return saison;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public Task<Saison> DeleteSaison(int SaisonId)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Saisonen] Where SaisonID= = @SaisonId";

            cmd.Parameters.AddWithValue("@SaisonID", SaisonId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;
       
        }

        public async Task<Saison> GetSaison(int SaisonId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen] Where SaisonID= " + SaisonId, conn);
                Saison saison = null;
                List<Saison> peList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saison = new Saison();

                        saison.SaisonID = (int)reader["saisonID"];
                        saison.LigaID = (int)reader["LigaID"];
                        saison.LandID = (int)reader["landID"];
                        saison.Saisonname = reader["Saisonname"].ToString();
                        saison.Liganame = reader["Liganame"].ToString();
                        saison.Ligahoehe = (int)reader["Ligahoehe"];
                        saison.AnzahlVereine = (int)reader["AnzahlVereine"];
                        saison.Aufsteiger = (int)reader["AnzahlAufsteiger"];
                        saison.Absteiger = (int)reader["AnzahlAbsteiger"];
                        saison.CL_League = (int)reader["AnzahlCL_Plaetze"];
                        saison.CF_League = (int)reader["AnzahlCF_Plaetze"];
                        saison.EL_League = (int)reader["AnzahlEL_Plaetze"];
                        saison.Relegation = (int)reader["Anzahl_Relegation"];
                        saison.Abgeschlossen = (bool)reader["Abgeschlossen"];
                        saison.Aktuell = (bool)reader["Aktuell"];
                    }
                }
                conn.Close();
                return saison;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<IEnumerable<Saison>> GetSaisonen()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen]", conn);
                Saison saison = null;
                List<Saison> saisonenList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saison = new Saison();

                        saison.SaisonID = (int)reader["saisonID"];
                        saison.LigaID = (int)reader["LigaID"];
                        saison.LandID = (int)reader["landID"];
                        saison.Saisonname = reader["Saisonname"].ToString();
                        saison.Liganame = reader["Liganame"].ToString();
                        saison.Ligahoehe = (int)reader["Ligahoehe"];
                        saison.AnzahlVereine = (int)reader["AnzahlVereine"];
                        saison.Aufsteiger = (int)reader["AnzahlAufsteiger"];
                        saison.Absteiger = (int)reader["AnzahlAbsteiger"];
                        saison.CL_League = (int)reader["AnzahlCL_Plaetze"];
                        saison.CF_League = (int)reader["AnzahlCF_Plaetze"];
                        saison.EL_League = (int)reader["AnzahlEL_Plaetze"];
                        saison.Relegation = (int)reader["Anzahl_Relegation"];
                        saison.Abgeschlossen = (bool)reader["Abgeschlossen"];
                        saison.Aktuell = (bool)reader["Aktuell"];

                        saisonenList.Add(saison);
                    }
                }
                conn.Close();
                return saisonenList;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Saison> GetSaisonID(string saisonname)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen] Where Saison= '" + saisonname + "'", conn);
                Saison saison = null;
                List<Saison> peList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saison = new Saison();

                        saison.SaisonID = (int)reader["saisonID"];
                       
                    }
                }
                conn.Close();
                return saison;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Saison> UpdateSaison(Saison saison)
        {
            int bAktuell;
            int bAbgeschlossen;

            try
            {

                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                
                if (saison.Aktuell == false)
                    bAktuell = 0;
                else
                    bAktuell = 1;

                if (saison.Abgeschlossen == false)
                    bAbgeschlossen = 0;
                else
                    bAbgeschlossen = 1;

                cmd.CommandText = "UPDATE [dbo].[Saisonen] SET " +                         
                          " [Aktuell] =" + bAktuell +
                          ",[AnzahlAbsteiger] = " + saison.Absteiger +
                          ",[AnzahlCL_Plaetze] =" + saison.CL_League +
                          ",[AnzahlCF_Plaetze] =" + saison.CF_League +
                          ",[AnzahlEL_Plaetze] =" + saison.EL_League +
                          ",[Anzahl_Relegation] =" + saison.Relegation +
                          ",[Abgeschlossen] =" + bAbgeschlossen +
                          " WHERE  [SaisonID] = " + saison.SaisonID;

                cmd.ExecuteNonQuery();

                conn.Close();

                return saison;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
    }
}

