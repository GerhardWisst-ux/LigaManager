using LigaManagement.Api.Models;
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
using System.Diagnostics.Eventing.Reader;
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
        public ILigaService LigaService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        public IEnumerable<Tabelle> Tabellen { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

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

                if (Globals.currentSaison.Substring(0, 4) == "1963" || Globals.currentSaison.Substring(0, 4) == "1964")
                    iSpieltage = 30;
                else if (Globals.currentSaison.Substring(0, 4) == "1991")
                    iSpieltage = 38;
                else
                    iSpieltage = 34;

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                for (int i = 1; i <= iSpieltage; i++)
                {
                    SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
                }

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                SpieltageRepository rep = new SpieltageRepository();

                Globals.SaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).SaisonID;

                currentspieltag = rep.AktSpieltag(Globals.SaisonID); 

                saison = Globals.currentSaison;
                
                Vereine = await VereineService.GetVereine();

                bool bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

                Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, SpieltagList.Count, Ligamanager.Components.Globals.currentSaison, 1);

                DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);

                DisplayElements = "none";

                var liga = await LigaService.GetLiga(Convert.ToInt32(Globals.currentLiga));

                Liganame = liga.Liganame;

                if (TabArt == 6)
                    DisplayEwig = "display:block;";
                else
                    DisplayEwig = "display:none;";
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

                if (Globals.currentSaison.Substring(0, 4) == "1963" || Globals.currentSaison.Substring(0, 4) == "1964")
                    iSpieltage = 30;
                else if (Globals.currentSaison.Substring(0, 4) == "1991")
                    iSpieltage = 38;
                else
                    iSpieltage = 34;

                Globals.maxSpieltag = iSpieltage;

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                for (int i = 1; i <= iSpieltage; i++)
                {
                    SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
                }

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                saison = Globals.currentSaison;
                currentspieltag = SpieltagList.Count;
                Vereine = await VereineService.GetVereine();
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
                Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, SpieltagList.Count, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);

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
                Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);
                DisplayElements = "block";               
                StateHasChanged();
            }
        }

        public async Task SpieltagZurueck()
        {
            if (currentspieltag > 1)
                currentspieltag = currentspieltag - 1;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
            Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);
            DisplayElements = "block";
            StateHasChanged();
        }

        public async Task SpieltagVor()
        {
            if (currentspieltag < Globals.maxSpieltag)
                currentspieltag = currentspieltag + 1;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
            Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);
            DisplayElements = "block";
            StateHasChanged();
        }
        public async Task TabArtChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                int TabArt = Convert.ToInt32(e.Value);
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, 17, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen,Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Rückrunde);
                else if (TabArt == 6)
                    Tabellen = await TabelleService.BerechneTabelleEwig(SpieltagService, SaisonenService, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);

                DisplayElements = "block";

                if (TabArt == 6)
                    DisplayEwig = "display:block;";
                else
                    DisplayEwig = "display:none;";

                StateHasChanged();
            }
        }
        public void HeaderFooterCellRender(DataGridCellRenderEventArgs<Tabelle> args)
        {            
            //args.Attributes.Add("style", $"font-weight: bold;");
            
        }

        public void RowRender(RowRenderEventArgs<Tabelle> args)
        {
            //args.Attributes.Add("style", $"font-weight: {(args.Data.Platz == 5 || args.Data.Platz == 16 ? "bold" : "normal")};");

        }

        public void CellRender(DataGridCellRenderEventArgs<Tabelle> args)
        {
            //if (args.Column.Property == "Punkte")
            //{
            //    args.Attributes.Add("style", $"color: red;"); 
                
            //}

            //if (args.Column.Property == "OrderID")
            //{
            //    if (args.Data.OrderID == 10248 && args.Data.ProductID == 11 || args.Data.OrderID == 10250 && args.Data.ProductID == 41)
            //    {
            //        args.Attributes.Add("rowspan", 3);
            //    }

            //    if (args.Data.OrderID == 10249 && args.Data.ProductID == 14 || args.Data.OrderID == 10251 && args.Data.ProductID == 22)
            //    {
            //        args.Attributes.Add("rowspan", 2);
            //    }
            //}
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

            public DisplaySaison(int saisonID, string saisonname)
            {
                SaisonID = saisonID;
                Saisonname = saisonname;
            }
            public int SaisonID { get; set; }
            public string Saisonname { get; set; }
        }
    }
}

