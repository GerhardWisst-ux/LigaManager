using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Spielergebnisse:Spieltag
    {      

        public int Gewonnen { get; set; }

        public int Untentschieden { get; set; }

        public int Verloren { get; set; }
    }
}
