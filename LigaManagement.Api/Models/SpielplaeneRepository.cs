using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LigaManagerManagement.Api.Models
{
    public class SpielplaeneRepository : ISpielplaeneRepository
    {
        public async Task<Spielplan> AddSpielplan(Spielplan Spielplan)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Spielplaene ([SpieltagNr], [Saison],[SaisonID],[LigaID],[Verein1_Nr],[Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum],[DatumString],[Ort],[Schiedrichter],[Abgeschlossen],[Zuschauer],[StadionID])" +
                    " VALUES(@SpieltagNr,@Saison,@SaisonID,@LigaID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@DatumString,@Ort,@Schiedrichter,@Abgeschlossen,@Zuschauer,@StadionID)";

                cmd.Parameters.AddWithValue("@SpieltagNr", Spielplan.SpieltagNr);
                cmd.Parameters.AddWithValue("@SaisonID", Spielplan.SaisonID);
                cmd.Parameters.AddWithValue("@Saison", Spielplan.Saison);                
                cmd.Parameters.AddWithValue("@StadionID", Spielplan.StadionID);
                cmd.Parameters.AddWithValue("@LigaID", Spielplan.LigaID);
                cmd.Parameters.AddWithValue("@Verein1_Nr", Spielplan.Verein1_Nr);
                cmd.Parameters.AddWithValue("@Verein2_Nr", Spielplan.Verein2_Nr);
                cmd.Parameters.AddWithValue("@Verein1", Spielplan.Verein1);
                cmd.Parameters.AddWithValue("@Verein2", Spielplan.Verein2);
                cmd.Parameters.AddWithValue("@Tore1_Nr", Spielplan.Tore1_Nr);
                cmd.Parameters.AddWithValue("@Tore2_Nr", Spielplan.Tore2_Nr);
                cmd.Parameters.AddWithValue("@Datum", Spielplan.Datum);
                cmd.Parameters.AddWithValue("@DatumString", Spielplan.DatumString);
                cmd.Parameters.AddWithValue("@Ort", Spielplan.Ort);
                cmd.Parameters.AddWithValue("@Schiedrichter", Spielplan.Schiedrichter);
                cmd.Parameters.AddWithValue("@Abgeschlossen", Spielplan.Abgeschlossen);
                cmd.Parameters.AddWithValue("@Zuschauer", Spielplan.Zuschauer);
                
                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return Spielplan;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
            
        }

        public async Task<Spielplan> DeleteSpielplan(int SpieltagId)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Spielplaene] Where [SpieltagId]= @SpieltagId";

            cmd.Parameters.AddWithValue("@SpieltagId", SpieltagId);

            await cmd.ExecuteNonQueryAsync();

            conn.Close();

            return null;
        }

        public Task<Spielplan> GetAktSpielplan()
        {
            throw new NotImplementedException();
        }

        public async Task<Spielplan> GetSpielplan(int SpieltagId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [Spielplaene] WHERE [SpieltagId] =" + SpieltagId, conn);
                Spielplan Spielplan = null;
                List<Spielplan> Spielplanlist = new List<Spielplan>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Spielplan = new Spielplan();

                        Spielplan.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        Spielplan.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        Spielplan.StadionID = int.Parse(reader["StadionID"].ToString());
                        Spielplan.LigaID = int.Parse(reader["LigaID"].ToString());
                        Spielplan.SpieltagNr = reader["SpieltagNr"].ToString();
                        Spielplan.Saison = reader["Saison"].ToString();
                        Spielplan.Verein1 = reader["Verein1"].ToString();
                        Spielplan.Verein2 = reader["Verein2"].ToString();
                        Spielplan.Verein1_Nr = reader["Verein1_Nr"].ToString();
                        Spielplan.Verein2_Nr = reader["Verein2_Nr"].ToString();
                        Spielplan.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        Spielplan.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        Spielplan.DatumString = reader["DatumString"].ToString();

                        try
                        {
                            Spielplan.Datum = DateTime.Parse(reader["Datum"].ToString()).AddDays(-1).AddMinutes(930);
                        }
                        catch (Exception ex)
                        {

                            Spielplan.Datum = DateTime.Now;
                        }

                        Spielplan.Ort = reader["Ort"].ToString();
                        Spielplan.Schiedrichter = reader["Schiedrichter"].ToString();
                        Spielplan.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                        Spielplan.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        Spielplan.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                        Spielplan.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();

                    }
                }
                conn.Close();
                return Spielplan;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Spielplan>> GetSpielplaene()
        {
            try
            {

                using (var conn = new SqlConnection(Globals.connstring))
                {
                    await conn.OpenAsync();

                    SqlCommand command = new SqlCommand("sp_Spielplaene", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    Spielplan Spielplan = null;
                    List<Spielplan> Spielplanlist = new List<Spielplan>();
                    await using (SqlDataReader reader = command.ExecuteReader())
                                         
                    {
                        while (await reader.ReadAsync())
                        {
                            Spielplan = new Spielplan();

                            Spielplan.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                            Spielplan.SaisonID = int.Parse(reader["SaisonID"].ToString());
                            Spielplan.StadionID = int.Parse(reader["StadionID"].ToString());
                            Spielplan.LigaID = int.Parse(reader["LigaID"].ToString());
                            Spielplan.SpieltagNr = reader["SpieltagNr"].ToString();
                            Spielplan.Saison = reader["Saison"].ToString();
                            Spielplan.Verein1 = reader["Verein1"].ToString();
                            Spielplan.Verein2 = reader["Verein2"].ToString();
                            Spielplan.Verein1_Nr = reader["Verein1_Nr"].ToString();
                            Spielplan.Verein2_Nr = reader["Verein2_Nr"].ToString();
                            Spielplan.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                            Spielplan.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                            Spielplan.DatumString = reader["DatumString"].ToString();
                                                       
                            try
                            {
                                Spielplan.Datum = DateTime.Parse(Microsoft.VisualBasic.Strings.Right(reader["Datum"].ToString().Trim(), 10)).AddDays(-1).AddMinutes(930);
                            }
                            catch (Exception ex)
                            {

                                Spielplan.Datum = DateTime.Now;
                            }
                            
                            Spielplan.Ort = reader["Ort"].ToString();
                            Spielplan.Schiedrichter = reader["Schiedrichter"].ToString();
                            Spielplan.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                            Spielplan.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                            Spielplan.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                            Spielplan.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();

                            Spielplanlist.Add(Spielplan);
                        }
                    }


                    return Spielplanlist;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public int AktSpielplan(int SaisonID, int LigaID)
        {
            int iMaxSpielplan = 0;
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            try
            {
                if (Globals.LigaNummer == 1 || Globals.LigaNummer == 2)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagId] +0) AS MAXSpielplan FROM [Spielplaene] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(reader["MAXSpielplan"].ToString()))
                            iMaxSpielplan = (int)reader["MAXSpielplan"];
                        else
                            iMaxSpielplan = 1;
                    }

                }              
               

                conn.Close();
                return iMaxSpielplan;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return 1;
            }
        }

        public async Task<Spielplan> UpdateSpielplan(Spielplan Spielplan)
        {
            int bAbgeschlossen;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
             

                if (Spielplan.Abgeschlossen == false)
                    bAbgeschlossen = 0;
                else
                    bAbgeschlossen = -1;

                cmd.CommandText = "UPDATE [dbo].[Spielplaene] SET " +
                         " [SpieltagNr] = " + Spielplan.SpieltagNr +
                         ",[Saison] = '" + Spielplan.Saison + "'" +
                         ",[SaisonID] = " + Spielplan.SaisonID +
                         ",[StadionID] = " + Spielplan.StadionID +
                         ",[LigaID] = " + Spielplan.LigaID +
                         ",[Verein1_Nr] = " + Spielplan.Verein1_Nr  +
                         //",[Verein1] = '" + Spielplan.Verein1 + "'" +
                         ",[Verein2_Nr] = " + Spielplan.Verein2_Nr +
                         //",[Verein2] = '" + Spielplan.Verein2 + "'" +
                         ",[Tore1_Nr] = " + Spielplan.Tore1_Nr +
                         ",[Tore2_Nr] = " + Spielplan.Tore2_Nr +
                         ",[DatumString] = '" + Spielplan.DatumString + "'" +
                         ",[Datum] = '" + Spielplan.Datum + "'" +
                         ",[Ort] = '" + Spielplan.Ort + "'" +
                         ",[Schiedrichter] = '" + Spielplan.Schiedrichter + "'" +
                         ",[Abgeschlossen] =" + bAbgeschlossen +
                         ",[Zuschauer] =" + Spielplan.Zuschauer +
                         " WHERE  [SpieltagId] = " + Spielplan.SpieltagId;

                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return Spielplan;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<int> GetAnzahlSpiele()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT COUNT([SpieltagId]) AS GesamtanzahlFROM (SELECT [SpieltagId] " +
                                                    "FROM [dbo].[Spielplaene] UNION ALLSELECT [SpieltagId]  " +
                                                    "FROM [dbo].[SpielplaeneL3] UNION ALLSELECT SpieltagId  " +
                                                    "FROM [dbo].[SpielplaeneBE] UNION ALLSELECT SpieltagId  " +
                                                    "FROM [dbo].[SpielplaeneCL] UNION ALLSELECT SpieltagId  " +
                                                    "FROM [dbo].[SpielplaeneEMWM] UNION ALL Select SpieltagId  " +
                                                    "FROM [dbo].[SpielplaeneES] UNION ALLSELECT SpieltagId " +
                                                    "FROM [dbo].[SpielplaeneFR] UNION ALLSELECT SpieltagId  " +
                                                    "FROM [dbo].[SpielplaeneNL] UNION ALLSELECT SpieltagId  " +
                                                    "FROM [dbo].[SpielplaeneNL] UNION ALLSELECT SpieltagId  " +
                                                    "FROM [dbo].[SpielplaenePL] UNION ALLSELECT SpieltagId  " +
                                                    "FROM [dbo].[SpielplaenePT]UNION ALLSELECT SpieltagId " +
                                                    "FROM [dbo].[SpielplaeneTU]) AS Combined;] ", conn)
                {

                };
                int iGesamtanzahl = 0;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        iGesamtanzahl = int.Parse(reader["Gesamtanzahl"].ToString());
                    }
                }
                conn.Close();
                return iGesamtanzahl;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return 0;
            }
        }

        public async Task<IEnumerable<Spielplan>> GetSpielplaeneL3()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [SpielplaeneL3] ", conn);
                Spielplan Spielplan = null;
                List<Spielplan> Spielplanlist = new List<Spielplan>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Spielplan = new Spielplan();

                        Spielplan.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        Spielplan.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        Spielplan.StadionID = int.Parse(reader["StadionID"].ToString());
                        Spielplan.LigaID = int.Parse(reader["LigaID"].ToString());
                        Spielplan.SpieltagNr = reader["SpieltagNr"].ToString();
                        Spielplan.Saison = reader["Saison"].ToString();
                        Spielplan.Verein1 = reader["Verein1"].ToString();
                        Spielplan.Verein2 = reader["Verein2"].ToString();
                        Spielplan.Verein1_Nr = reader["Verein1_Nr"].ToString();
                        Spielplan.Verein2_Nr = reader["Verein2_Nr"].ToString();
                        Spielplan.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        Spielplan.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        Spielplan.Datum = DateTime.Parse(reader["Datum"].ToString());
                        Spielplan.Ort = reader["Ort"].ToString();
                        Spielplan.Schiedrichter = reader["Schiedrichter"].ToString();
                        Spielplan.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                        Spielplan.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        Spielplan.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                        Spielplan.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();

                        Spielplanlist.Add(Spielplan);
                    }
                }
                conn.Close();
                return Spielplanlist;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Spielplan> GetSpielplanL3(int SpieltagId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [SpielplaeneL3] WHERE SpieltagId =" + SpieltagId, conn);
                Spielplan Spielplan = null;
                List<Spielplan> Spielplanlist = new List<Spielplan>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Spielplan = new Spielplan();

                        Spielplan.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        Spielplan.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        Spielplan.LigaID = int.Parse(reader["LigaID"].ToString());
                        Spielplan.StadionID = int.Parse(reader["StadionID"].ToString());
                        Spielplan.SpieltagNr = reader["SpieltagNr"].ToString();
                        Spielplan.Saison = reader["Saison"].ToString();
                        Spielplan.Verein1 = reader["Verein1"].ToString();
                        Spielplan.Verein2 = reader["Verein2"].ToString();
                        Spielplan.Verein1_Nr = reader["Verein1_Nr"].ToString();
                        Spielplan.Verein2_Nr = reader["Verein2_Nr"].ToString();
                        Spielplan.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        Spielplan.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        Spielplan.Datum = DateTime.Parse(reader["Datum"].ToString());
                        Spielplan.Ort = reader["Ort"].ToString();
                        Spielplan.Schiedrichter = reader["Schiedrichter"].ToString();
                        Spielplan.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                        Spielplan.Zuschauer = int.Parse(reader["Zuschauer"].ToString());

                    }
                }
                conn.Close();
                return Spielplan;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Spielplan> AddSpielplanL3(Spielplan Spielplan)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO SpielplaeneL3 ([SpieltagId],[Saison],[SaisonID],[StadionID],[LigaID],[Verein1_Nr],[Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum],[Ort],[Schiedrichter],[Abgeschlossen],[Zuschauer])" +
                    " VALUES(@SpieltagId,@Saison,@SaisonID,@StadionID,@LigaID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Abgeschlossen,@Zuschauer)";

                cmd.Parameters.AddWithValue("@SpieltagNr", Spielplan.SpieltagNr);
                cmd.Parameters.AddWithValue("@Saison", Spielplan.Saison);
                cmd.Parameters.AddWithValue("@SaisonID", Spielplan.SaisonID);
                cmd.Parameters.AddWithValue("@StadionID", Spielplan.StadionID);
                cmd.Parameters.AddWithValue("@LigaID", Spielplan.LigaID);
                cmd.Parameters.AddWithValue("@Verein1_Nr", Spielplan.Verein1_Nr);
                cmd.Parameters.AddWithValue("@Verein2_Nr", Spielplan.Verein2_Nr);
                cmd.Parameters.AddWithValue("@Verein1", Spielplan.Verein1);
                cmd.Parameters.AddWithValue("@Verein2", Spielplan.Verein2);
                cmd.Parameters.AddWithValue("@Tore1_Nr", Spielplan.Tore1_Nr);
                cmd.Parameters.AddWithValue("@Tore2_Nr", Spielplan.Tore2_Nr);
                cmd.Parameters.AddWithValue("@Datum", Spielplan.Datum);
                cmd.Parameters.AddWithValue("@Ort", Spielplan.Ort);
                cmd.Parameters.AddWithValue("@Schiedrichter", Spielplan.Schiedrichter);
                cmd.Parameters.AddWithValue("@Abgeschlossen", Spielplan.Abgeschlossen);
                cmd.Parameters.AddWithValue("@Zuschauer", Spielplan.Zuschauer);

                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return Spielplan;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Spielplan> UpdateSpielplanL3(Spielplan Spielplan)
        {
            int bAbgeschlossen;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
               
                if (Spielplan.Abgeschlossen == false)
                    bAbgeschlossen = 0;
                else
                    bAbgeschlossen = -1;

                cmd.CommandText = "UPDATE [dbo].[SpielplaeneL3] SET " +
                        " [SpieltagNr] = " + Spielplan.SpieltagNr +
                        ",[Saison] = '" + Spielplan.Saison + "'" +
                        ",[SaisonID] = " + Spielplan.SaisonID +
                        ",[StadionID] = " + Spielplan.StadionID +
                        ",[LigaID] = " + Spielplan.LigaID +
                        ",[Verein1_Nr] = '" + Spielplan.Verein1_Nr + "'" +
                        ",[Verein1] = '" + Spielplan.Verein1 + "'" +
                        ",[Verein2_Nr] = " + Spielplan.Verein2_Nr +
                        ",[Verein2] = '" + Spielplan.Verein2 + "'" +
                        ",[Tore1_Nr] = " + Spielplan.Tore1_Nr +
                        ",[Tore2_Nr] = " + Spielplan.Tore2_Nr +
                        ",[DatumString] = '" + Spielplan.DatumString + "'" +
                        ",[Datum] = " + Spielplan.Datum +
                        ",[Ort] = '" + Spielplan.Ort + "'" +
                        ",[Schiedrichter] = '" + Spielplan.Schiedrichter + "'" +
                        ",[Abgeschlossen] =" + bAbgeschlossen +
                        ",[Zuschauer] =" + Spielplan.Zuschauer +
                        " WHERE  [SpieltagId] = " + Spielplan.SpieltagId;

                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return Spielplan;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Spielplan> DeleteSpielplanL3(int SpieltagId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [dbo].[SpielplaeneL3] Where SpieltagId= @SpieltagId";

                cmd.Parameters.AddWithValue("@SpieltagId", SpieltagId);

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
    }
}

