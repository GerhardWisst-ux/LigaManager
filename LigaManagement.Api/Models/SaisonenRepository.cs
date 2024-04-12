using LigaManagement.Api.Migrations;
using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace LigaManagement.Api.Models
{
    public class SaisonenRepository : ISaisonRepository
    {
        private readonly AppDbContext appDbContext;

        public SaisonenRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Saison> AddSaison(Saison Saison)
        {
            //var result = await appDbContext.Saisonen.AddAsync(Saison);
            //await appDbContext.SaveChangesAsync();
            //return result.Entity;

            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Saison] (SaisonID, LigaID, Saisonname, Liganame, Aktuell,Abgeschlossen)" +
                    " VALUES(@SaisonID,@LigaID,@Saisonname,@Liganame,@Aktuell,@Abgeschlossen)";

                cmd.Parameters.AddWithValue("@SaisonID", Saison.SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", Saison.LigaID);
                cmd.Parameters.AddWithValue("@Saisonname", Saison.Saisonname);
                cmd.Parameters.AddWithValue("@Liganame", Saison.Liganame);
                cmd.Parameters.AddWithValue("@Aktuell", Saison.Aktuell);
                cmd.Parameters.AddWithValue("@Abgeschlossen", Saison.Abgeschlossen);
                

                cmd.ExecuteNonQuery();

                conn.Close();

                return Saison;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public Task<Saison> DeleteSaison(int SaisonId)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Saison]  Where SaisonID= = @SaisonId";

            cmd.Parameters.AddWithValue("@SaisonID", SaisonId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;

            //var result = await appDbContext.Saisonen
            //   .FirstOrDefaultAsync(e => e.SaisonID == SaisonId);
            //if (result != null)
            //{
            //    appDbContext.Saisonen.Remove(result);
            //    await appDbContext.SaveChangesAsync();
            //    return result;
            //}

            //return null;
        }

        public async Task<Saison> GetSaison(int SaisonId)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen] Where SaisonID= " + SaisonId, conn);
                Saison saison = null;
                List<Saison> peList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saison = new Saison();

                        saison.SaisonID = (int)reader["saisonID"];
                        saison.LigaID = (int)reader["LigaID"];
                        saison.Saisonname = reader["Saisonname"].ToString();
                        saison.Liganame = reader["Liganame"].ToString();
                        saison.Aktuell = (bool)reader["Aktuell"];
                        saison.Abgeschlossen = (bool)reader["Abgeschlossen"];
                        
                    }
                }
                conn.Close();
                return saison;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }
            

            //return await appDbContext.Saisonen
            //    .FirstOrDefaultAsync(d => d.SaisonID == SaisonId);
        }

        public async Task<IEnumerable<Saison>> GetSaisonen()
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen]", conn);
                Saison saison = null;
                List<Saison> saisonenList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saison = new Saison();
                                                
                        saison.SaisonID = (int)reader["saisonID"];                        
                        saison.LigaID = (int)reader["LigaID"];
                        saison.Saisonname = reader["Saisonname"].ToString();
                        saison.Liganame = reader["Liganame"].ToString();
                        saison.Aktuell = (bool)reader["Aktuell"];
                        saison.Abgeschlossen = (bool)reader["Abgeschlossen"];

                        saisonenList.Add(saison);
                    }
                }
                conn.Close();
                return saisonenList;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }
            //return await appDbContext.Saisonen.ToListAsync();
        }

        public async Task<Saison> GetSaisonID(string saisonname)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen] Where Saison= '" + saisonname + "'", conn);
                Saison saison = null;
                List<Saison> peList = new List<Saison>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saison = new Saison();

                        saison.SaisonID = (int)reader["saisonID"];
                       
                    }
                }
                conn.Close();
                return saison;
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return null;
            }
        }

        public async Task<Saison> UpdateSaison(Saison saison)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Saisonen(SaisonID,LigaID,Saisonname,Liganame,Aktuell,Abgeschlossen)" +
                " VALUES(@SaisonID,@LigaID,@Saisonname,@Liganame,@Aktuell,@Abgeschlossen)";

            cmd.Parameters.AddWithValue("@SaisonID", saison.SaisonID);
            cmd.Parameters.AddWithValue("@LigaID", saison.LigaID);
            cmd.Parameters.AddWithValue("@Saisonname", saison.Saisonname);
            cmd.Parameters.AddWithValue("@Liganame", saison.Liganame);
            cmd.Parameters.AddWithValue("@Aktuell", saison.Aktuell);
            cmd.Parameters.AddWithValue("@Abgeschlossen", saison.Abgeschlossen);
            cmd.ExecuteNonQuery();

            conn.Close();

            return saison;

            //var result = await appDbContext.Saisonen.AddAsync(Saison);
            //await appDbContext.SaveChangesAsync();
            //return result.Entity;
        }
    }
}

