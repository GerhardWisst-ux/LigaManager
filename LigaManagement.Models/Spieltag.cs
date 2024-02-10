using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Spieltag
    {
        public int SpieltagId { get; set; }

        [Required]
        public string SpieltagNr { get; set; }

        [Required]
        public string Saison { get; set; }

        public int SaisonID { get; set; }

        public int LigaID { get; set; }

        [Required]
        public string Verein1_Nr { get; set; }

        [Required]
        public string Verein1 { get; set; }

        [Required]
        public string Verein2_Nr { get; set; }
        
        [Required]
        public string Verein2 { get; set; }

        [Required]
        public int Tore1_Nr { get; set; }

        [Required]
        public int Tore2_Nr { get; set; }

        [Required]
        public DateTime Datum { get; set; }

        [Required]
        public string Ort { get; set; }

        public string Schiedrichter { get; set; }

        public bool Abgeschlossen { get; set; }
                
        public int Zuschauer { get; set; }        
    }
}
