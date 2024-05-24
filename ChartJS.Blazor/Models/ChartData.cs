using System.Collections.Generic;

namespace ChartJS.Blazor
{
    public class ChartData
    {
        public List<string> Labels { get; set; }

        public List<ChartDataset> Datasets { get; set; }
    }
}
