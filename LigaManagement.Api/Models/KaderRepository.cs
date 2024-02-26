using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace LigaManagement.Api.Models
{
    public class KaderRepository : IKaderRepository
    {
        private readonly AppDbContext appDbContext;

        public KaderRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Kader> AddSpieler(Kader Spieler)
        {
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO [Kader](SpielerName, Vorname, Rueckennummer, Geburtstag, ImVereinSeit,Tore,Einsaetze,Spielminuten,LandID,LigaID,SaisonId,VereinNr,Aktiv)" +
                " VALUES(@SpielerName,@Vorname,@Rueckennummer,@Geburtstag,@ImVereinSeit,@Tore,@Einsaetze,@Spielminuten,@LandID,@LigaID,@SaisonId,@VereinNr,@Aktiv)";

            cmd.Parameters.AddWithValue("@SpielerName", Spieler.SpielerName);
            cmd.Parameters.AddWithValue("@Vorname", Spieler.Vorname);
            cmd.Parameters.AddWithValue("@Rueckennummer", Spieler.Rueckennummer);
            cmd.Parameters.AddWithValue("@Geburtstag", Spieler.Geburtsdatum);
            cmd.Parameters.AddWithValue("@ImVereinSeit", Spieler.ImVereinSeit);
            cmd.Parameters.AddWithValue("@Tore", Spieler.Tore);
            cmd.Parameters.AddWithValue("@Einsaetze", Spieler.Einsaetze);
            cmd.Parameters.AddWithValue("@Spielminuten", Spieler.Spielminuten);
            cmd.Parameters.AddWithValue("@LandID", Spieler.LandID);
            cmd.Parameters.AddWithValue("@LigaID", Spieler.LigaID);
            cmd.Parameters.AddWithValue("@SaisonId", Spieler.SaisonId);
            cmd.Parameters.AddWithValue("@VereinNr", Spieler.VereinID);
            cmd.Parameters.AddWithValue("@Aktiv", Spieler.Aktiv);
                            
            cmd.ExecuteNonQuery();

            conn.Close();

            return Spieler;
        }

        public async Task<Kader> DeleteSpieler(int SpielerId)
        {
            //var result = await appDbContext.Spieler
            //   .FirstOrDefaultAsync(e => e.Id == SpielerId);
            //if (result != null)
            //{
            //    appDbContext.Spieler.Remove(result);
            //    await appDbContext.SaveChangesAsync();
            //    return result;
            //}

            return null;
        }

        public async Task<IEnumerable<Kader>> GetAllSpieler()
        {
            List<Kader> allspieler = new List<Kader>();

            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [Kader]", conn);                      
            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var spieler = new Kader();
                    spieler.Id = (int)reader["Id"];
                    spieler.SpielerName = reader["SpielerName"].ToString();                    
                    spieler.Vorname = reader["Vorname"].ToString();
                    spieler.Rueckennummer = (int)reader["Rueckennummer"];
                    spieler.Geburtsdatum = (DateTime)reader["Geburtstag"];
                    spieler.ImVereinSeit = (DateTime)reader["ImVereinSeit"];
                    spieler.Einsaetze = (int)reader["Einsaetze"];
                    spieler.Tore = (int)reader["Tore"];
                    spieler.VereinID = (int)reader["VereinNr"];
                    spieler.SaisonId = (int)reader["SaisonID"];
                    spieler.Aktiv = (bool)reader["Aktiv"];

                    allspieler.Add(spieler);
                }
                  
            }
            conn.Close();
            return allspieler;
          
        }

        public async Task<Kader> GetSpieler(int SpielerId)
        {            
            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [Kader] where ID =" + SpielerId, conn);
            Kader kaderspieler = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    kaderspieler = new Kader();
                    kaderspieler.Id = (int)reader["Id"];
                    kaderspieler.SpielerName = reader["SpielerName"].ToString();
                    kaderspieler.Vorname = reader["Vorname"].ToString();
                    kaderspieler.Rueckennummer = (int)reader["Rueckennummer"];
                    kaderspieler.Geburtsdatum = (DateTime)reader["Geburtstag"];
                    kaderspieler.Einsaetze = (int)reader["Einsaetze"];
                    kaderspieler.Tore = (int)reader["Tore"];
                    kaderspieler.VereinID = (int)reader["VereinNr"];
                    kaderspieler.ImVereinSeit = (DateTime)reader["ImVereinSeit"];
                    kaderspieler.SaisonId = (int)reader["SaisonID"];
                    kaderspieler.Aktiv = (bool)reader["Aktiv"];
                }
            }
            conn.Close();
            return kaderspieler;            
        }
              
        public async Task<Kader> UpdateSpieler(Kader Spieler)
        {
            try
            {                

                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE Kader(SpielerName, SpielerName, Vorname, Rueckennummer, Geburtstag, ImVereinSeit,Tore,Einsaetze,Spielminuten,LandID,LigaID,SaisonId,VereinNr,Aktiv)" +
                    " VALUES(@SpielerName,@Vorname,@Rueckennummer,@Geburtstag,@ImVereinSeit,@Tore,@Einsaetze,@Spielminuten,@LandID,@LigaID,@SaisonId,@VereinNr,@Aktiv)";

                cmd.Parameters.AddWithValue("@SpielerName", Spieler.SpielerName);
                cmd.Parameters.AddWithValue("@Vorname", Spieler.Vorname);
                cmd.Parameters.AddWithValue("@Rueckennummer", Spieler.Rueckennummer);
                cmd.Parameters.AddWithValue("@Geburtstag", Spieler.Geburtsdatum);
                cmd.Parameters.AddWithValue("@ImVereinSeit", Spieler.ImVereinSeit);
                cmd.Parameters.AddWithValue("@Tore", Spieler.Tore);
                cmd.Parameters.AddWithValue("@Einsaetze", Spieler.Einsaetze);
                cmd.Parameters.AddWithValue("@Spielminuten", Spieler.Spielminuten);
                cmd.Parameters.AddWithValue("@LandID", Spieler.LandID);
                cmd.Parameters.AddWithValue("@LigaID", Spieler.LigaID);
                cmd.Parameters.AddWithValue("@SaisonId", Spieler.SaisonId);
                cmd.Parameters.AddWithValue("@VereinNr", Spieler.VereinID);
                cmd.Parameters.AddWithValue("@Aktiv", Spieler.Aktiv);

                cmd.ExecuteNonQuery();

                conn.Close();

                return Spieler;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return null;
            }

        }
    }
}

