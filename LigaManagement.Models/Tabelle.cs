using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Tabelle
    {      
        public int? Id { get; set; }

        [Required]
        public int? VereinNr { get; set; }

        [Required]
        public string Verein { get; set; }

        [Required]
        public int? Tab_Sai_Id { get; set; }

        [Required]
        public string Liga { get; set; }

        public string Tab_Lig_Id { get; set; }

        [Required]
        public int? Platz { get; set; }

        [Required]
        public int? Spiele { get; set; }

        [Required]
        public int? Punkte { get; set; }

        [Required]
        public int? Gewonnen { get; set; }

        [Required]
        public int? Untentschieden { get; set; }

        [Required]
        public int? Verloren { get; set; }

        [Required]
        public int? TorePlus { get; set; }

        [Required]
        public int? ToreMinus { get; set; }

        public string Tore { get; set; }

        public int? Diff { get; set; }
    }
}
