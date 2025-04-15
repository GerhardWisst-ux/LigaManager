using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using static LigaManagement.Web.Pages.EinstiegListBase;

namespace LigaManagerManagement.Web.Pages
{
    public class CreateSaisonListBase : ComponentBase
    {
        protected string DisplayErrorLiga = "none";
        public string Liganame = "Bundesliga";
        protected int LigaID;
        public Density Density = Density.Compact;

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }
        
        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        [Inject]
        public ILandService LandService { get; set; }              

        public List<DisplayLiga> LigenList;
        public bool IsLoading = false;
        public List<Verein> Vereine { get; set; }
        public IEnumerable<Liga> Ligen { get; set; }
        public Verein Verein { get; set; }

        public IEnumerable<Saison> SaisonenList { get; set; }
        public Saison Saison { get; set; } = new Saison();


        public List<DisplayVerein> VereineList = new List<DisplayVerein>();
        public List<DisplayVerein> VereineSaisonList = new List<DisplayVerein>();

        public RadzenDataGrid<Saison> grid;
        IList<Tuple<Saison, RadzenDataGridColumn<Saison>>> selectedCellData = new List<Tuple<Saison, RadzenDataGridColumn<Saison>>>();

        public List<Verein> vereinesaison = new List<Verein>();

        public List<Verein> vereinesaisonSelected = new List<Verein>();

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<CreateSaisonListBase> Localizer { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;

            if (authenticationState.User.Identity == null || !authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/Ligamanager");
                NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                return;
            }

            IsLoading = true;

            await InitializeLigenList();
            await InitializeVereineList();

            await LigaChange(new ChangeEventArgs());

            DisplayErrorLiga = "none";
            Globals.bVisibleNavMenuElements = true;

            IsLoading = false;
        }
        private async Task InitializeLigenList()
        {
            Ligen = (await LigaService.GetLigen()).ToList();
            LigenList = Ligen.Select(columns => new DisplayLiga(columns.Aktiv, columns.Id, columns.Id, columns.Liganame, columns.EMWM)).ToList();
        }

        private async Task InitializeVereineList()
        {
            Vereine = (await VereineService.GetVereine()).ToList();
            var VereineSaison = (await VereineService.GetVereineSaison()).Where(x => x.SaisonID == Convert.ToInt32(Id)).ToList();

            VereineList = Vereine.Select(verein =>
            {
                var isChecked = Id != "0" && VereineSaison.Any(s => s.VereinNr == verein.VereinNr);
                return new DisplayVerein(verein.VereinNr.ToString(), verein.Vereinsname1, isChecked);
            }).ToList();
        }
        [Bind]
        public class DisplayVerein
        {
            public DisplayVerein(string vereinID, string vereinname, bool vereinchecked)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
                VereinChecked = vereinchecked;
            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }
            public bool VereinChecked { get; set; }
        }

        public async Task CheckboxClicked(string aSelectedId, object aChecked)
        {
            try
            {
                Verein = await VereineService.GetVerein(Convert.ToInt32(aSelectedId));

                var isVereinInList = vereinesaisonSelected.FirstOrDefault(x => x.Vereinsname1 == Verein.Vereinsname1);

                if (vereinesaisonSelected == null)
                    throw new Exception("vereinesaisonSelected null");

                if ((bool)aChecked)
                {
                    if (isVereinInList == null)
                        vereinesaisonSelected.Add(Verein);
                }
                else
                {
                    if (isVereinInList != null)
                        vereinesaisonSelected.Remove(isVereinInList);
                }

                StateHasChanged();
            }
            catch (ArgumentNullException ex)
            {
                ErrorLogger.WriteToErrorLog($"ArgumentNullException: {ex.Message}", ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog($"Unexpected error: {ex.Message}", ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        public async Task LigaChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                LigaID = Convert.ToInt32(e.Value);

                var liga = await LigaService.GetLiga(LigaID);
                Liganame = liga.Liganame;
            }
        }
    }
}

