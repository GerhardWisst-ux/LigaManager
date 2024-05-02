using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class VereinAUS
    {
        public int Id { get; set; }
        
        [Required]
        public int VereinNr { get; set; }

        [Required(ErrorMessage = "Vereinsanme erforderlich.")]
        public string Vereinsname1 { get; set; }

        [Required(ErrorMessage = "Vereinsanme erforderlich.")]
        public string Vereinsname2 { get; set; }

        [Required(ErrorMessage = "Stadion erforderlich.")]
        public string Stadion { get; set; }

        public string Fassungsvermoegen { get; set; }

        public string Erfolge { get; set; }

        public int Gegruendet { get; set; }
        
        public bool Pokal { get; set; }

        public bool Liga1 { get; set; }
        public bool Liga2 { get; set; }

    }

    public class VereinAktSaisonAUS: VereinAUS
    {
        public int SaisonID { get; set; }

       


    }
}
