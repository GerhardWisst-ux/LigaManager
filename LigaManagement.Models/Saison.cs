using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagerManagement.Models
{
    public class Saison
    {
        [Key]
        public int SaisonID { get; set; }

        [Required(ErrorMessage = "Feld muß angegeben werden")]
        public string Saisonname { get; set; }

        [Required]
        public int LigaID { get; set; }
                
        public int LandID { get; set; }

        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public int AnzahlVereine { get; set; }

        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public string Liganame { get; set; }
        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public bool Aktuell { get; set; }
        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public bool Abgeschlossen { get; set; }
        public int Ligahoehe { get; set; }
        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public int Aufsteiger { get; set; }
        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public int Absteiger { get; set; }
        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public int Relegation { get; set; }
        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public int CL_League { get; set; }
        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public int CF_League { get; set; }
        [Required(ErrorMessage = "Feld muß numerisch sein")]
        public int EL_League { get; set; }

        [Required]
        public bool SpielplanVorhanden { get; set; }
    }
}
