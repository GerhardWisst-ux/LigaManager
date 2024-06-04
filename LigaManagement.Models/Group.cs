using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Models
{
    public class Group
    {
        public int GroupOrderID { get; set; }
        public string GroupName { get; set; }
        public string GroupID { get; set; }
    }
}