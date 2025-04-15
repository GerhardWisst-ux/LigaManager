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
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Saisonen] (LigaID, LandID, Saisonname,Liganame,Aktuell,Abgeschlossen,AnzahlVereine,AnzahlAbsteiger,AnzahlCL_Plaetze,AnzahlEL_Plaetze,AnzahlCF_Plaetze,Anzahl_Relegation,SpielplanVorhanden,Ligahoehe, AnzahlAufsteiger)" +
                    " VALUES(@LigaID,@LandID, @Saisonname,@Liganame,@Aktuell,@Abgeschlossen,@AnzahlVereine,@AnzahlAbsteiger,@AnzahlCL_Plaetze,@AnzahlEL_Plaetze,@AnzahlCF_Plaetze,@Anzahl_Relegation,@SpielplanVorhanden,@Ligahoehe, @AnzahlAufsteiger)";
                                
                cmd.Parameters.AddWithValue("@LigaID", saison.LigaID);
                cmd.Parameters.AddWithValue("@Ligahoehe", saison.Ligahoehe);
                cmd.Parameters.AddWithValue("@LandID", saison.LandID);
                cmd.Parameters.AddWithValue("@Saisonname", saison.Saisonname);
                cmd.Parameters.AddWithValue("@Liganame", saison.Liganame);
                cmd.Parameters.AddWithValue("@Aktuell", bAktuell);
                cmd.Parameters.AddWithValue("@Abgeschlossen", bAbgeschlossen);
                cmd.Parameters.AddWithValue("@AnzahlVereine", saison.AnzahlVereine);
                cmd.Parameters.AddWithValue("@AnzahlAufsteiger", saison.Aufsteiger);
                cmd.Parameters.AddWithValue("@AnzahlAbsteiger", saison.Absteiger);
                cmd.Parameters.AddWithValue("@Anzahl_Relegation", saison.Relegation);
                cmd.Parameters.AddWithValue("@AnzahlCL_Plaetze", saison.CL_League);
                cmd.Parameters.AddWithValue("@AnzahlEL_Plaetze", saison.EL_League);
                cmd.Parameters.AddWithValue("@AnzahlCF_Plaetze", saison.CF_League);
                cmd.Parameters.AddWithValue("@SpielplanVorhanden", saison.SpielplanVorhanden);

                await cmd.ExecuteNonQueryAsync();

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
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen] Where SaisonID= " + SaisonId, conn);
                Saison saison = null;
                List<Saison> peList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saison = new Saison();

                        saison.SaisonID = reader["saisonID"] != DBNull.Value ? (int)reader["saisonID"] : 0;
                        saison.LigaID = reader["LigaID"] != DBNull.Value ? (int)reader["LigaID"] : 0;
                        saison.LandID = reader["LandID"] != DBNull.Value ? (int)reader["LandID"] : 0;
                        saison.Saisonname = reader["Saisonname"].ToString();
                        saison.Liganame = reader["Liganame"].ToString();
                        saison.Ligahoehe = reader["Ligahoehe"] != DBNull.Value ? (int)reader["Ligahoehe"] : 0;
                        saison.AnzahlVereine = reader["AnzahlVereine"] != DBNull.Value ? (int)reader["AnzahlVereine"] : 0;
                        saison.AnzahlVereine = reader["AnzahlVereine"] != DBNull.Value ? (int)reader["AnzahlVereine"] : 0;
                        saison.Aufsteiger = reader["AnzahlAufsteiger"] != DBNull.Value ? (int)reader["AnzahlAufsteiger"] : 0;
                        saison.Absteiger = reader["AnzahlAbsteiger"] != DBNull.Value ? (int)reader["AnzahlAbsteiger"] : 0;
                        saison.CL_League = reader["AnzahlCL_Plaetze"] != DBNull.Value ? (int)reader["AnzahlCL_Plaetze"] : 0;
                        saison.CF_League = reader["AnzahlCF_Plaetze"] != DBNull.Value ? (int)reader["AnzahlCF_Plaetze"] : 0;
                        saison.EL_League = reader["AnzahlEL_Plaetze"] != DBNull.Value ? (int)reader["AnzahlEL_Plaetze"] : 0;
                        saison.Relegation = reader["Anzahl_Relegation"] != DBNull.Value ? (int)reader["Anzahl_Relegation"] : 0;
                        saison.Abgeschlossen = (bool)reader["Abgeschlossen"];
                        saison.SpielplanVorhanden = (bool)reader["SpielplanVorhanden"];
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
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen] Order by LigaID, Saisonname DESC ", conn);
                Saison saison = null;
                List<Saison> saisonenList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        saison = new Saison();

                        saison.SaisonID = reader["saisonID"] != DBNull.Value ? (int)reader["saisonID"] : 0;
                        saison.LigaID = reader["LigaID"] != DBNull.Value ? (int)reader["LigaID"] : 0;
                        saison.LandID = reader["LandID"] != DBNull.Value ? (int)reader["LandID"] : 0;
                        saison.Saisonname = reader["Saisonname"].ToString();
                        saison.Liganame = reader["Liganame"].ToString();
                        saison.Ligahoehe = reader["Ligahoehe"] != DBNull.Value ? (int)reader["Ligahoehe"] : 0;
                        saison.AnzahlVereine = reader["AnzahlVereine"] != DBNull.Value ? (int)reader["AnzahlVereine"] : 0;
                        saison.Aufsteiger = reader["AnzahlAufsteiger"] != DBNull.Value ? (int)reader["AnzahlAufsteiger"] : 0;
                        saison.Absteiger = reader["AnzahlAbsteiger"] != DBNull.Value ? (int)reader["AnzahlAbsteiger"] : 0;
                        saison.CL_League = reader["AnzahlCL_Plaetze"] != DBNull.Value ? (int)reader["AnzahlCL_Plaetze"] : 0;
                        saison.CF_League = reader["AnzahlCF_Plaetze"] != DBNull.Value ? (int)reader["AnzahlCF_Plaetze"] : 0;
                        saison.EL_League = reader["AnzahlEL_Plaetze"] != DBNull.Value ? (int)reader["AnzahlEL_Plaetze"] : 0;
                        saison.Relegation = reader["Anzahl_Relegation"] != DBNull.Value ? (int)reader["Anzahl_Relegation"] : 0;
                        saison.Abgeschlossen = (bool)reader["Abgeschlossen"];
                        saison.SpielplanVorhanden = (bool)reader["SpielplanVorhanden"];
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
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen] Where Saison= '" + saisonname + "'", conn);
                Saison saison = null;
                List<Saison> peList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
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
            int bSpielplanVorhanden; 
            try
            {

                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();
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


                if (saison.SpielplanVorhanden == false)
                    bSpielplanVorhanden = 0;
                else
                    bSpielplanVorhanden = 1;

                cmd.CommandText = "UPDATE [dbo].[Saisonen] SET " +                         
                          " [Aktuell] =" + bAktuell +                          
                          ",[AnzahlAbsteiger] = " + saison.Absteiger +
                          ",[AnzahlCL_Plaetze] =" + saison.CL_League +
                          ",[AnzahlCF_Plaetze] =" + saison.CF_League +
                          ",[AnzahlEL_Plaetze] =" + saison.EL_League +
                          ",[Anzahl_Relegation] =" + saison.Relegation +
                          ",[Abgeschlossen] =" + bAbgeschlossen +
                          ",[SpielplanVorhanden] =" + bSpielplanVorhanden +
                          " WHERE  [SaisonID] = " + saison.SaisonID;

                await cmd.ExecuteNonQueryAsync();

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

