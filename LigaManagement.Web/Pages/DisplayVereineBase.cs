using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class DisplayVereinBase : ComponentBase
    {
        [Parameter]
        public Verein Verein { get; set; }

       
        [Parameter]
        public EventCallback<bool> OnVereinSelection { get; set; }

        [Parameter]
        public EventCallback<int> OnVereinDeleted { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected Ligamanager.Components.ConfirmBase DeleteConfirmation { get; set; }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }
        
        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await VereineService.DeleteVerein(Verein.Id);
                await OnVereinDeleted.InvokeAsync(Verein.Id);
            }
        }

        //protected async Task CheckBoxChanged(ChangeEventArgs e)
        //{
        //    await OnEmployeeSelection.InvokeAsync((bool)e.Value);
        //}
    }
}
