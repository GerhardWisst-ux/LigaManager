using LigaManagement.Models;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace LigaManagement.Api.Models
{
    public class SpielerSpieltagRepository : ISpielerSpieltagRepository
    {       
        public async Task<SpielerSpieltag> AddSpieler(SpielerSpieltag SpielerSpieltag)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO [SpielerSpieltag] (Tore, Einsatz, SaisonID, KaderID, SpieltagNr, Eingewechselt,EingewechseltMin,Spielminuten,Ausgewechselt,AusgewechseltMin,GelbeKarten,RoteKarten)" +
                " VALUES(@Tore,@Einsatz,@SaisonID,@KaderID,@SpieltagNr,@Eingewechselt,@EingewechseltMin,@Spielminuten,@Ausgewechselt,@AusgewechseltMin,@GelbeKarten,@RoteKarten)";

            cmd.Parameters.AddWithValue("@Tore", SpielerSpieltag.Tore);
            cmd.Parameters.AddWithValue("@Einsatz", SpielerSpieltag.Einsatz);
            cmd.Parameters.AddWithValue("@SaisonID", SpielerSpieltag.SaisonId);
            cmd.Parameters.AddWithValue("@KaderID", SpielerSpieltag.KaderId);
            cmd.Parameters.AddWithValue("@SpieltagNr", SpielerSpieltag.SpieltagId);
            cmd.Parameters.AddWithValue("@Eingewechselt", SpielerSpieltag.Eingewechselt);
            cmd.Parameters.AddWithValue("@EingewechseltMin", SpielerSpieltag.EingewechseltMin);
            cmd.Parameters.AddWithValue("@Ausgewechselt", SpielerSpieltag.Ausgewechselt);
            cmd.Parameters.AddWithValue("@AusgewechseltMin", SpielerSpieltag.AusgewechseltMin);
            cmd.Parameters.AddWithValue("@GelbeKarten", SpielerSpieltag.GelbeKarten);
            cmd.Parameters.AddWithValue("@RoteKarten", SpielerSpieltag.RoteKarten);
            cmd.Parameters.AddWithValue("@Spielminuten", SpielerSpieltag.Spielminuten);            
         
            cmd.ExecuteNonQuery();

            conn.Close();

            return SpielerSpieltag;
        }

        public Task<SpielerSpieltag> DeleteSpieler(SpielerSpieltag SpielerId)
        {
            throw new NotImplementedException();
        }

        public Task<SpielerSpieltag> DeleteSpielerSpieltag()
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[SpielerSpieltag]  where saisonID = 1 and SpieltagNr = 23" +
            
            cmd.ExecuteNonQuery();

            conn.Close();

            return null;
        }

        public Task<IEnumerable<SpielerSpieltag>> GetAllSpieler()
        {
            throw new NotImplementedException();
        }

        public async Task<SpielerSpieltag> GetSpieler(int Id)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [SpielerSpieltag] where ID =" + Id, conn);
            SpielerSpieltag spielerspieltag = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    spielerspieltag = new SpielerSpieltag();
                    spielerspieltag.Id = (int)reader["Id"];
                    spielerspieltag.KaderId = (int)reader["KaderId"];
                    spielerspieltag.SaisonId = (int)reader["SaisonID"];
                    spielerspieltag.SpieltagId = (int)reader["SpieltagId"];
                    spielerspieltag.Spielminuten = (int)reader["Spielminuten"];
                    spielerspieltag.Einsatz = (int)reader["Einsatz"];
                    spielerspieltag.Tore = (int)reader["Tore"];
                    spielerspieltag.GelbeKarten = (bool)reader["GelbeKarten"];
                    spielerspieltag.RoteKarten = (bool)reader["RoteKarten"];
                    spielerspieltag.Eingewechselt = (bool)reader["Eingewechselt"];
                    spielerspieltag.EingewechseltMin = (int)reader["EingewechseltMin"];
                    spielerspieltag.Ausgewechselt = (bool)reader["Ausgewechselt"];
                    spielerspieltag.AusgewechseltMin = (int)reader["AusgewechseltMin"];
                }
            }
            conn.Close();
            return spielerspieltag;
        }

        public async Task<SpielerSpieltag> UpdateSpieler(SpielerSpieltag SpielerSpieltag)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE SpielerSpieltag(Tore, Einsatz, SaisonID, KaderID, SpieltagID, Eingewechselt,EingewechseltMin,Spielminuten,Ausgewechselt,AusgewechseltMin,GelbeKarten,RoteKarten,Spielminuten)" +
                " VALUES(@Tore,@Einsatz,@SaisonID,@KaderID,@SpieltagID,@Eingewechselt,@EingewechseltMin,@Spielminuten,@Ausgewechselt,@AusgewechseltMin,@GelbeKarten,@RoteKarten,@Spielminuten)";

            cmd.Parameters.AddWithValue("@Tore", SpielerSpieltag.Tore);
            cmd.Parameters.AddWithValue("@Einsatz", SpielerSpieltag.Einsatz);
            cmd.Parameters.AddWithValue("@SaisonID", SpielerSpieltag.SaisonId);
            cmd.Parameters.AddWithValue("@KaderID", SpielerSpieltag.KaderId);
            cmd.Parameters.AddWithValue("@SpieltagID", SpielerSpieltag.SpieltagId);
            cmd.Parameters.AddWithValue("@Eingewechselt", SpielerSpieltag.Eingewechselt);
            cmd.Parameters.AddWithValue("@EingewechseltMin", SpielerSpieltag.EingewechseltMin);
            cmd.Parameters.AddWithValue("@Ausgewechselt", SpielerSpieltag.Ausgewechselt);
            cmd.Parameters.AddWithValue("@AusgewechseltMin", SpielerSpieltag.AusgewechseltMin);
            cmd.Parameters.AddWithValue("@GelbeKarten", SpielerSpieltag.GelbeKarten);
            cmd.Parameters.AddWithValue("@RoteKarten", SpielerSpieltag.RoteKarten);
            cmd.Parameters.AddWithValue("@Spielminuten", SpielerSpieltag.Spielminuten);

            cmd.ExecuteNonQuery();

            conn.Close();        

            return SpielerSpieltag;
        }

        
    }
}

