using LigaManagement.Api.Models;
using LigaManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Pokalergebnisse (SaisonID,Saison,Verein1_Nr,Verein1,Verein2_Nr,Verein2,Tore1_Nr,Tore2_Nr,Datum,Ort,Schiedrichter,Zuschauer,Verlängerung,Elfmeterschiessen,Runde)" +
                    " VALUES(@SaisonID,@Saison,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Zuschauer,@Verlängerung,@Elfmeterschiessen,@Runde)";

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

                cmd.ExecuteNonQuery();

                conn.Close();

                return null;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }       

        public Task<PokalergebnisSpieltag> DeletePokalergebnis(int SpieltagID)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Pokalergebnisse]  where SpieltagId = @SpieltagId";

            cmd.Parameters.AddWithValue("@SpieltagId", SpieltagID);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;
        }
              
        public async Task<PokalergebnisSpieltag> GetPokalergebnis(int SpieltagID)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [Pokalergebnisse] where SpieltagId =" + SpieltagID, conn);
            PokalergebnisSpieltag pe     = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
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
                    pe.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                    pe.Runde = reader["Runde"].ToString();
                    pe.Ort = reader["Ort"].ToString();
                    pe.Datum = (DateTime)reader["Datum"];
                    pe.Schiedrichter = reader["Schiedrichter"].ToString();
                    pe.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                    pe.Verlängerung = bool.Parse(reader["Verlängerung"].ToString());
                    pe.Elfmeterschiessen = bool.Parse(reader["Elfmeterschiessen"].ToString());
                }
            }
            conn.Close();
            return pe;
        }

        public async Task<IEnumerable<PokalergebnisSpieltag>> GetPokalergebnisse()
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Pokalergebnisse]", conn);
                PokalergebnisSpieltag pe = null;
                List<PokalergebnisSpieltag> peList = new List<PokalergebnisSpieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
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
                        pe.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        pe.Runde = reader["Runde"].ToString();                        
                        pe.Ort = reader["Ort"].ToString();
                        pe.Datum = (DateTime)reader["Datum"];
                        pe.Schiedrichter = reader["Schiedrichter"].ToString();
                        pe.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        pe.Verlängerung = bool.Parse(reader["Verlängerung"].ToString());
                        pe.Elfmeterschiessen = bool.Parse(reader["Elfmeterschiessen"].ToString());

                        peList.Add(pe);
                    }
                }
                conn.Close();
                return peList;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }
        }
      
      
        public async Task<PokalergebnisSpieltag> UpdatePokalergebnis(PokalergebnisSpieltag pokalspiel)
        {
            int bVerlängerung;
            int bElfmeterschiessen;
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = "UPDATE Pokalergebnisse(SaisonID,Saison,Verein1_Nr,Verein1,Verein2_Nr,Verein2,Tore1_Nr,Tore2_Nr,Datum,Ort,Schiedrichter,Zuschauer,Elfmeterschiessen,Runde)" +
                //    " VALUES(@SaisonID,@Saison,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Zuschauer,@Verlängerung,@Elfmeterschiessen,@Runde) WHERE SpieltagId =@SpieltagId";

                //cmd.Parameters.AddWithValue("@SpieltagId", pokalspiel.SpieltagId);
                //cmd.Parameters.AddWithValue("@SaisonID", pokalspiel.SaisonID);
                //cmd.Parameters.AddWithValue("@Saison", pokalspiel.Saison);
                //cmd.Parameters.AddWithValue("@Verein1_Nr", pokalspiel.Verein1_Nr);
                //cmd.Parameters.AddWithValue("@Verein2_Nr", pokalspiel.Verein2_Nr);
                //cmd.Parameters.AddWithValue("@Verein1", pokalspiel.Verein1);
                //cmd.Parameters.AddWithValue("@Verein2", pokalspiel.Verein2);
                //cmd.Parameters.AddWithValue("@Tore1_Nr", pokalspiel.Tore1_Nr);
                //cmd.Parameters.AddWithValue("@Tore2_Nr", pokalspiel.Tore2_Nr);
                //cmd.Parameters.AddWithValue("@Datum", pokalspiel.Datum);
                //cmd.Parameters.AddWithValue("@Ort", pokalspiel.Ort);
                //cmd.Parameters.AddWithValue("@Schiedrichter", pokalspiel.Schiedrichter);
                //cmd.Parameters.AddWithValue("@Zuschauer", pokalspiel.Zuschauer);
                //cmd.Parameters.AddWithValue("@Verlängerung", pokalspiel.Verlängerung);
                //cmd.Parameters.AddWithValue("@Runde", pokalspiel.Runde);
                //cmd.Parameters.AddWithValue("@Elfmeterschiessen", pokalspiel.Elfmeterschiessen);

                if (pokalspiel.Verlängerung == false)
                    bVerlängerung = 0;
                else
                    bVerlängerung = 1;

                if (pokalspiel.Elfmeterschiessen == false)
                    bElfmeterschiessen = 0;
                else
                    bElfmeterschiessen = 1;

                cmd.CommandText = "UPDATE [dbo].[Pokalergebnisse] SET " +                      
                      " [Saison] = '" + pokalspiel.Saison + "'" +
                      ",[SaisonID] = " + pokalspiel.SaisonID +              
                      ",[Verein1_Nr] = '" + pokalspiel.Verein1_Nr + "'" +
                      ",[Verein1] = '" + pokalspiel.Verein1 + "'" +
                      ",[Verein2_Nr] = " + pokalspiel.Verein2_Nr +
                      ",[Verein2] = '" + pokalspiel.Verein2 + "'" +
                      ",[Tore1_Nr] = " + pokalspiel.Tore1_Nr +
                      ",[Tore2_Nr] = " + pokalspiel.Tore2_Nr +
                      ",[Datum] = '" + pokalspiel.Datum + "'" +
                      ",[Ort] = '" + pokalspiel.Ort + "'" +
                      ",[Schiedrichter] = '" + pokalspiel.Schiedrichter + "'" +              
                      ",[Zuschauer] =" + pokalspiel.Zuschauer +                      
                      ",[Runde] = '" + pokalspiel.Runde + "'" +
                      ",[Verlängerung] = " + bVerlängerung +
                      ",[Elfmeterschiessen] = " + bElfmeterschiessen +
                      " WHERE [SpieltagId] = " + pokalspiel.SpieltagId;

                cmd.ExecuteNonQuery();

                conn.Close();

                return pokalspiel;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }
        }       
    }
}

