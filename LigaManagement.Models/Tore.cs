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

        public int? SaisonID { get; set; }

        public int? LigaID { get; set; }

        public int? TorVerein1 { get; set; }
        public int? TorVerein2 { get; set; }

        public int? Spielminute { get; set; }

        public int? SpielerID { get; set; }
        


    }

}
