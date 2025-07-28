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
using System.Reflection;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class ChartPlatz : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public IEnumerable<Tabelle> VereinePlaetze { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        public List<Verein> Vereinslist { get; set; }

        public List<DisplaySaison> SaisonenList;

        public List<DisplayChartVerein> VereineList = new List<DisplayChartVerein>();

        [Inject]
        public IStringLocalizer<ChartPunkte> Localizer { get; set; }

        public IEnumerable<Tabelle> Tabellen { get; set; }
        public IEnumerable<Verein> Vereine { get; set; }

        public string arrPunkteJson;
        public string Saison;
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

        public bool isLoading = false;
        protected string ChartVisible = "none";
        public int ChartVereinNr;
        public int ChartArt;
        public Int32 currentspieltag;
        public int VereinNr;
        public int ChartSaisonId;
        public int iSpieltage = 34;
        bool bAbgeschlossen;

        public static bool bLoad = false;

        public IEnumerable<Saison> Saisonen { get; set; }

        public List<ChartData> chartDataList = new List<ChartData>();

        private List<int> YAxisValues = new();

        private List<int> XAxisValues = new();


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //if (firstRender)
            //{
            //    await JSRuntime.InvokeVoidAsync("dispose");
            //}
        }
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            Saisonen = (await SaisonenService.GetSaisonen()).ToList().Where(x => x.LigaID == Globals.LigaID);
            SaisonenList = new List<DisplaySaison>();

            for (int i = 0; i < Saisonen.Count(); i++)
            {
                var columns = Saisonen.ElementAt(i);
                SaisonenList.Add(new DisplaySaison(columns.SaisonID, Globals.LigaID, columns.Saisonname));
            }
                        
            ChartSaisonId = Globals.SaisonID;
            Saison = Globals.currentSaison;

            iSpieltage = ErmittlenAktSpieltag();

            var vereineSaison = await VereineSaisonService.GetVereineSaison();
            List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == ChartSaisonId).ToList();

            VereineList.Clear();
            for (int i = 0; i < verList.Count(); i++)
            {
                var verein = await VereineService.GetVerein(verList[i].VereinNr);
                VereineList.Add(new DisplayChartVerein(verein.VereinNr, verein.Vereinsname1));
            }          
            
            DisplayErrorSaison = "none";
            DisplayErrorVerein = "none";
            DisplayErrorChartArt = "none";

            ChartArt = 0;
            ChartSaisonId = Globals.SaisonID;

            isLoading = false;
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

            var currentSaison = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison);
            if (currentSaison != null)
            {
                bAbgeschlossen = currentSaison.Abgeschlossen;
                if (bAbgeschlossen)
                    currentspieltag = iSpieltageSaison;
                else
                {
                    SpieltageRepository rep = new SpieltageRepository();
                    currentspieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                }
            }
            else
            {
                // Handle the case where currentSaison is null
                currentspieltag = 0; // Default value or appropriate fallback
            }

            return currentspieltag;
        }

       
        [JSInvokable]
        private async Task SetXYAxisValues(List<Tuple<int, int?>> xyAxisValues)
        {
            try
            {
                var spieltage = xyAxisValues.Select(_ => _.Item1).ToList();
                var plaetze = xyAxisValues.Select(_ => _.Item2).ToList();

                // JavaScript Interop Aufruf zum Setzen der Werte
                await JSRuntime.InvokeVoidAsync("updateChartXYValues", spieltage, plaetze);
            }
            catch (Exception)
            {
                //ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        public async Task SaisonChange(ChangeEventArgs e)
        {            
            if (e.Value != null)
            {                
                SaisonID = Convert.ToInt32(e.Value.ToString());
                
                SaisonenList = new List<DisplaySaison>();

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                Saisonen = Saisonen.Where(x => x.LigaID == Globals.LigaID);

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, Globals.LigaID, columns.Saisonname));                    
                }

                var vereineSaison = await VereineSaisonService.GetVereineSaison();
                List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == SaisonID).ToList();

                VereineList.Clear();
                for (int i = 0; i < verList.Count(); i++)
                {
                    var verein = await VereineService.GetVerein(verList[i].VereinNr);
                    VereineList.Add(new DisplayChartVerein(verein.VereinNr, verein.Vereinsname1));
                }
                                
                var aktsaison = await SaisonenService.GetSaison(SaisonID);
                                
                Saison = aktsaison.Saisonname;

                DisplayErrorSaison = "none";
                DisplayErrorVerein = "none";
                DisplayErrorChartArt = "none";

                ChartArt = 0;
                ChartSaisonId = SaisonID;               

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

                iSpieltage = ErmittlenAktSpieltag();

                // Beispiel: Dynamische Werte generieren oder laden
                XAxisValues = Enumerable.Range(1, iSpieltage).ToList();
                YAxisValues = Enumerable.Range(1, iSpieltage).Select(x => new Random().Next(1, 18)).ToList();

                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.SaisonID == ChartSaisonId).Abgeschlossen;

                Vereine = await VereineService.GetVereine();

                var vereineSaison = await VereineSaisonService.GetVereineSaison();
                List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == ChartSaisonId).ToList();

                var tab = await TabelleService.BerechnePlaetzeDE(SpieltagService, bAbgeschlossen, verList, Vereine, ChartSaisonId, VereinID, (int)Globals.Tabart.Gesamt);

                // Senden der Daten an das JavaScript
                await InvokeAsync(() => SetXYAxisValues(tab));                               

                StateHasChanged();
            }
        }


        public async void OnClickHandler()
        {

            iSpieltage = ErmittlenAktSpieltag();

            // Beispiel: Dynamische Werte generieren oder laden
            XAxisValues = Enumerable.Range(1, iSpieltage).ToList();
            YAxisValues = Enumerable.Range(1, iSpieltage).Select(x => new Random().Next(1, 18)).ToList();

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.SaisonID == ChartSaisonId).Abgeschlossen;

            Vereine = await VereineService.GetVereine();

            var vereineSaison = await VereineSaisonService.GetVereineSaison();
            List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == ChartSaisonId).ToList();

            var tab = await TabelleService.BerechnePlaetzeDE(SpieltagService, bAbgeschlossen, verList, Vereine, ChartSaisonId, VereinID, (int)Globals.Tabart.Gesamt);
            // Senden der Daten an das JavaScript
            await InvokeAsync(() => SetXYAxisValues(tab));

            StateHasChanged();           
        }
       

        public class DisplaySaison(int saisonID, int ligaID, string saisonname)
        {
            public int SaisonID { get; set; } = saisonID;
            public int LigaID { get; set; } = ligaID;
            public string Saisonname { get; set; } = saisonname;
        }
     
        public class DisplayChartVerein(int vereinnr, string vereinname)
        {
            public int VereinNr { get; set; } = vereinnr;
            public string Vereinname1 { get; set; } = vereinname;
        }
    }

}

