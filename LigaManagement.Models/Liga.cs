using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Liga
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Liganame { get; set; }

        public int Liganummer { get; set; }

        [Required]
        public string Verband { get; set; }

        public int LandID { get; set; }

        [Required]
        public DateTime Erstaustragung { get; set; }

        [Required]
        public int Saisonen { get; set; }

        public bool Aktiv { get; set; }
        public string Rekordspieler { get; set; }

        public int Spiele_Rekordspieler { get; set; }
    }
}
