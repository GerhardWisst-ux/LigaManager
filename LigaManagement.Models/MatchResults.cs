using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class MatchResults
    {
        public int ResultID { get; set; }
        public string ResultName { get; set; }
        public int PointsTeam1 { get; set; }
        public int PointsTeam2 { get; set; }
        public string LeagueName { get; set; }
        public string ResultDescription { get; set; }        
    }
}