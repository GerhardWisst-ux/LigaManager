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
    public class LandRepository : ILaenderRepository
    {       

        public async Task<Land> AddLand(Land land)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Laender] (Laendername, Code, Aktiv)" +
                    " VALUES(@Laendername,@Code,@Aktiv)";

                cmd.Parameters.AddWithValue("@Laendername", land.Laendername);
                cmd.Parameters.AddWithValue("@Code", land.Code);
                cmd.Parameters.AddWithValue("@Aktiv", land.Aktiv);

                cmd.ExecuteNonQuery();

                conn.Close();

                return land;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public Task<Land> DeleteLand(int LandId)
        {

            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Laender] Where ID= @Id";

            cmd.Parameters.AddWithValue("@Id", LandId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;

        }

        public async Task<Land> GetLand(int LandId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Laender] WHERE ID =" + LandId, conn);
                Land land = new Land();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {                        
                        land = new Land();
                        land.Id = int.Parse(reader["Id"].ToString());                        
                        land.Laendername = reader["Laendername"].ToString();
                        land.Code = reader["Code"].ToString();
                        land.Aktiv = bool.Parse(reader["Aktiv"].ToString());
                    }
                }
                conn.Close();
                return land;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<IEnumerable<Land>> GetLaender()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Laender]", conn);
                Land land = null;
                List<Land> laenderList = new List<Land>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        land = new Land();
                        land.Id = int.Parse(reader["Id"].ToString());
                        land.Laendername = reader["Laendername"].ToString();
                        land.Code = reader["Code"].ToString();                        
                        land.Aktiv = bool.Parse(reader["Aktiv"].ToString());

                        laenderList.Add(land);
                    }
                }
                conn.Close();
                return laenderList;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Land> UpdateLand(Land land)
        {
            int bAktiv;
            
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
               
                if (land.Aktiv == false)
                    bAktiv = 0;
                else
                    bAktiv = 1;

                cmd.CommandText = "UPDATE [dbo].[Laender] SET " +
                      "[Laendername] = '" + land.Laendername + "'" +                      
                      ",[Code] = '" + land.Code + "'" +                                            
                      ",[Aktiv] =" + bAktiv +                      
                      " WHERE  [Id] = " + land.Id;

                cmd.ExecuteNonQuery();

                conn.Close();

                return land;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
    }
}

