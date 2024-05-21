using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace LigaManagerManagement.Api.Models
{
    public class VereineSaisonAusRepository : IVereineSaisonAusRepository
    {

        public async Task<List<VereineSaisonAus>> AddVereineSaison(int LigaId, int SaisonID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                SqlCommand command = new SqlCommand();
                List<VereineSaisonAus> vereineSaisonAus = new List<VereineSaisonAus>();
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                if (LigaId < 3)
                {
                    command = new SqlCommand("SELECT DISTINCT [LigaID], [SaisonID], [Verein1_Nr] FROM [dbo].[Spieltage]  WHERE SaisonID = " + SaisonID + " and LIGAID =" + LigaId, conn);
                }
                else if (LigaId == 4  || LigaId ==  15)
                {
                    command = new SqlCommand("SELECT DISTINCT [LigaID], [SaisonID], [Verein1_Nr] FROM [dbo].[SpieltagePL]  WHERE SaisonID = " + SaisonID + " and LIGAID =" + LigaId, conn);
                }
                else if (LigaId == 6)
                {
                    command = new SqlCommand("SELECT DISTINCT [LigaID], [SaisonID], [Verein1_Nr] FROM [dbo].[SpieltageIT]  WHERE SaisonID = " + SaisonID + " and LIGAID =" + LigaId, conn);
                }
                else if (LigaId == 7)
                {
                    command = new SqlCommand("SELECT DISTINCT [LigaID], [SaisonID], [Verein1_Nr] FROM [dbo].[SpieltageFR]  WHERE SaisonID = " + SaisonID + " and LIGAID =" + LigaId, conn);
                }
                else if (LigaId == 8)
                {
                    command = new SqlCommand("SELECT DISTINCT [LigaID], [SaisonID], [Verein1_Nr] FROM [dbo].[SpieltageES]  WHERE SaisonID = " + SaisonID + " and LIGAID =" + LigaId, conn);
                }
                else if (LigaId == 9)
                {
                    command = new SqlCommand("SELECT DISTINCT [LigaID], [SaisonID], [Verein1_Nr] FROM [dbo].[SpieltageNL]  WHERE SaisonID = " + SaisonID + " and LIGAID =" + LigaId, conn);
                }
                else if (LigaId == 10)
                {
                    command = new SqlCommand("SELECT DISTINCT [LigaID], [SaisonID], [Verein1_Nr] FROM [dbo].[SpieltagePT]  WHERE SaisonID = " + SaisonID + " and LIGAID =" + LigaId, conn);
                }
                else if (LigaId == 11)
                {
                    command = new SqlCommand("SELECT DISTINCT [LigaID], [SaisonID], [Verein1_Nr] FROM [dbo].[SpieltageTU]  WHERE SaisonID = " + SaisonID + " and LIGAID =" + LigaId, conn);
                }
                else if (LigaId == 14)
                {
                    command = new SqlCommand("SELECT DISTINCT [LigaID], [SaisonID], [Verein1_Nr] FROM [dbo].[SpieltageBE]  WHERE SaisonID = " + SaisonID + " and LIGAID =" + LigaId, conn);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VereineSaisonAus vereinAus = new VereineSaisonAus();
                        vereinAus.VereinNr = (int)reader["Verein1_Nr"];
                        vereinAus.SaisonID = (int)reader["SaisonID"];
                        vereinAus.LigaID = (int)reader["LigaID"];

                        vereineSaisonAus.Add(vereinAus);
                    }
                }

                for (int i = 0; i <= vereineSaisonAus.Count - 1; i++)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT VereineSaisonAus (VereinNr, SaisonID, LigaID)" +
                        " VALUES(@VereinNr,@SaisonID,@LigaID)";

                    cmd.Parameters.AddWithValue("@VereinNr", vereineSaisonAus[i].VereinNr);
                    cmd.Parameters.AddWithValue("@SaisonID", vereineSaisonAus[i].SaisonID);
                    cmd.Parameters.AddWithValue("@LigaID", vereineSaisonAus[i].LigaID);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();

                return vereineSaisonAus;
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

        public async Task<IEnumerable<VereineSaisonAus>> GetVereineSaison(int SaisonID)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                List<VereineSaisonAus> vereineSaisonAus = new List<VereineSaisonAus>();

                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                command = new SqlCommand("SELECT [Id],[VereinNr],[SaisonID],[LigaID] FROM [dbo].[VereineSaisonAus] where saisonID =" + SaisonID, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VereineSaisonAus vereinAus = new VereineSaisonAus();
                        vereinAus.VereinNr = (int)reader["VereinNr"];
                        vereinAus.SaisonID = (int)reader["SaisonID"];
                        vereinAus.LigaID = (int)reader["LigaID"];

                        vereineSaisonAus.Add(vereinAus);
                    }
                }

                conn.Close();
                return vereineSaisonAus;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<bool> DeleteVereineSaison(int LigaID, int SaisonID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Delete from VereineSaisonAus WHERE LigaID =" + LigaID + " AND SaisonID=" + SaisonID;

                cmd.Parameters.AddWithValue("@SaisonID", SaisonID);
                cmd.Parameters.AddWithValue("@LigaID", LigaID);
                cmd.ExecuteNonQuery();


                conn.Close();

                return true;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return false;
            }
        }
        public async Task<bool> DeleteVereineAll()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Delete from VereineSaisonAus";
                
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return false;
            }
        }

    }
}

