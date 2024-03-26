using LigaManagement.Api.Models;
using LigaManagement.Models;
using ToremanagerManagement.Api.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using Ligamanager.Models;
using System.Threading;

namespace ToreManagerManagement.Api.Models
{
    public class ToreRepository : IToreRepository
    {
        private readonly AppDbContext appDbContext;

        public ToreRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Tore> AddTore(Tore Tore)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO [Tore] (SaisonID, SpieltagNr, TorVerein1, TorVerein2, Spielminute, SpielerID)" +
                " VALUES(@SaisonID, @SpieltagNr, @TorVerein1, @TorVerein2, @Spielminute, @SpielerID)";

            cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
            cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagId);
            cmd.Parameters.AddWithValue("@TorVerein1", Tore.TorVerein1);
            cmd.Parameters.AddWithValue("@TorVerein2", Tore.TorVerein2);
            cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
            cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
            
            cmd.ExecuteNonQuery();

            conn.Close();

            return Tore;
        }

        public async Task<Tore> DeleteTore(int ToreId)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Tore]  where ID = @ID";

            cmd.Parameters.AddWithValue("@SaisonID", ToreId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;

        }

        public async Task<Tore> GetTor(int ToreId)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [tore] where ID =" + ToreId, conn);
            Tore tore = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tore = new Tore();
                    tore.Id = (int)reader["Id"];
                    tore.SaisonID = (int)reader["saisonID"];
                    tore.SpieltagNr = reader["SpieltagNr"].ToString();
                    tore.TorVerein1 = (int)reader["TorVerein1"];
                    tore.TorVerein1 = (int)reader["TorVerein1"];
                    tore.Spielminute = (int)reader["Spielminute"];
                    tore.SpielerID = (int)reader["SpielerID"];                  
                }
            }
            conn.Close();
            return tore;
        }

        public async Task<IEnumerable<Tore>> GetTore()
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [tore]", conn);
            Tore tore = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tore = new Tore();
                    tore.Id = (int)reader["Id"];
                    tore.SaisonID = (int)reader["saisonID"];
                    tore.SpieltagNr = reader["SpieltagNr"].ToString();
                    tore.TorVerein1 = (int)reader["TorVerein1"];
                    tore.TorVerein1 = (int)reader["TorVerein1"];
                    tore.Spielminute = (int)reader["Spielminute"];
                    tore.SpielerID = (int)reader["SpielerID"];
                }
            }
            conn.Close();
            return (IEnumerable<Tore>)tore;
        }

        public async Task<Tore> UpdateTore(Tore Tore)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Tore(SaisonID, SpieltagNr, TorVerein1, TorVerein2, Spielminute, SpielerID)" +
                " VALUES(@SaisonID,@SpieltagNr,@TorVerein1,@TorVerein2,@Spielminute,@SpielerID)";

            cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
            cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagNr);
            cmd.Parameters.AddWithValue("@TorVerein1", Tore.TorVerein1);
            cmd.Parameters.AddWithValue("@TorVerein2", Tore.TorVerein2);
            cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
            cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
            
            cmd.ExecuteNonQuery();

            conn.Close();

            return Tore;
        }
    }
}

