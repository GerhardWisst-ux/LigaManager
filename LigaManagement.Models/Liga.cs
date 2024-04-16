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

        [Required]
        public string Verband { get; set; }

        [Required]
        public DateTime Erstaustragung { get; set; }

        [Required]
        public int Absteiger { get; set; }              

        public bool Aktiv { get; set; }


    }
}
