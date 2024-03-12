using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class VereineListBase : ComponentBase
    {
        public RadzenDataGrid<Verein> grid;
        IList<Tuple<Verein, RadzenDataGridColumn<Verein>>> selectedCellData = new List<Tuple<Verein, RadzenDataGridColumn<Verein>>>();

        public string Liganame = "";
        string type = "Click";
        bool multiple = true;

        [Inject]
        public IVereineService VereineService { get; set; }              
        public IEnumerable<Verein> VereineList { get; set; }


        [Inject]
        public ILigaService LigaService { get; set; }

        public Verein Vereine { get; set; } = new Verein();

        protected override async Task OnInitializedAsync()
        {
            VereineList = (await VereineService.GetVereine()).ToList();

            var liga = await LigaService.GetLiga(Convert.ToInt32(Globals.currentLiga));
            Liganame = liga.Liganame;
        }

        protected async Task VereinDeleted()
        {
            VereineList = (await VereineService.GetVereine()).ToList();
        }
        public void Select(DataGridCellMouseEventArgs<Verein> args)
        {
            if (!multiple)
            {
                selectedCellData.Clear();
            }

            var cellData = selectedCellData.FirstOrDefault(i => i.Item1 == args.Data && i.Item2 == args.Column);
            if (cellData != null)
            {
                selectedCellData.Remove(cellData);
            }
            else
            {
                selectedCellData.Add(new Tuple<Verein, RadzenDataGridColumn<Verein>>(args.Data, args.Column));
            }
        }

        int index;
        public void ResetIndex(bool shouldReset)
        {
            if (shouldReset)
            {
                index = 0;
            }
        }

        public void OnCellClick(DataGridCellMouseEventArgs<Verein> args)
        {
            if (type == "Click")
            {
                Select(args);
            }
        }

        public void OnCellDoubleClick(DataGridCellMouseEventArgs<Verein> args)
        {
            if (type != "Click")
            {
                Select(args);
            }
        }

        public void OnCellRender(DataGridCellRenderEventArgs<Verein> args)
        {
            if (selectedCellData.Any(i => i.Item1 == args.Data && i.Item2 == args.Column))
            {
                args.Attributes.Add("style", $"background-color: var(--rz-secondary-lighter);");
            }
        }
        public void OnFilter(DataGridColumnFilterEventArgs<Verein> args)
        {
            //
        }
    }
}
