using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Verein
    {
        public int Id { get; set; }
        
        [Required]
        public int VereinNr { get; set; }

        [Required(ErrorMessage = "Vereinsanme erforderlich.")]
        public string Vereinsname1 { get; set; }

        [Required(ErrorMessage = "Vereinsanme erforderlich.")]
        public string Vereinsname2 { get; set; }

        [Required(ErrorMessage = "Stadion erforderlich.")]
        public string Stadion { get; set; }

        public int? Fassungsvermoegen { get; set; }

        public string Erfolge { get; set; }

        public int Gegruendet { get; set; }
        
        public bool Pokal { get; set; }

        public bool Bundesliga { get; set; }

        public string Hyperlink { get; set; }

    }

    public class VereinAktSaison: Verein
    {
        public int SaisonID { get; set; }

        public int GroupID2024 { get; set; }

        public int GroupID2022 { get; set; }

        public int GroupID2020 { get; set; }

        public int GroupID2018 { get; set; }
        public int GroupID2016 { get; set; }
        public int GroupID2014 { get; set; }
        public int GroupID2012 { get; set; }
        public int GroupID2010 { get; set; }
        public int GroupID2008 { get; set; }
        public int GroupID2006 { get; set; }
        public int GroupID2004 { get; set; }
        public int GroupID2002 { get; set; }
        public int GroupID2000 { get; set; }
        public int GroupID1998 { get; set; }
        public int GroupID1996 { get; set; }
        public int GroupID1994 { get; set; }
        public int GroupID1992 { get; set; }
        public int GroupID1990 { get; set; }
        public int GroupID1988 { get; set; }
        public int GroupID1986 { get; set; }
        public int GroupID1984 { get; set; }
        public int GroupID1982 { get; set; }
        public int GroupID1980 { get; set; }
        public int GroupID1978 { get; set; }
        public int GroupID1976 { get; set; }
        public int GroupID1974 { get; set; }
        public int GroupID1972 { get; set; }
        public int GroupID1970 { get; set; }
        public int GroupID1966 { get; set; }
        public int GroupID1962 { get; set; }
        public int GroupID1958 { get; set; }
        public int GroupID1954 { get; set; }
        public int GroupID1950 { get; set; }
        public int GroupID1938 { get; set; }
        public int GroupID1934 { get; set; }
        public int GroupID1930 { get; set; }
    }

    public class VereinEMWM : Verein
    {
        public int GroupID2024 { get; set; }
        public int GroupID2022 { get; set; }
        public int GroupID2020 { get; set; }
        public int GroupID2018 { get; set; }
        public int GroupID2016 { get; set; }
        public int GroupID2014 { get; set; }
        public int GroupID2012 { get; set; }
        public int GroupID2010 { get; set; }
        public int GroupID2008 { get; set; }
        public int GroupID2006 { get; set; }
        public int GroupID2004 { get; set; }
        public int GroupID2002 { get; set; }
        public int GroupID2000 { get; set; }
        public int GroupID1998 { get; set; }
        public int GroupID1996 { get; set; }
        public int GroupID1994 { get; set; }
        public int GroupID1992 { get; set; }
        public int GroupID1990 { get; set; }
        public int GroupID1988 { get; set; }
        public int GroupID1986 { get; set; }
        public int GroupID1984 { get; set; }
        public int GroupID1982 { get; set; }
        public int GroupID1980 { get; set; }
        public int GroupID1978 { get; set; }
        public int GroupID1976 { get; set; }
        public int GroupID1974 { get; set; }
        public int GroupID1972 { get; set; }
        public int GroupID1970 { get; set; }
        public int GroupID1966 { get; set; }
        public int GroupID1962 { get; set; }
        public int GroupID1960 { get; set; }
        public int GroupID1958 { get; set; }
        public int GroupID1954 { get; set; }
        public int GroupID1950 { get; set; }
        public int GroupID1938 { get; set; }
        public int GroupID1934 { get; set; }
        public int GroupID1930 { get; set; }
    }
}
