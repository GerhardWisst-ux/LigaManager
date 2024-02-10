using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class SaisonenListBase : ComponentBase
    {
        public Int32 saison;

        //public List<DisplaySaison> SaisonenList;              

        [Inject]
        public ISaisonenService SaisonenService { get; set; }
              
        [Inject]
        public IVereineService VereineService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        protected override async Task OnInitializedAsync()
        {           
            Saisonen = await SaisonenService.GetSaisonen();           
        }                         

    }
}

