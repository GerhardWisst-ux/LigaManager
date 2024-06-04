using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Goals
    {
        public int GoalID { get; set; }
        public int? ScoreTeam1 { get; set; }
        public int? ScoreTeam2 { get; set; }
        public int? Matchminute { get; set; }
        public int? GoalGetterID { get; set; }
        public string GoalGetterName { get; set; }
        public bool IsPenalty { get; set; }
        public bool IsOwnGoal { get; set; }
        public bool IsOvertime { get; set; }
        public string Comment { get; set; }

    }
}