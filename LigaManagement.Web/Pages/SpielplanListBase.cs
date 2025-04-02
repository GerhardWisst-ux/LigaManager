using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class SpielplanListBase : ComponentBase
    {
        [Parameter]
        public string SaisonID { get; set; }
               
        public string SpieltagNr { get; set; }
        public bool VisibleFooter;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        public RadzenDataGrid<Spielplan> spielplanGrid;
        public IList<Spieltag> orders;

        public string Titel;
        public Saison saison;
        public Density Density = Density.Default;
        public List<string> DensityValues = new List<string> { "Standard", "Kompakt" };

        public int iSpieltage;
        public bool IsLoading = false;
        public List<DisplaySaison> SaisonenList;

        [Inject]
        public ISaisonenService SaisonenService { get; set; }
        public bool VisibleBtnNew { get; set; }

        public RadzenDataGrid<Spielplan> grid;
        IList<Tuple<Spieltag, RadzenDataGridColumn<Spieltag>>> selectedCellData = new List<Tuple<Spieltag, RadzenDataGridColumn<Spieltag>>>();

        public Int32 currentspieltag;
        public IEnumerable<Saison> Saisonen { get; set; }
        public bool VisibleVorZurueck { get; set; }

        public List<DisplaySpielplan> SpielplanList;

        public IEnumerable<Tabelle> Tabellen { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public ISpielplanService SpielplanService { get; set; }
        
        [Inject]
        public IVereineService VereineService { get; set; }
        
        [Inject]
        public ILigaService LigaService { get; set; }

        [Inject]
        public IStringLocalizer<SpieltageList> Localizer { get; set; }
        public IEnumerable<Spielplan> Spielplaene { get; set; }
        public IEnumerable<Verein> Vereine { get; set; }
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {

            try
            {
                var authenticationState = await authenticationStateTask;
                if (authenticationState.User.Identity == null)
                {
                    return;
                }

                if (!authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/Ligamanager");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                }

                IsLoading = true;

                SpieltagNr = "1";

                saison = await SaisonenService.GetSaison(Convert.ToInt32(SaisonID));
                                
                SaisonenList = new List<DisplaySaison>();

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }
                await DisplaySpielplanAkt();

                Titel = Localizer["Spielplan"].Value + " " + saison.Saisonname + " " + SpieltagNr + ". " + Localizer["Spieltag"].Value;

                IsLoading = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                IsLoading = false;
            }
        }

        protected async Task DisplaySpielplanAkt()
        {

            if (Globals.LigaNummer == 1)
            {
                if (saison.Saisonname == "1963/64" || saison.Saisonname == "1964")
                    iSpieltage = 30;
                else if (saison.Saisonname == "1991/92")
                    iSpieltage = 38;
                else
                    iSpieltage = 34;
            }
            else if (Globals.LigaNummer == 2)
            {
                if (saison.Saisonname == "1993/94")
                    iSpieltage = 38;
                else
                    iSpieltage = 34;
            }
            else if (Globals.LigaNummer == 3)
            {
                iSpieltage = 38;

            }
         
            SpielplanList = new List<DisplaySpielplan>();

            for (int i = 1; i <= iSpieltage; i++)
            {
                SpielplanList.Add(new DisplaySpielplan(i.ToString(), i.ToString() + "." + Localizer["Spieltag"].Value));
            }

            if (Globals.LigaNummer < 3)
            {
                Vereine = await VereineService.GetVereine();

                Spielplaene = (await SpielplanService.GetSpielplaene()).Where(st => st.SaisonID == Convert.ToInt32(SaisonID)  && st.SpieltagNr == SpieltagNr).ToList();
                Spielplaene = Spielplaene.OrderBy(o => Convert.ToInt32(o.SpieltagNr));
                
                for (int i = 0; i < Spielplaene.Count(); i++)
                {
                    var columns = Spielplaene.ElementAt(i);

                    if (Vereine == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;                    
                    columns.Verein1Anzeige = columns.Verein1;
                    columns.Verein2Anzeige = columns.Verein2;
                    columns.Doppelpunkt = ":";
                }
            }
            else if (Globals.LigaNummer == 3)
            {
                Vereine = await VereineService.GetVereineL3();

                Spielplaene = (await SpielplanService.GetSpielplaeneL3()).Where(st => st.SaisonID == Convert.ToInt32(SaisonID)).ToList();
                Spielplaene = Spielplaene.OrderBy(o => o.Datum);

                for (int i = 0; i < Spielplaene.Count(); i++)
                {
                    var columns = Spielplaene.ElementAt(i);

                    if (Vereine == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;                    
                    columns.Verein1Anzeige = columns.Verein1;
                    columns.Verein2Anzeige = columns.Verein2;
                    columns.Doppelpunkt = ":";
                }
            }                      

            
            if (Globals.LigaNummer == 1)
            {
                if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
                {
                    if (Spielplaene.Count() >= 8)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else if (Globals.currentSaison == "1991/92")
                {
                    if (Spielplaene.Count() >= 10)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else
                {
                    if (Spielplaene.Count() >= 9)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
            }
            else if (Globals.LigaNummer == 2)
            {
                if (Globals.currentSaison == "1993/94")
                {
                    if (Spielplaene.Count() >= 10)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else
                {
                    if (Spielplaene.Count() >= 9)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
            }
            else if (Globals.LigaNummer == 3)
            {
                if (Spielplaene.Count() >= 10)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }          
            else
                VisibleBtnNew = true;

            if (Spielplaene.Count() == 0)
            {
                VisibleVorZurueck = false;
                VisibleFooter = false;
            }
            else
            {
                VisibleFooter = true;
                VisibleVorZurueck = true;
            }

            if (Spielplaene.Any())
                VisibleVorZurueck = true;
            else
                VisibleVorZurueck = false;

        }
        public async Task SpieltagChange(ChangeEventArgs e)
        {
            try
            {
                if (e.Value != null)
                {
                    IsLoading = true;
                    SpieltagNr = e.Value.ToString();
                    currentspieltag = Convert.ToInt32(e.Value);

                    await DisplaySpielplanAkt();

                    Titel = Localizer["Spielplan"].Value + " " + saison.Saisonname + " " + SpieltagNr + ". " + Localizer["Spieltag"].Value;
                    if (Spielplaene.Any())
                        VisibleVorZurueck = true;
                    else
                        VisibleVorZurueck = false;

                    IsLoading = false;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

            }
        }
        public async Task SpieltagVor()
        {
            IsLoading = true;
            if (Convert.ToInt32(SpieltagNr) < Globals.maxSpieltag)
                SpieltagNr = (Convert.ToInt32(SpieltagNr) + 1).ToString();

            SpieltagNr = SpieltagNr.ToString();

            await DisplaySpielplanAkt();

            Titel = Localizer["Spielplan"].Value + " " + saison.Saisonname + " " + SpieltagNr + ". " + Localizer["Spieltag"].Value;

            IsLoading = false;
            StateHasChanged();
        }


        public async Task SpieltagZurueck()
        {
            IsLoading = true;
            if (Convert.ToInt32(SpieltagNr) > 1)
                SpieltagNr = (Convert.ToInt32(SpieltagNr) - 1).ToString();
            else
                return;

            SpieltagNr = SpieltagNr.ToString();

            await DisplaySpielplanAkt();

            Titel = Localizer["Spielplan"].Value + " " + saison.Saisonname + " " + SpieltagNr + ". " + Localizer["Spieltag"].Value;

            IsLoading = false;
            StateHasChanged();
        }



        public class DisplaySpielplan
        {
            public DisplaySpielplan(string nummer, string name)
            {
                Nummer = nummer;
                Name = name;
            }
            public string Nummer { get; set; }
            public string Name { get; set; }

        }      

    }

}
