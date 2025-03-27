using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class EditStadionListBase : ComponentBase
    {
        public string Stadionname = "";
        public string Ausrichterland = "";
        public Density Density = Density.Compact;
                
        [Inject]
        public IStadionService StadionService { get; set; }

        protected string CssClass { get; set; } = null;
        public IEnumerable<Stadion> StadienList { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }
        public List<DisplayVerein> VereineList = new List<DisplayVerein>();
        protected int VereinNr;
        private bool bChangedVerein;
        public bool VisibleAdd;

        public Liga Ligen { get; set; } = new Liga();

        public bool IsLoading = false;
        public RadzenDataGrid<Stadion> grid;
        IList<Tuple<Stadion, RadzenDataGridColumn<Stadion>>> selectedCellData = new List<Tuple<Stadion, RadzenDataGridColumn<Stadion>>>();

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime jsr { get; set; }

        [Inject]
        public IStringLocalizer<Ligen> Localizer { get; set; }

        protected override async Task OnInitializedAsync()
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
            StadienList = (await StadionService.GetStadien()).ToList();

            var stadion = (await StadionService.GetStadion(16));

            Stadionname = stadion.Stadionname;

            var verList = await VereineService.GetVereine();

            foreach (var ver in verList)
            {
                var verein = await VereineService.GetVerein(ver.VereinNr);
                VereineList.Add(new DisplayVerein(ver.VereinNr.ToString(), verein.Vereinsname1));
            }
                        
            Globals.bVisibleNavMenuElements = true;

            IsLoading = false;

        }
        public void VereinChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Value = 0;
                }

                VereinNr = Convert.ToInt32(e.Value);
                bChangedVerein = true;

                if (VereinNr > 0)
                {
                    VisibleAdd = false;
                }
            }
        }
        protected async Task StationDeleted()
        {
            StadienList = (await StadionService.GetStadien()).ToList();
        }


        [Bind]
        public class DisplayVerein
        {
            public DisplayVerein(string vereinID, string vereinname)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }
        }


    }
}

