using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
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

namespace LigaManagerManagement.Web.Pages
{
    public class SpieltagListBase : ComponentBase
    {
        public RadzenDataGrid<Spieltag> spieltageGrid;
        public IList<Spieltag> orders;
                
        public string Liganame;
        public string curentsaison;
        public Density Density = Density.Default;
        bool bAbgeschlossen;

        string type = "Click";
        bool multiple = true;
        public int iSpieltage;
        public List<DisplaySaison> SaisonenList;

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        [Parameter]
        public string SpieltagNr { get; set; }
        public bool VisibleBtnNew { get; set; }

        public RadzenDataGrid<Spieltag> grid;
        IList<Tuple<Spieltag, RadzenDataGridColumn<Spieltag>>> selectedCellData = new List<Tuple<Spieltag, RadzenDataGridColumn<Spieltag>>>();

        public Int32 currentspieltag;
        public IEnumerable<Saison> Saisonen { get; set; }
        public bool VisibleVorZurueck { get; set; }

        public List<DisplaySpieltag> SpieltagList;

        public IEnumerable<Tabelle> Tabellen { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        public IEnumerable<Spieltag> Spieltage { get; set; }
        public IEnumerable<Verein> Vereine { get; set; }
        public NavigationManager NavigationManager { get; set; }


        protected async override Task OnInitializedAsync()
        {

            try
            {
                var authenticationState = await authenticationStateTask;
                if (!authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/spieltage/1");
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }

                SpieltagList = new List<DisplaySpieltag>();

                for (int i = 1; i <= Globals.maxSpieltag; i++)
                {
                    SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
                }

                Vereine = await VereineService.GetVereine();

                Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString()).Where(st => st.Saison == Globals.currentSaison).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);
                    columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                    columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
                }

                if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
                {
                    if (Spieltage.Count() >= 8)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else if (Globals.currentSaison == "1991/92")
                {
                    if (Spieltage.Count() >= 10)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else
                {
                    if (Spieltage.Count() >= 9)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }

                if (Spieltage.Count() == 0)
                    VisibleVorZurueck = false;
                else
                    VisibleVorZurueck = true;

                SpieltagNr = Globals.Spieltag.ToString();

                var liga = await LigaService.GetLiga(Convert.ToInt32(Globals.currentLiga));

                Liganame = liga.Liganame;

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

                curentsaison = Globals.currentSaison;
            }
            catch (Exception ex)
            {

                Debug.Print(ex.Message);
            }
        }

        public async Task SpieltagChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                SpieltagNr = e.Value.ToString();

                int SpieltagNr2 = Convert.ToInt32(e.Value);
                Globals.Spieltag = SpieltagNr2;
                Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString()).Where(st => st.Saison == Ligamanager.Components.Globals.currentSaison).ToList();
                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);
                    columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                    columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
                }


                if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
                {
                    if (Spieltage.Count() >= 8)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else if (Globals.currentSaison == "1991/92")
                {
                    if (Spieltage.Count() >= 10)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else
                {
                    if (Spieltage.Count() >= 9)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }


                if (Spieltage.Count() == 0)
                    VisibleVorZurueck = false;
                else
                    VisibleVorZurueck = true;

                currentspieltag = Convert.ToInt32(e.Value);

                StateHasChanged();
            }
        }
        public async Task SpieltagZurueck()
        {
            if (Convert.ToInt32(SpieltagNr) > 1)
                SpieltagNr = (Convert.ToInt32(SpieltagNr) - 1).ToString();
            else
                return;


            Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString()).Where(st => st.Saison == Globals.currentSaison).ToList();
            for (int i = 0; i < Spieltage.Count(); i++)
            {
                var columns = Spieltage.ElementAt(i);
                columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
            }

            int SpieltagNr2 = Convert.ToInt32(SpieltagNr);
            Globals.Spieltag = SpieltagNr2;

            if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
            {
                if (Spieltage.Count() >= 8)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }
            else if (Globals.currentSaison == "1991/92")
            {
                if (Spieltage.Count() >= 10)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }
            else
            {
                if (Spieltage.Count() >= 9)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }

            if (Spieltage.Count() == 0)
                VisibleVorZurueck = false;
            else
                VisibleVorZurueck = true;

            StateHasChanged();
        }

        public async Task SpieltagVor()
        {
            if (Convert.ToInt32(SpieltagNr) < Globals.maxSpieltag)
                SpieltagNr = (Convert.ToInt32(SpieltagNr) + 1).ToString();


            Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString()).Where(st => st.Saison == Globals.currentSaison).ToList();
            for (int i = 0; i < Spieltage.Count(); i++)
            {
                var columns = Spieltage.ElementAt(i);
                columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
            }

            int SpieltagNr2 = Convert.ToInt32(SpieltagNr);
            Globals.Spieltag = SpieltagNr2;

            if (Spieltage.Count() >= 9)
                VisibleBtnNew = false;
            else
                VisibleBtnNew = true;


            if (Spieltage.Count() == 0)
                VisibleVorZurueck = false;
            else
                VisibleVorZurueck = true;

            StateHasChanged();
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

        public void OnCellRender(DataGridCellRenderEventArgs<Spieltag> args)
        {
            if (selectedCellData.Any(i => i.Item1 == args.Data && i.Item2 == args.Column))
            {
                args.Attributes.Add("style", $"background-color: var(--rz-secondary-lighter);");
            }
        }

        protected async Task Delete_Click()
        {
            //DeleteConfirmation.Show();

            //await SpieltagService.DeleteSpieltag(Spieltag.SpieltagId);
            //await OnSpieltagDeleted.InvokeAsync((int)Spieltag.SpieltagId);

            //NavigationManager.NavigateTo(($"/spieltage?spieltag={Globals.Spieltag}"));
        }

        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            //if (deleteConfirmed)
            //{
            //    await SpieltagService.DeleteSpieltag(Spieltag.SpieltagId);
            //    await OnSpieltagDeleted.InvokeAsync((int)Spieltag.SpieltagId);
            //}

            //NavigationManager.NavigateTo(($"/spieltage?spieltag={Globals.Spieltag}"));
        }
              

        public async Task SaisonChange(ChangeEventArgs e)
        {
            int iSpieltage;

            if (e.Value != null)
            {
                curentsaison = e.Value.ToString();
                Globals.currentSaison = curentsaison;

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

                curentsaison = Globals.currentSaison;
                currentspieltag = SpieltagList.Count;               

                Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString()).Where(st => st.Saison == Globals.currentSaison).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                StateHasChanged();
            }
        }      
    }
}
