using LigaManagement.Web.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Reflection;

namespace Ligamanager.Components
{
    public class LMSettings
    {
        public static string GetSprache_LandKZ()
        {
            string LandKz = "DE";
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT Sprache_LandKZ FROM [Einstellungen] ", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LandKz = reader["Sprache_LandKZ"].ToString().Trim();
                    }
                }
                conn.Close();
                return LandKz;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return LandKz;
            }
        }
        public static bool GetImportVisible()
        {
            bool btImportVisible = false;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT ImportVisible FROM [Einstellungen] ", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        btImportVisible = bool.Parse(reader["ImportVisible"].ToString());
                    }
                }
                conn.Close();
                return btImportVisible;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return btImportVisible;
            }
        }
        public static bool GetTabellenAnlegenVisible()
        {
            bool bTabellenAnlegenVisible = false;
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT TabellenAnlegenVisible FROM [Einstellungen] ", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bTabellenAnlegenVisible = bool.Parse(reader["TabellenAnlegenVisible"].ToString());
                    }
                }
                conn.Close();
                return bTabellenAnlegenVisible;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return bTabellenAnlegenVisible;
            }
        }
    }
}
