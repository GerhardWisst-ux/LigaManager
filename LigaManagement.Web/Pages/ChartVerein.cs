using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class ChartVerein : ComponentBase
    {
        [Parameter]
        public string VereinNr { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public IEnumerable<Tabelle> Tabellen { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        public List<DisplaySpieltag> SpieltagList;

        public List<DisplaySaison> SaisonenList;

        public List<DisplayChartVerein> VereineList = new List<DisplayChartVerein>();

        [Inject]
        public IStringLocalizer<ChartPunkte> Localizer { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }
        public List<int?> chartData = new List<int?>();

        public string arrPunkteJson;
        public int SaisonID;
        public int VereinID;

        public string arrSpielePunkte;
        public string Vereinsname;
        public double value = 0;

        public bool ProgressVisible = false;
        public static List<Tabelle> lstVereine = new List<Tabelle>();

        protected string DisplayErrorSaison = "none";
        protected string DisplayErrorVerein = "none";
        protected string DisplayErrorChartArt = "none";

        protected string ChartVisible = "none";
        public int ChartVereinNr;
        public Int32 currentspieltag;
        public int ChartSaisonId;
        int iSpieltage = 34;
        bool bAbgeschlossen;

        public static bool bLoad = false;
        SqlConnection conn;

        public IEnumerable<Saison> Saisonen { get; set; }

        public List<ChartData> chartDataList = new List<ChartData>();

        public string ChartArt = "";

        protected override async Task OnInitializedAsync()
        {
            Saisonen = (await SaisonenService.GetSaisonen()).ToList().Where(x => x.LigaID == Globals.LigaID);
            SaisonenList = new List<DisplaySaison>();

            var _saison = await SaisonenService.GetSaison(Globals.SaisonID);

            ChartSaisonId = _saison.SaisonID;

            iSpieltage = ErmittlenAktSpieltag();

            SpieltagList = new List<DisplaySpieltag>();
            for (int i = 1; i <= iSpieltage; i++)
            {
                SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + "." + Localizer["Spieltag"].Value));
            }

            for (int i = 0; i < Saisonen.Count(); i++)
            {
                var columns = Saisonen.ElementAt(i);
                SaisonenList.Add(new DisplaySaison(columns.SaisonID, Globals.LigaID, columns.Saisonname));
            }

            var vereineSaison = await VereineSaisonService.GetVereineSaison();
            List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == _saison.SaisonID).ToList();

            VereineList.Clear();
            for (int i = 0; i < verList.Count(); i++)
            {
                var verein = await VereineService.GetVerein(verList[i].VereinNr);
                VereineList.Add(new DisplayChartVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1));
            }

            var vereinname = await VereineService.GetVerein(Convert.ToInt32(VereinNr));
            Vereinsname = vereinname.Vereinsname2;

            PrepareChartPunkte();

            DisplayErrorSaison = "none";
            DisplayErrorVerein = "none";
            DisplayErrorChartArt = "none";

            ChartArt = "Punkte";
            ChartSaisonId = Globals.SaisonID;

            StateHasChanged();
        }
        private int ErmittlenAktSpieltag()
        {
            int iSpieltageSaison = 34;
            if (Globals.LigaNummer == 1)
            {
                if (Globals.currentSaison.Substring(0, 4) == "1963" || Globals.currentSaison.Substring(0, 4) == "1964")
                    iSpieltageSaison = 30;
                else if (Globals.currentSaison.Substring(0, 4) == "1991")
                    iSpieltageSaison = 38;
                else
                    iSpieltageSaison = 34;
            }

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
            if (bAbgeschlossen)
                currentspieltag = iSpieltageSaison;
            else
            {
                SpieltageRepository rep = new SpieltageRepository();
                currentspieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
            }

            return iSpieltageSaison;
        }
        protected async void PrepareChartPlatz()
        {
            var vereineSaison = await VereineSaisonService.GetVereineSaison();
            List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == Globals.SaisonID).ToList();

            Vereine = await VereineService.GetVereine();

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, true, verList, Vereine, currentspieltag, (int)Globals.Tabart.Gesamt);

            List<Tabelle> tab = Tabellen.ToList();

            lstVereine.Clear();
            for (int i = 0; i < Tabellen.Count(); i++)
            {
                lstVereine.Add(tab[i]);
            }

            ChartVisible = "block";

            ChartVereinNr = Convert.ToInt32(VereinNr);
            chartData = await TabelleService.CreateChartPlatz(SpieltagService, Vereine, ChartVereinNr, currentspieltag);

            ProgressVisible = true;
            StateHasChanged();

        }
        protected async void PrepareChartPunkte()
        {
            var vereineSaison = await VereineSaisonService.GetVereineSaison();
            List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == Globals.SaisonID).ToList();

            Vereine = await VereineService.GetVereine();

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, true, verList, Vereine, currentspieltag, (int)Globals.Tabart.Gesamt);

            List<Tabelle> tab = Tabellen.ToList();

            lstVereine.Clear();
            for (int i = 0; i < Tabellen.Count(); i++)
            {
                lstVereine.Add(tab[i]);
            }

            ChartVisible = "block";

            ChartVereinNr = Convert.ToInt32(VereinNr);
            chartData = await TabelleService.CreateChartPunkte(SpieltagService, Vereine, ChartVereinNr, currentspieltag);

            ProgressVisible = true;
            StateHasChanged();

        }

        public async Task ArtChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == "Punkte")
                {
                    ChartArt = "Punkte";
                    PrepareChartPunkte();
                }
                    
                else if (e.Value.ToString() == "Platz")
                {
                    ChartArt = "Platz";
                    PrepareChartPlatz();
                }

                StateHasChanged();
            }
        }
        public async Task SpieltagChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                currentspieltag = Convert.ToInt32(e.Value);

                PrepareChartPunkte();


            }
        }
        public async Task SaisonChange(ChangeEventArgs e)
        {
            string saison;
            if (e.Value != null)
            {
                saison = e.Value.ToString();
                Globals.currentSaison = saison;
                SaisonenList = new List<DisplaySaison>();

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                Saisonen = Saisonen.Where(x => x.LigaID == Globals.LigaID);

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, Globals.LigaID, columns.Saisonname));

                    if (columns.Saisonname == saison)
                        Globals.SaisonID = columns.SaisonID;
                }
                ChartSaisonId = Globals.SaisonID;

                saison = Globals.currentSaison;

                Vereine = await VereineService.GetVereine();

                PrepareChartPunkte();
                DisplayErrorSaison = "none";
                DisplayErrorVerein = "none";
                DisplayErrorChartArt = "none";

                ChartSaisonId = Globals.SaisonID;

                StateHasChanged();

            }
        }

        public async void ChartArtChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                ChartArt = e.Value.ToString();

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
            //if (saisonId == 0)
            //    DisplayErrorSaison = "block";
            //else
            //    DisplayErrorSaison = "none";

            //if (ChartVereinNr == 0)
            //    DisplayErrorVerein = "block";
            //else
            //    DisplayErrorVerein = "none";

            //if (ChartArt == 0)
            //    DisplayErrorChartArt = "block";
            //else
            //    DisplayErrorChartArt = "none";
            //if (saisonId == 0 || ChartVereinNr == 0 || ChartArt == 0)
            //{
            //    return;
            //}
            PrepareChartPunkte();
            StateHasChanged();

            //Vereine = await VereineService.GetVereine();
            //List<int?> cd = await TabelleService.CreateChart(SpieltagService, Vereine, ChartVereinNr, currentspieltag);

            //ChartData chartData = new ChartData();

            ////bool ret = chartData.InsertChartDataPunkte(cd, ChartVereinNr);

            //if (ret)
            //{
            //    chartDataList = ChartData(ChartVereinNr);

            //    arrSpielePunkte = string.Empty;
            //    arrPunkte = string.Empty;

            //    arrSpieltage = string.Empty;

            //    SpieltageRepository rep = new SpieltageRepository();

            //    bool bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            //    value = 20;

            //    int iAktSpieltag;
            //    if (bAbgeschlossen)
            //    {
            //        iAktSpieltag = Globals.maxSpieltag;
            //    }
            //    else
            //    {
            //        iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
            //    }


            //    for (int i = 0; i <= iAktSpieltag; i++)
            //    {
            //        arrSpieltage += String.Concat("[", (i + 1).ToString(), "] ", ", ");
            //    }

            //    //for (int i = 0; i < cd.Count; i++)
            //    //{
            //    //    // arrSpielePunkte += String.Concat("[", (i + 1).ToString(), ", ", String.Concat(cd[i].Value), "] ", ", ");

            //    //    arrPunkte += String.Concat("[", String.Concat(cd[i].Value).ToString(), "] ", ", ");
            //    //}
            //    //value = 40;

            //    //        arrSpieltage = "[" + 1 + ", " + 2 + ", " + 3 + ", " + 4 + ", " + 5 + ", " + 6 + ", " + 7 + ", " + 8 + ", " + 9 + ", " + 10 + ", " + 11 + ", " + 12 + ", " + 13 + ", " + 14 + ", " + 15 + ", " + 16 + ", " + 17 + ", " + 18 + ", " + 19 + ", " + 20 + ", " + 21 + ", " + 22 + ", " + 23 + ", " + 24 + ", " + 25 + ", " + 26 + ", " + 27 + ", " + 28 + ", " + 29 + ", " + 30 + ", " + 31 + ", " + 32 + ", " + 33 + ", " + 34 + "]";
            //    //public string arrPunkte = "[" + 3 + ", " + 3 + ", " + 3 + ", " + 4 + ", " + 5 + ", " + 6 + ", " + 7 + ", " + 8 + ", " + 9 + ", " + 10 + ", " + 11 + ", " + 12 + ", " + 13 + ", " + 14 + ", " + 15 + ", " + 16 + ", " + 17 + ", " + 18 + ", " + 19 + ", " + 20 + ", " + 21 + ", " + 22 + ", " + 23 + ", " + 24 + ", " + 25 + ", " + 26 + ", " + 27 + ", " + 28 + ", " + 29 + ", " + 30 + ", " + 31 + ", " + 32 + ", " + 33 + ", " + 34 + "]";
            //    arrSpieltage = arrSpieltage.Trim().Remove(arrSpieltage.Length - 2);
            //    arrPunkte2 = arrPunkte.Trim().Remove(arrPunkte.Length - 2);

            //    var opt = new JsonSerializerOptions() { WriteIndented = true };
            //    arrSpieltageJS = JsonSerializer.Serialize<string>(arrSpieltage, opt);
            //    arrPunkteJson = JsonSerializer.Serialize<string>(arrPunkte, opt);

            //    value = 60;
            //    //ShowAlertWindow();
            //    //InitArray();
            //    if (bLoad == false)
            //    {
            //        var timer = new Timer(new TimerCallback(_ =>
            //        {
            //            uriHelper.NavigateTo(uriHelper.Uri, forceLoad: true);
            //            bLoad = true;
            //        }), null, 100, 1000);
            //    }
            //    value = 80;
            //    value = 100;

            //    ProgressVisible = true;
            //    StateHasChanged();
            //}
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
        public class DisplaySaison
        {
            public DisplaySaison(int saisonID, int ligaID, string saisonname)
            {
                SaisonID = saisonID;
                LigaID = ligaID;
                Saisonname = saisonname;
            }
            public int SaisonID { get; set; }
            public int LigaID { get; set; }
            public string Saisonname { get; set; }
        }

        public class DisplaySpieltag
        {
            public DisplaySpieltag(string nummer, string name)
            {
                Nummer = nummer;
                Name = name;
            }
            public string Nummer { get; set; }
            public string Name { get; set; }

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

