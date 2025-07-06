using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class PokalergebnisSpieltag
    {
        public int? SpieltagId { get; set; }

        [Required]
        public int? SaisonID { get; set; }

        public string Saison { get; set; }
               
        [Required]
        public int Verein1_Nr { get; set; }

        [Required(ErrorMessage = "Verein 1 muß angegeben werden")]
        public string Verein1 { get; set; }

        [Required]
        public int Verein2_Nr { get; set; }

        [Required(ErrorMessage = "Verein 2 muß angegeben werden")]
        public string Verein2 { get; set; }

        [Required(ErrorMessage = "Tore 1 muß angegeben werden")]
        [Range(0, 100, ErrorMessage = "Tore 1 darf nicht größer als 100 sein")]
        public int? Tore1_Nr { get; set; }

        public string Doppelpunkt { get; set; }

        [Required(ErrorMessage = "Tore 2 muß angegeben werden")]
        [Range(0, 100, ErrorMessage = "Tore 2 darf nicht größer als 100 sein")]
        public int? Tore2_Nr { get; set; }

        [Required(ErrorMessage = "Spieldatum muß angegeben werden")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Ort muß angegeben werden")]

        public string Ort { get; set; }

        public string Schiedrichter { get; set; }
        
        public bool Verlängerung { get; set; }

        public bool Elfmeterschiessen { get; set; }

        public int? Zuschauer { get; set; }
    
        public string Runde { get; set; }              
    }

    public class PokalergebnisStatistik
    {

        public int Platz { get; set; }

        [Required(ErrorMessage = "Verein muß angegeben werden")]
        public string Verein { get; set; }

        [Required(ErrorMessage = "Anzahl muß angegeben werden")]
        public int Siege { get; set; }

        public int Finalteilnahmen { get; set; }

        public double Siegquote { get; set; }

    }

    public class PokalHistorieStatistik
    {
        public string Saison { get; set; }

        public string ErreichteRunde { get; set; }

        public string Gegner { get; set; }

        public string Ergebnis { get; set; }

    }
}
