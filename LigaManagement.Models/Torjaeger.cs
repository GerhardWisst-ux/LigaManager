using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Torjaeger
    {
        public int Id { get; set; }

        [Required]
        public string Saison { get; set; }

        [Required]
        public int? SaisonID { get; set; }

        [Required]
        public string LigaID { get; set; }

        [Required]
        public string Spielername { get; set; }

        [Required]
        public string Vereinsname { get; set; }

        [Required]
        public int? Tore { get; set; }

    }

}
