using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagerManagement.Models
{
    public class Kader
    {
        [Key]
        public int Id { get; set; }

        public int SaisonId { get; set; }
        public int LigaID { get; set; }
        public int LandID { get; set; }

        [Required(ErrorMessage = "Rückennummer erforderlich.")]
        [Range(1, 99, ErrorMessage = "Rückennummer muß zwischen 1 und 99 liegen.")]
        public int? Rueckennummer { get; set; }

        [Required(ErrorMessage = "Name erforderlich.")]
        public string SpielerName { get; set; }

        [Required(ErrorMessage = "Vorname erforderlich.")]
        public string Vorname { get; set; }
                

        [Required(ErrorMessage = "Geburtsdatum erforderlich.")]
        public DateTime? Geburtsdatum { get; set; }

        public int Alter { get; set; }

        public string Position { get; set; }

        [Required(ErrorMessage = "Position erforderlich.")]
        public int? PositionsNr { get; set; }               
              
        [Required]
        public int VereinID { get; set; }             

        public int Einsaetze { get; set; }

        public int Spielminuten { get; set; }              

        public int Tore { get; set; }

        public decimal Abloesesumme { get; set; }
        
        int Vorlagen { get; set; }

        [Required(ErrorMessage = "Gültiges Datum erforderlich.")]
        public DateTime? ImVereinSeit { get; set; }

        public bool Aktiv { get; set; }

        public Object Image { get; set; }        

    }
}
