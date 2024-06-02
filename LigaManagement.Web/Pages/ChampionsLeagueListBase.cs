using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using static LigamanagerManagement.Web.Pages.EditPokalspieltagBase;

namespace LigaManagement.Web.Pages
{
    public class ChampionsLeagueListBase : ComponentBase
    {
        public RadzenDataGrid<PokalergebnisCLSpieltag> grid;
        public Density Density = Density.Compact;
        public bool allowVirtualization;
        public string Titel { get; set; }
        protected string DisplayErrorRunde = "none";
        protected string DisplayErrorSaison = "none";

        public List<DisplayRunde> RundeList;

        public int SaisonChoosed = 0;
        public string RundeChoosed;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public ISaisonenCLService SaisonenCLService { get; set; }

        public List<DisplaySaison> SaisonenList;

        public string VisibleBtnNew { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ISpieltageCLService SpieltageCLService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        public IEnumerable<PokalergebnisCLSpieltag> PokalergebnisseCLSpieltage { get; set; }

        public IEnumerable<PokalergebnisCLSpieltag> PokalergebnisseCLSpieltageFinale { get; set; }

        protected override async Task OnInitializedAsync()
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
                    string returnUrl = WebUtility.UrlEncode($"/");
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }

                SaisonenList = new List<DisplaySaison>();


                Saisonen = (await SaisonenCLService.GetSaisonen()).ToList();


                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                SaisonChoosed = Globals.PokalSaisonID;

                PokalergebnisseCLSpieltage = await SpieltageCLService.GetSpielergebnisse();

                if (PokalergebnisseCLSpieltage == null)
                    return;

                PokalergebnisseCLSpieltageFinale = PokalergebnisseCLSpieltage.ToList().OrderByDescending(x => x.Datum);

                PokalergebnisseCLSpieltage = PokalergebnisseCLSpieltage.ToList().Where(x => x.Saison == Globals.currentSaison);
                
                DisplayErrorRunde = "none";
                DisplayErrorSaison = "none";

                VisibleBtnNew = "hidden";

                if (Globals.currentPokalRunde == null)
                    RundeChoosed = null;
                else
                    RundeChoosed = Globals.currentPokalRunde;
                                
                Globals.bVisibleNavMenuElements = true;

                RundeList = new List<DisplayRunde>
                {
                    new DisplayRunde("G", "Gruppenphase"),                    
                      new DisplayRunde("AF", "Achtelfinale"),
                       new DisplayRunde("VF", "Viertelfinale"),
                       new DisplayRunde("HF", "Halbfinale"),
                        new DisplayRunde("F", "Finale")
                };


                if (Globals.currentPokalRunde != null)
                    OnClickHandler();
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }


        public async void SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                SaisonChoosed = Convert.ToInt32(e.Value);

                Globals.PokalSaisonID = SaisonChoosed;

                var saison = await SaisonenCLService.GetSaison(Convert.ToInt32(SaisonChoosed));

                Globals.currentPokalSaison = saison.Saisonname;

                OnClickHandler();
            }
        }
        public async void RundeChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                RundeChoosed = e.Value.ToString();
                Globals.currentPokalRunde = RundeChoosed;

                var PokalergebnisseCLSpieltage = await SpieltageCLService.GetSpielergebnisse();

                if (PokalergebnisseCLSpieltage == null)
                    return;

                PokalergebnisseCLSpieltage = PokalergebnisseCLSpieltage.ToList().Where(x => x.Saison == Globals.currentSaison);
                //PokalergebnisseCLSpieltage = PokalergebnisseSpieltage.Where(x => x.SaisonID == SaisonChoosed).OrderBy(x => x.Datum);

                VisibleBtnNew = NewButtonVisible();

                OnClickHandler();
            }
        }

        public async void OnClickHandler()
        {
            try
            {
                if (SaisonChoosed == 0 && RundeChoosed == null)
                {
                    DisplayErrorSaison = "block";
                    DisplayErrorRunde = "block";
                    return;
                }

                if (SaisonChoosed == 0)
                {
                    DisplayErrorSaison = "block";
                    DisplayErrorRunde = "none";
                    return;
                }

                if (RundeChoosed == null)
                {
                    DisplayErrorRunde = "block";
                    DisplayErrorSaison = "none";
                    return;
                }

                DisplayErrorSaison = "none";
                DisplayErrorRunde = "none";

                var PokalergebnisseSpieltage = await SpieltageCLService.GetSpielergebnisse();

                if (PokalergebnisseSpieltage == null)
                    return;

                PokalergebnisseSpieltage = PokalergebnisseSpieltage.Where(x => x.SaisonID == SaisonChoosed).OrderBy(x => x.Datum);
                
                VisibleBtnNew = NewButtonVisible();

                Globals.bVisibleNavMenuElements = true;
                StateHasChanged();
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        private string NewButtonVisible()
        {
            string sButtonVisible = "hidden";

            //var PokalergebnisseSpieltage = SpieltageCLService.GetSpielergebnisse();

            //if (PokalergebnisseSpieltage == null)
            //    return sButtonVisible;

            //PokalergebnisseSpieltage = PokalergebnisseSpieltage.ToList().Where(x => x.SaisonID == SaisonChoosed);
            //if (RundeChoosed == "F" && PokalergebnisseSpieltage.Count() >= 1)
            //    sButtonVisible = "hidden";
            //else if (RundeChoosed == "HF" && PokalergebnisseSpieltage.Count() >= 2)
            //    sButtonVisible = "hidden";
            //else if (RundeChoosed == "VF" && PokalergebnisseSpieltage.Count() >= 4)
            //    sButtonVisible = "hidden";
            //else if (RundeChoosed == "AF" && PokalergebnisseSpieltage.Count() >= 8)
            //    sButtonVisible = "hidden";
            //else if (RundeChoosed == "G" && PokalergebnisseSpieltage.Count() >= 32)
            //    sButtonVisible = "hidden";
            //else
            //    sButtonVisible = "visible";

            return sButtonVisible;

        }

        [Bind]
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



