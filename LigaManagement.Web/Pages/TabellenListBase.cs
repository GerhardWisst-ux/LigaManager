using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

        public IEnumerable<Tabelle> Tabellen { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        public IEnumerable<VereinAUS> VereineAus { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        int iMaxSpieltag = 0;

        bool bAbgeschlossen;
        protected override async Task OnInitializedAsync()
        {
            int iSpieltage = 34;
            try
            {
                var authenticationState = await authenticationStateTask;

                if (authenticationState.User.Identity == null)
                {
                    return;
                }

                if (!authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/spieltage/{1}");
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }

                SpieltagList = new List<DisplaySpieltag>();
                SaisonenList = new List<DisplaySaison>();

                if (Globals.LigaID == 1)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1963" || Globals.currentSaison.Substring(0, 4) == "1964")
                        iSpieltage = 30;
                    else if (Globals.currentSaison.Substring(0, 4) == "1991")
                        iSpieltage = 38;
                    else
                        iSpieltage = 34;
                }
                else if (Globals.LigaID == 2)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1993")
                        iSpieltage = 38;
                    else
                        iSpieltage = 34;
                }
                else if (Globals.LigaID == 3)
                {
                    iSpieltage = 38;

                }
                else if (Globals.LigaID == 4)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1993" || Globals.currentSaison.Substring(0, 4) == "1994")
                        iSpieltage = 42;
                    else
                        iSpieltage = 38;
                }
                else if (Globals.LigaID == 6)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2003)
                        iSpieltage = 38;
                    else
                        iSpieltage = 34;
                }
                else if (Globals.LigaID == 7)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1993" || Globals.currentSaison.Substring(0, 4) == "1994")
                        iSpieltage = 42;
                    else
                        iSpieltage = 38;

                }
                else if (Globals.LigaID == 8)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1995" || Globals.currentSaison.Substring(0, 4) == "1996")
                        iSpieltage = 42;
                    else
                        iSpieltage = 38;
                }
                else if (Globals.LigaID == 9)
                {
                    iSpieltage = 34;
                }
                else if (Globals.LigaID == 10)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2013)
                        iSpieltage = 34;
                    else
                        iSpieltage = 30;
                }
                else if (Globals.LigaID == 11)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2019)
                        iSpieltage = 38;
                    else
                        iSpieltage = 34;
                }

                LigenList = new List<DisplayLiga>();
                Ligen = (await LigaService.GetLigen()).ToList();

                for (int i = 0; i < Ligen.Count(); i++)
                {
                    var columns = Ligen.ElementAt(i);
                    LigenList.Add(new DisplayLiga(columns.Aktiv, columns.Id, columns.LandID, columns.Liganame));
                }

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                Saisonen = Saisonen.Where(x => x.LigaID == Globals.LigaID);

                for (int i = 1; i <= iSpieltage; i++)
                {
                    SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
                }

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, Globals.LigaID, columns.Saisonname));
                }

                SpieltageRepository rep = new SpieltageRepository();

                currentspieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);

                saison = Globals.currentSaison;


                bool bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

                if (Globals.LigaID < 4)
                {
                    Vereine = await VereineService.GetVereine();
                    Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, Vereine, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineService.GetVerein((int)item.VereinNr);
                        item.Verein = verein.Vereinsname2;
                    }
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }
                else if (Globals.LigaID == 4)
                {
                    VereineAus = await VereineServicePL.GetVereine();

                    Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinPL((int)item.VereinNr);
                        item.Verein = verein.Vereinsname2;
                    }

                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }
                else if (Globals.LigaID == 6)
                {
                    VereineAus = await VereineAusService.GetVereineIT();
                    Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinIT((int)item.VereinNr);
                        item.Verein = verein.Vereinsname2;
                    }
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }
                else if (Globals.LigaID == 7)
                {
                    VereineAus = await VereineAusService.GetVereineFR();
                    Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinFR((int)item.VereinNr);
                        item.Verein = verein.Vereinsname2;
                    }
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }
                else if (Globals.LigaID == 8)
                {
                    VereineAus = await VereineAusService.GetVereineES();
                    Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinES((int)item.VereinNr);
                        item.Verein = verein.Vereinsname2;
                    }
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }
                else if (Globals.LigaID == 9)
                {
                    VereineAus = await VereineAusService.GetVereineNL();
                    Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinNL((int)item.VereinNr);
                        item.Verein = verein.Vereinsname2;
                    }
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }
                else if (Globals.LigaID == 10)
                {
                    VereineAus = await VereineAusService.GetVereinePT();
                    Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    int current = 0;
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinPT((int)item.VereinNr);
                        item.Verein = verein.Vereinsname2;
                    }
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }
                else if (Globals.LigaID == 11)
                {
                    VereineAus = await VereineAusService.GetVereineTU();
                    Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    int current = 0;
                    foreach (var item in Tabellen)
                    {
                        var verein = await VereineAusService.GetVereinTU((int)item.VereinNr);
                        item.Verein = verein.Vereinsname2;
                    }
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }




                DisplayElements = "none";

                var liga = await LigaService.GetLiga(Globals.LigaID);

                Liganame = liga.Liganame;

                TabArt = 1;

            }
            catch (Exception ex)
            {

                Debug.Print(ex.Message);
            }
        }

        public async Task SaisonChange(ChangeEventArgs e)
        {
            int iSpieltage = 34;

            if (e.Value != null)
            {
                saison = e.Value.ToString();
                Globals.currentSaison = saison;
                SpieltagList = new List<DisplaySpieltag>();
                SaisonenList = new List<DisplaySaison>();

                if (Globals.LigaID == 1)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1963" || Globals.currentSaison.Substring(0, 4) == "1964")
                        iSpieltage = 30;
                    else if (Globals.currentSaison.Substring(0, 4) == "1991")
                        iSpieltage = 38;
                    else
                        iSpieltage = 34;
                }
                else if (Globals.LigaID == 2)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1993")
                        iSpieltage = 38;
                    else
                        iSpieltage = 34;
                }
                else if (Globals.LigaID == 3)
                {
                    iSpieltage = 38;

                }
                else if (Globals.LigaID == 4)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1993" || Globals.currentSaison.Substring(0, 4) == "1994")
                        iSpieltage = 42;
                    else
                        iSpieltage = 38;
                }
                else if (Globals.LigaID == 6)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2003)
                        iSpieltage = 38;
                    else
                        iSpieltage = 34;
                }
                else if (Globals.LigaID == 7)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1993" || Globals.currentSaison.Substring(0, 4) == "1994")
                        iSpieltage = 42;
                    else
                        iSpieltage = 38;

                }
                else if (Globals.LigaID == 8)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1995" || Globals.currentSaison.Substring(0, 4) == "1996")
                        iSpieltage = 42;
                    else
                        iSpieltage = 38;
                }

                else if (Globals.LigaID == 9)
                {
                    iSpieltage = 34;
                }

                else if (Globals.LigaID == 10)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2013)
                        iSpieltage = 34;
                    else
                        iSpieltage = 30;
                }

                Globals.maxSpieltag = iSpieltage;

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                Saisonen = Saisonen.Where(x => x.LigaID == Globals.LigaID);

                for (int i = 1; i <= iSpieltage; i++)
                {
                    SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
                }

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, Globals.LigaID, columns.Saisonname));

                    if (columns.Saisonname == saison)
                        Globals.SaisonID = columns.SaisonID;
                }

                saison = Globals.currentSaison;

                Vereine = await VereineService.GetVereine();
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

                if (bAbgeschlossen)
                    currentspieltag = SpieltagList.Count;
                else
                {
                    SpieltageRepository rep = new SpieltageRepository();
                    currentspieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                }

                if (Globals.LigaID < 4)
                {
                    Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, Vereine, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                }
                if (Globals.LigaID == 4)
                {
                    Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                }
                if (Globals.LigaID == 6)
                {
                    Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                }
                if (Globals.LigaID == 7)
                {
                    Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                }
                if (Globals.LigaID == 8)
                {
                    Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                }
                if (Globals.LigaID == 9)
                {
                    Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                }
                if (Globals.LigaID == 10)
                {
                    Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                }
                if (Globals.LigaID == 11)
                {
                    Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                }

                DisplayElements = "block";

                if (TabArt == 6)
                    DisplayEwig = "display:block;";
                else
                    DisplayEwig = "display:none;";

                DisplayErrorLiga = "display:none;";

                StateHasChanged();

            }
        }
        public async Task SpieltagChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                currentspieltag = Convert.ToInt32(e.Value);
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

                await TabelleBerechnen(TabArt);

                DisplayElements = "block";
                StateHasChanged();
            }
        }

        public async Task SpieltagZurueck()
        {
            if (currentspieltag > 1)
                currentspieltag = currentspieltag - 1;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            await TabelleBerechnen(TabArt);

            DisplayElements = "block";
            StateHasChanged();
        }

        public async Task SpieltagVor()
        {
            if (currentspieltag < Globals.maxSpieltag)
                currentspieltag = currentspieltag + 1;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            await TabelleBerechnen(TabArt);

            DisplayElements = "block";
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
            if (Globals.LigaID < 3)
            {
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, Vereine, 17, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                else if (TabArt == 6)
                    Tabellen = await TabelleService.BerechneTabelleEwig(SpieltagService, SaisonenService, Vereine, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);
                
            }
            else if (Globals.LigaID == 4)
            {
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, 19, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                //else if (TabArt == 6)
                //    Tabellen = await TabelleService.BerechneTabellePL(SpieltagENService, SaisonenService, VereinePL, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);
                foreach (var item in Tabellen)
                {
                    var verein = await VereineAusService.GetVereinPL((int)item.VereinNr);
                    item.Verein = verein.Vereinsname2;
                }
            }
            else if (Globals.LigaID == 6)
            {
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabelleAus(SpieltagAusService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, 17, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                //else if (TabArt == 6)
                //    Tabellen = await TabelleService.BerechneTabelleIT(SpieltagITService, SaisonenService, VereinePL, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);

                foreach (var item in Tabellen)
                {
                    var verein = await VereineAusService.GetVereinIT((int)item.VereinNr);
                    item.Verein = verein.Vereinsname2;
                }
            }
            else if (Globals.LigaID == 7)
            {
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, 17, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabelleFR(SpieltagFRService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                //else if (TabArt == 6)
                //    Tabellen = await TabelleService.BerechneTabelleFR(SpieltagENService, SaisonenService, VereinePL, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);

                foreach (var item in Tabellen)
                {
                    var verein = await VereineAusService.GetVereinFR((int)item.VereinNr);
                    item.Verein = verein.Vereinsname2;
                }
            }
            else if (Globals.LigaID == 8)
            {
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, 17, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabelleES(SpieltagESService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                //else if (TabArt == 6)
                //    Tabellen = await TabelleService.BerechneTabellePL(SpieltagESService, SaisonenService, VereinePL, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);

                foreach (var item in Tabellen)
                {
                    var verein = await VereineAusService.GetVereinES((int)item.VereinNr);
                    item.Verein = verein.Vereinsname2;
                }
            }          
            else if (Globals.LigaID == 9)
            {
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, 17, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabelleNL(SpieltagNLService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                //else if (TabArt == 6)
                //    Tabellen = await TabelleService.BerechneTabellePL(SpieltagESService, SaisonenService, VereinePL, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);

                foreach (var item in Tabellen)
                {
                    var verein = await VereineAusService.GetVereinNL((int)item.VereinNr);
                    item.Verein = verein.Vereinsname2;
                }
            }

            else if (Globals.LigaID == 10)
            {
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, 17, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabellePT(SpieltagPTService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                //else if (TabArt == 6)
                //    Tabellen = await TabelleService.BerechneTabellePL(SpieltagESService, SaisonenService, VereinePL, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);

                foreach (var item in Tabellen)
                {
                    var verein = await VereineAusService.GetVereinPT((int)item.VereinNr);
                    item.Verein = verein.Vereinsname2;
                }
            }
            else if (Globals.LigaID == 11)
            {
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus,  currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, 19, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabelleTU(SpieltagTUService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                //else if (TabArt == 6)
                //    Tabellen = await TabelleService.BerechneTabellePL(SpieltagESService, SaisonenService, VereinePL, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);

                foreach (var item in Tabellen)
                {
                    var verein = await VereineAusService.GetVereinTU((int)item.VereinNr);
                    item.Verein = verein.Vereinsname2;
                }
            }
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
    }
}

