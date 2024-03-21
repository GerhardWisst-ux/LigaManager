using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LigamanagerManagement.Web.Pages
{
    public class DisplaySpieltagBase : ComponentBase
    {
        [Parameter]
        public bool DisplayButtons { get; set; }
                
        [Parameter]
        public Spieltag Spieltag { get; set; }

        [Parameter]
        public EventCallback<int> OnSpielDeleted { get; set; }

        [Parameter]
        public int Zaehler { get; set; }

        string filterText = "";

        public int SpieltagNr { get; set; }

        [Parameter]
        public EventCallback<bool> OnSpieltagSelection { get; set; }

        [Parameter]
        public EventCallback<int> OnSpieltagDeleted { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ConfirmBase DeleteConfirmation { get; set; }

        protected async Task Delete_Click()
        {
            //DeleteConfirmation.Show();

            await SpieltagService.DeleteSpieltag(Spieltag.SpieltagId);
            await OnSpieltagDeleted.InvokeAsync((int)Spieltag.SpieltagId);

            NavigationManager.NavigateTo(($"/spieltage?spieltag={Globals.Spieltag}"));
        }

        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            //if (deleteConfirmed)
            //{
            //    await SpieltagService.DeleteSpieltag(Spieltag.SpieltagId);
            //    await OnSpieltagDeleted.InvokeAsync((int)Spieltag.SpieltagId);
            //}

            //NavigationManager.NavigateTo(($"/spieltage?spieltag={Globals.Spieltag}"));
        }


    }


}
