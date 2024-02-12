using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class SaisonenListBase : ComponentBase
    {
        [Inject]
        public ISaisonenService SaisonenService { get; set; }
        public IEnumerable<Saison> SaisonenList { get; set; }
        public Saison Saisonen { get; set; } = new Saison();                           
                

        protected override async Task OnInitializedAsync()
        {
            SaisonenList = (await SaisonenService.GetSaisonen()).ToList();
        }                         

    }
}

