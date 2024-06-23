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
    public class SpieltageEMWMRepository : ISpieltageEMWMRepository
    {
        public async Task<PokalergebnisCLSpieltag> AddSpieltag(PokalergebnisCLSpieltag spieltag)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO SpieltageEMWM ([Saison],[SaisonID],[LigaID],[Verein1_Nr],[Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum],[Ort],[Schiedrichter],[Zuschauer],[Land1_Nr],[Land2_Nr],Verlängerung,Runde,RundeDetail,Elfmeterschiessen,GroupID,Gruppe,Abgeschlossen,TeamIconUrl1,TeamIconUrl2)" +
                    " VALUES(@Saison,@SaisonID,@LigaID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Zuschauer, @Land1_Nr, @Land2_Nr,@Verlängerung,@Runde,@RundeDetail,@Elfmeterschiessen,@GroupID,@Gruppe,@Abgeschlossen,@TeamIconUrl1,@TeamIconUrl2)";
                
                cmd.Parameters.AddWithValue("@Saison", spieltag.Saison);
                cmd.Parameters.AddWithValue("@SaisonID", spieltag.SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", spieltag.LigaID);
                cmd.Parameters.AddWithValue("@Verein1_Nr", spieltag.Verein1_Nr);
                cmd.Parameters.AddWithValue("@Land1_Nr", spieltag.Land1_Nr);
                cmd.Parameters.AddWithValue("@Verein2_Nr", spieltag.Verein2_Nr);
                cmd.Parameters.AddWithValue("@Land2_Nr", spieltag.Land2_Nr);
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
                cmd.Parameters.AddWithValue("@RundeDetail", spieltag.RundeDetail);
                cmd.Parameters.AddWithValue("@GroupID", spieltag.GroupID);
                cmd.Parameters.AddWithValue("@Gruppe", spieltag.GroupID);
                cmd.Parameters.AddWithValue("@Elfmeterschiessen", spieltag.Elfmeterschiessen);
                cmd.Parameters.AddWithValue("@Abgeschlossen", spieltag.Elfmeterschiessen);
                cmd.Parameters.AddWithValue("@TeamIconUrl1", "");
                cmd.Parameters.AddWithValue("@TeamIconUrl2", "");

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

        public async Task<PokalergebnisCLSpieltag> DeleteSpieltag(int SpieltagId)
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

        public Task<PokalergebnisCLSpieltag> GetAktSpieltag()
        {
            throw new NotImplementedException();
        }

        public async Task<PokalergebnisCLSpieltag> GetSpieltag(int spieltagId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [SpieltageEMWM] WHERE SpieltagId =" + spieltagId, conn);
                PokalergebnisCLSpieltag spieltag = null;
                List<Spieltag> Spieltaglist = new List<Spieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spieltag = new PokalergebnisCLSpieltag();

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

        public async Task<IEnumerable<PokalergebnisCLSpieltag>> GetSpieltage()
        {

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [SpieltageEMWM] ", conn);
                PokalergebnisCLSpieltag spieltag = null;
                List<PokalergebnisCLSpieltag> Spieltaglist = new List<PokalergebnisCLSpieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spieltag = new PokalergebnisCLSpieltag();

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

            SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [SpieltageEMWM] WHERE Datum<GETDATE() and SaisonID = '" + SaisonID + "'", conn);

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

        public async Task<PokalergebnisCLSpieltag> UpdateSpieltag(PokalergebnisCLSpieltag spieltag)
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

                cmd.CommandText = "UPDATE [dbo].[SpieltageEMWM] SET " +                        
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

        public async Task<List<VereinEMWM>> GetVereine(int iGroupID)
        {
            try
            {
                SqlCommand command = new SqlCommand();

                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                if (Globals.currentEMWMSaison == "EM 2024")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                        " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2024 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2022")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                       " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2022 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2022")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                        " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2020 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2020")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                        " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM]where GroupID2018 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2016")
                   command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                        " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2016 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2014")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                       " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2014 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2012")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                        " [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2012 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2010")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID] ,[Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                        " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2010 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2008")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                        " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2008 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2006")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                        " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2006 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2004")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                       " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2004 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2000")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                       " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID2000 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1996")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                    " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] FROM [dbo].[MannschaftEMWM] where GroupID2000 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1996")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                    " [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID1996 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1992")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                    " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID1992 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1988")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                    " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM]where GroupID1988 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1984")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                    " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID1984 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1980")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                   " [GroupID2000], [GroupID1996], [GroupID1992], [GroupID1988], [GroupID1984], [GroupID1980] FROM [dbo].[MannschaftEMWM] where GroupID1980 = " + iGroupID, conn);

                VereinEMWM verein = null;
                List<VereinEMWM> vereineList = new List<VereinEMWM>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verein = new VereinEMWM();                        
                        verein.VereinNr = int.Parse(reader["MannschaftNr"].ToString());
                        verein.Vereinsname1 = reader["MannschaftName1"].ToString();
                        verein.Vereinsname2 = reader["MannschaftName2"].ToString();
                        verein.GroupID2024 = int.Parse(reader["GroupID2024"].ToString());
                        verein.GroupID2022 = int.Parse(reader["GroupID2022"].ToString());
                        verein.GroupID2020 = int.Parse(reader["GroupID2020"].ToString());
                        verein.GroupID2018 = int.Parse(reader["GroupID2018"].ToString());
                        verein.GroupID2016 = int.Parse(reader["GroupID2016"].ToString());
                        verein.GroupID2014 = int.Parse(reader["GroupID2014"].ToString());
                        verein.GroupID2012 = int.Parse(reader["GroupID2012"].ToString());
                        verein.GroupID2010 = int.Parse(reader["GroupID2010"].ToString());
                        verein.GroupID2008 = int.Parse(reader["GroupID2008"].ToString());
                        verein.GroupID2004 = int.Parse(reader["GroupID2004"].ToString());
                        verein.GroupID2000 = int.Parse(reader["GroupID2000"].ToString());
                        verein.GroupID1996 = int.Parse(reader["GroupID1996"].ToString());
                        verein.GroupID1992 = int.Parse(reader["GroupID1992"].ToString());
                        verein.GroupID1988 = int.Parse(reader["GroupID1988"].ToString());
                        verein.GroupID1984 = int.Parse(reader["GroupID1984"].ToString());
                        verein.GroupID1980 = int.Parse(reader["GroupID1980"].ToString());
                        //verein.GroupID1996 = int.Parse(reader["GroupID1996"].ToString());                        
                        verein.Hyperlink = reader["Hyperlink"].ToString();
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

