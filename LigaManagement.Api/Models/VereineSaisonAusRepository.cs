using LigaManagement.Api.Models;
using LigaManagement.Models;
using Ligamanager.Components;
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
    public class VereineSaisonAusRepository : IVereineSaisonAusRepository
    {

        public async Task<List<VereineSaisonAus>> AddVereineSaison(List<VereineSaisonAus> vereineSaison)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            for (int i = 0; i < vereineSaison.Count - 1; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT VereineSaisonAus (VereinNr, SaisonID, LigaID)" +
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

        public async Task<IEnumerable<VereineSaisonAus>> GetVereineSaison(int SaisonID)
        {
            try
            {
                List<VereineSaisonAus> vereineSaison = new List<VereineSaisonAus>();

                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT [Id],[VereinNr],[SaisonID],[LigaID] FROM [dbo].[VereineSaisonAus] where saisonID =" + SaisonID, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VereineSaisonAus verein = new VereineSaisonAus();
                        verein.VereinNr = (int)reader["VereinNr"];
                        verein.SaisonID = (int)reader["SaisonID"];
                        verein.LigaID = (int)reader["LigaID"];

                        vereineSaison.Add(verein);
                    }
                }

                conn.Close();
                return vereineSaison;
            }
            catch (Exception ex)
            {

                Debug.Print(ex.Message);
                return null;
            }


        }

    }
}

