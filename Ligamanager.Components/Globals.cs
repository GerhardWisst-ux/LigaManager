using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Ligamanager.Components
{
    public class Globals
    {
        public static string connstring = "Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true;";

        public static string currentSaison;
        public static string currentLiga;
        public static int Spieltag;
        public static int SaisonID;
        public static int maxSpieltag;
        public static bool bVisibleNavMenuElements = false;

        public static Dictionary<string, string> VereinAktSaison = new Dictionary<string, string>();
        public static int currentVereinID;

        public enum Tabart
        {
            Gesamt = 1,
            Heim = 2,
            Auswärts = 3,
            Vorrunde = 4,
            Rückrunde = 5,
            EwigeTabelle = 6
        }


        //public static int MaxSpieltag(int SaisonID)
        //{
        //    int iMaxSpieltag = 0;
        //    SqlConnection conn = new SqlConnection(connstring);
        //    conn.Open();

        //    SqlCommand command = new SqlCommand("SELECT Max([SpieltagNr] +0) AS MAXSPIELTAG FROM [Spieltage] WHERE Datum<GETDATE() and Saison = '" + SaisonID, conn);

        //    using (SqlDataReader reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            iMaxSpieltag = (int)reader["MAXSPIELTAG"];
        //        }
        //    }
        //    conn.Close();
        //    return iMaxSpieltag;
        //}
    }
}
