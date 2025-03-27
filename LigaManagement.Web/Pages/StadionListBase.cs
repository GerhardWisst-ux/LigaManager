using BootstrapBlazor.Components;
using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class StadionListBase : ComponentBase
    {
        public string Stadionname = "";
        public Density Density = Density.Compact;

        //[Parameter]
        //public int VereinNr { get; set; }

        [Inject]
       public IStadionService StadionService { get; set; }

        public IEnumerable<Stadion> StadionList { get; set; }

        public Stadion Stadien { get; set; } = new Stadion();

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
                StadionList = (await StadionService.GetStadien()).ToList();

                //var stadion = (await StadionService.GetStadion(16));

                //if (stadion != null)
                //    Stadionname = stadion.Stadionname;

                Globals.bVisibleNavMenuElements = true;

                IsLoading = false;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);

                return;
            }

        }

        protected async Task StadionDeleted()
        {
            StadionList = (await StadionService.GetStadien()).ToList();
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

