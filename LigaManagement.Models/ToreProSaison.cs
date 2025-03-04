using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class ToreProSaison
    {
        public int Id { get; set; }

        [Required]
        public string Saison { get; set; }

        [Required]
        public int? AnzahlTore { get; set; }

        [Required]
        public int? AnzahlSpiele { get; set; }               
        
        [Required]
        public double? ToreAVG { get; set; }
     
    }

}
