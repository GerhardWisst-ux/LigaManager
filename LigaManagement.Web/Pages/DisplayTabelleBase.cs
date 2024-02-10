using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LigamanagerManagement.Web.Pages
{
    public class DisplayTabelleBase : ComponentBase
    {
        [Parameter]
        public Tabelle Tabelle { get; set; }

        [Parameter]
        public Verein Verein { get; set; }

        [Parameter]
        public EventCallback<bool> OnTabelleSelection { get; set; }

        [Parameter]
        public EventCallback<int> OnTabelleDeleted { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
                       
        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await TabelleService.DeleteTabelle(Tabelle.Id);
                await OnTabelleDeleted.InvokeAsync(Tabelle.Id);
            }
        }

        protected int currentCount = 0;

        protected void IncrementCount()
        {
            currentCount++;
        }

    }

}

