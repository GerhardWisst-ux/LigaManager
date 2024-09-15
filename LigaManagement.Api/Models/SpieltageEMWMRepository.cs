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
        public async Task<PokalergebnisCL_EM_WMSpieltag> AddSpieltag(PokalergebnisCL_EM_WMSpieltag spieltag)
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

        public async Task<PokalergebnisCL_EM_WMSpieltag> DeleteSpieltag(int SpieltagId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [dbo].[SpieltageEMWM] Where SpieltagId= @SpieltagId";

                cmd.Parameters.AddWithValue("@SpieltagId", SpieltagId);

                cmd.ExecuteNonQuery();

                conn.Close();

                return null;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
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

                SqlCommand command = new SqlCommand("SELECT * FROM [SpieltageEMWM] WHERE SpieltagId =" + spieltagId, conn);
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

                        if (reader["GroupID"].ToString() == "1")
                            spieltag.Gruppe = "A";
                        else if (reader["GroupID"].ToString() == "2")
                            spieltag.Gruppe = "B";
                        else if (reader["GroupID"].ToString() == "3")
                            spieltag.Gruppe = "C";
                        else if (reader["GroupID"].ToString() == "4")
                            spieltag.Gruppe = "D";
                        else if (reader["GroupID"].ToString() == "5")
                            spieltag.Gruppe = "E";
                        else if (reader["GroupID"].ToString() == "6")
                            spieltag.Gruppe = "F";
                        else if (reader["GroupID"].ToString() == "7")
                            spieltag.Gruppe = "G";
                        else if (reader["GroupID"].ToString() == "8")
                            spieltag.Gruppe = "H";
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

                SqlCommand command = new SqlCommand("SELECT * FROM [SpieltageEMWM] ", conn);
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
                        spieltag.Elfmeterschiessen = bool.Parse(reader["Elfmeterschiessen"].ToString());
                        spieltag.Verlängerung = bool.Parse(reader["Verlängerung"].ToString());
                        spieltag.Schiedrichter = reader["Schiedrichter"].ToString();
                        spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        spieltag.GroupID = int.Parse(reader["GroupID"].ToString());
                        spieltag.Runde = reader["Runde"].ToString();
                        spieltag.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                        spieltag.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();

                        if (spieltag.GroupID == 0 && spieltag.Tore1_Nr > spieltag.Tore2_Nr)
                            spieltag.FontWeight1 = "font-weight:bold";
                        else
                            spieltag.FontWeight1 = "font-weight:normal";

                        if (spieltag.GroupID == 0 && spieltag.Tore2_Nr > spieltag.Tore1_Nr)
                            spieltag.FontWeight2 = "font-weight:bold";
                        else
                            spieltag.FontWeight2 = "font-weight:normal";

                        if (reader["GroupID"].ToString() == "1")
                            spieltag.Gruppe = "A";
                        else if (reader["GroupID"].ToString() == "2")
                            spieltag.Gruppe = "B";
                        else if (reader["GroupID"].ToString() == "3")
                            spieltag.Gruppe = "C";
                        else if (reader["GroupID"].ToString() == "4")
                            spieltag.Gruppe = "D";
                        else if (reader["GroupID"].ToString() == "5")
                            spieltag.Gruppe = "E";
                        else if (reader["GroupID"].ToString() == "6")
                            spieltag.Gruppe = "F";
                        else if (reader["GroupID"].ToString() == "7")
                            spieltag.Gruppe = "G";
                        else if (reader["GroupID"].ToString() == "8")
                            spieltag.Gruppe = "H";

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
                        ",[Gruppe] = '" + spieltag.Gruppe + "'" +
                        ",[GroupID] = " + spieltag.GroupID +
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
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2024 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2022")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                      " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2022 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2020")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2020 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2018")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM]where GroupID2018 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2016")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                        " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2016 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2014")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                      " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2014 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2012")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2012 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2010")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID] ,[Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2010 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2008")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2008 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2006")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2006 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2004")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                      " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2004 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 2002")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                      " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930]FROM [dbo].[MannschaftEMWM] where GroupID2002 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 2000")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004], " +
                      " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID2000 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1998")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                    " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1998 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1996")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1996 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1994")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1994 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1992")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                   " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1992 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1990")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1990 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1988")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                   " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1988 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1986")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                       " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1986 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1984")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                   " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1984 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1982")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                   " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1982 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "EM 1980")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978],[GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934] FROM [dbo].[MannschaftEMWM] where GroupID1980 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1978")
                    if (iGroupID < 9)
                    {
                        command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1978 = " + iGroupID, conn);
                    }
                    else
                    {
                        command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                                          " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978_2], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1978_2 = " + iGroupID, conn);
                    }
                else if (Globals.currentEMWMSaison == "WM 1974")
                    if (iGroupID < 9)
                    {
                        command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1974 = " + iGroupID, conn);
                    }
                    else
                    {
                        command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974_2], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1974_2 = " + iGroupID, conn);
                    }

                else if (Globals.currentEMWMSaison == "WM 1970")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930]  FROM [dbo].[MannschaftEMWM] where GroupID1970 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1966")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1966 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1962")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1962 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1958")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1958 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1954")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930]  FROM [dbo].[MannschaftEMWM] where GroupID1954 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1950")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1950 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1938")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1938 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1934")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1934 = " + iGroupID, conn);
                else if (Globals.currentEMWMSaison == "WM 1930")
                    command = new SqlCommand("SELECT DISTINCT [Id],[MannschaftNr],[MannschaftName1],[MannschaftName2],[LandID], [Hyperlink], [GroupID2024],[GroupID2022],[GroupID2020],[GroupID2018],[GroupID2016],[GroupID2014],[GroupID2012],[GroupID2010],[GroupID2008],[GroupID2006],[GroupID2004]," +
                  " [GroupID2002], [GroupID2000], [GroupID1998], [GroupID1996], [GroupID1994], [GroupID1992], [GroupID1990], [GroupID1988], [GroupID1986], [GroupID1984], [GroupID1982], [GroupID1980], [GroupID1978], [GroupID1974], [GroupID1970], [GroupID1966], [GroupID1962], [GroupID1958], [GroupID1954], [GroupID1950], [GroupID1938], [GroupID1934], [GroupID1930], [GroupID1930] FROM [dbo].[MannschaftEMWM] where GroupID1930 = " + iGroupID, conn);

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
                        verein.GroupID2002 = int.Parse(reader["GroupID2002"].ToString());
                        verein.GroupID2000 = int.Parse(reader["GroupID2000"].ToString());
                        verein.GroupID1998 = int.Parse(reader["GroupID1998"].ToString());
                        verein.GroupID1996 = int.Parse(reader["GroupID1996"].ToString());
                        verein.GroupID1994 = int.Parse(reader["GroupID1994"].ToString());
                        verein.GroupID1992 = int.Parse(reader["GroupID1992"].ToString());
                        verein.GroupID1990 = int.Parse(reader["GroupID1990"].ToString());
                        verein.GroupID1988 = int.Parse(reader["GroupID1988"].ToString());
                        verein.GroupID1986 = int.Parse(reader["GroupID1986"].ToString());
                        verein.GroupID1984 = int.Parse(reader["GroupID1984"].ToString());
                        verein.GroupID1982 = int.Parse(reader["GroupID1982"].ToString());
                        verein.GroupID1980 = int.Parse(reader["GroupID1980"].ToString());

                        if (iGroupID < 9)
                            verein.GroupID1978 = int.Parse(reader["GroupID1978"].ToString());
                        else
                            verein.GroupID1978 = int.Parse(reader["GroupID1978_2"].ToString());

                        if (iGroupID < 9)
                            verein.GroupID1974 = int.Parse(reader["GroupID1974"].ToString());
                        else
                            verein.GroupID1974 = int.Parse(reader["GroupID1974_2"].ToString());

                        verein.GroupID1970 = int.Parse(reader["GroupID1970"].ToString());
                        verein.GroupID1966 = int.Parse(reader["GroupID1966"].ToString());
                        verein.GroupID1962 = int.Parse(reader["GroupID1962"].ToString());
                        verein.GroupID1958 = int.Parse(reader["GroupID1958"].ToString());
                        verein.GroupID1954 = int.Parse(reader["GroupID1954"].ToString());
                        verein.GroupID1950 = int.Parse(reader["GroupID1950"].ToString());
                        verein.GroupID1938 = int.Parse(reader["GroupID1938"].ToString());
                        verein.GroupID1934 = int.Parse(reader["GroupID1934"].ToString());
                        verein.GroupID1930 = int.Parse(reader["GroupID1930"].ToString());

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


