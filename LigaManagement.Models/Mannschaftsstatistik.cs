namespace LigaManagerManagement.Web.Pages
{
    public class Mannschaftsstatistik
    {        
        public string VereinNr { get; set; }
        public string StatText { get; set; }
        public string Gesamt { get; set; }
        public string Heim { get; set; }
        public string Auswaerts { get; set; }
    }

    public class SerienStatistik
    {
        public string Verein { get; set; }
        public string StatText { get; set; }
        public string SaisonText { get; set; }
        public string AnzahlText { get; set; }

    }
}