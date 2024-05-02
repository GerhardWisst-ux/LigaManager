using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LigamanagerManagement.Web.Pages
{
    public class TabellenListBase : ComponentBase
    {
        public string DisplayEwig = "display:none;";
        public Int32 currentspieltag;
        public string saison;
        public string Liganame;
        public int TabArt;

        public RadzenDataGrid<Tabelle> grid;
        IList<Tuple<Tabelle, RadzenDataGridColumn<Tabelle>>> selectedCellData = new List<Tuple<Tabelle, RadzenDataGridColumn<Tabelle>>>();

        protected string DisplayElements = "none";

        public List<DisplaySpieltag> SpieltagList;

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
        public ISpieltagPLService SpieltagPLService { get; set; }

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

        public IEnumerable<VereinAUS> VereinePL { get; set; }

        public IEnumerable<VereinAUS> VereineAus { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        int iMaxSpieltag = 0;

        bool bAbgeschlossen;
        protected override async Task OnInitializedAsync()
        {
            int iSpieltage;
            try
            {
                var authenticationState = await authenticationStateTask;
                if (!authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/spieltage/1");
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
                    if (Globals.currentSaison == "1993/94" || Globals.currentSaison == "1994/95")                  
                        iSpieltage = 42;                  
                    else
                        iSpieltage = 38;
                }
                else if (Globals.LigaID == 6)
                {
                    iSpieltage = 38;

                }
                else
                {
                    iSpieltage = 38;
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

                Globals.SaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).SaisonID;

                currentspieltag = rep.AktSpieltag(Globals.SaisonID);

                saison = Globals.currentSaison;

                                

                bool bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

                if (Globals.LigaID < 4)
                {
                    Vereine = await VereineService.GetVereine();
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }
                else if(Globals.LigaID == 4)
                {
                    VereineAus = await VereineServicePL.GetVereine();

                    Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, bAbgeschlossen, VereinePL, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }
                else if (Globals.LigaID == 6)
                {
                    VereineAus = await VereineAusService.GetVereineIT();
                    Tabellen = await TabelleService.BerechneTabelleAus(SpieltagAusService, bAbgeschlossen, VereineAus, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);
                    DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);
                }

                DisplayElements = "none";

                var liga = await LigaService.GetLiga(Convert.ToInt32(Globals.currentLiga));

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
            int iSpieltage;

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
                else
                {
                    iSpieltage = 38;
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
                }

                saison = Globals.currentSaison;
                currentspieltag = SpieltagList.Count;
                Vereine = await VereineService.GetVereine();
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
                
                if (Globals.LigaID < 4)
                {
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);                    
                }
                else
                {
                    Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, bAbgeschlossen, VereinePL, SpieltagList.Count, Globals.currentSaison, Globals.LigaID, 1);                    
                }

                DisplayElements = "block";

                if (TabArt == 6)
                    DisplayEwig = "display:block;";
                else
                    DisplayEwig = "display:none;";

                StateHasChanged();

            }
        }
        public async Task SpieltagChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                currentspieltag = Convert.ToInt32(e.Value);
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
                Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                DisplayElements = "block";
                StateHasChanged();
            }
        }

        public async Task SpieltagZurueck()
        {
            if (currentspieltag > 1)
                currentspieltag = currentspieltag - 1;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
            if (Globals.LigaID < 4)
            {
                Vereine = await VereineService.GetVereine();
                Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, 1);
            }
            else if (Globals.LigaID == 4)
            {
                VereineAus = await VereineServicePL.GetVereine();

                Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, bAbgeschlossen, VereinePL, currentspieltag, Globals.currentSaison, Globals.LigaID, 1);

            }
            else if (Globals.LigaID == 6)
            {
                VereineAus = await VereineAusService.GetVereineIT();
                Tabellen = await TabelleService.BerechneTabelleAus(SpieltagAusService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, 1);
            }

            DisplayElements = "block";
            StateHasChanged();
        }

        public async Task SpieltagVor()
        {
            if (currentspieltag < Globals.maxSpieltag)
                currentspieltag = currentspieltag + 1;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            if (Globals.LigaID < 4)
            {
                Vereine = await VereineService.GetVereine();
                Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, 1);            
            }
            else if (Globals.LigaID == 4)
            {
                VereineAus = await VereineServicePL.GetVereine();

                Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, bAbgeschlossen, VereinePL, currentspieltag, Globals.currentSaison, Globals.LigaID, 1);
            
            }
            else if (Globals.LigaID == 6)
            {
                VereineAus = await VereineAusService.GetVereineIT();
                Tabellen = await TabelleService.BerechneTabelleAus(SpieltagAusService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, 1);            
            }

            DisplayElements = "block";
            StateHasChanged();
        }
        public async Task TabArtChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                int TabArt = Convert.ToInt32(e.Value);
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
               
                if (Globals.LigaID < 3)
                {
                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, 17, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                    else if (TabArt == 6)
                        Tabellen = await TabelleService.BerechneTabelleEwig(SpieltagService, SaisonenService, Vereine, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);
                }
                else if (Globals.LigaID ==4)
                {
                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, bAbgeschlossen, VereinePL, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, bAbgeschlossen, VereinePL, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, bAbgeschlossen, VereinePL, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, bAbgeschlossen, VereinePL, 17, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, bAbgeschlossen, VereinePL, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                    //else if (TabArt == 6)
                    //    Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, SaisonenService, VereinePL, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);
                }
                else if (Globals.LigaID == 6)
                {
                    if (TabArt == 1)
                        Tabellen = await TabelleService.BerechneTabelleAus(SpieltagAusService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Gesamt);
                    else if (TabArt == 2)
                        Tabellen = await TabelleService.BerechneTabelleAus(SpieltagAusService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Heim);
                    else if (TabArt == 3)
                        Tabellen = await TabelleService.BerechneTabelleAus(SpieltagAusService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Auswärts);
                    else if (TabArt == 4)
                        Tabellen = await TabelleService.BerechneTabelleAus(SpieltagAusService, bAbgeschlossen, VereineAus, 17, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Vorrunde);
                    else if (TabArt == 5)
                        Tabellen = await TabelleService.BerechneTabelleAus(SpieltagAusService, bAbgeschlossen, VereineAus, currentspieltag, Globals.currentSaison, Globals.LigaID, (int)Globals.Tabart.Rückrunde);
                    //else if (TabArt == 6)
                    //    Tabellen = await TabelleService.BerechneTabellePL(SpieltagPLService, SaisonenService, VereinePL, currentspieltag, Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);
                }

                DisplayElements = "block";

                if (TabArt == 6)
                    DisplayEwig = "display:block;";
                else
                    DisplayEwig = "display:none;";

                StateHasChanged();
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

