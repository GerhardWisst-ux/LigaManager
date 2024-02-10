using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class LigenListBase : ComponentBase
    {
        [Inject]
        public ILigaService LigaService { get; set; }

        protected string CssClass { get; set; } = null;
        public IEnumerable<Liga> LigenList { get; set; }

        public Liga Ligen { get; set; } = new Liga();

        protected override async Task OnInitializedAsync()
        {
            LigenList = (await LigaService.GetLigen()).ToList();
        }

        protected async Task VereinDeleted()
        {
            LigenList = (await LigaService.GetLigen()).ToList();
        }
    }
}
