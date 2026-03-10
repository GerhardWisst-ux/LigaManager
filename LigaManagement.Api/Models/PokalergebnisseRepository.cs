using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ToremanagerManagement.Api.Models.Repository;

namespace ToreManagerManagement.Api.Models
{
    public class PokalergebnisseRepository : IPokalergebnisseRepository
    {       
        public async Task<PokalergebnisSpieltag> CreatePokalergebnis(PokalergebnisSpieltag pokalspiel)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Pokalergebnisse (SaisonID,Saison,Verein1_Nr,Verein1,Verein2_Nr,Verein2,Tore1_Nr,Tore2_Nr,Datum,Ort,Schiedrichter,Zuschauer,Verlängerung,Elfmeterschiessen,Runde,Supercup,Closed)" +
                    " VALUES(@SaisonID,@Saison,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Zuschauer,@Verlängerung,@Elfmeterschiessen,@Runde,@Supercup,@Closed); SELECT SCOPE_IDENTITY();"; 

                //cmd.Parameters.AddWithValue("@SpieltagId", pokalspiel.SpieltagId);
                cmd.Parameters.AddWithValue("@SaisonID", pokalspiel.SaisonID);
                cmd.Parameters.AddWithValue("@Saison", pokalspiel.Saison);
                cmd.Parameters.AddWithValue("@Verein1_Nr", pokalspiel.Verein1_Nr);
                cmd.Parameters.AddWithValue("@Verein2_Nr", pokalspiel.Verein2_Nr);
                cmd.Parameters.AddWithValue("@Verein1", pokalspiel.Verein1);
                cmd.Parameters.AddWithValue("@Verein2", pokalspiel.Verein2);
                cmd.Parameters.AddWithValue("@Tore1_Nr", pokalspiel.Tore1_Nr);
                cmd.Parameters.AddWithValue("@Tore2_Nr", pokalspiel.Tore2_Nr);
                cmd.Parameters.AddWithValue("@Datum", pokalspiel.Datum);
                cmd.Parameters.AddWithValue("@Ort", pokalspiel.Ort);
                cmd.Parameters.AddWithValue("@Schiedrichter", pokalspiel.Schiedrichter);
                cmd.Parameters.AddWithValue("@Zuschauer", pokalspiel.Zuschauer);
                cmd.Parameters.AddWithValue("@Verlängerung", pokalspiel.Verlängerung);
                cmd.Parameters.AddWithValue("@Runde", pokalspiel.Runde);
                cmd.Parameters.AddWithValue("@Elfmeterschiessen", pokalspiel.Elfmeterschiessen);
                cmd.Parameters.AddWithValue("@Supercup", pokalspiel.Supercup);
                cmd.Parameters.AddWithValue("@Closed", pokalspiel.Beendet);

                var result = await cmd.ExecuteScalarAsync();                

                pokalspiel.SpieltagId = Convert.ToInt32(result);

                conn.Close();

                return pokalspiel;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }       

        public async Task<PokalergebnisSpieltag> DeletePokalergebnis(int SpieltagID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [dbo].[Pokalergebnisse]  where SpieltagId = @SpieltagId";

                cmd.Parameters.AddWithValue("@SpieltagId", SpieltagID);

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
              
        public async Task<PokalergebnisSpieltag> GetPokalergebnis(int SpieltagID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [Pokalergebnisse] where SpieltagId =" + SpieltagID, conn);
                PokalergebnisSpieltag pe = null;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        pe = new PokalergebnisSpieltag();

                        pe.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        pe.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        pe.Saison = reader["Saison"].ToString();
                        pe.Verein1 = reader["Verein1"].ToString();
                        pe.Verein2 = reader["Verein2"].ToString();
                        pe.Verein1_Nr = int.Parse(reader["Verein1_Nr"].ToString());
                        pe.Verein2_Nr = int.Parse(reader["Verein2_Nr"].ToString());
                        pe.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        pe.Doppelpunkt = ":";
                        pe.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        pe.Runde = reader["Runde"].ToString();
                        pe.Ort = reader["Ort"].ToString();
                        pe.Datum = (DateTime)reader["Datum"];
                        pe.Schiedrichter = reader["Schiedrichter"].ToString();
                        pe.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        pe.Verlängerung = bool.Parse(reader["Verlängerung"].ToString());
                        pe.Elfmeterschiessen = bool.Parse(reader["Elfmeterschiessen"].ToString());
                        pe.Supercup = bool.Parse(reader["Supercup"].ToString());
                        pe.Beendet = bool.Parse(reader["Closed"].ToString());
                    }
                }
                conn.Close();
                return pe;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<PokalergebnisSpieltag>> GetPokalergebnisse()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [Pokalergebnisse]", conn);
                PokalergebnisSpieltag pe = null;
                List<PokalergebnisSpieltag> peList = new List<PokalergebnisSpieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        pe = new PokalergebnisSpieltag();

                        pe.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        pe.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        pe.Saison = reader["Saison"].ToString();
                        pe.Verein1 = reader["Verein1"].ToString();
                        pe.Verein2 = reader["Verein2"].ToString();
                        pe.Verein1_Nr = int.Parse(reader["Verein1_Nr"].ToString());
                        pe.Verein2_Nr = int.Parse(reader["Verein2_Nr"].ToString());
                        pe.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        pe.Doppelpunkt = ":";
                        pe.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        pe.Runde = reader["Runde"].ToString();                        
                        pe.Ort = reader["Ort"].ToString();
                        pe.Datum = (DateTime)reader["Datum"];
                        pe.Schiedrichter = reader["Schiedrichter"].ToString();
                        pe.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        pe.Verlängerung = bool.Parse(reader["Verlängerung"].ToString());
                        pe.Elfmeterschiessen = bool.Parse(reader["Elfmeterschiessen"].ToString());
                        pe.Supercup = bool.Parse(reader["Supercup"].ToString());
                        pe.Beendet = false; // bool.Parse(reader["Closed"].ToString());
                        peList.Add(pe);
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

        public async  Task<IEnumerable<PokalHistorieStatistik>> GetPokalergebnisseHistorie(string vereinid)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("WITH SpieleMitNummer AS (SELECT [SpieltagId],[Saison], [SaisonID], [Verein1_Nr], [Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum], [Ort],        [Schiedrichter],        [Runde],        [Zuschauer],        [Verlängerung],        [Elfmeterschiessen], [Supercup], ROW_NUMBER() OVER (PARTITION BY Saison ORDER BY Datum DESC) AS rn    FROM [dbo].[Pokalergebnisse]  WHERE supercup = 0 AND (Verein1_Nr = " +  vereinid  + " OR Verein2_Nr =  +  " + vereinid  + ")) SELECT * FROM SpieleMitNummer WHERE rn = 1 ORDER BY Saison DESC;", conn);
                PokalHistorieStatistik pe = null;
                List<PokalHistorieStatistik> phList = new List<PokalHistorieStatistik>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        pe = new PokalHistorieStatistik();

                        if (reader["Verein1_Nr"].ToString() == vereinid)
                            pe.Gegner = reader["Verein2"].ToString();
                        else
                            pe.Gegner = reader["Verein1"].ToString();

                        if (reader["Runde"].ToString() == "F")
                            pe.ErreichteRunde = "Finale";
                        else if (reader["Runde"].ToString() == "HF")
                            pe.ErreichteRunde = "Halbfinale";
                        else if (reader["Runde"].ToString() == "VF")
                            pe.ErreichteRunde = "Viertelfinale";
                        else if (reader["Runde"].ToString() == "AF")
                            pe.ErreichteRunde = "Achtelfinale";
                        else if (reader["Runde"].ToString() == "2")
                            pe.ErreichteRunde = "2. Runde";
                        else if (reader["Runde"].ToString() == "1")
                            pe.ErreichteRunde = "1. Runde";

                        if (reader["Verein1_Nr"].ToString() == vereinid)
                            pe.Ergebnis = reader["Tore1_Nr"].ToString() + " : " + reader["Tore2_Nr"].ToString();
                        else
                            pe.Ergebnis = reader["Tore2_Nr"].ToString() + " : " + reader["Tore1_Nr"].ToString();

                        pe.Saison = reader["Saison"].ToString();

                        phList.Add(pe);
                    }
                }
                conn.Close();
                return phList;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
        
        public async Task<IEnumerable<PokalergebnisStatistik>> GetPokalergebnisseStatistik()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("WITH Pokalstatistik AS (SELECT Verein, SUM(CASE WHEN Verein = Verein1 THEN 1 ELSE 0 END) AS Siege,COUNT(*) AS Finalteilnahmen,  ROUND(100.0 * SUM(CASE WHEN Verein = Verein1 THEN 1 ELSE 0 END) / COUNT(*), 1) AS Siegquote FROM (SELECT Verein1 AS Verein, Verein1, Verein2 FROM [LigaDB].[dbo].[Pokalergebnisse] WHERE Runde = 'F' UNION ALL SELECT Verein2 AS Verein, Verein1, Verein2 FROM [LigaDB].[dbo].[Pokalergebnisse] WHERE Runde = 'F') AS Finals GROUP BY Verein) SELECT ROW_NUMBER() OVER (ORDER BY Siege DESC, Finalteilnahmen DESC) AS Platz, Verein,Siege,Finalteilnahmen,Siegquote FROM Pokalstatistik ORDER BY Platz;", conn);
                PokalergebnisStatistik pe = null;
                List<PokalergebnisStatistik> peList = new List<PokalergebnisStatistik>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        pe = new PokalergebnisStatistik();

                        pe.Platz = int.Parse(reader["Platz"].ToString());
                        pe.Verein = reader["Verein"].ToString();
                        pe.Siege = int.Parse(reader["Siege"].ToString());
                        pe.Finalteilnahmen = int.Parse(reader["Finalteilnahmen"].ToString());
                        pe.Siegquote = double.Parse(reader["Siegquote"].ToString());

                        peList.Add(pe);
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

        public async Task<PokalergebnisSpieltag> UpdatePokalergebnis(PokalergebnisSpieltag pokalspiel)
        {
            int bVerlängerung;
            int bElfmeterschiessen;
            int bSupercup;
            int bBeendet;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
             

                if (pokalspiel.Verlängerung == false)
                    bVerlängerung = 0;
                else
                    bVerlängerung = 1;

                if (pokalspiel.Elfmeterschiessen == false)
                    bElfmeterschiessen = 0;
                else
                    bElfmeterschiessen = 1;

                if (pokalspiel.Supercup == false)
                    bSupercup = 0;
                else
                    bSupercup = 1;

                if (pokalspiel.Beendet == false)
                    bBeendet = 0;
                else
                    bBeendet = 1;

                cmd.CommandText = "UPDATE [dbo].[Pokalergebnisse] SET " +                      
                      " [Saison] = '" + pokalspiel.Saison + "'" +
                      ",[SaisonID] = " + pokalspiel.SaisonID +
                      ",[Verein1_Nr] = " + pokalspiel.Verein1_Nr +
                      ",[Verein1] = '" + pokalspiel.Verein1 + "'" +
                      ",[Verein2_Nr] = " + pokalspiel.Verein2_Nr +
                      ",[Verein2] = '" + pokalspiel.Verein2 + "'" +
                      ",[Tore1_Nr] = " + pokalspiel.Tore1_Nr +
                      ",[Tore2_Nr] = " + pokalspiel.Tore2_Nr +
                      ",[Datum] = '" + pokalspiel.Datum + "'" +
                      ",[Ort] = '" + pokalspiel.Ort + "'" +
                      ",[Schiedrichter] = '" + pokalspiel.Schiedrichter + "'" +              
                      ",[Zuschauer] = " + pokalspiel.Zuschauer +                      
                      ",[Runde] = '" + pokalspiel.Runde + "'" +
                      ",[Verlängerung] = " + bVerlängerung +
                      ",[Supercup] = " + bSupercup +
                      ",[Closed] = " + bBeendet +
                      ",[Elfmeterschiessen] = " + bElfmeterschiessen +
                      " WHERE [SpieltagId] = " + pokalspiel.SpieltagId;

                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return pokalspiel;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }       
    }
}

