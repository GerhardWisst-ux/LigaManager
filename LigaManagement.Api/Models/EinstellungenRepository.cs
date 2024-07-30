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
                SqlConnection connReader = new SqlConnection(Globals.connstring);
                conn.Open();
                connReader.Open();

                SqlCommand cmd = new SqlCommand();
                SqlCommand cmdKader = new SqlCommand();
                cmd.Connection = conn;
                cmdKader.Connection = conn;

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


                if (einstellungen.SaisonIDVon > 0 && einstellungen.SaisonIDNach > 0)
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM [Kader] where SaisonID = " + einstellungen.SaisonIDVon, connReader);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int number;
                            double dec;
                            cmdKader = new SqlCommand();
                            cmdKader.Connection = conn;
                            cmdKader.CommandText = "INSERT INTO [Kader] (SpielerName,Vorname,Geburtstag,Groesse,Gewicht,Laenderspiele,LaenderspieleTore,VereinNr,LandID,SaisonID,LigaID,Rueckennummer,Einsaetze,Spielminuten,Tore,Abloesesumme,ImVereinSeit,Aktiv,Position,PositionsNr)" +
                     " VALUES(@SpielerName,@Vorname,@Geburtstag,@Groesse,@Gewicht,@Laenderspiele,@LaenderspieleTore,@VereinNr,@LandID,@SaisonID,@LigaID,@Rueckennummer,@Einsaetze,@Spielminuten,@Tore,@Abloesesumme,@ImVereinSeit,@Aktiv,@Position,@PositionsNr)";

                            cmdKader.Parameters.AddWithValue("@SpielerName", reader["SpielerName"].ToString());
                            cmdKader.Parameters.AddWithValue("@Vorname", reader["Vorname"].ToString());
                            cmdKader.Parameters.AddWithValue("@Geburtstag", reader["Geburtstag"].ToString());
                            cmdKader.Parameters.AddWithValue("@Laenderspiele", int.TryParse(reader["Laenderspiele"].ToString(), out number));
                            cmdKader.Parameters.AddWithValue("@LaenderspieleTore", int.TryParse(reader["LaenderspieleTore"].ToString(), out number));
                            cmdKader.Parameters.AddWithValue("@Groesse", double.TryParse(reader["Groesse"].ToString(), out dec));
                            cmdKader.Parameters.AddWithValue("@Gewicht", double.TryParse(reader["Gewicht"].ToString(), out dec));
                            cmdKader.Parameters.AddWithValue("@VereinNr", int.Parse(reader["VereinNr"].ToString()));
                            cmdKader.Parameters.AddWithValue("@LandID", int.Parse(reader["LandID"].ToString()));
                            cmdKader.Parameters.AddWithValue("@SaisonID", einstellungen.SaisonIDNach);
                            cmdKader.Parameters.AddWithValue("@LigaID", int.Parse(reader["LigaID"].ToString()));
                            cmdKader.Parameters.AddWithValue("@Rueckennummer", int.Parse(reader["Rueckennummer"].ToString()));
                            cmdKader.Parameters.AddWithValue("@Einsaetze", 0);
                            cmdKader.Parameters.AddWithValue("@Spielminuten", 0);
                            cmdKader.Parameters.AddWithValue("@Tore", 0);
                            cmdKader.Parameters.AddWithValue("@Abloesesumme", double.TryParse(reader["Abloesesumme"].ToString(), out dec));

                            if (bool.Parse(reader["Aktiv"].ToString()))
                                cmdKader.Parameters.AddWithValue("@Aktiv", 1);
                            else
                                cmdKader.Parameters.AddWithValue("@Aktiv", 0);

                            cmdKader.Parameters.AddWithValue("@ImVereinSeit", reader["ImVereinSeit"].ToString());
                            //cmdKader.Parameters.AddWithValue("@Image", null);
                            cmdKader.Parameters.AddWithValue("@Position", reader["Position"].ToString());
                            cmdKader.Parameters.AddWithValue("@PositionsNr", int.Parse(reader["PositionsNr"].ToString()));

                            cmdKader.ExecuteNonQuery();
                        }
                    }
                }

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
