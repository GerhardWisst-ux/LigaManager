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
        public Spieltag Spieltag { get; set; }

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

        protected ConfirmBase DeleteConfirmation { get; set; }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }
        
        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await SpieltagService.DeleteSpieltag(Spieltag.SpieltagId);
                await OnSpieltagDeleted.InvokeAsync(Spieltag.SpieltagId);
            }
        }

        //protected async Task CheckBoxChanged(ChangeEventArgs e)
        //{
        //    await OnEmployeeSelection.InvokeAsync((bool)e.Value);
        //}
    }
}
