﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Pokalergebnisse : Spieltag
    {      

        public int Gewonnen { get; set; }

        public int Untentschieden { get; set; }

        public int Verloren { get; set; }
    }
}
