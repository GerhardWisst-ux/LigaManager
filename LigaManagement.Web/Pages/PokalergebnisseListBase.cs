using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using static LigamanagerManagement.Web.Pages.EditPokalspieltagBase;

namespace LigaManagement.Web.Pages
{
    public class PokalergebnisseListBase : ComponentBase
    {
        public RadzenDataGrid<PokalergebnisSpieltag> grid;
        public Density Density = Density.Compact;
        public bool allowVirtualization;
        public string Titel { get; set; }
        protected string DisplayErrorRunde = "none";
        protected string DisplayErrorSaison = "none";
                
        public int SaisonChoosed = 0;
        public string RundeChoosed;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        public List<DisplayRunde> RundeList;

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public List<DisplaySaison> SaisonenList;

        public string VisibleBtnNew { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IPokalergebnisseService PokalergebnisseService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        public IEnumerable<PokalergebnisSpieltag> PokalergebnisseSpieltage { get; set; }

        public IEnumerable<PokalergebnisSpieltag> PokalergebnisseSpieltageFinale { get; set; }

        [Inject]
        public IStringLocalizer<Pokalergebnisse> Localizer { get; set; }
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
                    string returnUrl = WebUtility.UrlEncode($"/Ligamanager");
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }


              
                SaisonenList = new List<DisplaySaison>();


                Saisonen = (await SaisonenService.GetSaisonen()).ToList().Where(x => x.LigaID == 1);

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                PokalergebnisseSpieltageFinale = await PokalergebnisseService.GetPokalergebnisseSpieltag();

                if (PokalergebnisseSpieltageFinale == null)
                    return;

                PokalergebnisseSpieltageFinale = PokalergebnisseSpieltageFinale.ToList().Where(x => x.Runde == "F").OrderByDescending(x => x.Datum);

                DisplayErrorRunde = "none";
                DisplayErrorSaison = "none";


                VisibleBtnNew = "hidden";

                if (Globals.currentPokalRunde == null)
                    RundeChoosed = null;
                else
                    RundeChoosed = Globals.currentPokalRunde;

                SaisonChoosed = Globals.CLSaisonID;

                if (Globals.currentPokalRunde != null)
                    OnClickHandler();

                RundeList = new List<DisplayRunde>
                {
                    new DisplayRunde("2",Localizer["2. Runde"].Value),
                    new DisplayRunde("AF", Localizer["Achtelfinale"].Value),
                    new DisplayRunde("VF", Localizer["Viertelfinale"].Value),
                    new DisplayRunde("HF", Localizer["Halbfinale"].Value),
                    new DisplayRunde("F", Localizer["Finale"].Value),
                };

                Globals.bVisibleNavMenuElements = true;
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

                Globals.CLSaisonID = SaisonChoosed;

                var saison = await SaisonenService.GetSaison(Convert.ToInt32(SaisonChoosed));

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

                PokalergebnisseSpieltage = await PokalergebnisseService.GetPokalergebnisseSpieltag();

                if (PokalergebnisseSpieltage == null)
                    return;

                PokalergebnisseSpieltage = PokalergebnisseSpieltage.ToList();
                PokalergebnisseSpieltage = PokalergebnisseSpieltage.Where(x => x.SaisonID == SaisonChoosed && x.Runde == RundeChoosed).OrderBy(x => x.Datum);

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

                PokalergebnisseSpieltage = await PokalergebnisseService.GetPokalergebnisseSpieltag();

                if (PokalergebnisseSpieltage == null)
                    return;

                PokalergebnisseSpieltage = PokalergebnisseSpieltage.Where(x => x.SaisonID == SaisonChoosed && x.Runde == RundeChoosed).OrderBy(x => x.Datum);
                
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

            if (RundeChoosed == "F" && PokalergebnisseSpieltage.Count() >= 1)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "HF" && PokalergebnisseSpieltage.Count() >= 2)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "VF" && PokalergebnisseSpieltage.Count() >= 4)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "AF" && PokalergebnisseSpieltage.Count() >= 8)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "2" && PokalergebnisseSpieltage.Count() >= 16)
                sButtonVisible = "hidden";
            else
                sButtonVisible = "visible";

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



