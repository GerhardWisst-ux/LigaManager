using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Spieltag
    {
        public int? SpieltagId { get; set; }

        [Required]
        public string SpieltagNr { get; set; }

        [Required]
        public string Saison { get; set; }

        public int? SaisonID { get; set; }

        public int? LigaID { get; set; }

        [Required]
        public string Verein1_Nr { get; set; }

        [Required(ErrorMessage = "Verein 1 muß angegeben werden.")]
        public string Verein1 { get; set; }

        public string Verein1Anzeige { get; set; }

        [Required]
        public string Verein2_Nr { get; set; }

        [Required(ErrorMessage = "Verein 2 muß angegeben werden.")]
        public string Verein2 { get; set; }

        public string Verein2Anzeige { get; set; }

        [Required(ErrorMessage = "Tore 1 muß angegeben werden.")]
        [Range(0, 100, ErrorMessage = "Tore 1 darf nicht größer als 100 sein.")]
        public int? Tore1_Nr { get; set; }

        public string Doppelpunkt { get; set; }

        [Required(ErrorMessage = "Tore 2 muß angegeben werden")]
        [Range(0, 100, ErrorMessage = "Tore 2 darf nicht größer als 100 sein.")]
        public int? Tore2_Nr { get; set; }

        [Required(ErrorMessage = "Spieldatum muß angegeben werden.")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Ort muß angegeben werden.")]

        public string Ort { get; set; }

        [Required(ErrorMessage = "Schiedrichter muß angegeben werden.")]
        public string Schiedsrichter { get; set; }

        public bool Abgeschlossen { get; set; }

        [Required(ErrorMessage = "Zuschauer müssen angegeben werden.")]
        [Range(0, 150000, ErrorMessage = "Zuschauer müssen zwischen 0 und 150000 liegen.")]
        public int Zuschauer { get; set; }

        public string TeamIconUrl1 { get; set; }
        public string TeamIconUrl2 { get; set; }
        public int? StadionID { get; set; }

        public int AnzahlSiege { get; set; } // für Serien
        public int AnzahlNiederlagen { get; set; } // für Serien
        public int AnzahlUnentschieden { get; set; } // für Serien
    }

    public class PokalergebnisCL_EM_WMSpieltag : PokalergebnisSpieltag
    {
        public int? LigaID { get; set; }
        public int? Land1_Nr { get; set; }
        public int? Land2_Nr { get; set; }
        public int? GroupID { get; set; }
        public string Gruppe { get; set; }
        public string RundeDetail { get; set; }
        public string TeamIconUrl1 { get; set; }
        public string TeamIconUrl2 { get; set; }
        public string FontWeight1 { get; set; }
        public string FontWeight2 { get; set; }
    }

    public class Spielplan : Spieltag
    {
        public string DatumString { get; set; }
    }

    public class SpieltageSerien
    {
        public string Saison { get; set; }
        public int VereinID { get; set; }
        public int StartSpieltag { get; set; }
        public int EndeSpieltag { get; set; }
        public int AnzahlSiege { get; set; }
        public int AnzahlNiederlagen { get; set; }

        public int AnzahlUnentschieden { get; set; }

        public string SpieltagIDs { get; set; }

        public List<Spieltag> Spieltage { get; set; }
    }

    public class SpieltageSerieGruppe
    {
        public string Saison { get; set; }
        public int AnzahlSiege { get; set; }

        public int AnzahlNiederlagen { get; set; }  

        public List<Spieltag> Spieltage { get; set; }
        public string AnzeigenameSiege => $"{Saison} – {AnzahlSiege} Siege";

        public string AnzeigenameUnentschieden => $"{Saison} – {AnzeigenameUnentschieden} Unentschieden";

        public string AnzeigenameNiederlagen => $"{Saison} – {AnzahlNiederlagen} Niederlagen";
    }

    public class SpieltageSerieGruppeNL
    {
        public string Saison { get; set; }        
        public int AnzahlNiederlagen { get; set; }

        public List<Spieltag> Spieltage { get; set; }

        public string AnzeigenameNiederlagen => $"{Saison} – {AnzahlNiederlagen} Niederlagen";
    }

    public class SpieltageSerieGruppeUS
    {
        public string Saison { get; set; }
        public int AnzahlUntentschieden { get; set; }

        public List<Spieltag> Spieltage { get; set; }

        public string AnzeigenameUnentschieden => $"{Saison} – {AnzahlUntentschieden} Unentschieden";

    }
}

    
