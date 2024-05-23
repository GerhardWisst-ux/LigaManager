using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class LigaListBase : ComponentBase
    {
        public RadzenDataGrid<Verein> grid;
        IList<Tuple<Liga, RadzenDataGridColumn<Liga>>> selectedCellData = new List<Tuple<Liga, RadzenDataGridColumn<Liga>>>();
        public Density Density = Density.Compact;

        public string Liganame = "";
        string type = "Click";
        bool multiple = true;

        [Inject]
        public ILigaService LigaService { get; set; }

        protected override async Task OnInitializedAsync()
        {

            var liga = await LigaService.GetLiga(Convert.ToInt32(Globals.currentLiga));
            Liganame = liga.Liganame;
        }

    }
}
