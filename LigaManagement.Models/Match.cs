using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamIconUrl { get; set; }
    }
}