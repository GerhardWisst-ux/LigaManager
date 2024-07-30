using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen.Blazor;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using System.Linq;


namespace LigaManagerManagement.Web.Pages
{
    public class EinstellungenBase : ComponentBase
    {
        [Inject]
        public IStringLocalizer<Einstellungen> Localizer { get; set; }

        public NavigationManager _navigationManager;
        protected string DisplayErrorSaisonVon = "none";
        protected string DisplayErrorSaisonNach = "none";
        private string Titel { get; set; }
        private string currentSaisonVon { get; set; }
        private string currentSaisonNach { get; set; }

        protected int SaisonIDVon { get; set; }
        protected int SaisonIDNach { get; set; }

        public bool IsLoading { get; set; }

        protected bool isDropdownDisabledSaison = true;
               

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public List<DisplaySaison> SaisonenVonList;

        public List<DisplaySaison> SaisonenNachList;

        public IEnumerable<Saison> Saisonen { get; set; }
        protected override async Task OnInitializedAsync()
        {
            SaisonenVonList = new List<DisplaySaison>();
            SaisonenNachList = new List<DisplaySaison>();
            Saisonen = (await SaisonenService.GetSaisonen()).Where(x => x.LigaID == 1).ToList();

            for (int i = 0; i < Saisonen.Count(); i++)
            {
                var columns = Saisonen.ElementAt(i);
                Globals.currentLiga = Saisonen.ElementAt(0).Liganame;
                Globals.currentLigaUrl = Globals.currentLiga;

                SaisonenVonList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                SaisonenNachList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
            }

            isDropdownDisabledSaison = false;

            SaisonIDVon = 0;
            SaisonIDNach = 0;

            DisplayErrorSaisonVon = "none";
            DisplayErrorSaisonNach = "none";

            await LoadSettings();
        }

        public async void OnClickHandler()
        {
            if (Globals.currentSaison == null)
                DisplayErrorSaisonVon = "block";
            else
                DisplayErrorSaisonVon = "none";

            if (Globals.currentSaison == null)
                DisplayErrorSaisonNach = "block";
            else
                DisplayErrorSaisonNach = "none";

        }
        public void SaisonVonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == "0")
                    return;

                currentSaisonVon = e.Value.ToString();
                
                if (Saisonen == null || currentSaisonVon == null)
                    throw new Exception("Saisonen = null oder Globals.currentSaison = null");

                if (Saisonen.FirstOrDefault(x => x.Saisonname == currentSaisonVon) != null)
                {
                    SaisonIDVon = Saisonen.FirstOrDefault(x => x.Saisonname == currentSaisonVon).SaisonID;                    
                }
                else
                {
                    SaisonIDVon = 1;                    
                }

                StateHasChanged();
            }
        }
        public void SaisonNachChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == "0")
                    return;

                currentSaisonNach = e.Value.ToString();

                if (Saisonen == null || currentSaisonNach == null)
                    throw new Exception("Saisonen = null oder Globals.currentSaison = null");

                if (Saisonen.FirstOrDefault(x => x.Saisonname == currentSaisonVon) != null)
                {
                    SaisonIDNach = Saisonen.FirstOrDefault(x => x.Saisonname == currentSaisonNach).SaisonID;
                }
                else
                {
                    SaisonIDNach = 1;
                }

                StateHasChanged();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Titel = Localizer["Bearbeiten"].Value;
                await LoadSettings();
            }
        }

        private async Task LoadSettings()
        {
            IsLoading = true;

            //settings.Sprache_LandKZ = "DE";
            //settings.ImportVisible = false;
            //settings.TabellenAnlegenVisible = false;

            //settings = null;

            StateHasChanged();
        }





    }
}