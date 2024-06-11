using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ligamanager.Components;
using System.Net;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using LigaManagement.Web.Pages;
using Microsoft.Extensions.Localization;

namespace LigaManagerManagement.Web.Pages
{
    public class LigenListBase : ComponentBase
    {
        public string Liganame = "";
        public Density Density = Density.Compact;

        [Inject]
        public ILigaService LigaService { get; set; }

        protected string CssClass { get; set; } = null;
        public IEnumerable<Liga> LigenList { get; set; }

        public Liga Ligen { get; set; } = new Liga();

        public RadzenDataGrid<Liga> grid;
        IList<Tuple<Liga, RadzenDataGridColumn<Liga>>> selectedCellData = new List<Tuple<Liga, RadzenDataGridColumn<Liga>>>();

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
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            LigenList = (await LigaService.GetLigen()).ToList();

            var liga = (await LigaService.GetLiga(Globals.LigaID));
            Liganame = liga.Liganame;
                       
            Globals.bVisibleNavMenuElements = true;

        }

        protected async Task VereinDeleted()
        {
            LigenList = (await LigaService.GetLigen()).ToList();
        }

       
        int index;
        public void ResetIndex(bool shouldReset)
        {
            if (shouldReset)
            {
                index = 0;
            }
        }        
        

    }
}

