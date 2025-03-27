using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace LigaManagerManagement.Web.Pages
{
    public class ChartVerein : ComponentBase
    {
        public RadzenDataGrid<Verein> grid;
        public RadzenDataGrid<Spieltag> gridSpieltage;

        public bool IsLoading = false;
        public Density Density = Density.Compact;
        public bool allowVirtualization;
        double prozentsiege = 0;
        double prozentuntentschieden = 0;
        double prozentniederlagen = 0;
        public string VisibleChart = "none";
        
        [Parameter]
        public string VereinNr { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }      

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        
        public List<DisplaySpieltag> SpieltagList;

        public List<DisplaySaison> SaisonenList;

        public List<Mannschaftsstatistik> Statistik = new List<Mannschaftsstatistik>();

        public List<Mannschaftsstatistik> StatistikAktSaison = new List<Mannschaftsstatistik>();

        public IEnumerable<Spieltag> Spieltage { get; set; }

        public List<LVereinsinfo> Vereinsinfo = new List<LVereinsinfo>();

        [Inject]
        public IStringLocalizer<ChartPunkte> Localizer { get; set; }

        
        public List<int?> chartData = new List<int?>();

        public int SaisonID;       
        
        public string Vereinsname;
        

        protected override async Task OnInitializedAsync()
        {
            try
            {
               IsLoading = true;

               await ErzeugeVereinsinfo();
               await ErzeugeStatistik();
               await ErzeugeLangZeitStatistik();               
                var verein = await VereineService.GetVerein(Convert.ToInt32(VereinNr));
                Vereinsname = verein.Vereinsname2;

                IsLoading = false;
                StateHasChanged();
                OnTabChange(0);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                IsLoading = false;
            }
        }


        private async Task ErzeugeVereinsinfo()
        {
            var vereinsinfo = await VereineService.GetVerein(Convert.ToInt32(VereinNr));

            LVereinsinfo item = new LVereinsinfo();

            item.StatText = "Verein";
            item.Eigenschaft = vereinsinfo.Vereinsname2;
            Vereinsinfo.Add(item);

            item = new LVereinsinfo();
            item.StatText = "Straße";
            item.Eigenschaft = vereinsinfo.Strasse;
            Vereinsinfo.Add(item);

            item = new LVereinsinfo();
            item.StatText = "Ort";
            item.Eigenschaft = vereinsinfo.Ort;
            Vereinsinfo.Add(item);

            item = new LVereinsinfo();
            item.StatText = "Fax";
            item.Eigenschaft = vereinsinfo.Fax;
            Vereinsinfo.Add(item);

            item = new LVereinsinfo();
            item.StatText = "Telefon";
            item.Eigenschaft = vereinsinfo.Telefon;
            Vereinsinfo.Add(item);

            item = new LVereinsinfo();
            item.StatText = "E-Mail";
            item.Eigenschaft = vereinsinfo.EMail;
            Vereinsinfo.Add(item);

            item = new LVereinsinfo();
            item.StatText = "Homepage";
            item.Eigenschaft = vereinsinfo.Hyperlink;
            Vereinsinfo.Add(item);
        }

               
        

        private async Task ErzeugeStatistik()
        {
            var spieltage = await SpieltagService.GetSpieltage();
            Spieltage = spieltage.Where(x => x.LigaID == 1 && x.SaisonID == Globals.SaisonID && (x.Verein1_Nr == VereinNr || x.Verein2_Nr == VereinNr));

            int spielegesamt = spieltage.Where(x => (x.Verein1_Nr == VereinNr || x.Verein2_Nr == VereinNr) && x.LigaID == 1 && x.SaisonID == Globals.SaisonID).Count();
            int spieleheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr) && x.LigaID == 1 && x.SaisonID == Globals.SaisonID).Count();
            int spieleausw = spieltage.Where(x => (x.Verein2_Nr == VereinNr) && x.LigaID == 1 && x.SaisonID == Globals.SaisonID).Count();

            Mannschaftsstatistik item = new Mannschaftsstatistik();
            item.StatText = "Spiele insgesamt";
            item.Gesamt = spielegesamt.ToString();
            item.Heim = spieleheim.ToString();
            item.Auswaerts = spieleausw.ToString();

            StatistikAktSaison.Add(item);

            int siegeheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == 1 &&  x.SaisonID == Globals.SaisonID && (x.Tore1_Nr > x.Tore2_Nr ))).Count();
            int siegeaus = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == 1 && x.SaisonID == Globals.SaisonID &&  (x.Tore1_Nr < x.Tore2_Nr))).Count();

            item = new Mannschaftsstatistik();
            item.StatText = "Siege";
            item.Gesamt = (siegeheim + siegeaus).ToString();
            item.Heim = siegeheim.ToString();
            item.Auswaerts = siegeaus.ToString();
            StatistikAktSaison.Add(item);

            int unentschiedenheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == 1 && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr == x.Tore2_Nr))).Count();
            int unentschiedenauswaerts = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == 1 && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr == x.Tore2_Nr))).Count();

            item = new Mannschaftsstatistik();
            item.StatText = "Unentschieden";
            item.Gesamt = (unentschiedenheim + unentschiedenauswaerts).ToString();
            item.Heim = unentschiedenheim.ToString();
            item.Auswaerts = unentschiedenauswaerts.ToString();
            StatistikAktSaison.Add(item);

            int niederlagenheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == 1 && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr < x.Tore2_Nr))).Count();
            int niederlagenauswaerts = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == 1 && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr > x.Tore2_Nr))).Count();

            item = new Mannschaftsstatistik();
            item.StatText = "Niederlagen";
            item.Gesamt = (niederlagenheim + niederlagenauswaerts).ToString();
            item.Heim = niederlagenheim.ToString();
            item.Auswaerts = niederlagenauswaerts.ToString();
            StatistikAktSaison.Add(item);

            int? toreheim = spieltage.Where(x => x.Verein1_Nr == VereinNr && x.LigaID == 1 && x.SaisonID == Globals.SaisonID).Sum(x => x.Tore1_Nr);
            int? toreauswaerts = spieltage.Where(x => x.Verein2_Nr == VereinNr && x.LigaID == 1 && x.SaisonID == Globals.SaisonID).Sum(x => x.Tore2_Nr);
                        
            int? gegentoreheim = spieltage.Where(x => x.Verein1_Nr == VereinNr && x.LigaID == 1 && x.SaisonID == Globals.SaisonID).Sum(x => x.Tore2_Nr);
            int? gegentoreauswaerts = spieltage.Where(x => x.Verein2_Nr == VereinNr && x.LigaID == 1 && x.SaisonID == Globals.SaisonID).Sum(x => x.Tore1_Nr);

            item = new Mannschaftsstatistik();
            item.StatText = "Tore : Gegentore";
            item.Gesamt = (toreheim + toreauswaerts).ToString() + ": " + (gegentoreheim + gegentoreauswaerts).ToString();
            item.Heim = toreheim.ToString() + ": " + gegentoreheim.ToString();
            item.Auswaerts = toreauswaerts.ToString() + ": " + gegentoreauswaerts.ToString();
            StatistikAktSaison.Add(item);

            item = new Mannschaftsstatistik();
            int? toregesamt = toreheim + toreauswaerts;
            int? gegentoregesamt = gegentoreheim + gegentoreauswaerts;
            item.StatText = "Tore : Gegentore (Durchschn.)";
            item.Gesamt = Math.Round((decimal)(toregesamt * 1.0 / spielegesamt), 2).ToString() + ": " + Math.Round((decimal)(gegentoregesamt * 1.0 / spielegesamt), 2).ToString();
            item.Heim = Math.Round((decimal)(toreheim * 1.0 / spieleheim), 2).ToString() + ": " + Math.Round((decimal)(gegentoreheim * 1.0 / spieleheim), 2).ToString();
            item.Auswaerts = Math.Round((decimal)(toreauswaerts * 1.0 / spieleausw), 2).ToString() + ": " + Math.Round((decimal)(gegentoreauswaerts * 1.0 / spieleheim), 2).ToString();
            StatistikAktSaison.Add(item);

            prozentsiege = Math.Round((siegeheim + siegeaus * 1.0) / spielegesamt * 100, 2);
            prozentuntentschieden = Math.Round((unentschiedenheim + unentschiedenauswaerts * 1.0) / spielegesamt * 100, 2);
            prozentniederlagen = Math.Round((niederlagenheim + niederlagenauswaerts * 1.0) / spielegesamt * 100, 2);
            
        }

        public async void OnTabChange(int index)
        {
            try
            {
                // Index des Tabs prüfen (Langzeitstatistik ist z. B. das 2. Tab mit Index 2)
                if (index == 2)
                {
                    VisibleChart = "block";
                    await JSRuntime.InvokeVoidAsync("renderChart");
                }
                else
                {
                    VisibleChart = "none";
                }

                if (index == 0)
                {
                    var verein = await VereineService.GetVerein(Convert.ToInt32(VereinNr));

                    decimal? latitude = verein.Latitude;
                    decimal? longitude = verein.Longitude;

                    if (latitude.HasValue && longitude.HasValue)
                    {
                        await JSRuntime.InvokeVoidAsync("renderMap", latitude.Value, longitude.Value);
                    }
                    else
                    {
                        Console.WriteLine("Latitude oder Longitude sind null.");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                IsLoading = false;
            }
        }

        private async Task ErzeugeLangZeitStatistik()
        {
            try
            {
                var spieltage = await SpieltagService.GetSpieltage();

                int spielegesamt = spieltage.Where(x => (x.Verein1_Nr == VereinNr || x.Verein2_Nr == VereinNr) && x.LigaID == 1).Count();
                int spieleheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr) && x.LigaID == 1).Count();
                int spieleausw = spieltage.Where(x => (x.Verein2_Nr == VereinNr) && x.LigaID == 1).Count();

                Mannschaftsstatistik item = new Mannschaftsstatistik();
                item.StatText = "Spiele insgesamt";
                item.Gesamt = spielegesamt.ToString();
                item.Heim = spieleheim.ToString();
                item.Auswaerts = spieleausw.ToString();

                Statistik.Add(item);

                int siegeheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == 1 && (x.Tore1_Nr > x.Tore2_Nr))).Count();
                int siegeaus = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == 1 && (x.Tore1_Nr < x.Tore2_Nr))).Count();

                item = new Mannschaftsstatistik();
                item.StatText = "Siege";
                item.Gesamt = (siegeheim + siegeaus).ToString();
                item.Heim = siegeheim.ToString();
                item.Auswaerts = siegeaus.ToString();
                Statistik.Add(item);

                int unentschiedenheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == 1 && (x.Tore1_Nr == x.Tore2_Nr))).Count();
                int unentschiedenauswaerts = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == 1 && (x.Tore1_Nr == x.Tore2_Nr))).Count();

                item = new Mannschaftsstatistik();
                item.StatText = "Unentschieden";
                item.Gesamt = (unentschiedenheim + unentschiedenauswaerts).ToString();
                item.Heim = unentschiedenheim.ToString();
                item.Auswaerts = unentschiedenauswaerts.ToString();
                Statistik.Add(item);

                int niederlagenheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == 1 && (x.Tore1_Nr < x.Tore2_Nr))).Count();
                int niederlagenauswaerts = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == 1 && (x.Tore1_Nr > x.Tore2_Nr))).Count();

                item = new Mannschaftsstatistik();
                item.StatText = "Niederlagen";
                item.Gesamt = (niederlagenheim + niederlagenauswaerts).ToString();
                item.Heim = niederlagenheim.ToString();
                item.Auswaerts = niederlagenauswaerts.ToString();
                Statistik.Add(item);

                int? toreheim = spieltage.Where(x => x.Verein1_Nr == VereinNr && x.LigaID == 1).Sum(x => x.Tore1_Nr);
                int? toreauswaerts = spieltage.Where(x => x.Verein2_Nr == VereinNr && x.LigaID == 1).Sum(x => x.Tore2_Nr);

                int? gegentoreheim = spieltage.Where(x => x.Verein1_Nr == VereinNr && x.LigaID == 1).Sum(x => x.Tore2_Nr);
                int? gegentoreauswaerts = spieltage.Where(x => x.Verein2_Nr == VereinNr && x.LigaID == 1).Sum(x => x.Tore1_Nr);

                item = new Mannschaftsstatistik();
                item.StatText = "Tore : Gegentore";
                item.Gesamt = (toreheim + toreauswaerts).ToString() + ": " + (gegentoreheim + gegentoreauswaerts).ToString();
                item.Heim = toreheim.ToString() + ": " + gegentoreheim.ToString();
                item.Auswaerts = toreauswaerts.ToString() + ": " + gegentoreauswaerts.ToString();
                Statistik.Add(item);

                item = new Mannschaftsstatistik();
                int? toregesamt = toreheim + toreauswaerts;
                int? gegentoregesamt = gegentoreheim + gegentoreauswaerts;
                item.StatText = "Tore : Gegentore (Durchschn.)";
                item.Gesamt = Math.Round((decimal)(toregesamt * 1.0 / spielegesamt), 2).ToString() + ": " + Math.Round((decimal)(gegentoregesamt * 1.0 / spielegesamt), 2).ToString();
                item.Heim = Math.Round((decimal)(toreheim * 1.0 / spieleheim), 2).ToString() + ": " + Math.Round((decimal)(gegentoreheim * 1.0 / spieleheim), 2).ToString();
                item.Auswaerts = Math.Round((decimal)(toreauswaerts * 1.0 / spieleausw), 2).ToString() + ": " + Math.Round((decimal)(gegentoreauswaerts * 1.0 / spieleheim), 2).ToString();
                Statistik.Add(item);

                prozentsiege = Math.Round((siegeheim + siegeaus * 1.0) / spielegesamt * 100, 2);
                prozentuntentschieden = Math.Round((unentschiedenheim + unentschiedenauswaerts * 1.0) / spielegesamt * 100, 2);
                prozentniederlagen = Math.Round((niederlagenheim + niederlagenauswaerts * 1.0) / spielegesamt * 100, 2);

                await JSRuntime.InvokeVoidAsync("updateChartValues", prozentsiege, prozentuntentschieden, prozentniederlagen);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }
    }

}

public class LVereinsinfo
{   
    public string StatText { get; set; }
    public string Eigenschaft { get; set; }
    
}

