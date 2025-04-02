using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using LigaManagement.Models;
using Ligamanager.Components;
using LigaManagement.Web.Classes;
using LigamanagerManagement.Api.Models.Repository;

namespace InfoTextManagerManagement.Api.Models
{
    public class InfoTexteRepository : IInfoTexteRepository
    {       

        public async Task<InfoText> AddInfoText(InfoText InfoText)
        {
            try
            {             

                SqlConnection conn = new(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new()
                {
                    Connection = conn,
                    CommandText = "INSERT INTO [InfoTexte] (Title, NewsContent, SaisonID, VereinID, LigaID, PublishedAt,ChangedAt)" +
                    " VALUES(@Title, @NewsContent, @SaisonID, @VereinID, @LigaID, @PublishedAt,@ChangedAt)"
                };

                cmd.Parameters.AddWithValue("@Title", InfoText.Title);
                cmd.Parameters.AddWithValue("@NewsContent", InfoText.NewsContent);
                cmd.Parameters.AddWithValue("@SaisonID", InfoText.SaisonID);
                cmd.Parameters.AddWithValue("@VereinID", InfoText.VereinID);
                cmd.Parameters.AddWithValue("@LigaID", InfoText.LigaID);                
                cmd.Parameters.AddWithValue("@PublishedAt", InfoText.PublishedAt);
                cmd.Parameters.AddWithValue("@ChangedAt", InfoText.ChangedAt);


                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return InfoText;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<InfoText> DeleteInfoText(int InfoTextId)
        {

            try
            {
                SqlConnection conn = new(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [dbo].[InfoText] Where ID= @InfoTextId";

                cmd.Parameters.AddWithValue("@InfoTextId", InfoTextId);

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

        public async Task<InfoText> GetInfoText(int InfoTextId)
        {
            try
            {
                SqlConnection conn = new(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [InfoTexte] WHERE ID =" + InfoTextId, conn);
                InfoText InfoText = new();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        InfoText = new InfoText();

                        InfoText.Id = int.Parse(reader["Id"].ToString());
                        InfoText.NewsContent = reader["NewsContent"].ToString();
                        InfoText.Title = reader["Title"].ToString();
                        InfoText.VereinID = int.Parse(reader["VereinID"].ToString());
                        InfoText.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        InfoText.LigaID = int.Parse(reader["LigaID"].ToString());
                        InfoText.PublishedAt = DateTime.Parse(reader["PublishedAt"].ToString());
                        InfoText.ChangedAt = DateTime.Parse(reader["ChangedAt"].ToString());
                    }
                }
                conn.Close();
                return InfoText;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<IEnumerable<InfoText>> GetInfoTexte()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new("SELECT * FROM [InfoTexte]", conn);
                InfoText InfoText = null;
                List<InfoText> ListTexte = [];
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        InfoText = new InfoText();

                        InfoText.Id = int.Parse(reader["Id"].ToString());
                        InfoText.NewsContent = reader["NewsContent"].ToString();
                        InfoText.Title = reader["Title"].ToString();
                        InfoText.VereinID = int.Parse(reader["VereinID"].ToString());
                        InfoText.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        InfoText.LigaID = int.Parse(reader["LigaID"].ToString());
                        InfoText.PublishedAt = DateTime.Parse(reader["PublishedAt"].ToString());
                        InfoText.ChangedAt = DateTime.Parse(reader["ChangedAt"].ToString());
                        ListTexte.Add(InfoText);
                    }
                }
                conn.Close();
                return ListTexte;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
     

        public async Task<InfoText> UpdateInfoText(InfoText InfoText)
        {            
            try
            {
                SqlConnection conn = new(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new()
                {
                    Connection = conn
                };
                                
                cmd.CommandText = "UPDATE [dbo].[InfoTexte] SET " +
                                  "[Title] = @Title, " +
                                  "[NewsContent] = @NewsContent, " +
                                  "[VereinID] = @VereinID, " +
                                  "[SaisonID] = @SaisonID, " +                                  
                                  "[LigaID] = @LigaID, " +
                                  "[PublishedAt] = @PublishedAt " +                                  
                                  "WHERE [ID] = @ID";

                cmd.Parameters.AddWithValue("@Title", InfoText.Title);
                cmd.Parameters.AddWithValue("@NewsContent", InfoText.NewsContent);
                cmd.Parameters.AddWithValue("@VereinID", InfoText.VereinID);
                cmd.Parameters.AddWithValue("@SaisonID", InfoText.SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", InfoText.LigaID);
                cmd.Parameters.AddWithValue("@PublishedAt", InfoText.PublishedAt);
                cmd.Parameters.AddWithValue("@ChangedAt", InfoText.ChangedAt);
                cmd.Parameters.AddWithValue("@ID", InfoText.Id); 
                                
                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return InfoText;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
    }
}

