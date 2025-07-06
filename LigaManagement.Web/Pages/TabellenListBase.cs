using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using static LigaManagement.Web.Pages.EinstiegListBase;

namespace LigamanagerManagement.Web.Pages
{
    public class TabellenListBase : ComponentBase
    {
        [Parameter]
        public string CurrentligaUrl { get; set; }

        public string DisplayEwig = "display:none;";
        protected string DisplayErrorLiga = "none";
        public Int32 currentspieltag;
        public string saison;
        public string Liganame;
        public int TabArt;
        public int LigaID;
        public bool IsLoading = false;

        public RadzenDataGrid<Tabelle> grid;

        NotificationService NotificationService = new NotificationService();

        protected string DisplayElements = "none";

        public List<DisplaySpieltag> SpieltagList;

        public List<DisplayLiga> LigenList;

        public IEnumerable<Liga> Ligen { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public List<DisplaySaison> SaisonenList;


        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public ISpieltageBEService SpieltagBEService { get; set; }

        [Inject]
        public ISpieltageENService SpieltagENService { get; set; }

        [Inject]
        public ISpieltageFRService SpieltagFRService { get; set; }

        [Inject]
        public ISpieltageITService SpieltagITService { get; set; }

        [Inject]
        public ISpieltageESService SpieltagESService { get; set; }

        [Inject]
        public ISpieltageNLService SpieltagNLService { get; set; }

        [Inject]
        public ISpieltagePTService SpieltagPTService { get; set; }

        [Inject]
        public ISpieltageTUService SpieltagTUService { get; set; }

        [Inject]
        public ISpieltagAusService SpieltagAusService { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereinePLService VereineServicePL { get; set; }

        [Inject]
        public IVereineAusService VereineAusService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        [Inject]
        public IVereineSaisonAusService VereineSaisonAusService { get; set; }

        public IEnumerable<Tabelle> Tabellen { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        public IEnumerable<VereinAktSaison> VereineL3 { get; set; }

        public IEnumerable<VereinAUS> VereineA { get; set; }

        public IEnumerable<VereinAUS> VereineAus { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        [Inject]
        public IStringLocalizer<TabellenList> Localizer { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private Saison saisonFormat;

        int iMaxSpieltag = 0;
        int iSpieltage = 34;
        bool bAbgeschlossen;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authenticationState = await authenticationStateTask;

                if (authenticationState.User.Identity == null || !authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode("/Ligamanager/tabellen");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                    return;
                }

                IsLoading = true;

                var ligenTask = LigaService.GetLigen();
                var saisTask = SaisonenService.GetSaisonen();

                await Task.WhenAll(ligenTask, saisTask);

                Ligen = (await ligenTask).ToList();
                Saisonen = (await saisTask).Where(x => x.LigaID == Globals.LigaID).ToList();

                LigenList = Ligen.Select(l => new DisplayLiga(l.Aktiv, l.LandID, l.LandID, l.Liganame, l.EMWM)).ToList();
                SaisonenList = Saisonen.Select(s => new DisplaySaison(s.SaisonID, Globals.LigaID, s.Saisonname)).ToList();

                iSpieltage = ErmittlenAktSpieltag();
                SpieltagList = Enumerable.Range(1, iSpieltage)
                    .Select(i => new DisplaySpieltag(i.ToString(), $"{i}. {Localizer["Spieltag"].Value}"))
                    .ToList();

                currentspieltag = new SpieltageRepository().AktSpieltag(Globals.SaisonID, Globals.LigaID);

                saison = Globals.currentSaison;
                var aktuelleSaison = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison);
                bAbgeschlossen = aktuelleSaison?.Abgeschlossen ?? false;

                await TabelleBerechnen(1);

                DisplayElements = "none";
                var liga = await LigaService.GetLiga(Globals.LigaID);
                Liganame = liga.Liganame;
                saisonFormat = await SaisonenService.GetSaison(Globals.SaisonID);

                IsLoading = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }


        public void CellRender(DataGridCellRenderEventArgs<Tabelle> args)
        {
            if (saisonFormat.Ligahoehe == 1)
            {
                //if (args.Data.Platz == 6)
                //    Debug.Print("EL");

                if (args.Data.Platz > saisonFormat.AnzahlVereine - saisonFormat.Absteiger)
                {
                    args.Attributes.Add("style", $"background-color:lightcoral;");
                }
                else if (args.Data.Platz == saisonFormat.AnzahlVereine - saisonFormat.Absteiger && saisonFormat.Relegation > 0)
                    args.Attributes.Add("style", $"background-color:orange;");
                else if (args.Data.Platz > 1 && args.Data.Platz <= saisonFormat.CL_League)
                {
                    args.Attributes.Add("style", $"background-color:springgreen;");
                }
                else if (args.Data.Platz > saisonFormat.CL_League && args.Data.Platz <= saisonFormat.CL_League  + saisonFormat.EL_League)
                {
                    args.Attributes.Add("style", $"background-color:lightblue;");
                }
                else if (args.Data.Platz == saisonFormat.CL_League + saisonFormat.EL_League + saisonFormat.CF_League)
                {
                    args.Attributes.Add("style", $"background-color:coral;");
                }
                else if (args.Data.Platz == 1)
                    args.Attributes.Add("style", $"background-color:gold;");
                else
                    args.Attributes.Add("style", $"background-color:wheat;");
            }
            else if (saisonFormat.Ligahoehe == 2)
            {
                if (args.Data.Platz > saisonFormat.AnzahlVereine - saisonFormat.Absteiger)
                {
                    args.Attributes.Add("style", $"background-color:lightcoral;");
                }
                else if (args.Data.Platz == 3 || (args.Data.Platz == saisonFormat.AnzahlVereine - (saisonFormat.Absteiger + saisonFormat.Relegation) && saisonFormat.Relegation > 0))
                    args.Attributes.Add("style", $"background-color:lightgreen;");
                else if (args.Data.Platz == saisonFormat.AnzahlVereine - saisonFormat.Absteiger)
                    args.Attributes.Add("style", $"background-color:orange;");
                else if (args.Data.Platz == 1 || args.Data.Platz == 2)
                    args.Attributes.Add("style", $"background-color:gold;");
                else
                    args.Attributes.Add("style", $"background-color:wheat;");

            }
            else if (saisonFormat.Ligahoehe == 3)
            {
                if (args.Data.Platz > saisonFormat.AnzahlVereine - saisonFormat.Absteiger)
                {
                    args.Attributes.Add("style", $"background-color:lightcoral;");
                }
                else if (args.Data.Platz == 3 || (args.Data.Platz == saisonFormat.AnzahlVereine - (saisonFormat.Absteiger + saisonFormat.Relegation) && saisonFormat.Relegation > 0))
                    args.Attributes.Add("style", $"background-color:lightgreen;");                
                else if (args.Data.Platz == 1 || args.Data.Platz == 2)
                    args.Attributes.Add("style", $"background-color:gold;");
                else
                    args.Attributes.Add("style", $"background-color:wheat;");
            }

            //args.Attributes.Add("style", $"background-color: coral: {(args.Data.Platz > 1 && args.Data.Platz < 6 ? "blue" : "white")};");
            //args.Attributes.Add("style", $"background-color: coral: {(args.Data.Platz == 16 ? "coral" : "white")};");
            //args.Attributes.Add("style", $"background-color: coral: {(args.Data.Platz == 17 || args.Data.Platz == 18 ? "red" : "white")};");

        }
        private int ErmittlenAktSpieltag()
        {
            var spieltagMapping = new Dictionary<int, Func<int>>()
                {
                    { 1, () => Globals.currentSaison.StartsWith("1963") || Globals.currentSaison.StartsWith("1964") ? 30 : Globals.currentSaison.StartsWith("1991") ? 38 : 34 },
                    { 2, () => Globals.currentSaison.StartsWith("1993") ? 38 : 34 },
                    { 3, () => 38 },
                    { 4, () => Globals.currentSaison.StartsWith("1993") || Globals.currentSaison.StartsWith("1994") ? 42 : 38 },
                    { 5, () => int.Parse(Globals.currentSaison.Substring(0, 4)) > 2003 ? 38 : 34 },
                    { 6, () => Globals.currentSaison.StartsWith("1993") || Globals.currentSaison.StartsWith("1994") ? 42 : 38 },
                    { 7, () => Globals.currentSaison.StartsWith("1995") || Globals.currentSaison.StartsWith("1996") ? 42 : 38 },
                    { 8, () => 34 },
                    { 9, () => int.Parse(Globals.currentSaison.Substring(0, 4)) > 2013 ? 34 : 30 },
                    { 10, () => int.Parse(Globals.currentSaison.Substring(0, 4)) > 2019 ? 38 : 34 },
                    { 11, () => int.Parse(Globals.currentSaison.Substring(0, 4)) > 2022 ? 30 : int.Parse(Globals.currentSaison.Substring(0, 4)) > 2020 ? 34 : 30 },
                    { 12, () => 46 },
                    { 20, () => 34 },
                    { 21, () => 34 }
                };

            return spieltagMapping.TryGetValue(Globals.LigaNummer, out var func) ? func() : 34;
        }

        public async Task SaisonChange(ChangeEventArgs e)
        {
            int iSpieltag = 34;

            try
            {

                if (e.Value != null)
                {
                    IsLoading = true;
                    saison = e.Value.ToString();
                    Globals.currentSaison = saison;
                    SpieltagList = new List<DisplaySpieltag>();
                    SaisonenList = new List<DisplaySaison>();

                    Globals.maxSpieltag = iSpieltag;

                    Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                    Saisonen = Saisonen.Where(x => x.LigaID == Globals.LigaID);

                    for (int i = 0; i < Saisonen.Count(); i++)
                    {
                        var columns = Saisonen.ElementAt(i);
                        SaisonenList.Add(new DisplaySaison(columns.SaisonID, Globals.LigaID, columns.Saisonname));

                        if (columns.Saisonname == saison)
                            Globals.SaisonID = columns.SaisonID;
                    }

                    saison = Globals.currentSaison;
                    iSpieltag = ErmittlenAktSpieltag();

                    for (int i = 1; i <= iSpieltag; i++)
                    {
                        SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
                    }

                    Vereine = await VereineService.GetVereine();


                    await TabelleBerechnen((int)Globals.Tabart.Gesamt);

                    DisplayElements = "block";

                    if (TabArt == 6)
                        DisplayEwig = "display:block;";
                    else
                        DisplayEwig = "display:none;";

                    DisplayErrorLiga = "display:none;";

                    IsLoading = false;
                    StateHasChanged();

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

            }
        }
        public async Task SpieltagChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                IsLoading = true;
                currentspieltag = Convert.ToInt32(e.Value);
                iSpieltage = currentspieltag;
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

                await TabelleBerechnen(1);

                DisplayElements = "block";

                TabArt = 1;

                IsLoading = false;
                StateHasChanged();
            }
        }

        public async Task SpieltagZurueck()
        {
            IsLoading = true;
            if (currentspieltag > 1)
                currentspieltag--;

            iSpieltage = currentspieltag;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            await TabelleBerechnen(1);

            DisplayElements = "block";
            IsLoading = false;
            StateHasChanged();
        }

        public async Task SpieltagVor()
        {
            IsLoading = true;
            if (currentspieltag < Globals.maxSpieltag)
                currentspieltag++;

            iSpieltage = currentspieltag;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            await TabelleBerechnen(1);

            DisplayElements = "block";
            IsLoading = false;
            StateHasChanged();
        }

        public async Task TabArtChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                TabArt = Convert.ToInt32(e.Value);

                await TabelleBerechnen(TabArt);

                DisplayElements = "block";

                if (TabArt == 6)
                    DisplayEwig = "display:block;";
                else
                    DisplayEwig = "display:none;";

                DisplayErrorLiga = "display:none;";

                StateHasChanged();
            }
        }

        private async Task TabelleBerechnen(int TabArt)
        {

            try
            {
                if (Globals.LigaNummer < 3)
                {
                    IsLoading = true;
                    var vereineSaison = await VereineSaisonService.GetVereineSaison();
                    Vereine = await VereineService.GetVereine();
                    List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == Globals.SaisonID).ToList();


                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, verList, Vereine, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, verList, Vereine, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, verList, Vereine, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, verList, Vereine, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, verList, Vereine, SpieltagList.Count, (int)Globals.Tabart.Rückrunde);
                    else if (TabArt == 6)
                        Tabellen = await TabelleService.BerechneTabelleEwig(SpieltagService, SaisonenService, Vereine, iSpieltage, (int)Globals.Tabart.EwigeTabelle);

                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineService.GetVerein((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }
                }
                if (Globals.LigaNummer == 3 || Globals.LigaNummer == 20 || Globals.LigaNummer == 21)
                {
                    var vereineSaison = await SpieltagService.GetVereineL3();
                    Vereine = await VereineService.GetVereine();
                    List<VereinAktSaison> verList = vereineSaison.Where(x => x.SaisonID == Globals.SaisonID).ToList();

                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelleDEL3(SpieltagService, bAbgeschlossen, verList, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelleDEL3(SpieltagService, bAbgeschlossen, verList, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelleDEL3(SpieltagService, bAbgeschlossen, verList, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelleDEL3(SpieltagService, bAbgeschlossen, verList, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelleDEL3(SpieltagService, bAbgeschlossen, verList, SpieltagList.Count, (int)Globals.Tabart.Rückrunde);
                    //else if (TabArt == 6)
                    //    Tabellen = await TabelleService.BerechneTabelleEwig(SpieltagService, SaisonenService, Vereine, iSpieltage, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineService.GetVerein((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }

                }
                else if (Globals.LigaNummer == 4 || Globals.LigaNummer == 12)
                {
                    VereineAus = await VereineAusService.GetVereinePL();

                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Rückrunde);
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinPL((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }
                }
                else if (Globals.LigaNummer == 5)
                {
                    VereineAus = await VereineAusService.GetVereineIT();

                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Rückrunde);
                    //else if (TabArt == 6)
                    //    Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, SaisonenService, VereinePL, iSpieltage, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);

                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinIT((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }
                }
                else if (Globals.LigaNummer == 6)
                {
                    VereineAus = await VereineAusService.GetVereineFR();

                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Rückrunde);

                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinFR((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }
                }
                else if (Globals.LigaNummer == 7)
                {
                    VereineAus = await VereineAusService.GetVereineES();

                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Rückrunde);

                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinES((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }
                }
                else if (Globals.LigaNummer == 8)
                {
                    VereineAus = await VereineAusService.GetVereineNL();

                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Rückrunde);

                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinNL((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }
                }

                else if (Globals.LigaNummer == 9)
                {
                    VereineAus = await VereineAusService.GetVereinePT();

                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Rückrunde);

                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinPT((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }
                }
                else if (Globals.LigaNummer == 10)
                {
                    VereineAus = await VereineAusService.GetVereineTU();

                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Rückrunde);

                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinTU((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }
                }
                else if (Globals.LigaNummer == 11)
                {
                    VereineAus = await VereineAusService.GetVereineBE();

                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelleBE(SpieltagBEService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelleBE(SpieltagBEService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelleBE(SpieltagBEService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelleBE(SpieltagBEService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelleBE(SpieltagBEService, bAbgeschlossen, VereineAus, iSpieltage, (int)Globals.Tabart.Rückrunde);

                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinBE((int)item.VereinNr);
                        item.Verein = verein.Vereinsname1;
                    }
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

            }
        }

        public class DisplaySpieltag(string nummer, string name)
        {
            public string Nummer { get; set; } = nummer;
            public string Name { get; set; } = name;
        }

        public class DisplaySaison(int saisonID, int ligaID, string saisonname)
        {
            public int SaisonID { get; set; } = saisonID;
            public int LigaID { get; set; } = ligaID;
            public string Saisonname { get; set; } = saisonname;
        }
    }
}

