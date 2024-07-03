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
    public class VereineListBase : ComponentBase
    {
        public RadzenDataGrid<Verein> grid;
        public RadzenDataGrid<VereinAUS> gridPL;
        public RadzenDataGrid<VereinAUS> gridIT;
        public RadzenDataGrid<VereinAUS> gridFR;
        public RadzenDataGrid<VereinAUS> gridES;
        public RadzenDataGrid<VereinAUS> gridNL;
        public RadzenDataGrid<VereinAUS> gridPT;
        public RadzenDataGrid<VereinAUS> gridTU;
        public RadzenDataGrid<VereinAUS> gridBE;

        public Density Density = Density.Compact;
        public int LandID;
        public int LigaID;
        public string Liganame = "";


        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }


        public List<DisplayLaender> LaenderList;

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

        [Inject]
        public IVereineNLService VereineNLService { get; set; }

        [Inject]
        public IVereinePTService VereinePTService { get; set; }
       
        [Inject]
        public IVereineTUService VereineTUService { get; set; }

        [Inject]
        public IVereineBEService VereineBEService { get; set; }

        public IEnumerable<Verein> VereineList { get; set; }

        public IEnumerable<VereinAUS> VereineListPL { get; set; }

        public IEnumerable<VereinAUS> VereineListIT { get; set; }

        public IEnumerable<VereinAUS> VereineListFR { get; set; }

        public IEnumerable<VereinAUS> VereineListES { get; set; }

        public IEnumerable<VereinAUS> VereineListNL { get; set; }

        public IEnumerable<VereinAUS> VereineListPT { get; set; }

        public IEnumerable<VereinAUS> VereineListTU { get; set; }
        public IEnumerable<VereinAUS> VereineListBE { get; set; }

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
        public IStringLocalizer<VereineList> Localizer { get; set; }

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

            VereineList = (await VereineService.GetVereine()).ToList();

            VereineListPL = (await VereinePLService.GetVereine()).ToList();

            VereineListIT = (await VereineITService.GetVereine()).ToList();

            VereineListFR = (await VereineFRService.GetVereine()).ToList();

            VereineListES = (await VereineESService.GetVereine()).ToList();

            VereineListNL = (await VereineNLService.GetVereine()).ToList();

            VereineListPT = (await VereinePTService.GetVereine()).ToList();

            VereineListTU = (await VereineTUService.GetVereine()).ToList();

            VereineListBE = (await VereineBEService.GetVereine()).ToList();

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
                else if (land.Laendername == "England")
                    LigaID = 4;
                else if (land.Laendername == "Italien")
                    LigaID = 6;                
                else if (land.Laendername == "Frankreich")
                    LigaID = 7;
                else if (land.Laendername == "Spanien")
                    LigaID = 8;
                else if (land.Laendername == "Niederlande")
                    LigaID = 9;
                else if (land.Laendername == "Portugal")
                    LigaID = 10;
                else if (land.Laendername == "Türkei")
                    LigaID = 11;
                else if (land.Laendername == "Belgien")
                    LigaID = 14;

                StateHasChanged();
            }
        }
        protected async Task VereinDeleted()
        {
            VereineList = (await VereineService.GetVereine()).ToList();
        }

    }
}
