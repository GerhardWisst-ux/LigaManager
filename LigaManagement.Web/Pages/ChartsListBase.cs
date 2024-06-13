using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class ChartsListBase : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

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

        [Inject]
        public NavigationManager uriHelper { get; set; }

        public List<DisplaySpieltag> SpieltagList;

        public List<DisplayChartVerein> VereineList = new List<DisplayChartVerein>();
        public IEnumerable<Verein> Vereine { get; set; }

        public string arrPunkteJson;
        public int SaisonID;
        public int VereinID;

        public string arrSpielePunkte;
        public string Vereinsname;
        public double value = 0;

        public bool ProgressVisible = false;
        public string arrSpieltage = "[" + 1 + ", " + 2 + ", " + 3 + ", " + 4 + ", " + 5 + ", " + 6 + ", " + 7 + ", " + 8 + ", " + 9 + ", " + 10 + ", " + 11 + ", " + 12 + ", " + 13 + ", " + 14 + ", " + 15 + ", " + 16 + ", " + 17 + ", " + 18 + ", " + 19 + ", " + 20 + ", " + 21 + ", " + 22 + ", " + 23 + ", " + 24 + ", " + 25 + ", " + 26 + ", " + 27 + ", " + 28 + ", " + 29 + ", " + 30 + ", " + 31 + ", " + 32 + ", " + 33 + ", " + 34 + "]";
        public string arrPunkte = "[" + 3 + ", " + 3 + ", " + 3 + ", " + 4 + ", " + 5 + ", " + 6 + ", " + 7 + ", " + 8 + ", " + 9 + ", " + 10 + ", " + 11 + ", " + 12 + ", " + 13 + ", " + 14 + ", " + 15 + ", " + 16 + ", " + 17 + ", " + 18 + ", " + 19 + ", " + 20 + ", " + 21 + ", " + 22 + ", " + 23 + ", " + 24 + ", " + 25 + ", " + 26 + ", " + 27 + ", " + 28 + ", " + 29 + ", " + 30 + ", " + 31 + ", " + 32 + ", " + 33 + ", " + 34 + "]";

        protected string DisplayErrorSaison = "none";
        protected string DisplayErrorVerein = "none";
        protected string DisplayErrorChartArt = "none";

        public string arrSpieltageJS;
        public string arrPunkteJS;
        public int ChartVereinNr;
        public int ChartArt;
        public Int32 currentspieltag;
        public int VereinNr;
        public int saisonId;

        public static bool bLoad = false;
        SqlConnection conn;
        public List<DisplaySaison> SaisonenList;
        public IEnumerable<Saison> Saisonen { get; set; }

        public List<ChartData> chartDataList = new List<ChartData>();

        protected override async Task OnInitializedAsync()
        {
            Saisonen = (await SaisonenService.GetSaisonen()).ToList().Where(x => x.LigaID == Globals.LigaID);
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
            DisplayErrorChartArt = "none";

            ChartArt = 0;
            saisonId = Globals.SaisonID;

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

        public async void ChartArtChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                ChartArt = Convert.ToInt32(e.Value);

                StateHasChanged();
            }
        }


        public async void VereinChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {

                ChartVereinNr = Convert.ToInt32(e.Value);
                var verein = await VereineService.GetVerein(ChartVereinNr);
                Vereinsname = verein.Vereinsname2;
                VereinID = Convert.ToInt32(e.Value);

                StateHasChanged();
            }
        }

        public async void OnClickHandler()
        {
            if (saisonId == 0)
                DisplayErrorSaison = "block";
            else
                DisplayErrorSaison = "none";

            if (ChartVereinNr == 0)
                DisplayErrorVerein = "block";
            else
                DisplayErrorVerein = "none";

            if (ChartArt == 0)
                DisplayErrorChartArt = "block";
            else
                DisplayErrorChartArt = "none";

            if (saisonId == 0 || ChartVereinNr == 0 || ChartArt == 0)
            {
                return;
            }

            currentspieltag = Globals.Spieltag;
            Vereine = await VereineService.GetVereine();
            List<int?> cd = await TabelleService.CreateChart(SpieltagService, Vereine, ChartVereinNr, currentspieltag);

            ChartData chartData = new ChartData();

            bool ret = chartData.InsertChartDataPunkte(cd, ChartVereinNr);

            if (ret)
            {
                chartDataList = ChartData(ChartVereinNr);

                arrSpielePunkte = string.Empty;
                arrPunkte = string.Empty;

                arrSpieltage = string.Empty;

                SpieltageRepository rep = new SpieltageRepository();

                bool bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

                value = 20;

                int iAktSpieltag;
                if (bAbgeschlossen)
                {
                    iAktSpieltag = Globals.maxSpieltag;
                }
                else
                {
                    iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                }


                for (int i = 1; i <= iAktSpieltag; i++)
                {
                    arrSpieltage += String.Concat("[", (i + 1).ToString(), "] ", ", ");
                }

                for (int i = 0; i < cd.Count; i++)
                {
                    // arrSpielePunkte += String.Concat("[", (i + 1).ToString(), ", ", String.Concat(cd[i].Value), "] ", ", ");

                    arrPunkte += String.Concat("[", String.Concat(cd[i].Value).ToString(), "] ", ", ");
                }
                value = 40;

                //        arrSpieltage = "[" + 1 + ", " + 2 + ", " + 3 + ", " + 4 + ", " + 5 + ", " + 6 + ", " + 7 + ", " + 8 + ", " + 9 + ", " + 10 + ", " + 11 + ", " + 12 + ", " + 13 + ", " + 14 + ", " + 15 + ", " + 16 + ", " + 17 + ", " + 18 + ", " + 19 + ", " + 20 + ", " + 21 + ", " + 22 + ", " + 23 + ", " + 24 + ", " + 25 + ", " + 26 + ", " + 27 + ", " + 28 + ", " + 29 + ", " + 30 + ", " + 31 + ", " + 32 + ", " + 33 + ", " + 34 + "]";
                //public string arrPunkte = "[" + 3 + ", " + 3 + ", " + 3 + ", " + 4 + ", " + 5 + ", " + 6 + ", " + 7 + ", " + 8 + ", " + 9 + ", " + 10 + ", " + 11 + ", " + 12 + ", " + 13 + ", " + 14 + ", " + 15 + ", " + 16 + ", " + 17 + ", " + 18 + ", " + 19 + ", " + 20 + ", " + 21 + ", " + 22 + ", " + 23 + ", " + 24 + ", " + 25 + ", " + 26 + ", " + 27 + ", " + 28 + ", " + 29 + ", " + 30 + ", " + 31 + ", " + 32 + ", " + 33 + ", " + 34 + "]";
                arrSpieltage = arrSpieltage.Trim().Remove(arrSpieltage.Length - 2);
                arrPunkte = arrPunkte.Trim().Remove(arrPunkte.Length - 2);

                var opt = new JsonSerializerOptions() { WriteIndented = true };
                arrSpieltageJS = JsonSerializer.Serialize<string>(arrSpieltage, opt);
                arrPunkteJson = JsonSerializer.Serialize<string>(arrPunkte, opt);

                value = 60;
                //ShowAlertWindow();
                //InitArray();
                if (bLoad == false)
                {
                    var timer = new Timer(new TimerCallback(_ =>
                    {
                        uriHelper.NavigateTo(uriHelper.Uri, forceLoad: true);
                        bLoad = true;
                    }), null, 100, 1000);
                }
                value = 80;
                value = 100;

                ProgressVisible = true;
                StateHasChanged();
            }
        }


        public async Task InitArray()
        {
            await JSRuntime.InvokeVoidAsync("InitArray");
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

