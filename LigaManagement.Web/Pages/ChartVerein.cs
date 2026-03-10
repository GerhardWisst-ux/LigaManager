using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Web.Services;
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
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using ToreManagerManagement.Web.Services;

namespace LigaManagerManagement.Web.Pages
{
    public class ChartVerein : ComponentBase
    {
        public RadzenDataGrid<Verein> grid;
        public RadzenDataGrid<PokalHistorieStatistik> gridPokalHistorie;
        public RadzenDataGrid<Spieltag> gridSpieltage;
        public RadzenDataGrid<Spieltag> gridSpieltageSerien;

        public bool IsLoading = false;
        public Density Density = Density.Compact;
        public bool allowVirtualization;
        double prozentsiege = 0;
        double prozentuntentschieden = 0;
        double prozentniederlagen = 0;
        int vereinligaid = 1;
        public string VisibleChart = "none";

        [Parameter]
        public string VereinNr { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IPokalergebnisseService PokalergebnisseService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        public List<DisplaySpieltag> SpieltagList;

        public List<DisplaySaison> SaisonenList;

        public List<Mannschaftsstatistik> Statistik = new List<Mannschaftsstatistik>();

        public List<SerienStatistik> SerienStatistik = new List<SerienStatistik>();
        public List<PokalHistorieStatistik> PokalHistorieStatistik = new List<PokalHistorieStatistik>();

        public List<Mannschaftsstatistik> StatistikAktSaison = new List<Mannschaftsstatistik>();
        
        public List<SpieltageSerien> SerieSiegeVerein = new List<SpieltageSerien>();
        public List<SpieltageSerien> SerieNiederlagenVerein = new List<SpieltageSerien>();
        public List<SpieltageSerien> SerieUnentschiedenVerein = new List<SpieltageSerien>();

        public List<Spieltag> SerieSiegeVerein2 = new List<Spieltag>();
        public List<Spieltag> SerieNiederlagenVerein2 = new List<Spieltag>();
        public List<Spieltag> SerieUnentschiedenVerein2 = new List<Spieltag>();

        public List<Spieltag> AlleSpieltage = new();
        public List<Spieltag> AlleSpieltageNL = new();
        public List<Spieltag> AlleSpieltageUS = new();

        public List<SpieltageSerieGruppe> Gruppen = new();
        public List<SpieltageSerieGruppeNL> GruppenNL = new();
        public List<SpieltageSerieGruppeNL> GruppenUS = new();

        public SpieltageSerieGruppe AktuelleGruppeSi;
        public SpieltageSerieGruppeNL AktuelleGruppeNL;


        public List<SpieltageSerieGruppe> SpieltageSerienGruppen = new();
        public List<SpieltageSerieGruppeNL> SpieltageSerienGruppenNL = new();
        public List<SpieltageSerieGruppeUE> SpieltageSerienGruppenUS = new();

        public SpieltageSerieGruppe AktuelleSiegesSerie;
        public SpieltageSerieGruppeNL AktuelleNiederlagenSerie;
        public SpieltageSerieGruppeUE AktuelleUnentschiedenSerie;

        public int AktuelleGruppeIndex = 0;
        public int AktuelleGruppeIndexNL = 0;
        public int AktuelleGruppeIndexUS = 0;

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
                await ErzeugeSerienStatistik();
                await ErzeugePokalhistorie();
                await ErzeugeSerienStatistik();
                await GetSiegeSerienVerein();
                await GetNiederlagenSerienVerein();
                await GetUnentschiedenSerienVerein();

                var verein = await VereineService.GetVerein(Convert.ToInt32(VereinNr));
                Vereinsname = verein.Vereinsname2;

                IsLoading = false;
                OnTabChange(0);
                               
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                IsLoading = false;
            }
        }

 

        private async Task ErzeugePokalhistorie()
        {
            try
            {
                var pokalHistorieStatistik = await PokalergebnisseService.GetPokalergebnisseHistorie(VereinNr);
                PokalHistorieStatistik = pokalHistorieStatistik.ToList();
                StateHasChanged();
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
            try
            {
                var spieltage = await SpieltagService.GetSpieltage();
                Spieltage = spieltage.Where(x => x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID && (x.Verein1_Nr == VereinNr || x.Verein2_Nr == VereinNr));

                int spielegesamt = spieltage.Where(x => (x.Verein1_Nr == VereinNr || x.Verein2_Nr == VereinNr) && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID).Count();
                int spieleheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr) && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID).Count();
                int spieleausw = spieltage.Where(x => (x.Verein2_Nr == VereinNr) && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID).Count();

                Mannschaftsstatistik item = new Mannschaftsstatistik();
                item.StatText = "Spiele insgesamt";
                item.Gesamt = spielegesamt.ToString();
                item.Heim = spieleheim.ToString();
                item.Auswaerts = spieleausw.ToString();

                StatistikAktSaison.Add(item);

                int siegeheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr > x.Tore2_Nr))).Count();
                int siegeaus = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr < x.Tore2_Nr))).Count();

                item = new Mannschaftsstatistik();
                item.StatText = "Siege";
                item.Gesamt = (siegeheim + siegeaus).ToString();
                item.Heim = siegeheim.ToString();
                item.Auswaerts = siegeaus.ToString();
                StatistikAktSaison.Add(item);

                int unentschiedenheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr == x.Tore2_Nr))).Count();
                int unentschiedenauswaerts = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr == x.Tore2_Nr))).Count();

                item = new Mannschaftsstatistik();
                item.StatText = "Unentschieden";
                item.Gesamt = (unentschiedenheim + unentschiedenauswaerts).ToString();
                item.Heim = unentschiedenheim.ToString();
                item.Auswaerts = unentschiedenauswaerts.ToString();
                StatistikAktSaison.Add(item);

                int niederlagenheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr < x.Tore2_Nr))).Count();
                int niederlagenauswaerts = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID && (x.Tore1_Nr > x.Tore2_Nr))).Count();

                item = new Mannschaftsstatistik();
                item.StatText = "Niederlagen";
                item.Gesamt = (niederlagenheim + niederlagenauswaerts).ToString();
                item.Heim = niederlagenheim.ToString();
                item.Auswaerts = niederlagenauswaerts.ToString();
                StatistikAktSaison.Add(item);

                int? toreheim = spieltage.Where(x => x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID).Sum(x => x.Tore1_Nr);
                int? toreauswaerts = spieltage.Where(x => x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID).Sum(x => x.Tore2_Nr);

                int? gegentoreheim = spieltage.Where(x => x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID).Sum(x => x.Tore2_Nr);
                int? gegentoreauswaerts = spieltage.Where(x => x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid && x.SaisonID == Globals.SaisonID).Sum(x => x.Tore1_Nr);

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
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }

        }

        public async void OnTabChange(int index)
        {
            try
            {
                // Index des Tabs prüfen (Langzeitstatistik ist z. B. das 2. Tab mit Index 2)
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
                else if (index == 2)
                {
                    VisibleChart = "block";
                    await JSRuntime.InvokeVoidAsync("renderChart");
                }              
                else
                {
                    VisibleChart = "none";
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

                int spielegesamt = spieltage.Where(x => (x.Verein1_Nr == VereinNr || x.Verein2_Nr == VereinNr) && x.LigaID == vereinligaid).Count();
                int spieleheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr) && x.LigaID == vereinligaid).Count();
                int spieleausw = spieltage.Where(x => (x.Verein2_Nr == VereinNr) && x.LigaID == vereinligaid).Count();

                Mannschaftsstatistik item = new Mannschaftsstatistik();
                item.StatText = "Spiele insgesamt";
                item.Gesamt = spielegesamt.ToString();
                item.Heim = spieleheim.ToString();
                item.Auswaerts = spieleausw.ToString();

                Statistik.Add(item);

                int siegeheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid && (x.Tore1_Nr > x.Tore2_Nr))).Count();
                int siegeaus = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid && (x.Tore1_Nr < x.Tore2_Nr))).Count();

                item = new Mannschaftsstatistik();
                item.StatText = "Siege";
                item.Gesamt = (siegeheim + siegeaus).ToString();
                item.Heim = siegeheim.ToString();
                item.Auswaerts = siegeaus.ToString();
                Statistik.Add(item);

                int unentschiedenheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid && (x.Tore1_Nr == x.Tore2_Nr))).Count();
                int unentschiedenauswaerts = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid && (x.Tore1_Nr == x.Tore2_Nr))).Count();

                item = new Mannschaftsstatistik();
                item.StatText = "Unentschieden";
                item.Gesamt = (unentschiedenheim + unentschiedenauswaerts).ToString();
                item.Heim = unentschiedenheim.ToString();
                item.Auswaerts = unentschiedenauswaerts.ToString();
                Statistik.Add(item);

                int niederlagenheim = spieltage.Where(x => (x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid && (x.Tore1_Nr < x.Tore2_Nr))).Count();
                int niederlagenauswaerts = spieltage.Where(x => (x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid && (x.Tore1_Nr > x.Tore2_Nr))).Count();

                item = new Mannschaftsstatistik();
                item.StatText = "Niederlagen";
                item.Gesamt = (niederlagenheim + niederlagenauswaerts).ToString();
                item.Heim = niederlagenheim.ToString();
                item.Auswaerts = niederlagenauswaerts.ToString();
                Statistik.Add(item);

                int? toreheim = spieltage.Where(x => x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid).Sum(x => x.Tore1_Nr);
                int? toreauswaerts = spieltage.Where(x => x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid).Sum(x => x.Tore2_Nr);

                int? gegentoreheim = spieltage.Where(x => x.Verein1_Nr == VereinNr && x.LigaID == vereinligaid).Sum(x => x.Tore2_Nr);
                int? gegentoreauswaerts = spieltage.Where(x => x.Verein2_Nr == VereinNr && x.LigaID == vereinligaid).Sum(x => x.Tore1_Nr);

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


                //await JSRuntime.InvokeVoidAsync("updateChartValues", prozentsiege, prozentuntentschieden, prozentniederlagen);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        private async Task ErzeugeSerienStatistik()
        {
            try
            {
                var spieltage = await SpieltagService.GetSpieltage();

                var meisteSiege = spieltage.Where(s => ((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr > s.Tore2_Nr) || (s.Verein2_Nr == VereinNr.ToString() && s.Tore1_Nr < s.Tore2_Nr) &&
                s.LigaID == vereinligaid).GroupBy(s => s.Saison).
                Select(group => new
                {
                    Saison = group.Key,
                    Siege = group.Count()
                })
                .OrderByDescending(x => x.Siege)
                .ThenByDescending(x => x.Saison)
                .ToList().Take(1);

                SerienStatistik serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Meiste Siege: ";
                serie.SaisonText = "Saison " + meisteSiege.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = meisteSiege.FirstOrDefault()?.Siege.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var meisteUntentschieden = spieltage.Where(s => ((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr == s.Tore2_Nr) || (s.Verein2_Nr == VereinNr.ToString() && s.Tore1_Nr == s.Tore2_Nr) &&
                s.LigaID == vereinligaid).GroupBy(s => s.Saison).
                Select(group => new
                {
                    Saison = group.Key,
                    Unentschieden = group.Count()
                })
                .OrderByDescending(x => x.Unentschieden)
                .ThenByDescending(x => x.Saison)
                .ToList().Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Meiste Unentschieden: ";
                serie.SaisonText = "Saison " + meisteUntentschieden.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = meisteUntentschieden.FirstOrDefault()?.Unentschieden.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var meisteNiederlagen = spieltage.Where(s => ((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr < s.Tore2_Nr) || (s.Verein2_Nr == VereinNr.ToString() && s.Tore1_Nr > s.Tore2_Nr) &&
                s.LigaID == vereinligaid).GroupBy(s => s.Saison).
                Select(group => new
                {
                    Saison = group.Key,
                    Niederlagen = group.Count()
                })
                .OrderByDescending(x => x.Niederlagen)
                .ThenByDescending(x => x.Saison)
                .ToList().Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Meiste Niederlagen: ";
                serie.SaisonText = "Saison " + meisteNiederlagen.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = meisteNiederlagen.FirstOrDefault()?.Niederlagen.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var wenigsteSiege = spieltage.Where(s => s.LigaID == vereinligaid && (((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr > s.Tore2_Nr) || (s.Verein2_Nr == VereinNr.ToString() && s.Tore1_Nr < s.Tore2_Nr)))
                    .GroupBy(s => s.Saison).
                Select(group => new
                {
                    Saison = group.Key,
                    Siege = group.Count()
                })
                .OrderBy(x => x.Siege)
                .ThenBy(x => x.Saison)
                .ToList().Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Wenigste Siege: ";
                serie.SaisonText = "Saison " + wenigsteSiege.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = wenigsteSiege.FirstOrDefault()?.Siege.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var wenigsteUntentschieden = spieltage
                .Where(s => s.LigaID == vereinligaid && (((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr == s.Tore2_Nr) || (s.Verein2_Nr == VereinNr.ToString() && s.Tore1_Nr == s.Tore2_Nr)))
                .GroupBy(s => s.Saison)
                .Select(group => new
                {
                    Saison = group.Key,
                    Unentschieden = group.Count()
                })
                .OrderBy(x => x.Unentschieden)
                .ThenBy(x => x.Saison)
                .ToList()
                .Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Wenigste Unentschieden: ";
                serie.SaisonText = "Saison " + wenigsteUntentschieden.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = wenigsteUntentschieden.FirstOrDefault()?.Unentschieden.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var wenigsteNiederlagen = spieltage
                .Where(s => s.LigaID == vereinligaid && (((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr < s.Tore2_Nr) || (s.Verein2_Nr == VereinNr.ToString() && s.Tore1_Nr > s.Tore2_Nr)))
                .GroupBy(s => s.Saison)
                .Select(group => new
                {
                    Saison = group.Key,
                    Niederlagen = group.Count()
                })
                .OrderBy(x => x.Niederlagen)
                .ThenBy(x => x.Saison)
                .ToList()
                .Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Wenigste Niederlagen: ";
                serie.SaisonText = "Saison " + wenigsteNiederlagen.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = wenigsteNiederlagen.FirstOrDefault()?.Niederlagen.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var meisteheimSiege = spieltage.Where(s => ((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr > s.Tore2_Nr) &&
                s.LigaID == vereinligaid).GroupBy(s => s.Saison).
                Select(group => new
                {
                    Saison = group.Key,
                    Siege = group.Count()
                })
                .OrderByDescending(x => x.Siege)
                .ThenByDescending(x => x.Saison)
                .ToList().Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Meiste Heimsiege: ";
                serie.SaisonText = "Saison " + meisteheimSiege.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = meisteheimSiege.FirstOrDefault()?.Siege.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var meisteheimunentschieden = spieltage.Where(s => ((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr == s.Tore2_Nr) &&
                s.LigaID == vereinligaid).GroupBy(s => s.Saison).
                Select(group => new
                {
                    Saison = group.Key,
                    Siege = group.Count()
                })
                .OrderByDescending(x => x.Siege)
                .ThenByDescending(x => x.Saison)
                .ToList().Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Meiste Heim-Unentschieden: ";
                serie.SaisonText = "Saison " + meisteheimunentschieden.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = meisteheimunentschieden.FirstOrDefault()?.Siege.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var meisteheimniederlagen = spieltage.Where(s => ((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr < s.Tore2_Nr) &&
                s.LigaID == vereinligaid).GroupBy(s => s.Saison).
                Select(group => new
                {
                    Saison = group.Key,
                    Siege = group.Count()
                })
                .OrderByDescending(x => x.Siege)
                .ThenByDescending(x => x.Saison)
                .ToList().Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Meiste Heimniederlagen: ";
                serie.SaisonText = "Saison " + meisteheimniederlagen.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = meisteheimniederlagen.FirstOrDefault()?.Siege.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var meisteAuswaertsSiege = spieltage.Where(s => ((s.Verein2_Nr == VereinNr.ToString()) && s.Tore1_Nr < s.Tore2_Nr) && s.LigaID == vereinligaid)
                .GroupBy(s => s.Saison)
                .Select(group => new
                {
                    Saison = group.Key,
                    Siege = group.Count()
                })
                .OrderByDescending(x => x.Siege)
                .ThenByDescending(x => x.Saison)
                .ToList()
                .Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Meiste Auswärtssiege: ";
                serie.SaisonText = "Saison " + meisteAuswaertsSiege.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = meisteAuswaertsSiege.FirstOrDefault()?.Siege.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var meisteAuswaertsUnentschieden = spieltage.Where(s => ((s.Verein2_Nr == VereinNr.ToString()) && s.Tore1_Nr == s.Tore2_Nr) && s.LigaID == vereinligaid)
                    .GroupBy(s => s.Saison)
                    .Select(group => new
                    {
                        Saison = group.Key,
                        Unentschieden = group.Count()
                    })

                .OrderByDescending(x => x.Unentschieden)
                .ThenByDescending(x => x.Saison)
                .ToList().Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Meiste Auswärts-Unentschieden: ";
                serie.SaisonText = "Saison " + meisteAuswaertsUnentschieden.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = meisteAuswaertsUnentschieden.FirstOrDefault()?.Unentschieden.ToString() ?? "0";
                SerienStatistik.Add(serie);

                var meisteauswaertsniederlagen = spieltage.Where(s => ((s.Verein2_Nr == VereinNr.ToString()) && s.Tore1_Nr > s.Tore2_Nr) &&
                s.LigaID == vereinligaid).GroupBy(s => s.Saison).
                Select(group => new
                {
                    Saison = group.Key,
                    Siege = group.Count()
                })
                .OrderByDescending(x => x.Siege)
                .ThenByDescending(x => x.Saison)
                .ToList().Take(1);

                serie = new SerienStatistik();
                serie.Verein = (await VereineService.GetVerein(Convert.ToInt32(VereinNr))).ToString();
                serie.StatText = "Meiste Auswärtsniederlagen: ";
                serie.SaisonText = "Saison " + meisteauswaertsniederlagen.FirstOrDefault()?.Saison.ToString() ?? "0";
                serie.AnzahlText = meisteauswaertsniederlagen.FirstOrDefault()?.Siege.ToString() ?? "0";
                SerienStatistik.Add(serie);

                //await JSRuntime.InvokeVoidAsync("updateChartValues", prozentsiege, prozentuntentschieden, prozentniederlagen);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        public async Task<List<Spieltag>> GetUnentschiedenSerienVerein()
        {
            List<Spieltag> SpieltageSerie = null;
            IsLoading = true;
            try
            {
                SerieUnentschiedenVerein = await TabelleService.SerieUnentschiedenVerein(SpieltagService, Convert.ToInt32(VereinNr));

                var Spieltage = (await SpieltagService.GetSpieltage()).Where(x => x.LigaID == vereinligaid);
                SpieltageSerie = new List<Spieltag>();
                foreach (var serie in SerieUnentschiedenVerein)
                {
                    foreach (string id in serie.SpieltagIDs.Split(',').Select(s => s.Trim()))
                    {
                        var spieltag = Spieltage.FirstOrDefault(s => s.SpieltagId.ToString() == id);

                        if (spieltag != null)
                        {
                            spieltag.AnzahlUnentschieden = serie.AnzahlUnentschieden;
                            SpieltageSerie.Add(spieltag);
                        }
                    }

                }
                SerieUnentschiedenVerein2 = SpieltageSerie;
                var gruppen = new List<SpieltageSerieGruppeUE>();

                foreach (var serie in SerieUnentschiedenVerein)
                {
                    var spieltagIds = serie.SpieltagIDs.Split(',').Select(s => s.Trim());
                    var idSet = new HashSet<string>(spieltagIds);

                    var zugeordneteSpieltage = Spieltage
                        .Where(s => idSet.Contains(s.SpieltagId.ToString()))
                        .Where(s => s.LigaID == vereinligaid)
                        .ToList();

                    if (zugeordneteSpieltage.Any())
                    {
                        gruppen.Add(new SpieltageSerieGruppeUE
                        {
                            Saison = serie.Saison,
                            AnzahlUntentschieden = serie.AnzahlUnentschieden,
                            Spieltage = zugeordneteSpieltage
                        });
                    }
                }

                SpieltageSerienGruppenUS = gruppen
                    .OrderByDescending(g => g.AnzahlUntentschieden)
                    .ThenByDescending(g => g.Saison)
                    .ToList();

                AktuelleGruppeIndexUS = 0;
                AktuelleUnentschiedenSerie = SpieltageSerienGruppenUS.FirstOrDefault();
                AlleSpieltageUS = SerieUnentschiedenVerein2;

                return SerieUnentschiedenVerein2;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
        

        public async Task<List<Spieltag>> GetSiegeSerienVerein()
        {
            List<Spieltag> SpieltageSerie = null;
            IsLoading = true;
            try
            {
                SerieSiegeVerein = await TabelleService.SerieSiegeVerein(SpieltagService, Convert.ToInt32(VereinNr));

                var Spieltage = (await SpieltagService.GetSpieltage()).Where(x => x.LigaID == vereinligaid);
                SpieltageSerie = new List<Spieltag>();
                foreach (var serie in SerieSiegeVerein)
                {                    
                    foreach (string id in serie.SpieltagIDs.Split(',').Select(s => s.Trim()))
                    {
                        var spieltag = Spieltage.FirstOrDefault(s => s.SpieltagId.ToString() == id);

                        if (spieltag != null)
                        {
                            spieltag.AnzahlSiege = serie.AnzahlSiege;                            
                            SpieltageSerie.Add(spieltag);
                        }
                    }
                    
                }
                SerieSiegeVerein2 = SpieltageSerie;
                var gruppen = new List<SpieltageSerieGruppe>();

                foreach (var serie in SerieSiegeVerein)
                {
                    var spieltagIds = serie.SpieltagIDs.Split(',').Select(s => s.Trim());
                    var idSet = new HashSet<string>(spieltagIds);

                    var zugeordneteSpieltage = Spieltage
                        .Where(s => idSet.Contains(s.SpieltagId.ToString()))
                        .Where(s => s.LigaID == vereinligaid)
                        .ToList();

                    if (zugeordneteSpieltage.Any())
                    {
                        gruppen.Add(new SpieltageSerieGruppe
                        {
                            Saison = serie.Saison,
                            AnzahlSiege = serie.AnzahlSiege,
                            Spieltage = zugeordneteSpieltage
                        });
                    }
                }

                SpieltageSerienGruppen = gruppen
                    .OrderByDescending(g => g.AnzahlSiege)
                    .ThenByDescending(g => g.Saison)                    
                    .ToList();

                AktuelleGruppeIndex = 0;
                AktuelleSiegesSerie = SpieltageSerienGruppen.FirstOrDefault();
                AlleSpieltage = SerieSiegeVerein2;

               
                return SerieSiegeVerein2;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
        public async Task<List<Spieltag>> GetNiederlagenSerienVerein()
        {
            List<Spieltag> SpieltageSerie = null;
            IsLoading = true;
            try
            {
                SerieNiederlagenVerein = await TabelleService.SerieNiederlagenVerein(SpieltagService, Convert.ToInt32(VereinNr));

                var Spieltage = (await SpieltagService.GetSpieltage()).Where(x => x.LigaID == vereinligaid);
                SpieltageSerie = new List<Spieltag>();
                foreach (var serie in SerieNiederlagenVerein)
                {
                    foreach (string id in serie.SpieltagIDs.Split(',').Select(s => s.Trim()))
                    {
                        var spieltag = Spieltage.FirstOrDefault(s => s.SpieltagId.ToString() == id);

                        if (spieltag != null)
                        {
                            spieltag.AnzahlNiederlagen = serie.AnzahlNiederlagen;
                            SpieltageSerie.Add(spieltag);
                        }
                    }

                }
                SerieNiederlagenVerein2 = SpieltageSerie;
                var gruppen = new List<SpieltageSerieGruppeNL>();

                foreach (var serie in SerieNiederlagenVerein)
                {
                    var spieltagIds = serie.SpieltagIDs.Split(',').Select(s => s.Trim());
                    var idSet = new HashSet<string>(spieltagIds);

                    var zugeordneteSpieltage = Spieltage
                        .Where(s => idSet.Contains(s.SpieltagId.ToString()))
                        .Where(s => s.LigaID == vereinligaid)
                        .ToList();

                    if (zugeordneteSpieltage.Any())
                    {
                        gruppen.Add(new SpieltageSerieGruppeNL
                        {
                            Saison = serie.Saison,
                            AnzahlNiederlagen = serie.AnzahlNiederlagen,
                            Spieltage = zugeordneteSpieltage
                        });
                    }
                }

                SpieltageSerienGruppenNL = gruppen
                    .OrderByDescending(g => g.AnzahlNiederlagen)
                    .ThenByDescending(g => g.Saison)
                    .ToList();

                AktuelleGruppeIndex = 0;
                AktuelleNiederlagenSerie = SpieltageSerienGruppenNL.FirstOrDefault();
                AlleSpieltageNL = SerieNiederlagenVerein2;

                return SerieNiederlagenVerein2;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        protected IEnumerable<Spieltag> GefilterteSpieltage => AlleSpieltage
        .Where(s => s.Saison == AktuelleGruppeSi.Saison && s.AnzahlSiege == AktuelleGruppeSi.AnzahlSiege);

        
        protected async Task NächsteGruppe()
        {
            if (AktuelleGruppeIndex < SpieltageSerienGruppen.Count - 1)
            {
                AktuelleGruppeIndex++;
                AktuelleSiegesSerie = SpieltageSerienGruppen[AktuelleGruppeIndex];
            }
        }

        protected async Task VorherigeGruppe()
        {
            if (AktuelleGruppeIndex > 0)
            {
                AktuelleGruppeIndex--;
                AktuelleSiegesSerie = SpieltageSerienGruppen[AktuelleGruppeIndex];
            }
        }

        protected async Task NächsteGruppeNL()
        {
            if (AktuelleGruppeIndexNL < SpieltageSerienGruppen.Count - 1)
            {
                AktuelleGruppeIndexNL++;
                AktuelleNiederlagenSerie = SpieltageSerienGruppenNL[AktuelleGruppeIndexNL];
            }
        }

        protected async Task VorherigeGruppeNL()
        {
            if (AktuelleGruppeIndexNL > 0)
            {
                AktuelleGruppeIndexNL--;
                AktuelleNiederlagenSerie = SpieltageSerienGruppenNL[AktuelleGruppeIndexNL];
            }
        }

        protected async Task NächsteGruppeUS()
        {
            if (AktuelleGruppeIndexUS < SpieltageSerienGruppen.Count - 1)
            {
                AktuelleGruppeIndexUS++;
                AktuelleUnentschiedenSerie = SpieltageSerienGruppenUS[AktuelleGruppeIndexUS];
            }
        }

        protected async Task VorherigeGruppeUS()
        {
            if (AktuelleGruppeIndexUS > 0)
            {
                AktuelleGruppeIndexUS--;
                AktuelleUnentschiedenSerie = SpieltageSerienGruppenUS[AktuelleGruppeIndexUS];
            }
        }

        public async Task<List<SpieltageSerien>> SerieSiegeVereinList()
        {
            try
            {
                SerieSiegeVerein = await TabelleService.SerieSiegeVerein(SpieltagService, Convert.ToInt32(VereinNr));

                return SerieSiegeVerein;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

    }
}

public class LVereinsinfo
{
    public string StatText { get; set; }
    public string Eigenschaft { get; set; }

}

