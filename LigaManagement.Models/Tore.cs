using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Tore
    {
        public int Id { get; set; }

        public int? SpieltagId { get; set; }

        [Required]
        public string SpieltagNr { get; set; }

        [Required]
        public string Saison { get; set; }

        [Required]
        public int? SaisonID { get; set; }

        [Required]
        public int? LigaID { get; set; }

        [Required]
        public int Spielminute { get; set; }

        [Required]
        public int SpielerID { get; set; }               

        [Required]
        public string Spielstand { get; set; }
       
        public bool Eigentor { get; set; }

        public string Torart { get; set; }



    }

}
