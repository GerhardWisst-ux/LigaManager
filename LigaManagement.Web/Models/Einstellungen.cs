using System;

namespace LigaManagement.Web.Models
{
    public class EinstellungenModel    {
        public string Sprache_LandKZ { get; set; }
                
        public bool ImportVisible { get; set; }

        public bool TabellenAnlegenVisible { get; set; }
        public bool Spielverlauf { get; set; }
        public bool Aufstellungen { get; set; }
    }
}
