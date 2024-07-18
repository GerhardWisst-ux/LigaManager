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
        public static string connstring = "server=PC-WISST\\SQLEXPRESS;database=LigaDB;User='IIS APPPOOL\\Ligamanager';Password='';Trusted_Connection=true;TrustServerCertificate=true";

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

        public enum UserGroup
        {
            Admin = 1,
            User = 2,
            Gast = 3,
        }
        public static int iUserGroup;


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

        public static string CurrentRole = "USER";

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

        public string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }
        public static int GetAgeFromDate(DateTime geburtstag)
        {
            int age = DateTime.Now.Year - geburtstag.Year;
            geburtstag = geburtstag.AddYears(age);
            if (DateTime.Now.CompareTo(geburtstag) < 0) { age--; }
            return age;
        }
    }
}
