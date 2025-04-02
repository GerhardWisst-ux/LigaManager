using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class InfoText
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titel ist erforderlich.")]
        public string Title { get; set; }
                
        [Required(ErrorMessage = "Inhalt ist erforderlich.")]
        public string NewsContent { get; set; }        
        public DateTime PublishedAt { get; set; }
        public DateTime ChangedAt { get; set; }
        public int VereinID { get; set; }
        public string Vereinsname { get; set; }
        public int SaisonID { get; set; }
        public int LigaID { get; set; }
    }
}