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

                if (authenticationState.User.Identity == null || !authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode("/Ligamanager");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                    return;
                }

                IsLoading = true;
                SpieltagNr = "1";

                
                // With this corrected code:
                var tasks = new Task<object>[]
                {
                    SaisonenService.GetSaison(Convert.ToInt32(SaisonID)).ContinueWith(t => (object)t.Result),
                    SaisonenService.GetSaisonen().ContinueWith(t => (object)t.Result)
                };                  

                var results = await Task.WhenAll(tasks);

                saison = results[0] as Saison;
                SaisonenList = (results[1] as IEnumerable<Saison>)?
                    .Select(s => new DisplaySaison(s.SaisonID, s.Saisonname))
                    .ToList();

                await DisplaySpielplanAkt();

                Titel = $"{Localizer["Spielplan"].Value} {saison?.Saisonname} {SpieltagNr}. {Localizer["Spieltag"].Value}";

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
            // Einstellen der Spieltage basierend auf LigaNummer und Saisonname
            iSpieltage = Globals.LigaNummer switch
            {
                1 => saison.Saisonname switch
                {
                    "1963/64" or "1964" => 30,
                    "1991/92" => 38,
                    _ => 34
                },
                2 => saison.Saisonname == "1993/94" ? 38 : 34,
                3 => 38,
                _ => 34
            };

            // Spielplan erstellen
            SpielplanList = Enumerable
                .Range(1, iSpieltage)
                .Select(i => new DisplaySpielplan(i.ToString(), $"{i}. {Localizer["Spieltag"].Value}"))
                .ToList();

            // Vereine und Spielpläne laden
            if (Globals.LigaNummer < 3)
            {
                Vereine = await VereineService.GetVereine();
                Spielplaene = (await SpielplanService.GetSpielplaene())
                    .Where(st => st.SaisonID == Convert.ToInt32(SaisonID) && st.SpieltagNr == SpieltagNr)
                    .OrderBy(st => Convert.ToInt32(st.SpieltagNr))
                    .ToList();
            }
            else
            {
                Vereine = await VereineService.GetVereineL3();
                Spielplaene = (await SpielplanService.GetSpielplaeneL3())
                    .Where(st => st.SaisonID == Convert.ToInt32(SaisonID))
                    .OrderBy(st => st.Datum)
                    .ToList();
            }

            // Vereine zuordnen
            if (Vereine == null)
                throw new Exception("Vereine sind null");

            foreach (var spiel in Spielplaene)
            {
                spiel.Verein1 = Vereine.FirstOrDefault(v => v.VereinNr == Convert.ToInt32(spiel.Verein1_Nr))?.Vereinsname1;
                spiel.Verein2 = Vereine.FirstOrDefault(v => v.VereinNr == Convert.ToInt32(spiel.Verein2_Nr))?.Vereinsname1;
                spiel.Verein1Anzeige = spiel.Verein1;
                spiel.Verein2Anzeige = spiel.Verein2;
                spiel.Doppelpunkt = ":";
            }

            // Sichtbarkeit des Buttons "Neu" einstellen
            int maxSpieltage = Globals.LigaNummer switch
            {
                1 when Globals.currentSaison is "1963/64" or "1964/65" => 8,
                1 when Globals.currentSaison == "1991/92" => 10,
                1 => 9,
                2 when Globals.currentSaison == "1993/94" => 10,
                2 => 9,
                3 => 10,
                _ => 0
            };

            VisibleBtnNew = Spielplaene.Count() < maxSpieltage;

            // Sichtbarkeit von Footer und Navigationselementen
            bool hatSpielplaene = Spielplaene.Any();
            VisibleFooter = hatSpielplaene;
            VisibleVorZurueck = hatSpielplaene;
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
