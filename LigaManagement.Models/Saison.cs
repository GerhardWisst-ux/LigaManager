using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagerManagement.Models
{
    public class Saison
    {
        [Key]
        public int SaisonID { get; set; }

        [Required]
        public string Saisonname { get; set; }

        [Required]
        public int LigaID { get; set; }
                
        public int LandID { get; set; }

        public int AnzahlVereine { get; set; }

        [Required]
        public string Liganame { get; set; }

        [Required]
        public bool Aktuell { get; set; }

        [Required]
        public bool Abgeschlossen { get; set; }
    }
}
