using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class EinstellungenLM
    {               
        public int Id { get; set; }
        public string Sprache_LandKZ { get; set; }
        public bool ImportVisible { get; set; }
        public bool TabellenAnlegenVisible { get; set; }
        public bool Aufstellungen { get; set; }
        public bool Spielverlauf { get; set; }
        public int SaisonIDVon { get; set; }
        public int SaisonIDNach { get; set; }

    }
}
