using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Land
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Laendername { get; set; }              

        public string Code { get; set; }

        public bool Aktiv { get; set; }

    }
}
