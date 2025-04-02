using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Web.Services;
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
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class InfoListBase : ComponentBase
    {        
        public Density Density = Density.Compact;

        //[Parameter]
        //public int VereinNr { get; set; }

        [Inject]
        public IInfoTexteService InfoTexteService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }


        public IEnumerable<InfoText> InfoTexteList { get; set; }

        public InfoText InfoTexte { get; set; } = new InfoText();

        public bool IsLoading = false;
        public RadzenDataGrid<InfoText> grid;
        IList<Tuple<InfoText, RadzenDataGridColumn<InfoText>>> selectedCellData = new List<Tuple<InfoText, RadzenDataGridColumn<InfoText>>>();

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime jsr { get; set; }

        [Inject]
        public IStringLocalizer<InfoText> Localizer { get; set; }

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
                InfoTexteList = (await InfoTexteService.GetTexte()).ToList();


                InfoTexteList = InfoTexteList.Where(x => x.PublishedAt > DateTime.Now.AddDays(-30)).ToList().OrderByDescending(x => x.PublishedAt);

                foreach (var infotext in InfoTexteList)
                {
                    var verein = await VereineService.GetVerein(infotext.VereinID);
                    infotext.Vereinsname = verein.Vereinsname2;
                }

                Globals.bVisibleNavMenuElements = true;

                IsLoading = false;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);

                return;
            }

        }

        protected async Task InfoTextDeleted()
        {
            InfoTexteList = (await InfoTexteService.GetTexte()).ToList();
        }




    }
}

