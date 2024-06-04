using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LigaManagement.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        public DateTime MatchDateTime { get; set; }

        public Team Team1 { get; set; }
        public Team Team2 { get; set; }

        public string LeagueName { get; set; }
        public Group Group { get; set; }

        //public MatchResults MatchResults { get; set; }
    }
}