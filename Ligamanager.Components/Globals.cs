using LigaManagement.Web.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ligamanager.Components
{
    public class Globals
    {
        public static string currentLigaUrl;
        public static string connstring = "Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true;";

        public static string currentLand;
        public static string currentLiga;
        public static string currentSaison;
        public static string currentLiganame;

        public static string currentPokalSaison;
        public static string currentPokalRunde;
        public static string currentCLSaison;
        public static string currentEMWMSaison;
        public static string currentClRunde;
        public static string currentEMWMRunde;
        public static int Spieltag;
        public static int LandID;
        public static int LandIDVerein;
        public static int LigaID;
        public static int EMMWMLigaId;
        public static int LigaNummer;
        public static int SaisonID;

        public static int KaderSaisonID;
        public static int CLSaisonID;
        public static int CLPokalSaisonID;
        public static int EMWMSaisonID;
        public static int KaderVereinNr;
        public static int maxSpieltag;
        public static bool bVisibleNavMenuElements;

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
