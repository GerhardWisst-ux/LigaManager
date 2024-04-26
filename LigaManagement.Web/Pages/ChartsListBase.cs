using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class ChartsListBase : ComponentBase
    {
        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        public List<DisplaySpieltag> SpieltagList;

        public List<DisplayChartVerein> VereineList = new List<DisplayChartVerein>();
        public IEnumerable<Verein> Vereine { get; set; }

        public string multiDimensionalArray2 = "[1, 3], [2, 3], [3, 6], [4, 9], [5, 12], [6, 15], [7, 18], [8, 21],[9, 24], [10, 24], [11, 24], [12, 27], [13, 30], [14, 31],[15, 31], [16, 34], [17, 34], [18, 34], [19, 37], [20, 40],[21, 43], [22, 46], [23, 47], [24, 50], [25, 53], [26, 56],[27, 57], [28, 60], [29, 63], [30, 63], [31, 63], [32, 64],[33, 67], [34, 68]";

        protected string DisplayErrorSaison = "none";
        protected string DisplayErrorVerein = "none";

        public int ChartVereinNr;
        public Int32 currentspieltag;
        public int VereinNr;
        public int saisonId;
        SqlConnection conn;
        public List<DisplaySaison> SaisonenList;
        public IEnumerable<Saison> Saisonen { get; set; }

        public List<ChartData> chartDataList = new List<ChartData>();

        protected override async Task OnInitializedAsync()
        {
            Saisonen = (await SaisonenService.GetSaisonen()).ToList();
            SaisonenList = new List<DisplaySaison>();

            var _saison = await SaisonenService.GetSaison(Globals.SaisonID);

            saisonId = _saison.SaisonID;

            for (int i = 0; i < Saisonen.Count(); i++)
            {
                var columns = Saisonen.ElementAt(i);
                SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
            }

            var vereineSaison = await VereineSaisonService.GetVereineSaison();
            List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == _saison.SaisonID).ToList();

            for (int i = 0; i < verList.Count(); i++)
            {
                var verein = await VereineService.GetVerein(verList[i].VereinNr);
                VereineList.Add(new DisplayChartVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1));
            }

            DisplayErrorSaison = "none";
            DisplayErrorVerein = "none";
            StateHasChanged();
        }


        public async Task SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                saisonId = Convert.ToInt32(e.Value);

                var vereineSaison = await VereineSaisonService.GetVereineSaison();
                List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == saisonId).ToList();

                VereineList.Clear();
                for (int i = 0; i < verList.Count(); i++)
                {
                    var verein = await VereineService.GetVerein(verList[i].VereinNr);
                    VereineList.Add(new DisplayChartVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1));
                }

                if (saisonId > 0)
                    DisplayErrorSaison = "block";

                StateHasChanged();
            }
        }

        public async void VereinChange(ChangeEventArgs e)
        {
            if (e.Value != null)            {               

                ChartVereinNr = Convert.ToInt32(e.Value);

                StateHasChanged();
            }
        }

        public async void OnClickHandler()
        {
            if (saisonId == 0)
            {
                DisplayErrorSaison = "block";
                return;
            }

            if (ChartVereinNr == 0)
            {
                DisplayErrorVerein = "block";
                return;
            }

            currentspieltag = Globals.Spieltag;
            Vereine = await VereineService.GetVereine();
            List<int?> cd = await TabelleService.CreateChart(SpieltagService, Vereine, ChartVereinNr, currentspieltag);

            ChartData chartData = new ChartData();

            bool ret = chartData.InsertChartData(cd, ChartVereinNr);

            if (ret)
            {
                chartDataList = ChartData(ChartVereinNr);

                StateHasChanged();
            }
            
        }

        private List<ChartData> ChartData(int vereinnr)
        {
            conn = new SqlConnection(Globals.connstring);

            List<ChartData> chartDataList = new List<ChartData>();
            ChartData chartData = new ChartData();

            chartDataList = chartData.GetChartData(vereinnr);
            return chartDataList;
        }

        public class DisplayChartVerein
        {
            public DisplayChartVerein(string vereinID, string vereinname)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }
        }
    }

}

