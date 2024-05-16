using System;
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

        public string Doppelpunkt  { get; set; }

    [Required(ErrorMessage = "Tore 2 muß angegeben werden")]
        [Range(0, 100, ErrorMessage = "Tore 2 darf nicht größer als 100 sein.")]
        public int? Tore2_Nr { get; set; }

        [Required(ErrorMessage = "Spieldatum muß angegeben werden.")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Ort muß angegeben werden.")]

        public string Ort { get; set; }

        [Required(ErrorMessage = "Schiedrichter muß angegeben werden.")]
        public string Schiedrichter { get; set; }

        public bool Abgeschlossen { get; set; }

        [Required(ErrorMessage = "Zuschauer müssen angegeben werden.")]
        [Range(0, 150000, ErrorMessage = "Zuschauer müssen zwischen 0 und 150000 liegen.")]
        public int Zuschauer { get; set; }
    }  
}
