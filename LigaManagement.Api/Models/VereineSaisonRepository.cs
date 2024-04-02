using LigaManagement.Api.Migrations;
using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


namespace LigaManagerManagement.Api.Models
{
    public class VereineSaisonRepository : IVereineSaisonRepository
    {
        private readonly AppDbContext appDbContext;

        public VereineSaisonRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<List<VereineSaison>> AddVereineSaison(List<VereineSaison> vereineSaison)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            for (int i = 0; i < vereineSaison.Count -1 ; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT VereineSaison (VereinNr, SaisonID, LigaID)" +
                    " VALUES(@VereinNr,@SaisonID,@LigaID)";

                cmd.Parameters.AddWithValue("@VereinNr", vereineSaison[i].VereinNr);
                cmd.Parameters.AddWithValue("@SaisonID", vereineSaison[i].SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", vereineSaison[i].LigaID);
                cmd.ExecuteNonQuery();
            }         
           
            conn.Close();
                        
            return vereineSaison;
        }

        public Task<IEnumerable<VereinAktSaison>> GetVereineAktSaison()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VereineSaison>> GetVereineSaison()
        {
            List<VereineSaison> vereineSaison = new List<VereineSaison>();

            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT [Id],[VereinNr],[SaisonID],[LigaID] FROM [dbo].[VereineSaison]", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    VereineSaison verein = new VereineSaison();
                    verein.VereinNr = (int)reader["VereinNr"];                  
                    verein.SaisonID = (int)reader["SaisonID"];
                    verein.LigaID = (int)reader["LigaID"];

                    vereineSaison.Add(verein);
                }
            }

            conn.Close();
            return vereineSaison;
        }       

    }
}

