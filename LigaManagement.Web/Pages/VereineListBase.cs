using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class VereineListBase : ComponentBase
    {
        [Inject]
        public IVereineService VereineService { get; set; }              
        public IEnumerable<Verein> VereineList { get; set; }

        public Verein Vereine { get; set; } = new Verein();

        protected override async Task OnInitializedAsync()
        {
            VereineList = (await VereineService.GetVereine()).ToList();
        }

        protected async Task VereinDeleted()
        {
            VereineList = (await VereineService.GetVereine()).ToList();
        }
    }
}
