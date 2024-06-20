using LigaManagement.Models;
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
    public class LigaRepository : ILigenRepository
    {       

        public async Task<Liga> AddLiga(Liga liga)
        {
            string sAktiv = string.Empty;
            string sEMWM = string.Empty;

            try
            {
                if (liga.Aktiv == true)
                    sAktiv = "TRUE";
                else
                    sAktiv = "FALSE";

                if (liga.EMWM == true)
                    sEMWM = "TRUE";
                else
                    sEMWM = "FALSE";

                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO [Ligen] (Liganame, Verband, Erstaustragung, Saisonen,Liganummer,Aktiv,LandID,Rekordspieler,Spiele_Rekordspieler, EMWM)" +
                    " VALUES(@Liganame,@Verband,@Erstaustragung,@Saisonen,@Liganummer,@Aktiv,@LandID,@Rekordspieler,@Spiele_Rekordspieler,@EMWM)"
                };

                cmd.Parameters.AddWithValue("@Liganame", liga.Liganame);
                cmd.Parameters.AddWithValue("@Verband", liga.Verband);
                cmd.Parameters.AddWithValue("@Erstaustragung", liga.Erstaustragung);
                cmd.Parameters.AddWithValue("@Saisonen", liga.Saisonen);
                cmd.Parameters.AddWithValue("@Liganummer", liga.Liganummer);
                cmd.Parameters.AddWithValue("@Aktiv", sAktiv);
                cmd.Parameters.AddWithValue("@LandID", liga.LandID);
                if (liga.Rekordspieler != null)
                    cmd.Parameters.AddWithValue("@Rekordspieler", liga.Rekordspieler);
                else
                    cmd.Parameters.AddWithValue("@Rekordspieler", "");
                cmd.Parameters.AddWithValue("@Spiele_Rekordspieler", liga.Spiele_Rekordspieler);
                cmd.Parameters.AddWithValue("@EMWM", sEMWM);

                cmd.ExecuteNonQuery();

                conn.Close();

                return liga;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public Task<Liga> DeleteLiga(int LigaId)
        {

            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Ligen] Where ID= @LigaId";

            cmd.Parameters.AddWithValue("@LigaId", LigaId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;
          
        }

        public async Task<Liga> GetLiga(int LigaId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Ligen] WHERE ID =" + LigaId, conn);
                Liga liga = new Liga();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        liga = new Liga();

                        liga.Id = (int)reader["Id"];
                        liga.Liganummer = int.Parse(reader["Liganummer"].ToString());
                        liga.Liganame = reader["Liganame"].ToString();
                        liga.Verband = reader["Verband"].ToString();
                        liga.Erstaustragung = (DateTime)reader["Erstaustragung"];
                        liga.Liganame = reader["Liganame"].ToString();
                        liga.Saisonen = (int)reader["Saisonen"];
                        liga.Aktiv = bool.Parse(reader["Aktiv"].ToString());
                        liga.LandID = int.Parse(reader["LandID"].ToString());
                        liga.Rekordspieler = reader["Rekordspieler"].ToString();
                        liga.Spiele_Rekordspieler = int.TryParse(reader["Spiele_Rekordspieler"].ToString(), out var f) ? f : 0;
                        liga.EMWM  = bool.Parse(reader["EMWM"].ToString());
                    }
                }
                conn.Close();
                return liga;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }

        }

        public async Task<IEnumerable<Liga>> GetLigen()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Ligen]", conn);
                Liga liga = null;
                List<Liga> peList = new List<Liga>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        liga = new Liga();

                        liga.Id = int.Parse(reader["Id"].ToString());                        
                        liga.Liganummer = int.Parse(reader["Liganummer"].ToString());
                        liga.Liganame = reader["Liganame"].ToString();
                        liga.Verband = reader["Verband"].ToString();
                        liga.Erstaustragung = DateTime.Parse(reader["Erstaustragung"].ToString());
                        liga.Liganame = reader["Liganame"].ToString();
                        liga.Saisonen = (int)reader["Saisonen"];
                        liga.Aktiv = bool.Parse(reader["Aktiv"].ToString());
                        liga.LandID = int.Parse(reader["LandID"].ToString());
                        liga.Rekordspieler = reader["Rekordspieler"].ToString();
                        liga.Spiele_Rekordspieler = int.TryParse(reader["Spiele_Rekordspieler"].ToString(), out var f) ? f : 0;
                        liga.EMWM = bool.Parse(reader["EMWM"].ToString());
                        peList.Add(liga);
                    }
                }
                conn.Close();
                return peList;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
     

        public async Task<Liga> UpdateLiga(Liga liga)
        {
            string sAktiv;
            string sEMWM;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;               

                if (liga.Aktiv == true)
                    sAktiv = "TRUE";
                else
                    sAktiv = "FALSE";

                if (liga.EMWM == true)
                    sEMWM = "TRUE";
                else
                    sEMWM = "FALSE";

                cmd.CommandText = "UPDATE [dbo].[Ligen] SET " +                    
                       "[Liganame] = '" + liga.Liganame + "'" +
                      ",[Verband] = '" + liga.Verband + "'" +                     
                      ",[Erstaustragung] = '" + liga.Erstaustragung + "'" +                      
                      ",[Saisonen] =" + liga.Saisonen +
                      ",[LandID] =" + liga.LandID +
                      ",[Liganummer] =" + liga.Liganummer +
                      ",[Rekordspieler] ='" + liga.Rekordspieler + "'" +
                      ",[Spiele_Rekordspieler] =" + liga.Spiele_Rekordspieler +
                      ",[Aktiv] = '" + sAktiv + "'" +
                      ",[EMWM] = '" + sEMWM + "'" +
                      " WHERE [Id] = " + liga.Id;

                cmd.ExecuteNonQuery();

                conn.Close();

                return liga;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
    }
}

