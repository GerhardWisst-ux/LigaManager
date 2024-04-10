using LigaManagement.Api.Models;
using LigaManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using ToremanagerManagement.Api.Models.Repository;

namespace ToreManagerManagement.Api.Models
{
    public class PokalergebnisseRepository : IPokalergebnisseRepository
    {
        private readonly AppDbContext appDbContext;

        public PokalergebnisseRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<PokalergebnisSpieltag> CreatePokalergebnis(PokalergebnisSpieltag SpieltagID)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Tore] (SaisonID, LigaID, SpieltagNr, Spielminute, SpielerID,Spielstand,SpieltagId,Eigentor)" +
                    " VALUES(@SaisonID,@LigaID,@SpieltagNr, @Spielminute, @SpielerID,@Spielstand,@SpieltagId,@Eigentor)";

                //cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
                //cmd.Parameters.AddWithValue("@LigaID", Tore.LigaID);
                //cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagNr);
                //cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
                //cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
                //cmd.Parameters.AddWithValue("@Spielstand", Tore.Spielstand);
                //cmd.Parameters.AddWithValue("@SpieltagId", Tore.SpieltagId);
                //cmd.Parameters.AddWithValue("@Eigentor", Tore.Eigentor);

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
            cmd.CommandText = "DELETE FROM [dbo].[Tore]  where SpieltagId = @SpieltagId";

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

                    pe.SpieltagId = (int)reader["SpieltagId"];                    
                    pe.SaisonID = (int)reader["saisonID"];
                    pe.LigaID = (int)reader["LigaID"];                    
                    pe.Verein1 = reader["Verein1"].ToString();
                    pe.Verein2 = reader["Verein2"].ToString();
                    pe.Verein1_Nr = Convert.ToInt32(reader["Verein1_Nr"]);
                    pe.Verein2_Nr = Convert.ToInt32(reader["Verein2_Nr"]);
                    pe.Tore1_Nr = Convert.ToInt32(reader["Tore1_Nr"]);
                    pe.Tore2_Nr = Convert.ToInt32(reader["Tore2_Nr"]);
                    pe.Runde = reader["Runde"].ToString();                    
                    pe.Datum = (DateTime)reader["Datum"];
                    pe.Ort = reader["Ort"].ToString();
                    pe.Schiedrichter = reader["Schiedrichter"].ToString();
                    pe.Zuschauer = (int)reader["Zuschauer"];
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

                        pe.SpieltagId = (int)reader["SpieltagId"];                       
                        pe.SaisonID = (int)reader["saisonID"];
                        pe.LigaID = (int)reader["LigaID"];
                        pe.Verein1 = reader["Verein1"].ToString();
                        pe.Verein2 = reader["Verein2"].ToString();
                        pe.Verein1_Nr = Convert.ToInt32(reader["Verein1_Nr"]);
                        pe.Verein2_Nr = Convert.ToInt32(reader["Verein2_Nr"]);
                        pe.Tore1_Nr = Convert.ToInt32(reader["Tore1_Nr"]);
                        pe.Tore2_Nr = Convert.ToInt32(reader["Tore2_Nr"]);
                        pe.Runde = reader["Runde"].ToString();                        
                        pe.Ort = reader["Ort"].ToString();
                        pe.Datum = (DateTime)reader["Datum"];
                        pe.Schiedrichter = reader["Schiedrichter"].ToString();
                        pe.Zuschauer = (int)reader["Zuschauer"];

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

      
      
        public Task<PokalergebnisSpieltag> UpdatePokalergebnis(PokalergebnisSpieltag SpieltagID)
        {
            //SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            //conn.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //cmd.CommandText = "UPDATE Tore(SaisonID,LigaID, SpieltagNr,Spielminute,SpielerID, Spielstand,SpieltagId,Eigentor)" +
            //    " VALUES(@SaisonID,@SpieltagNr,@LigaID,@Spielminute,@SpielerID,@Spielstand,@SpieltagId,@Eigentor)";

            //cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
            //cmd.Parameters.AddWithValue("@LigaID", Tore.LigaID);
            //cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagId);
            //cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
            //cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
            //cmd.Parameters.AddWithValue("@Spielstand", Tore.Spielstand);
            //cmd.Parameters.AddWithValue("@SpieltagId", Tore.SpieltagId);
            //cmd.Parameters.AddWithValue("@Eigentor", Tore.Eigentor);

            //cmd.ExecuteNonQuery();

            //conn.Close();

            //return Tore;
            return null; 
        }       
    }
}

