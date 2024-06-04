using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LigaManagement.Models
{
    public class MatchDetail
    {
        public int MatchID { get; set; }
        public int LeagueSeason { get; set; }
        public DateTime MatchDateTime { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public string LeagueName { get; set; }
        public Group Group { get; set; }
        public List<MatchResults> matchResults { get; set; }
        public List<Goals> goals { get; set; }
    }
}