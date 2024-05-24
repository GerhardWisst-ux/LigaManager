using System.Collections.Generic;

namespace ChartJS.Blazor
{
    public class ChartDataset
    {
        public string Label { get; set; }
        public List<double> Data { get; set; }
        public List<string> BackgroundColor { get; set; }

        public List<string> BorderColor { get; set; } 
        public double? BorderWidth { get; set; } = 1;

        public bool? Fill { get; set; } 
        public bool? Stepped { get; set; } 
    }
}
