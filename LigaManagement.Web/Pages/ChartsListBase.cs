using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class ChartsListBase : ComponentBase
    {
        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public List<DisplaySpieltag> SpieltagList;

        public int VereinNr;
        public string saison;
        SqlConnection conn;
        public List<DisplaySaison> SaisonenList;
        public IEnumerable<Saison> Saisonen { get; set; }

        public List<ChartData> chartDataList = new List<ChartData>();

        protected override async Task OnInitializedAsync()
        {
            Saisonen = (await SaisonenService.GetSaisonen()).ToList();
            SaisonenList = new List<DisplaySaison>();

            var _saison = await SaisonenService.GetSaison(Globals.SaisonID);

            saison = _saison.Saisonname;

            for (int i = 0; i < Saisonen.Count(); i++)
            {
                var columns = Saisonen.ElementAt(i);
                SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
            }

            chartDataList = ChartData();
            StateHasChanged();
        }
        
        private List<ChartData> ChartData()
        {
            conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");

            List<ChartData> chartDataList = new List<ChartData>();
            ChartData chartData = new ChartData();

            chartDataList = chartData.GetChartData(16);           
            return chartDataList;
            
        }

        public async Task SaisonChange(ChangeEventArgs e)
        {
            int iSpieltage;

            if (e.Value != null)
            {
                saison = e.Value.ToString();
                // Globals.currentSaison = saison;

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();                

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                StateHasChanged();
            }
        }
    }
}

