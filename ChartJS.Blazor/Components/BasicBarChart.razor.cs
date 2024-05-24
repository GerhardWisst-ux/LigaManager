using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChartJS.Blazor
{
    public partial class BasicBarChart
    {
        [Parameter]
        public ChartConfig Configs { get; set; }

        [Parameter]
        public Dictionary<string, double> Data { get; set; }


        [Inject]
        public ILogger<BasicBarChart> Logger { get; set; }
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public bool DisplayTitle { get; set; } = true;

        private bool _created = false;
        [Parameter]
        public bool Responsive { get; set; } = true;

        [Parameter]
        public string LegendPosition { get; set; } = ChartJsPositions.Top;


        [Parameter]
        public List<string> Colors { get; set; }

        public static string ChartId { get; set; } = Guid.NewGuid().ToString();

        public async Task InitChartAsync()
        {
            if (_created)
            {
                await ChartJSInterop.DestroyChart(ChartId);
            }
            if (Data is null && Data.Count == 0)
            {
                return;
            }

            if (Colors is null || Colors?.Count != Data?.Count)
            {
                Colors = string.IsNullOrEmpty(Color)
                  ? ChartJSColors.GetRandomEnumerable(Data.Count, 0.6f).ToList()
                  : ChartJSColors.GetEnumerableOfSame(Data.Count, Color).ToList();
            }
            var config = new ChartConfig();
            var dataSets = new List<ChartDataset>();
            for (int i = 0; i < Data.Count; i++)
            {
                dataSets.Add(new ChartDataset
                {
                    Data = new List<double> { (double)Data.Values.ToList()[i] },
                    Label = Data.Keys.ToList()[i],
                    BackgroundColor = new List<string> { Colors[i] },
                    BorderColor = new List<string> { Colors[i] }
                });
            }
            config.Data = new ChartData
            {
                Datasets = dataSets,
                Labels = new List<string> { Title },

            };
            config.Type = ChartJSTypes.Bar;
            config.Options = new ChartOptions
            {
                Responsive = Responsive,
                Plugins = new ChartPlugin
                {
                    Legend = new ChartLegend
                    {
                        Position = LegendPosition
                    }, 
                    Title = new ChartTitle
                    {
                        Display = DisplayTitle,
                        Text = Title
                    }
                }

            };

           

            _created =  await ChartJSInterop.CreateChart(ChartId, config);
        }
        protected override async Task OnInitializedAsync()
        {
            await InitChartAsync();
        }

    }
}
