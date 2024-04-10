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

        public async Task<Tore> CreateTor(Tore Tore)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Tore] (SaisonID, LigaID, SpieltagNr, Spielminute, SpielerID,Spielstand,SpieltagId,Eigentor)" +
                    " VALUES(@SaisonID,@LigaID,@SpieltagNr, @Spielminute, @SpielerID,@Spielstand,@SpieltagId,@Eigentor)";

                cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", Tore.LigaID);
                cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagNr);
                cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
                cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
                cmd.Parameters.AddWithValue("@Spielstand", Tore.Spielstand);                
                cmd.Parameters.AddWithValue("@SpieltagId", Tore.SpieltagId);
                cmd.Parameters.AddWithValue("@Eigentor", Tore.Eigentor);

                cmd.ExecuteNonQuery();

                conn.Close();

                return Tore;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
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
                    tor.Eigentor = (bool)reader["Eigentor"];
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
                    tor.SaisonID = (int)reader["saisonID"];
                    tor.LigaID = (int)reader["LigaID"];                    
                    tor.SpieltagNr = reader["SpieltagNr"].ToString();                    
                    tor.Spielminute = (int)reader["Spielminute"];
                    tor.SpielerID = (int)reader["SpielerID"];
                    tor.Spielstand = reader["Spielstand"].ToString();
                    tor.SpieltagId = (int)reader["SpieltagId"];
                    tor.Eigentor = (bool)reader["Eigentor"];

                    tore.Add(tor);
                }
            }
            conn.Close();
            return tore;
        }

        public async Task<Tore> UpdateTor(Tore Tore)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Tore(SaisonID,LigaID, SpieltagNr,Spielminute,SpielerID, Spielstand,SpieltagId,Eigentor)" +
                " VALUES(@SaisonID,@SpieltagNr,@LigaID,@Spielminute,@SpielerID,@Spielstand,@SpieltagId,@Eigentor)";

            cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
            cmd.Parameters.AddWithValue("@LigaID", Tore.LigaID);
            cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagId);
            cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
            cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
            cmd.Parameters.AddWithValue("@Spielstand", Tore.Spielstand);            
            cmd.Parameters.AddWithValue("@SpieltagId", Tore.SpieltagId);
            cmd.Parameters.AddWithValue("@Eigentor", Tore.Eigentor);

            cmd.ExecuteNonQuery();

            conn.Close();

            return Tore;
        }
    }
}

