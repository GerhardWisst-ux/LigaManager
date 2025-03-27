using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using LigaManagement.Models;
using Ligamanager.Components;
using LigaManagement.Web.Classes;
using LigamanagerManagement.Api.Models.Repository;

namespace StadionManagerManagement.Api.Models
{
    public class StadionRepository : IStadionRepository
    {       

        public async Task<Stadion> AddStadion(Stadion Stadion)
        {
            try
            {             

                SqlConnection conn = new(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new()
                {
                    Connection = conn,
                    CommandText = "INSERT INTO [Stadion] (VereinNr, Stadionname, Kapazitaet, Ort, JahrVon, JahrBis, JahrVonDate, JahrBisDate)" +
                    " VALUES(@VereinNr, @Stadionname, @Kapazitaet, @Ort, @JahrVon, @JahrBis, @JahrVonDate, @JahrBisDate)"
                };

                cmd.Parameters.AddWithValue("@Stadionname", Stadion.Stadionname);
                cmd.Parameters.AddWithValue("@VereinNr", Stadion.VereinNr);
                cmd.Parameters.AddWithValue("@Kapazitaet", Stadion.Kapazitaet);
                cmd.Parameters.AddWithValue("@Ort", Stadion.Ort);
                cmd.Parameters.AddWithValue("@JahrVon", Stadion.JahrVon);
                cmd.Parameters.AddWithValue("@JahrBis", Stadion.JahrBis);
                cmd.Parameters.AddWithValue("@JahrVonDate", Stadion.JahrVonDate);
                cmd.Parameters.AddWithValue("@JahrBisDate", Stadion.JahrBisDate);


                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return Stadion;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Stadion> DeleteStadion(int StadionId)
        {

            try
            {
                SqlConnection conn = new(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [dbo].[Stadion] Where ID= @StadionId";

                cmd.Parameters.AddWithValue("@StadionId", StadionId);

                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return null;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<Stadion> GetStadion(int StadionId)
        {
            try
            {
                SqlConnection conn = new(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [Stadion] WHERE ID =" + StadionId, conn);
                Stadion Stadion = new();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Stadion = new Stadion();

                        Stadion.Id = int.Parse(reader["Id"].ToString());
                        Stadion.Stadionname = reader["Stadionname"].ToString();
                        Stadion.Ort = reader["Ort"].ToString();
                        Stadion.VereinNr = int.Parse(reader["VereinNr"].ToString());
                        Stadion.Kapazitaet = int.Parse(reader["Kapazitaet"].ToString());
                        Stadion.JahrVon = int.Parse(reader["JahrVon"].ToString());
                        Stadion.JahrBis = int.Parse(reader["JahrBis"].ToString());
                        Stadion.JahrVonDate = DateTime.Parse(reader["JahrVonDate"].ToString());
                        Stadion.JahrBisDate = DateTime.Parse(reader["JahrBisdate"].ToString());
                    }
                }
                conn.Close();
                return Stadion;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<IEnumerable<Stadion>> GetStadien()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new("SELECT * FROM [Stadion]", conn);
                Stadion Stadion = null;
                List<Stadion> ListStadien = [];
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Stadion = new Stadion();

                        Stadion.Id = int.Parse(reader["Id"].ToString());                                                
                        Stadion.Stadionname = reader["Stadionname"].ToString();
                        Stadion.Ort = reader["Ort"].ToString();
                        Stadion.VereinNr = int.Parse(reader["VereinNr"].ToString());
                        Stadion.Kapazitaet = int.Parse(reader["Kapazitaet"].ToString());
                        Stadion.JahrVon = int.Parse(reader["JahrVon"].ToString());
                        Stadion.JahrBis = int.Parse(reader["JahrBis"].ToString());
                        Stadion.JahrVonDate = DateTime.Parse(reader["JahrVonDate"].ToString());
                        Stadion.JahrBisDate = DateTime.Parse(reader["JahrBisdate"].ToString());
                        ListStadien.Add(Stadion);
                    }
                }
                conn.Close();
                return ListStadien;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
     

        public async Task<Stadion> UpdateStadion(Stadion Stadion)
        {            
            try
            {
                SqlConnection conn = new(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new()
                {
                    Connection = conn
                };
                                
                cmd.CommandText = "UPDATE [dbo].[Stadion] SET " +
                                  "[Stadionname] = @Stadionname, " +
                                  "[Ort] = @Ort, " +
                                  "[VereinNr] = @VereinNr, " +
                                  "[Kapazitaet] = @Kapazitaet, " +
                                  "[JahrVon] = @JahrVon, " +
                                  "[JahrBis] = @JahrBis, " +
                                  "[JahrVonDate] = @JahrVonDate, " +
                                  "[JahrBisDate] = @JahrBisDate " +
                                  "WHERE [ID] = @ID";

                cmd.Parameters.AddWithValue("@Stadionname", Stadion.Stadionname);
                cmd.Parameters.AddWithValue("@Ort", Stadion.Ort);
                cmd.Parameters.AddWithValue("@VereinNr", Stadion.VereinNr);
                cmd.Parameters.AddWithValue("@Kapazitaet", Stadion.Kapazitaet);
                cmd.Parameters.AddWithValue("@JahrVon", Stadion.JahrVon);
                cmd.Parameters.AddWithValue("@JahrBis", Stadion.JahrBis);
                cmd.Parameters.AddWithValue("@JahrVonDate", Stadion.JahrVonDate);
                cmd.Parameters.AddWithValue("@JahrBisDate", Stadion.JahrBisDate);
                cmd.Parameters.AddWithValue("@ID", Stadion.Id); 
                                
                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return Stadion;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
    }
}

