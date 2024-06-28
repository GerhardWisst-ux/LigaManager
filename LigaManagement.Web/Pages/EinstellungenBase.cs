using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;


namespace LigaManagerManagement.Web.Pages
{
    public class EinstellungenBase : ComponentBase
    {
        [Inject]
        public IStringLocalizer<Einstellungen> Localizer { get; set; }
                
        public NavigationManager _navigationManager;
        private string Titel { get; set; }
        public bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadSettings();
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