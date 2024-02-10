using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagerManagement.Models
{
    public class Spieler
    {
        [Key]
        public int Id { get; set; }

        public int SaisonId { get; set; }

        [Required(ErrorMessage = "Rückennummer erforderlich.")]
        public string Rueckennummer { get; set; }

        public int LigaID { get; set; }

        [Required(ErrorMessage = "Name erforderlich.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vorname erforderlich.")]
        public string Vorname { get; set; }

        [Required]
        public int VereinID { get; set; }

        [Required(ErrorMessage = "Geburtsdatum erforderlich.")]
        public DateTime Geburtsdatum { get; set; }

        public int Tore { get; set; }
        
        int Vorlagen { get; set; }

        public string GelbeKarten { get; set; }

        public string RoteKarten { get; set; }

        [Required(ErrorMessage = "Im Verein seit erforderlich.")]
        public DateTime ImVereinSeit { get; set; }

        public string Aktiv { get; set; }




    }
}
