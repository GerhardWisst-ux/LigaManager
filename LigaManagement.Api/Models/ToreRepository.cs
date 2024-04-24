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
    public class ToreRepository : IToreRepository
    {
        public async Task<Tore> CreateTor(Tore Tore)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Tore] (SaisonID, LigaID, SpieltagNr, Spielminute, SpielerID,Spielstand,SpieltagId,Eigentor,Torart,Elfmeter)" +
                    " VALUES(@SaisonID,@LigaID,@SpieltagNr, @Spielminute, @SpielerID,@Spielstand,@SpieltagId,@Eigentor,@Torart,@Elfmeter)";

                cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", Tore.LigaID);
                cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagNr);
                cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
                cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
                cmd.Parameters.AddWithValue("@Spielstand", Tore.Spielstand);
                cmd.Parameters.AddWithValue("@SpieltagId", Tore.SpieltagId);
                
                if (Tore.Eigentor)
                    cmd.Parameters.AddWithValue("@Eigentor", 1);
                else
                    cmd.Parameters.AddWithValue("@Eigentor", 0);

                if (Tore.Elfmeter)
                    cmd.Parameters.AddWithValue("@Elfmeter", 1);
                else
                    cmd.Parameters.AddWithValue("@Elfmeter", 0);

                cmd.Parameters.AddWithValue("@Torart", "");

                cmd.ExecuteNonQuery();

                conn.Close();

                return Tore;
            }
            catch (System.Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Tore> DeleteTor(int ToreId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [dbo].[Tore]  where ID = @ID";

                cmd.Parameters.AddWithValue("@SaisonID", ToreId);

                cmd.ExecuteNonQuery();

                conn.Close();

                return null;
            }
            catch (System.Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<Tore> GetTor(int ToreId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [tore] where ID =" + ToreId, conn);
                Tore tor = null;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tor = new Tore();
                        tor.Id = (int)reader["Id"];
                        tor.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        tor.LigaID = int.Parse(reader["LigaID"].ToString());
                        tor.SpieltagNr = reader["SpieltagNr"].ToString();
                        tor.Spielminute = int.Parse(reader["Spielminute"].ToString());
                        tor.SpielerID = int.Parse(reader["SpielerID"].ToString());
                        tor.Spielstand = reader["Spielstand"].ToString();
                        tor.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        tor.Eigentor = bool.Parse(reader["Eigentor"].ToString());
                        tor.Elfmeter = bool.Parse(reader["Eigentor"].ToString());
                        tor.Torart = ""; // reader["Torart"].ToString();
                    }
                }
                conn.Close();
                return tor;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;

            }
        }

        public async Task<IEnumerable<Tore>> GetTore()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
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
                        tor.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        tor.LigaID = int.Parse(reader["LigaID"].ToString());
                        tor.SpieltagNr = reader["SpieltagNr"].ToString();
                        tor.Spielminute = int.Parse(reader["Spielminute"].ToString());
                        tor.SpielerID = int.Parse(reader["SpielerID"].ToString());
                        tor.Spielstand = reader["Spielstand"].ToString();
                        tor.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        tor.Eigentor = bool.Parse(reader["Eigentor"].ToString());
                        tor.Elfmeter = bool.Parse(reader["Elfmeter"].ToString());
                        tor.Torart = ""; //reader["Torart"].ToString();

                        tore.Add(tor);
                    }
                }
                conn.Close();
                return tore;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
                
            }
        }

        public async Task<Tore> UpdateTor(Tore Tore)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Tore(SaisonID,LigaID, SpieltagNr,Spielminute,SpielerID, Spielstand,SpieltagId,Eigentor,Torart,Elfmeter)" +
                " VALUES(@SaisonID,@SpieltagNr,@LigaID,@Spielminute,@SpielerID,@Spielstand,@SpieltagId,@Eigentor,@Torart,@Elfmeter)";

            cmd.Parameters.AddWithValue("@SaisonID", Tore.SaisonID);
            cmd.Parameters.AddWithValue("@LigaID", Tore.LigaID);
            cmd.Parameters.AddWithValue("@SpieltagNr", Tore.SpieltagId);
            cmd.Parameters.AddWithValue("@Spielminute", Tore.Spielminute);
            cmd.Parameters.AddWithValue("@SpielerID", Tore.SpielerID);
            cmd.Parameters.AddWithValue("@Spielstand", Tore.Spielstand);
            cmd.Parameters.AddWithValue("@SpieltagId", Tore.SpieltagId);
            
            if (Tore.Eigentor)
                cmd.Parameters.AddWithValue("@Eigentor", 1);
            else
                cmd.Parameters.AddWithValue("@Eigentor", 0);

            if (Tore.Elfmeter)
                cmd.Parameters.AddWithValue("@Elfmeter", 1);
            else
                cmd.Parameters.AddWithValue("@Elfmeter", 0);

            cmd.Parameters.AddWithValue("@Torart", "");

            cmd.ExecuteNonQuery();

            conn.Close();

            return Tore;
        }
    }
}

