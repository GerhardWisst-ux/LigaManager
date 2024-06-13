using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Einstellungen
    {
               
        public int Id { get; set; }

        public string Sprache_LandKZ { get; set; }

        public bool ImportVisible { get; set; }

        public bool TabellenAnlegenVisible { get; set; }

    }
}
