using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Stadion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Verein ist erforderlich.")]
        public int VereinNr { get; set; }

        [Required(ErrorMessage = "Stadionname ist erforderlich.")]        
        public string Stadionname { get; set; }
        [Required(ErrorMessage = "Ort ist erforderlich.")]
        public string Ort { get; set; }

        [Required(ErrorMessage = "Kapazität ist erforderlich.")]
        [Range(1, 100000, ErrorMessage = "Die Kapazität muss zwischen 1 und 100000 liegen.")]
        public int Kapazitaet { get; set; }

        [Range(1, 100000, ErrorMessage = "Das Jahr Von muß zwischen 1900 und 2050 liegen.")]
        public int JahrVon { get; set; }

        [Range(1, 100000, ErrorMessage = "Das Jahr Bis muß zwischen 1900 und 2050 liegen.")]
        public int JahrBis { get; set; }
        public DateTime JahrVonDate { get; set; }
        public DateTime JahrBisDate { get; set; }
    }
}