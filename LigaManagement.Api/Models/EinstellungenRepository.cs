using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ToremanagerManagement.Api.Models.Repository;

namespace ToreManagerManagement.Api.Models
{
    public class EinstellungenRepository : IEinstellungenRepository
    {
        public async Task<EinstellungenLM> GetEinstellungen()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Einstellungen]", conn);

                EinstellungenLM einstellung = new EinstellungenLM();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        einstellung = new EinstellungenLM();
                        einstellung.Sprache_LandKZ = reader["Sprache_LandKZ"].ToString();
                        einstellung.ImportVisible = bool.Parse(reader["ImportVisible"].ToString());
                        einstellung.Spielverlauf = bool.Parse(reader["Spielverlauf"].ToString());
                        einstellung.Aufstellungen = bool.Parse(reader["Aufstellungen"].ToString());
                        einstellung.TabellenAnlegenVisible = bool.Parse(reader["TabellenAnlegenVisible"].ToString());

                    }
                }
                conn.Close();
                return einstellung;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;

            }
        }


        public async Task<EinstellungenLM> UpdateEinstellungen(EinstellungenLM einstellungen)
        {
            int bImportVisible;
            int bSpielverlauf;
            int bAufstellungen;
            int bTabellenAnlegenVisible;

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (einstellungen.ImportVisible == false)
                    bImportVisible = 0;
                else
                    bImportVisible = 1;

                if (einstellungen.Spielverlauf == false)
                    bSpielverlauf = 0;
                else
                    bSpielverlauf = 1;

                if (einstellungen.Aufstellungen == false)
                    bAufstellungen = 0;
                else
                    bAufstellungen = 1;

                if (einstellungen.TabellenAnlegenVisible == false)
                    bTabellenAnlegenVisible = 0;
                else
                    bTabellenAnlegenVisible = 1;

                cmd.CommandText = "UPDATE [dbo].[Einstellungen] SET " +
                    "[Sprache_LandKZ] = '" + einstellungen.Sprache_LandKZ + "'" +
                    ",[ImportVisible] =" + bImportVisible +
                    ",[Spielverlauf] =" + bSpielverlauf +
                    ",[Aufstellungen] =" + bAufstellungen +
                    ",[TabellenAnlegenVisible] =" + bTabellenAnlegenVisible;


                cmd.ExecuteNonQuery();

                conn.Close();

                return einstellungen;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;

            }
        }
    }


}
