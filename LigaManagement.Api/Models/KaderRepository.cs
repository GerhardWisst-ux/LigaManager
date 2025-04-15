using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;


namespace LigaManagement.Api.Models
{
    public class KaderRepository : IKaderRepository
    {
        int bAktiv;
                
        public async Task<Kader> AddSpieler(Kader spieler)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Kader](SpielerName, Vorname, Rueckennummer, Geburtstag, ImVereinSeit,Tore,Einsaetze,Spielminuten,LandID,LigaID,SaisonId,VereinNr,Aktiv,Position,PositionsNr,Groesse,Gewicht,Laenderspiele,LaenderspieleTore,Abloesesumme)" +
                    " VALUES(@SpielerName,@Vorname,@Rueckennummer,@Geburtstag,@ImVereinSeit,@Tore,@Einsaetze,@Spielminuten,@LandID,@LigaID,@SaisonId,@VereinNr,@Aktiv,@Position, @PositionsNr,@Groesse,@Gewicht,@Laenderspiele,@LaenderspieleTore,@Abloesesumme)";

                if (spieler.Aktiv == false)
                    bAktiv = 0;
                else
                    bAktiv = 1;

                cmd.Parameters.AddWithValue("@SpielerName", spieler.SpielerName.ToString());
                cmd.Parameters.AddWithValue("@LandID", spieler.LandID);
                cmd.Parameters.AddWithValue("@SaisonId", spieler.SaisonId);
                cmd.Parameters.AddWithValue("@LigaID", spieler.LigaID);
                cmd.Parameters.AddWithValue("@Vorname", spieler.Vorname.ToString());
                cmd.Parameters.AddWithValue("@Rueckennummer", spieler.Rueckennummer);
                cmd.Parameters.AddWithValue("@Geburtstag", spieler.Geburtsdatum);
                cmd.Parameters.AddWithValue("@ImVereinSeit", spieler.ImVereinSeit);
                cmd.Parameters.AddWithValue("@Tore", spieler.Tore);
                cmd.Parameters.AddWithValue("@Einsaetze", spieler.Einsaetze);
                cmd.Parameters.AddWithValue("@Spielminuten", spieler.Spielminuten);
                cmd.Parameters.AddWithValue("@VereinNr", spieler.VereinID);
                cmd.Parameters.AddWithValue("@Aktiv", bAktiv);
                cmd.Parameters.AddWithValue("@Position", spieler.Position.ToString());
                cmd.Parameters.AddWithValue("@PositionsNr", spieler.PositionsNr);                
                cmd.Parameters.AddWithValue("@Groesse", 0);
                cmd.Parameters.AddWithValue("@Gewicht", 0);
                cmd.Parameters.AddWithValue("@Laenderspiele",0);
                cmd.Parameters.AddWithValue("@LaenderspieleTore", 0);
                cmd.Parameters.AddWithValue("@Abloesesumme", 0);

                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return spieler;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Kader> DeleteSpieler(int SpielerId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [dbo].[Kader] Where Id= @Id";

                cmd.Parameters.AddWithValue("@Id", SpielerId);

                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return null;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }       

        public async Task<IEnumerable<Kader>> GetAllSpieler()
        {
            try
            {
                List<Kader> allspieler = new List<Kader>();

                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [Kader]", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        var kaderspieler = new Kader();
                        kaderspieler.Id = (int)reader["Id"];
                        kaderspieler.SaisonId = (int)reader["SaisonID"];
                        kaderspieler.VereinID = (int)reader["VereinNr"];
                        kaderspieler.LandID = (int)reader["LandID"];
                        kaderspieler.SpielerName = reader["SpielerName"].ToString();
                        kaderspieler.Vorname = reader["Vorname"].ToString();
                        kaderspieler.Rueckennummer = (int)reader["Rueckennummer"];
                        kaderspieler.Geburtsdatum = (DateTime)reader["Geburtstag"];
                        kaderspieler.Alter = Globals.GetAgeFromDate((DateTime)reader["Geburtstag"]);
                        kaderspieler.ImVereinSeit = (DateTime)reader["ImVereinSeit"];
                        kaderspieler.Einsaetze = (int)reader["Einsaetze"];
                        kaderspieler.Tore = (int)reader["Tore"];
                        kaderspieler.Aktiv = (bool)reader["Aktiv"];
                        kaderspieler.PositionsNr = (int)reader["PositionsNr"];
                        kaderspieler.Position = (string)reader["Position"].ToString();
                        //kaderspieler.Groesse = (int)reader["Groesse"];
                        //kaderspieler.Gewicht = (int)reader["Gewicht"];
                        //kaderspieler.Laenderspiele = (int)reader["Laenderspiele"];
                        //kaderspieler.LaenderspieleTore = (int)reader["LaenderspieleTore"];
                        //kaderspieler.Abloesesumme = (int)reader["Abloesesumme"];

                        allspieler.Add(kaderspieler);
                    }

                }
                conn.Close();
                return allspieler;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<Kader> GetSpieler(int SpielerId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM [Kader] where ID =" + SpielerId, conn);
                Kader kaderspieler = null;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        kaderspieler = new Kader();
                        kaderspieler.Id = (int)reader["Id"];
                        kaderspieler.LandID = (int)reader["LandID"];
                        kaderspieler.SaisonId = (int)reader["SaisonID"];
                        kaderspieler.VereinID = (int)reader["VereinNr"];
                        kaderspieler.SpielerName = reader["SpielerName"].ToString();
                        kaderspieler.Vorname = reader["Vorname"].ToString();
                        kaderspieler.Rueckennummer = (int)reader["Rueckennummer"];
                        kaderspieler.Geburtsdatum = (DateTime)reader["Geburtstag"];
                        kaderspieler.Alter = Globals.GetAgeFromDate((DateTime)reader["Geburtstag"]);
                        kaderspieler.ImVereinSeit = (DateTime)reader["ImVereinSeit"];
                        kaderspieler.Einsaetze = (int)reader["Einsaetze"];
                        kaderspieler.Tore = (int)reader["Tore"];
                        kaderspieler.Aktiv = (bool)reader["Aktiv"];
                        kaderspieler.PositionsNr = (int)reader["PositionsNr"];
                        kaderspieler.Position = (string)reader["Position"].ToString();
                        //kaderspieler.Groesse = (int)reader["Groesse"];
                        //kaderspieler.Gewicht = (int)reader["Gewicht"];
                        //kaderspieler.Laenderspiele = (int)reader["Laenderspiele"];
                        //kaderspieler.LaenderspieleTore = (int)reader["LaenderspieleTore"];
                        //kaderspieler.Abloesesumme = (int)reader["Abloesesumme"];
                    }
                }
                conn.Close();
                return kaderspieler;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
              
        public async Task<Kader> UpdateSpieler(Kader Spieler)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;               

                if (Spieler.Aktiv == false)
                    bAktiv = 0;
                else
                    bAktiv = -1;

                cmd.CommandText = "UPDATE [dbo].[Kader] SET " +
                        "[Rueckennummer] = '" + Spieler.Rueckennummer + "'" +
                        ",[SpielerName] = '" + Spieler.SpielerName + "'" +
                        ",[Vorname] = '" + Spieler.Vorname + "'" +
                        ",[Geburtstag] = '" + Spieler.Geburtsdatum + "'" +
                        ",[ImVereinSeit] = '" + Spieler.ImVereinSeit + "'" +
                        ",[Tore] = " + Spieler.Tore +
                        ",[Position] = '" + Spieler.Position + "'" +
                        ",[PositionsNr] = " + Spieler.PositionsNr +
                        ",[Spielminuten] = " + Spieler.Spielminuten +
                        ",[Laenderspiele] = " + Spieler.Laenderspiele +
                        ",[LaenderspieleTore] = " + Spieler.LaenderspieleTore +
                        ",[Groesse] = " + Spieler.Groesse +
                        ",[Gewicht] = " + Spieler.Gewicht +
                        ",[Abloesesumme] = " + Spieler.Abloesesumme +
                        ",[SaisonID] = " + Spieler.SaisonId +
                        ",[LigaID] = " + Spieler.LigaID +
                        ",[Aktiv] = " + bAktiv +
                        ",[LandID] = " + Spieler.LandID +
                        ",[VereinNr] = " + Spieler.VereinID +                        
                        " WHERE [Id] = " + Spieler.Id;
                               
                await cmd.ExecuteNonQueryAsync();

                conn.Close();

                return Spieler;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }
    }
}

