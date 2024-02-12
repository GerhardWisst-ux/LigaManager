using System.Collections.Generic;

namespace Ligamanager.Components
{
    public class Globals
    {
        public static string connstring = "Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True";

        public static string currentSaison;
        public static string currentLiga;
        public static int Spieltag;
        public static int SaisonID;
        public static int maxSpieltag;
        public static bool bVisibleNavMenuElements = false;

        public static Dictionary<string, string> VereinAktSaison = new Dictionary<string, string>();

        public enum Tabart
        {
            Gesamt = 1,
            Heim = 2,
            Auswärts = 3,
            Vorrunde = 4,
            Rückrunde = 5,
            EwigeTabelle = 6
        }        
    }
}
