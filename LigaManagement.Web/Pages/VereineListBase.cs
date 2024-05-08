using LigaManagement.Models;
using LigaManagement.Web.Pages;
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
        public RadzenDataGrid<VereinAUS> gridPL;
        public RadzenDataGrid<VereinAUS> gridIT;
        public RadzenDataGrid<VereinAUS> gridFR;
        public RadzenDataGrid<VereinAUS> gridES;

        public Density Density = Density.Default;

        public string Liganame = "";

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereinePLService VereinePLService { get; set; }

        [Inject]
        public IVereineESService VereineESService { get; set; }
        [Inject]
        public IVereineFRService VereineFRService { get; set; }

        [Inject]
        public IVereineITService VereineITService { get; set; }

        public IEnumerable<Verein> VereineList { get; set; }

        public IEnumerable<VereinAUS> VereineListPL { get; set; }

        public IEnumerable<VereinAUS> VereineListIT { get; set; }

        public IEnumerable<VereinAUS> VereineListFR { get; set; }

        public IEnumerable<VereinAUS> VereineListES { get; set; }

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

            if (authenticationState.User.Identity == null)
            {
                return;
            }

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            VereineList = (await VereineService.GetVereine()).ToList();

            VereineListPL = (await VereinePLService.GetVereine()).ToList();

            VereineListIT = (await VereineITService.GetVereine()).ToList();

            VereineListFR = (await VereineFRService.GetVereine()).ToList();

            VereineListES = (await VereineESService.GetVereine()).ToList();


            var liga = await LigaService.GetLiga(Globals.LigaID);
            Liganame = liga.Liganame;
        }


        protected async Task VereinDeleted()
        {
            VereineList = (await VereineService.GetVereine()).ToList();
        }

    }
}
