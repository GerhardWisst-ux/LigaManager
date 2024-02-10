using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LigamanagerManagement.Web.Pages
{
    public class DisplayErgebnisBase : ComponentBase
    {
        [Parameter]
        public Spielergebnisse spielergebnis { get; set; }

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
              

        //protected async Task CheckBoxChanged(ChangeEventArgs e)
        //{
        //    await OnEmployeeSelection.InvokeAsync((bool)e.Value);
        //}
    }
}
