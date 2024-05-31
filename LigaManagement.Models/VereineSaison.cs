using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class VereineSaison
    {
        public int Id { get; set; }

        [Required]
        public int VereinNr { get; set; }

        [Required]
        public int SaisonID { get; set; }

        [Required]
        public int LigaID { get; set; }

        [Required]
        public int LandID { get; set; }

    }

    public class VereineSaisonAus
    {
        public int Id { get; set; }

        [Required]
        public int VereinNr { get; set; }

        [Required]
        public int SaisonID { get; set; }

        [Required]
        public int LigaID { get; set; }

    }
}