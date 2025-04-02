using System;

namespace LigaManagement.Models
{
    public class Infotexte
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string NewContent { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }
    }
}
