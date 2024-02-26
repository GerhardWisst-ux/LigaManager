using System.ComponentModel.DataAnnotations;

namespace LigaManagerManagement.Models
{
    public class SpielerSpieltag
    {
        [Key]
        public int Id { get; set; }

        public int KaderId { get; set; }

        public int SaisonId { get; set; }

        public int SpieltagId { get; set; }               

        public int Einsatz { get; set; }

        public int Spielminuten { get; set; }

        public int Tore { get; set; }

        public bool Eingewechselt { get; set; }

        public int EingewechseltMin { get; set; }

        public bool Ausgewechselt { get; set; }

        public int AusgewechseltMin { get; set; }

        public bool GelbeKarten { get; set; }

        public bool RoteKarten { get; set; }

    }
}
