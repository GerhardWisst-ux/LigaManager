using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TextManagerManagement.Web.Services;

namespace LigaManagerManagement.Web.Pages
{
    public class EditInfoTextListBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        protected InfoText InfoTexte { get; set; } = new InfoText();
        protected string Titel { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        public NavigationManager NavigationManager { get; set; }

        public Density Density = Density.Compact;
        protected string DisplayErrorVerein = "none";
        [Inject]
        public IInfoTexteService InfoTextService { get; set; }

        protected string CssClass { get; set; } = null;
        public IEnumerable<InfoText> StadienList { get; set; }
        private readonly ILogger<InfoText> _logger;
        [Inject]
        public IVereineService VereineService { get; set; }
        public List<DisplayVerein> VereineList = new List<DisplayVerein>();
        
        protected int VereinNr;
        protected DateTime PublishedAt;
        public bool VisibleAdd;
                
        public bool IsLoading = false;
        public RadzenDataGrid<InfoText> grid;

        [Inject]
        public IStringLocalizer<InfoText> Localizer { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }
        [Inject]
        public ISaisonenService SaisonenService { get; set; }
        public IEnumerable<Saison> Saisonen { get; set; }

        [Inject]
        public IJSRuntime jsr { get; set; }

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
                
                var verList = await VereineSaisonService.GetVereineSaison();
                Saisonen = (await SaisonenService.GetSaisonen()).OrderBy(x => x.Saisonname).ToList();

                var aktuelleSaison = Saisonen.FirstOrDefault(x => x.SaisonID == Globals.SaisonID);
                if (aktuelleSaison != null)
                {
                    var vereineSaison = await VereineSaisonService.GetVereineSaison();
                    verList = vereineSaison.Where(x => x.SaisonID == aktuelleSaison.SaisonID).ToList();

                    foreach (var ver in verList)
                    {
                        var verein = await VereineService.GetVerein(ver.VereinNr);                         
                        VereineList.Add(new DisplayVerein(ver.VereinNr.ToString(), verein.Vereinsname2));
                    }
                }

                if (Id == "0" || Id is null)
                {                    
                    Id = "0";
                    Titel = @Localizer["Neuanlage InfoText"].Value;
                    InfoTexte = await InfoTextService.GetText(Convert.ToInt32(Id));
                }
                else
                {                 
                    Titel = @Localizer["Bearbeiten InfoText"].Value;
                    InfoTexte = await InfoTextService.GetText(Convert.ToInt32(Id));
                    VereinNr = InfoTexte.VereinID;
                }

                Globals.bVisibleNavMenuElements = true;

                IsLoading = false;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                IsLoading = false;
            }

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
                
                if (VereinNr > 0)
                {
                    VisibleAdd = false;
                }
            }
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

