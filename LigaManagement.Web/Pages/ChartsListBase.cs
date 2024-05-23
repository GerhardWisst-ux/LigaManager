<<<<<<< HEAD
﻿using LigaManagement.Models;
=======
﻿using Azure.Core;
using LigaManagement.Models;
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
<<<<<<< HEAD
using System;
=======
using Microsoft.JSInterop;
using System;
using System.Collections;
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using static System.Net.WebRequestMethods;

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

<<<<<<< HEAD
=======
        [Inject]
        public NavigationManager uriHelper { get; set; }

>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
        public List<DisplaySpieltag> SpieltagList;

        public List<DisplayChartVerein> VereineList = new List<DisplayChartVerein>();
        public IEnumerable<Verein> Vereine { get; set; }

<<<<<<< HEAD
        public string arrSpielePunkte;
        public string multiDimensionalArray2 = "[1, 3], [2, 3], [3, 6], [4, 9], [5, 12], [6, 15], [7, 18], [8, 21],[9, 24], [10, 24], [11, 24], [12, 27], [13, 30], [14, 31],[15, 31], [16, 34], [17, 34], [18, 34], [19, 37], [20, 40],[21, 43], [22, 46], [23, 47], [24, 50], [25, 53], [26, 56],[27, 57], [28, 60], [29, 63], [30, 63], [31, 63], [32, 64],[33, 67], [34, 68]";
=======
        public string arrPunkteJson;
        public int SaisonID;
        public int VereinID;

        public string arrSpielePunkte;
        public string Vereinsname;
        public double value = 0;

        public bool ProgressVisible = false;
        string arrSpieltage = "[" + 1 + ", " + 2 + ", " + 3 + ", " + 4 + ", " + 5 + ", " + 6 + ", " + 7 + ", " + 8 + ", " + 9 + ", " + 10 + ", " + 11 + ", " + 12 + ", " + 13 + ", " + 14 + ", " + 15 + ", " + 16 + ", " + 17 + ", " + 18 + ", " + 19 + ", " + 20 + ", " + 21 + ", " + 22 + ", " + 23 + ", " + 24 + ", " + 25 + ", " + 26 + ", " + 27 + ", " + 28 + ", " + 29 + ", " + 30 + ", " + 31 + ", " + 32 + ", " + 33 + ", " + 34 + "]";
        public string arrPunkte = "[" + 3 + ", " + 3 + ", " + 3 + ", " + 4 + ", " + 5 + ", " + 6 + ", " + 7 + ", " + 8 + ", " + 9 + ", " + 10 + ", " + 11 + ", " + 12 + ", " + 13 + ", " + 14 + ", " + 15 + ", " + 16 + ", " + 17 + ", " + 18 + ", " + 19 + ", " + 20 + ", " + 21 + ", " + 22 + ", " + 23 + ", " + 24 + ", " + 25 + ", " + 26 + ", " + 27 + ", " + 28 + ", " + 29 + ", " + 30 + ", " + 31 + ", " + 32 + ", " + 33 + ", " + 34 + "]";
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc

        protected string DisplayErrorSaison = "none";
        protected string DisplayErrorVerein = "none";
        protected string DisplayErrorChartArt = "none";

<<<<<<< HEAD
=======
        public string arrSpieltageJS;
        public string arrPunkteJS;
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
        public int ChartVereinNr;
        public int ChartArt;
        public Int32 currentspieltag;
        public int VereinNr;
        public int saisonId;
<<<<<<< HEAD
=======

        public static bool bLoad = false;
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
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

<<<<<<< HEAD
=======

>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
            DisplayErrorSaison = "none";
            DisplayErrorVerein = "none";
            DisplayErrorChartArt = "none";

<<<<<<< HEAD
=======
            ChartArt = 0;
            saisonId = Globals.SaisonID;

>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
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
<<<<<<< HEAD
=======
                var verein = await VereineService.GetVerein(ChartVereinNr);
                Vereinsname = verein.Vereinsname2;
                VereinID = Convert.ToInt32(e.Value);
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc

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
<<<<<<< HEAD

                for (int i = 0; i < cd.Count; i++)
                {                    
                    arrSpielePunkte += String.Concat("[", (i + 1).ToString(), ", ", String.Concat(cd[i].Value), "] ",", ");                    
                }
                StateHasChanged();
                arrSpielePunkte = arrSpielePunkte.Trim().Remove(arrSpielePunkte.Length - 2);

                
                System.Threading.Thread.Sleep(2000);

                StateHasChanged();
            }

=======
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
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
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

