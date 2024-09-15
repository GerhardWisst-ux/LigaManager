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
    public class SpieltageCLRepository : ISpieltageCLRepository
    {
        public async Task<PokalergebnisCL_EM_WMSpieltag> AddSpieltag(PokalergebnisCL_EM_WMSpieltag spieltag)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO SpieltageCL ([Saison],[SaisonID],[Verein1_Nr],[Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum],[Ort],[Schiedrichter],[Zuschauer],[Land1_Nr],[Land2_Nr],Verlängerung,Runde,Elfmeterschiessen)" +
                    " VALUES(@Saison,@SaisonID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Zuschauer, @Land1_Nr, @Land2_Nr,@Verlängerung,@Runde,@Elfmeterschiessen)";
                
                cmd.Parameters.AddWithValue("@Saison", spieltag.Saison);
                cmd.Parameters.AddWithValue("@SaisonID", spieltag.SaisonID);                
                cmd.Parameters.AddWithValue("@Verein1_Nr", spieltag.Verein1_Nr);
                cmd.Parameters.AddWithValue("@Land1_Nr", spieltag.Land1_Nr);
                cmd.Parameters.AddWithValue("@Verein2_Nr", spieltag.Verein2_Nr);
                cmd.Parameters.AddWithValue("@Land2_Nr", spieltag.Verein2_Nr);
                cmd.Parameters.AddWithValue("@Verein1", spieltag.Verein1);
                cmd.Parameters.AddWithValue("@Verein2", spieltag.Verein2);
                cmd.Parameters.AddWithValue("@Tore1_Nr", spieltag.Tore1_Nr);
                cmd.Parameters.AddWithValue("@Tore2_Nr", spieltag.Tore2_Nr);
                cmd.Parameters.AddWithValue("@Datum", spieltag.Datum);
                cmd.Parameters.AddWithValue("@Ort", spieltag.Ort);
                cmd.Parameters.AddWithValue("@Schiedrichter", spieltag.Schiedrichter);                
                cmd.Parameters.AddWithValue("@Zuschauer", spieltag.Zuschauer);
                cmd.Parameters.AddWithValue("@Verlängerung", spieltag.Verlängerung);
                cmd.Parameters.AddWithValue("@Runde", spieltag.Runde);
                cmd.Parameters.AddWithValue("@Elfmeterschiessen", spieltag.Elfmeterschiessen);

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

        public async Task<PokalergebnisCL_EM_WMSpieltag> DeleteSpieltag(int SpieltagId)
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

        public Task<PokalergebnisCL_EM_WMSpieltag> GetAktSpieltag()
        {
            throw new NotImplementedException();
        }

        public async Task<PokalergebnisCL_EM_WMSpieltag> GetSpieltag(int spieltagId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [SpieltageCL] WHERE SpieltagId =" + spieltagId, conn);
                PokalergebnisCL_EM_WMSpieltag spieltag = null;
                List<Spieltag> Spieltaglist = new List<Spieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spieltag = new PokalergebnisCL_EM_WMSpieltag();

                        spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());                                                
                        spieltag.Saison = reader["Saison"].ToString();
                        spieltag.Verein1 = reader["Verein1"].ToString();
                        spieltag.Verein2 = reader["Verein2"].ToString();
                        spieltag.Verein1_Nr = int.Parse(reader["Verein1_Nr"].ToString());
                        spieltag.Land1_Nr = int.Parse(reader["Land1_Nr"].ToString());
                        spieltag.Verein2_Nr = int.Parse(reader["Verein2_Nr"].ToString());
                        spieltag.Land2_Nr = int.Parse(reader["Land2_Nr"].ToString());
                        spieltag.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        spieltag.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());                        
                        spieltag.Datum = DateTime.Parse(reader["Datum"].ToString());
                        spieltag.Ort = reader["Ort"].ToString();
                        spieltag.Schiedrichter = reader["Schiedrichter"].ToString();                        
                        spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        spieltag.Land1_Nr = int.Parse(reader["Land1_Nr"].ToString());
                        spieltag.Elfmeterschiessen = bool.Parse(reader["Elfmeterschiessen"].ToString());
                        spieltag.Verlängerung = bool.Parse(reader["Verlängerung"].ToString());
                        spieltag.GroupID = int.Parse(reader["GroupID"].ToString());
                        spieltag.Runde = reader["Runde"].ToString();
                        spieltag.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                        spieltag.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();
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
            //return await appDbContext.Spieltage               
            //    .FirstOrDefaultAsync(d => d.SpieltagId == SpieltagId);
        }

        public async Task<IEnumerable<PokalergebnisCL_EM_WMSpieltag>> GetSpieltage()
        {

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [SpieltageCL] ", conn);
                PokalergebnisCL_EM_WMSpieltag spieltag = null;
                List<PokalergebnisCL_EM_WMSpieltag> Spieltaglist = new List<PokalergebnisCL_EM_WMSpieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spieltag = new PokalergebnisCL_EM_WMSpieltag();

                        spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());                        
                        spieltag.Saison = reader["Saison"].ToString();
                        spieltag.Verein1 = reader["Verein1"].ToString();
                        spieltag.Verein2 = reader["Verein2"].ToString();
                        spieltag.Verein1_Nr = int.Parse(reader["Verein1_Nr"].ToString());
                        spieltag.Land1_Nr = int.Parse(reader["Land1_Nr"].ToString());
                        spieltag.Verein2_Nr = int.Parse(reader["Verein2_Nr"].ToString());
                        spieltag.Land2_Nr = int.Parse(reader["Land2_Nr"].ToString());
                        spieltag.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        spieltag.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());                        
                        spieltag.Datum = DateTime.Parse(reader["Datum"].ToString());
                        spieltag.Ort = reader["Ort"].ToString();
                        spieltag.Schiedrichter = reader["Schiedrichter"].ToString();
                        spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        spieltag.Elfmeterschiessen = bool.Parse(reader["Elfmeterschiessen"].ToString());
                        spieltag.Verlängerung = bool.Parse(reader["Verlängerung"].ToString());
                        spieltag.GroupID = int.Parse(reader["GroupID"].ToString());
                        spieltag.Runde = reader["Runde"].ToString();
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

                throw ex;
            }
               
            
            
            //return await appDbContext.Spieltage                
            //    .ToListAsync();
        }

        public int AktSpieltag(int SaisonID)
        {
            int iMaxSpieltag = 0;
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltageCL] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "'", conn);

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
            conn.Close();
            return iMaxSpieltag;
        }

        public async Task<PokalergebnisCL_EM_WMSpieltag> UpdateSpieltag(PokalergebnisCL_EM_WMSpieltag spieltag)
        {
           
            try
            {
                int bVerlängerung;
                int bElfmeterschiessen;

                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;


                if (spieltag.Verlängerung == false)
                    bVerlängerung = 0;
                else
                    bVerlängerung = 1;

                if (spieltag.Elfmeterschiessen == false)
                    bElfmeterschiessen = 0;
                else
                    bElfmeterschiessen = 1;

                cmd.CommandText = "UPDATE [dbo].[SpieltageCL] SET " +                        
                        " [Saison] = '" + spieltag.Saison + "'" +
                        ",[SaisonID] = " + spieltag.SaisonID +                        
                        ",[Verein1_Nr] = '" + spieltag.Verein1_Nr + "'" +
                        ",[Land1_Nr] = '" + spieltag.Land1_Nr + "'" +
                        ",[Verein1] = '" + spieltag.Verein1 + "'" +
                        ",[Land2_Nr] = '" + spieltag.Land2_Nr + "'" +
                        ",[Verein2_Nr] = " + spieltag.Verein2_Nr +
                        ",[Verein2] = '" + spieltag.Verein2 + "'" +
                        ",[Tore1_Nr] = " + spieltag.Tore1_Nr +
                        ",[Tore2_Nr] = " + spieltag.Tore2_Nr +
                        ",[Datum] = '" + spieltag.Datum + "'" +
                        ",[Ort] = '" + spieltag.Ort + "'" +
                        ",[Schiedrichter] = '" + spieltag.Schiedrichter + "'" +
                        ",[Zuschauer] =" + spieltag.Zuschauer +
                        ",[Runde] = '" + spieltag.Runde + "'" +
                        ",[Verlängerung] = " + bVerlängerung +
                        ",[Elfmeterschiessen] = " + bElfmeterschiessen +
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

        public async Task<List<Verein>> GetVereine(int iGroupID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT DISTINCT [Verein1_Nr],[Verein1] FROM [dbo].[SpieltageCL] where groupID = " + iGroupID, conn);

                Verein verein = null;
                List<Verein> vereineList = new List<Verein>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new Verein();                        
                        verein.VereinNr = int.Parse(reader["Verein1_Nr"].ToString());
                        verein.Vereinsname1 = reader["Verein1"].ToString();

                        vereineList.Add(verein);
                    }
                }
                conn.Close();
                return vereineList;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
    }
}

