using LigaManagement.Api.Models;
using LigaManagement.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToremanagerManagement.Api.Models.Repository;

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
            cmd.CommandText = "INSERT INTO [Tore] (SaisonID, SpieltagNr, Spielminute, SpielerID,Spielstand,SpieltagsID)" +
                " VALUES(@SaisonID, @SpieltagNr, @Spielminute, @SpielerID,@Spielstand,@SpieltagsID)";

            cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
            cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagId);            
            cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
            cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
            cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
            cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);

            cmd.ExecuteNonQuery();

            conn.Close();

            return Tore;
        }

        public async Task<Tore> DeleteTor(int ToreId)
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
            Tore tor = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tor = new Tore();
                    tor.Id = (int)reader["Id"];
                    tor.SaisonID = (int)reader["saisonID"];
                    tor.SpieltagNr = reader["SpieltagNr"].ToString();                    
                    tor.Spielminute = (int)reader["Spielminute"];
                    tor.SpielerID = (int)reader["SpielerID"];
                    tor.Spielstand = reader["Spielstand"].ToString();
                    tor.SpieltagsID = (int)reader["SpieltagsID"];
                }
            }
            conn.Close();
            return tor;
        }

        public async Task<IEnumerable<Tore>> GetTore()
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [tore]", conn);
            List<Tore> tore = new List<Tore>();
            Tore tor;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tor = new Tore();
                    tor.Id = (int)reader["Id"];
                    tor.LigaID = (int)reader["LigaID"];
                    tor.SaisonID = (int)reader["saisonID"];
                    tor.SpieltagNr = reader["SpieltagNr"].ToString();                    
                    tor.Spielminute = (int)reader["Spielminute"];
                    tor.SpielerID = (int)reader["SpielerID"];
                    tor.Spielstand = reader["Spielstand"].ToString();
                    tor.SpieltagsID = (int)reader["SpieltagsID"];

                    tore.Add(tor);
                }
            }
            conn.Close();
            return tore;
        }

        public async Task<Tore> UpdateTore(Tore Tore)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Tore(SaisonID, SpieltagNr,Spielminute,SpielerID, Spielstand,SpieltagsID)" +
                " VALUES(@SaisonID,@SpieltagNr,@Spielminute,@SpielerID,@Spielstand,@SpieltagsID)";

            cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
            cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagNr);            
            cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
            cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
            cmd.Parameters.AddWithValue("@Spielstand", Tore.Spielstand);
            cmd.Parameters.AddWithValue("@SpieltagsID", Tore.SpieltagsID);

            cmd.ExecuteNonQuery();

            conn.Close();

            return Tore;
        }
    }
}

