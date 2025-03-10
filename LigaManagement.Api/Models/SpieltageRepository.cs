﻿using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace LigaManagerManagement.Api.Models
{
    public class SpieltageRepository : ISpieltageRepository
    {
        public async Task<Spieltag> AddSpieltag(Spieltag spieltag)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Spieltage ([SpieltagNr],[Saison],[SaisonID],[LigaID],[Verein1_Nr],[Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum],[Ort],[Schiedrichter],[Abgeschlossen],[Zuschauer])" +
                    " VALUES(@SpieltagNr,@Saison,@SaisonID,@LigaID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Abgeschlossen,@Zuschauer)";

                cmd.Parameters.AddWithValue("@SpieltagNr", spieltag.SpieltagNr);
                cmd.Parameters.AddWithValue("@Saison", spieltag.Saison);
                cmd.Parameters.AddWithValue("@SaisonID", spieltag.SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", spieltag.LigaID);
                cmd.Parameters.AddWithValue("@Verein1_Nr", spieltag.Verein1_Nr);
                cmd.Parameters.AddWithValue("@Verein2_Nr", spieltag.Verein2_Nr);
                cmd.Parameters.AddWithValue("@Verein1", spieltag.Verein1);
                cmd.Parameters.AddWithValue("@Verein2", spieltag.Verein2);
                cmd.Parameters.AddWithValue("@Tore1_Nr", spieltag.Tore1_Nr);
                cmd.Parameters.AddWithValue("@Tore2_Nr", spieltag.Tore2_Nr);
                cmd.Parameters.AddWithValue("@Datum", spieltag.Datum);
                cmd.Parameters.AddWithValue("@Ort", spieltag.Ort);
                cmd.Parameters.AddWithValue("@Schiedrichter", spieltag.Schiedrichter);
                cmd.Parameters.AddWithValue("@Abgeschlossen", spieltag.Abgeschlossen);
                cmd.Parameters.AddWithValue("@Zuschauer", spieltag.Zuschauer);                

                cmd.ExecuteNonQuery();

                conn.Close();

                return spieltag;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

            //var result = await appDbContext.Spieltage.AddAsync(spieltag);
            //await appDbContext.SaveChangesAsync();
            //return result.Entity;
        }

        public async Task<Spieltag> DeleteSpieltag(int SpieltagId)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Spieltage] Where SpieltagId= @SpieltagId";

            cmd.Parameters.AddWithValue("@Id", SpieltagId);

          //  cmd.ExecuteNonQuery();

            conn.Close();

            return null;            
        }

        public Task<Spieltag> GetAktSpieltag()
        {
            throw new NotImplementedException();
        }

        public async Task<Spieltag> GetSpieltag(int spieltagId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Spieltage] WHERE SpieltagId =" + spieltagId, conn);
                Spieltag spieltag = null;
                List<Spieltag> Spieltaglist = new List<Spieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spieltag = new Spieltag();

                        spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        spieltag.LigaID = int.Parse(reader["LigaID"].ToString());
                        spieltag.SpieltagNr = reader["SpieltagNr"].ToString();
                        spieltag.Saison = reader["Saison"].ToString();
                        spieltag.Verein1 = reader["Verein1"].ToString();
                        spieltag.Verein2 = reader["Verein2"].ToString();
                        spieltag.Verein1_Nr = reader["Verein1_Nr"].ToString();
                        spieltag.Verein2_Nr = reader["Verein2_Nr"].ToString();
                        spieltag.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        spieltag.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        spieltag.Datum = DateTime.Parse(reader["Datum"].ToString());
                        spieltag.Ort = reader["Ort"].ToString();
                        spieltag.Schiedrichter = reader["Schiedrichter"].ToString();
                        spieltag.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                        spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());

                    }
                }
                conn.Close();
                return spieltag;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }            
        }

        public async Task<IEnumerable<Spieltag>> GetSpieltage()
        {
            
            try
            {

                using (var conn = new SqlConnection(Globals.connstring))
                {
                    await conn.OpenAsync();

                    SqlCommand command = new SqlCommand("sp_spieltage", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    Spieltag spieltag = null;
                    List<Spieltag> Spieltaglist = new List<Spieltag>();
                    await using (SqlDataReader reader = command.ExecuteReader())

                    ////SqlCommand command = new SqlCommand("SELECT * FROM [Spieltage] ", conn);
                    ////Spieltag spieltag = null;
                    ////List<Spieltag> Spieltaglist = new List<Spieltag>();
                    ////using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            spieltag = new Spieltag();

                            spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                            spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());
                            spieltag.LigaID = int.Parse(reader["LigaID"].ToString());
                            spieltag.SpieltagNr = reader["SpieltagNr"].ToString();
                            spieltag.Saison = reader["Saison"].ToString();
                            spieltag.Verein1 = reader["Verein1"].ToString();
                            spieltag.Verein2 = reader["Verein2"].ToString();
                            spieltag.Verein1_Nr = reader["Verein1_Nr"].ToString();
                            spieltag.Verein2_Nr = reader["Verein2_Nr"].ToString();
                            spieltag.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                            spieltag.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                            spieltag.Datum = DateTime.Parse(reader["Datum"].ToString());
                            spieltag.Ort = reader["Ort"].ToString();
                            spieltag.Schiedrichter = reader["Schiedrichter"].ToString();
                            spieltag.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                            spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                            spieltag.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                            spieltag.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();

                            Spieltaglist.Add(spieltag);
                        }
                    }


                    //{
                    //    while (reader.Read())
                    //    {
                    //        spieltag = new Spieltag();

                    //        spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                    //        spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());
                    //        spieltag.LigaID = int.Parse(reader["LigaID"].ToString());
                    //        spieltag.SpieltagNr = reader["SpieltagNr"].ToString();
                    //        spieltag.Saison = reader["Saison"].ToString();
                    //        spieltag.Verein1 = reader["Verein1"].ToString();
                    //        spieltag.Verein2 = reader["Verein2"].ToString();
                    //        spieltag.Verein1_Nr = reader["Verein1_Nr"].ToString();
                    //        spieltag.Verein2_Nr = reader["Verein2_Nr"].ToString();
                    //        spieltag.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                    //        spieltag.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                    //        spieltag.Datum = DateTime.Parse(reader["Datum"].ToString());
                    //        spieltag.Ort = reader["Ort"].ToString();
                    //        spieltag.Schiedrichter = reader["Schiedrichter"].ToString();
                    //        spieltag.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                    //        spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                    //        spieltag.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                    //        spieltag.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();

                    //        Spieltaglist.Add(spieltag);
                    //    }
                    //}
                    return Spieltaglist;
                }
                
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public int AktSpieltag(int SaisonID, int LigaID)
        {
            int iMaxSpieltag = 0;
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            try
            {
                if (Globals.LigaNummer == 1 || Globals.LigaNummer == 2)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [Spieltage] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                            iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                        else
                            iMaxSpieltag = 1;
                    }

                } 
                else if (Globals.LigaNummer == 4 || Globals.LigaNummer == 15)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltagePL] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                                iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                            else
                                iMaxSpieltag = 1;
                        }
                    }
                }                             

                else if (Globals.LigaNummer == 5)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltageIT] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                                iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                            else
                                iMaxSpieltag = 1;
                        }
                    }
                }
                else if (Globals.LigaNummer == 6)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltageFR] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                                iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                            else
                                iMaxSpieltag = 1;
                        }
                    }
                }
                else if (Globals.LigaNummer == 7)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltageES] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                                iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                            else
                                iMaxSpieltag = 1;
                        }
                    }
                }
                else if (Globals.LigaNummer == 8)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltageNL] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                                iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                            else
                                iMaxSpieltag = 1;
                        }
                    }
                }
                else if (Globals.LigaNummer == 9)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltagePT] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                                iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                            else
                                iMaxSpieltag = 1;
                        }
                    }
                }
                else if (Globals.LigaNummer == 10)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltageTU] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                                iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                            else
                                iMaxSpieltag = 1;
                        }
                    }
                }
                else if (Globals.LigaNummer == 11)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltageBE] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                                iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                            else
                                iMaxSpieltag = 1;
                        }
                    }
                }
                else if (Globals.LigaNummer == 3 || Globals.LigaNummer == 20)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltageL3] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["MAXSPIELTAG"].ToString()))
                                iMaxSpieltag = (int)reader["MAXSPIELTAG"];
                            else
                                iMaxSpieltag = 1;
                        }
                    }

                }

                conn.Close();
                return iMaxSpieltag;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return 1;
            }
        }

        public async Task<Spieltag> UpdateSpieltag(Spieltag spieltag)
        {
            int bAbgeschlossen;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = "UPDATE Spieltage (SpieltagNr,Saison,SaisonID,LigaID,Verein1_Nr,Verein1,Verein2_Nr,Verein2,Tore1_Nr,Tore2_Nr,Datum,Ort,Schiedrichter,Abgeschlossen,Zuschauer)" +
                //" VALUES(@SpieltagNr,@Saison,@SaisonID,@LigaID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Abgeschlossen,@Zuschauer)";

                //cmd.CommandText = "UPDATE Spieltage(Datum)" +
                //" VALUES(@Datum)";

                if (spieltag.Abgeschlossen == false)
                    bAbgeschlossen = 0;
                else
                    bAbgeschlossen = -1;

                cmd.CommandText = "UPDATE [dbo].[Spieltage] SET " +
                        " [SpieltagNr] = " + spieltag.SpieltagNr +
                        ",[Saison] = '" + spieltag.Saison + "'" +
                        ",[SaisonID] = " + spieltag.SaisonID +
                        ",[LigaID] = " + spieltag.LigaID +
                        ",[Verein1_Nr] = '" + spieltag.Verein1_Nr + "'" +
                        //",[Verein1] = '" + spieltag.Verein1 + "'" +
                        ",[Verein2_Nr] = " + spieltag.Verein2_Nr +
                        //",[Verein2] = '" + spieltag.Verein2 + "'" +
                        ",[Tore1_Nr] = " + spieltag.Tore1_Nr +
                        ",[Tore2_Nr] = " + spieltag.Tore2_Nr +
                        ",[Datum] = '" + spieltag.Datum + "'" +
                        ",[Ort] = '" + spieltag.Ort + "'" +
                        ",[Schiedrichter] = '" + spieltag.Schiedrichter + "'" +
                        ",[Abgeschlossen] =" + bAbgeschlossen +
                        ",[Zuschauer] =" + spieltag.Zuschauer +
                        " WHERE  [SpieltagId] = " + spieltag.SpieltagId;

                //cmd.Parameters.AddWithValue("@SpieltagNr", spieltag.SpieltagNr);
                //cmd.Parameters.AddWithValue("@Saison", spieltag.Saison);
                //cmd.Parameters.AddWithValue("@SaisonID", spieltag.SaisonID);
                //cmd.Parameters.AddWithValue("@LigaID", spieltag.LigaID);
                //cmd.Parameters.AddWithValue("@Verein1_Nr", spieltag.Verein1_Nr);
                //cmd.Parameters.AddWithValue("@Verein2_Nr", spieltag.Verein2_Nr);
                //cmd.Parameters.AddWithValue("@Verein1", spieltag.Verein2);
                //cmd.Parameters.AddWithValue("@Verein2", spieltag.Verein2);
                //cmd.Parameters.AddWithValue("@Tore1_Nr", spieltag.Tore1_Nr);
                //cmd.Parameters.AddWithValue("@Tore2_Nr", spieltag.Tore2_Nr);
                //cmd.Parameters.AddWithValue("@Datum", spieltag.Datum);
                //cmd.Parameters.AddWithValue("@Ort", spieltag.Ort);
                //cmd.Parameters.AddWithValue("@Schiedrichter", spieltag.Schiedrichter);
                //cmd.Parameters.AddWithValue("@Abgeschlossen", spieltag.Abgeschlossen);
                //cmd.Parameters.AddWithValue("@Zuschauer", spieltag.Zuschauer);

                cmd.ExecuteNonQuery();

                conn.Close();

                return spieltag;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<IEnumerable<Spieltag>> GetSpieltageL3()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [SpieltageL3] ", conn);
                Spieltag spieltag = null;
                List<Spieltag> Spieltaglist = new List<Spieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spieltag = new Spieltag();

                        spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        spieltag.LigaID = int.Parse(reader["LigaID"].ToString());
                        spieltag.SpieltagNr = reader["SpieltagNr"].ToString();
                        spieltag.Saison = reader["Saison"].ToString();
                        spieltag.Verein1 = reader["Verein1"].ToString();
                        spieltag.Verein2 = reader["Verein2"].ToString();
                        spieltag.Verein1_Nr = reader["Verein1_Nr"].ToString();
                        spieltag.Verein2_Nr = reader["Verein2_Nr"].ToString();
                        spieltag.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        spieltag.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        spieltag.Datum = DateTime.Parse(reader["Datum"].ToString());
                        spieltag.Ort = reader["Ort"].ToString();
                        spieltag.Schiedrichter = reader["Schiedrichter"].ToString();
                        spieltag.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                        spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        spieltag.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                        spieltag.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();

                        Spieltaglist.Add(spieltag);
                    }
                }
                conn.Close();
                return Spieltaglist;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Spieltag> GetSpieltagL3(int spieltagId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [SpieltageL3] WHERE SpieltagId =" + spieltagId, conn);
                Spieltag spieltag = null;
                List<Spieltag> Spieltaglist = new List<Spieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spieltag = new Spieltag();

                        spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        spieltag.LigaID = int.Parse(reader["LigaID"].ToString());
                        spieltag.SpieltagNr = reader["SpieltagNr"].ToString();
                        spieltag.Saison = reader["Saison"].ToString();
                        spieltag.Verein1 = reader["Verein1"].ToString();
                        spieltag.Verein2 = reader["Verein2"].ToString();
                        spieltag.Verein1_Nr = reader["Verein1_Nr"].ToString();
                        spieltag.Verein2_Nr = reader["Verein2_Nr"].ToString();
                        spieltag.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        spieltag.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        spieltag.Datum = DateTime.Parse(reader["Datum"].ToString());
                        spieltag.Ort = reader["Ort"].ToString();
                        spieltag.Schiedrichter = reader["Schiedrichter"].ToString();
                        spieltag.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                        spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());

                    }
                }
                conn.Close();
                return spieltag;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
            
        }

        public async Task<Spieltag> AddSpieltagL3(Spieltag spieltag)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO SpieltageL3 ([SpieltagNr],[Saison],[SaisonID],[LigaID],[Verein1_Nr],[Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum],[Ort],[Schiedrichter],[Abgeschlossen],[Zuschauer])" +
                    " VALUES(@SpieltagNr,@Saison,@SaisonID,@LigaID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Abgeschlossen,@Zuschauer)";

                cmd.Parameters.AddWithValue("@SpieltagNr", spieltag.SpieltagNr);
                cmd.Parameters.AddWithValue("@Saison", spieltag.Saison);
                cmd.Parameters.AddWithValue("@SaisonID", spieltag.SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", spieltag.LigaID);
                cmd.Parameters.AddWithValue("@Verein1_Nr", spieltag.Verein1_Nr);
                cmd.Parameters.AddWithValue("@Verein2_Nr", spieltag.Verein2_Nr);
                cmd.Parameters.AddWithValue("@Verein1", spieltag.Verein1);
                cmd.Parameters.AddWithValue("@Verein2", spieltag.Verein2);
                cmd.Parameters.AddWithValue("@Tore1_Nr", spieltag.Tore1_Nr);
                cmd.Parameters.AddWithValue("@Tore2_Nr", spieltag.Tore2_Nr);
                cmd.Parameters.AddWithValue("@Datum", spieltag.Datum);
                cmd.Parameters.AddWithValue("@Ort", spieltag.Ort);
                cmd.Parameters.AddWithValue("@Schiedrichter", spieltag.Schiedrichter);
                cmd.Parameters.AddWithValue("@Abgeschlossen", spieltag.Abgeschlossen);
                cmd.Parameters.AddWithValue("@Zuschauer", spieltag.Zuschauer);

                cmd.ExecuteNonQuery();

                conn.Close();

                return spieltag;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Spieltag> UpdateSpieltagL3(Spieltag spieltag)
        {
            int bAbgeschlossen;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = "UPDATE Spieltage (SpieltagNr,Saison,SaisonID,LigaID,Verein1_Nr,Verein1,Verein2_Nr,Verein2,Tore1_Nr,Tore2_Nr,Datum,Ort,Schiedrichter,Abgeschlossen,Zuschauer)" +
                //" VALUES(@SpieltagNr,@Saison,@SaisonID,@LigaID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Abgeschlossen,@Zuschauer)";

                //cmd.CommandText = "UPDATE Spieltage(Datum)" +
                //" VALUES(@Datum)";

                if (spieltag.Abgeschlossen == false)
                    bAbgeschlossen = 0;
                else
                    bAbgeschlossen = -1;

                cmd.CommandText = "UPDATE [dbo].[SpieltageL3] SET " +
                        " [SpieltagNr] = " + spieltag.SpieltagNr +
                        ",[Saison] = '" + spieltag.Saison + "'" +
                        ",[SaisonID] = " + spieltag.SaisonID +
                        ",[LigaID] = " + spieltag.LigaID +
                        ",[Verein1_Nr] = '" + spieltag.Verein1_Nr + "'" +
                        ",[Verein1] = '" + spieltag.Verein1 + "'" +
                        ",[Verein2_Nr] = " + spieltag.Verein2_Nr +
                        ",[Verein2] = '" + spieltag.Verein2 + "'" +
                        ",[Tore1_Nr] = " + spieltag.Tore1_Nr +
                        ",[Tore2_Nr] = " + spieltag.Tore2_Nr +
                        ",[Datum] = '" + spieltag.Datum + "'" +
                        ",[Ort] = '" + spieltag.Ort + "'" +
                        ",[Schiedrichter] = '" + spieltag.Schiedrichter + "'" +
                        ",[Abgeschlossen] =" + bAbgeschlossen +
                        ",[Zuschauer] =" + spieltag.Zuschauer +
                        " WHERE  [SpieltagId] = " + spieltag.SpieltagId;               

                cmd.ExecuteNonQuery();

                conn.Close();

                return spieltag;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Spieltag> DeleteSpieltagL3(int SpieltagId)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[SpieltageL3] Where SpieltagId= @SpieltagId";

            cmd.Parameters.AddWithValue("@Id", SpieltagId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;
        }
    }
}

