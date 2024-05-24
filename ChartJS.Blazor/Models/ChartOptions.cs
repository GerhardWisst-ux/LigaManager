namespace ChartJS.Blazor
{
    public class ChartOptions
    {
        public char? IndexAxis  { get; set; }
        public bool? Responsive { get; set; }
        public ChartPlugin Plugins { get; set; }
        public ChartElements Elements { get; set; }
        public ChartScales Scales { get; set; }
    }
}
