using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class VereineListBase : ComponentBase
    {
        public RadzenDataGrid<Verein> grid;        
        public Density Density = Density.Default;

        public string Liganame = "";               

        [Inject]
        public IVereineService VereineService { get; set; }
        public IEnumerable<Verein> VereineList { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        public Verein Vereine { get; set; } = new Verein();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/spieltage/1");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            VereineList = (await VereineService.GetVereine()).ToList();

            var liga = await LigaService.GetLiga(Convert.ToInt32(Globals.currentLiga));
            Liganame = liga.Liganame;
        }
     

        protected async Task VereinDeleted()
        {
            VereineList = (await VereineService.GetVereine()).ToList();
        }
      
    }
}
