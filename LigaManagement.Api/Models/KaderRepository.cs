﻿using LigaManagement.Models;
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
using System.Xml.Linq;
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
            cmd.CommandText = "INSERT INTO [Kader](SpielerName, Vorname, Rueckennummer, Geburtstag, ImVereinSeit,Tore,Einsaetze,Spielminuten,LandID,LigaID,SaisonId,VereinNr,Aktiv,Position, PositionsNr)" +
                " VALUES(@SpielerName,@Vorname,@Rueckennummer,@Geburtstag,@ImVereinSeit,@Tore,@Einsaetze,@Spielminuten,@LandID,@LigaID,@SaisonId,@VereinNr,@Aktiv,@Position, @PositionsNr)";

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
            cmd.Parameters.AddWithValue("@Position", Spieler.Position);
            cmd.Parameters.AddWithValue("@PositionsNr", Spieler.PositionsNr);

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
                    var kaderspieler = new Kader();
                    kaderspieler.Id = (int)reader["Id"];
                    kaderspieler.SpielerName = reader["SpielerName"].ToString();                    
                    kaderspieler.Vorname = reader["Vorname"].ToString();
                    kaderspieler.Rueckennummer = (int)reader["Rueckennummer"];
                    kaderspieler.Geburtsdatum = (DateTime)reader["Geburtstag"];
                    kaderspieler.ImVereinSeit = (DateTime)reader["ImVereinSeit"];
                    kaderspieler.Einsaetze = (int)reader["Einsaetze"];
                    kaderspieler.Tore = (int)reader["Tore"];
                    kaderspieler.VereinID = (int)reader["VereinNr"];
                    kaderspieler.SaisonId = (int)reader["SaisonID"];
                    kaderspieler.Aktiv = (bool)reader["Aktiv"];
                    kaderspieler.Position = (string)reader["Position"].ToString();
                    kaderspieler.PositionsNr = (int)reader["PositionsNr"];

                    allspieler.Add(kaderspieler);
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
                    kaderspieler.Position = (string)reader["Position"].ToString();
                    kaderspieler.PositionsNr = (int)reader["PositionsNr"];
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
                //cmd.CommandText = "UPDATE Kader SET (SpielerName, Vorname, Rueckennummer, Geburtstag, ImVereinSeit,Einsaetze,VereinNr)" +
                //    " VALUES(@SpielerName,@Vorname,@Rueckennummer,@Geburtstag,@ImVereinSeit,@Einsaetze,@VereinNr) WHERE ID=@ID" + Spieler.Id;

                cmd.CommandText = "UPDATE Kader SET SpielerName= @SpielerName, Vorname= @Vorname, Rueckennummer= @Rueckennummer, Geburtstag= @Geburtstag," +
                    "Tore= @Tore, Einsaetze= @Einsaetze, @Position = @Position, PositionsNr = @PositionsNr, @Spielminuten=@Spielminuten,LandID=@LandID, " +
                    "LigaID =@LigaID,SaisonId=@SaisonId,VereinNr=@VereinNr,Aktiv=@Aktiv, ImVereinSeit= @ImVereinSeit WHERE ID=@ID";

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
                cmd.Parameters.AddWithValue("@Position", Spieler.Position.ToString());                
                cmd.Parameters.AddWithValue("@PositionsNr", Spieler.PositionsNr.ToString());
                cmd.Parameters.AddWithValue("@ID", Spieler.Id);

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
