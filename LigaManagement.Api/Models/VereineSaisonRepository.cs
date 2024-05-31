using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;


namespace LigaManagerManagement.Api.Models
{
    public class VereineSaisonRepository : IVereineSaisonRepository
    { 

        public async Task<List<VereineSaison>> AddVereineSaison(List<VereineSaison> vereineSaison)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                for (int i = 0; i <= vereineSaison.Count - 1; i++)
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
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public Task<IEnumerable<VereinAktSaison>> GetVereineAktSaison()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VereineSaison>> GetVereineSaison()
        {
            try
            {
                List<VereineSaison> vereineSaison = new List<VereineSaison>();

                SqlConnection conn = new SqlConnection(Globals.connstring);
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
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }       

    }
}

