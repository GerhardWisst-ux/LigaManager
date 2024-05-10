using LigaManagement.Api.Models;
using LigaManagement.Models;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;

namespace LigaManagerManagement.Api.Models
{
    public class SpieltageRepository : ISpieltagRepository
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

                Debug.Print(ex.Message);
                return null;
            }

            //var result = await appDbContext.Spieltage.AddAsync(spieltag);
            //await appDbContext.SaveChangesAsync();
            //return result.Entity;
        }

        public async Task<Spieltag> DeleteSpieltag(int SpieltagId)
        {
            //var result = await appDbContext.Spieltage
            //   .FirstOrDefaultAsync(e => e.SpieltagId == SpieltagId);
            //if (result != null)
            //{
            //    appDbContext.Spieltage.Remove(result);
            //    await appDbContext.SaveChangesAsync();
            //    return result;
            //}

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
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }
            //return await appDbContext.Spieltage               
            //    .FirstOrDefaultAsync(d => d.SpieltagId == SpieltagId);
        }

        public async Task<IEnumerable<Spieltag>> GetSpieltage()
        {

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Spieltage] ", conn);
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

                        Spieltaglist.Add(spieltag);
                    }
                }
                conn.Close();
                return Spieltaglist;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }
            //return await appDbContext.Spieltage                
            //    .ToListAsync();
        }

        public int AktSpieltag(int SaisonID, int LigaID )
        {
            int iMaxSpieltag = 0;
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();



            try
            {
                if (LigaID < 4)
                {
                    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [Spieltage] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "' and LigaID = '" + LigaID + "'", conn);
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
                else if (LigaID == 4)
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
                else if (LigaID == 6)
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
                else if (LigaID == 7)
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
                else if (LigaID == 8)
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
                else if (LigaID == 9)
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

                conn.Close();
                return iMaxSpieltag;
            }
            catch (Exception)
            {

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

                Debug.Print(ex.Message);
                return null;
            }

        }
    }
}

