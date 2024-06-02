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
    public class SaisonenCLRepository : ISaisonenCLRepository
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
                cmd.CommandText = "INSERT INTO [SaisonenCL] (LigaID, Saisonname,Liganame,Aktuell,Abgeschlossen)" +
                    " VALUES(@LigaID,@Saisonname,@Liganame,@Aktuell,@Abgeschlossen)";
                                
                cmd.Parameters.AddWithValue("@LigaID", saison.LigaID);                
                cmd.Parameters.AddWithValue("@Saisonname", saison.Saisonname);
                cmd.Parameters.AddWithValue("@Liganame", saison.Liganame);
                cmd.Parameters.AddWithValue("@Aktuell", bAktuell);
                cmd.Parameters.AddWithValue("@Abgeschlossen", bAbgeschlossen);                
                
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
            cmd.CommandText = "DELETE FROM [dbo].[SaisonenCL] Where SaisonID= = @SaisonId";

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

                SqlCommand command = new SqlCommand("SELECT * FROM [SaisonenCL] Where SaisonID= " + SaisonId, conn);
                Saison saison = null;
                List<Saison> peList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saison = new Saison();

                        saison.SaisonID = (int)reader["saisonID"];
                        saison.LigaID = (int)reader["LigaID"];
                        saison.Saisonname = reader["Saisonname"].ToString();
                        saison.Liganame = reader["Liganame"].ToString();
                        saison.Aktuell = (bool)reader["Aktuell"];
                        saison.Abgeschlossen = (bool)reader["Abgeschlossen"];
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

                SqlCommand command = new SqlCommand("SELECT * FROM [SaisonenCL]", conn);
                Saison saison = null;
                List<Saison> saisonenList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saison = new Saison();
                                                
                        saison.SaisonID = (int)reader["saisonID"];                        
                        saison.LigaID = (int)reader["LigaID"];                        
                        saison.Saisonname = reader["Saisonname"].ToString();
                        saison.Liganame = reader["Liganame"].ToString();
                        saison.Aktuell = (bool)reader["Aktuell"];                        
                        saison.Abgeschlossen = (bool)reader["Abgeschlossen"];

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

                SqlCommand command = new SqlCommand("SELECT * FROM [SaisonenCL] Where Saison= '" + saisonname + "'", conn);
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

                cmd.CommandText = "UPDATE [dbo].[SaisonenCL] SET " +                         
                          " [Aktuell] =" + bAktuell +                          
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

