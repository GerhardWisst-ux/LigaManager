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
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static LigaManagement.Web.Pages.EinstiegListBase;

namespace LigaManagerManagement.Web.Pages
{
    public class EditVereinEMWMListBase : ComponentBase
    {
        public RadzenDataGrid<Verein> grid;
        
        public Density Density = Density.Compact;
        public int LandID;
        public int LigaID;
        public string Liganame = "";


        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }


        public List<DisplayLaender> LaenderList;

        [Inject]
        public IVereineService VereineService { get; set; }
               

        public IEnumerable<Verein> VereineList { get; set; }
                
        public IEnumerable<Land> Laender { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        public Verein Vereine { get; set; } = new Verein();

        [Inject]
        public ILandService LaenderService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime jsr { get; set; }

        [Inject]
        public IStringLocalizer<EditVereinEMWM> Localizer { get; set; }

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

            VereineList = (await VereineService.GetVereineEMWM()).ToList();

            
            LaenderList = new List<DisplayLaender>();
            Laender = (await LaenderService.GetLaender()).ToList();

            for (int i = 0; i < Laender.Count(); i++)
            {
                var columns = Laender.ElementAt(i);
                LaenderList.Add(new DisplayLaender(columns.Aktiv, columns.Id, columns.Laendername));
            }

            var liga = await LigaService.GetLiga(Globals.LigaID);
            Liganame = liga.Liganame;

            LigaID = (Globals.LigaID);
        }

        public async Task LandChangeAsync(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var LandID = Convert.ToInt32(e.Value);

                var land = await LaenderService.GetLand(LandID);

                if (land.Laendername == "Deutschland")
                    LigaID = 1;
                
                
                StateHasChanged();
            }
        }
        protected async Task VereinDeleted()
        {
            VereineList = (await VereineService.GetVereineEMWM()).ToList();
        }

    }
}
