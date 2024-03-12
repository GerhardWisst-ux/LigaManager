using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ligamanager.Components;

namespace LigaManagerManagement.Web.Pages
{
    public class LigenListBase : ComponentBase
    {
        public string Liganame = "";
        [Inject]
        public ILigaService LigaService { get; set; }


        protected string CssClass { get; set; } = null;
        public IEnumerable<Liga> LigenList { get; set; }

        public Liga Ligen { get; set; } = new Liga();

        public RadzenDataGrid<Liga> grid;
        IList<Tuple<Liga, RadzenDataGridColumn<Liga>>> selectedCellData = new List<Tuple<Liga, RadzenDataGridColumn<Liga>>>();

        string type = "Click";
        bool multiple = true;
        protected override async Task OnInitializedAsync()
        {
            LigenList = (await LigaService.GetLigen()).ToList();

            var liga = (await LigaService.GetLiga(Convert.ToInt32(Globals.currentLiga)));
            Liganame = liga.Liganame;
        }

        protected async Task VereinDeleted()
        {
            LigenList = (await LigaService.GetLigen()).ToList();
        }

        public void Select(DataGridCellMouseEventArgs<Liga> args)
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
                selectedCellData.Add(new Tuple<Liga, RadzenDataGridColumn<Liga>>(args.Data, args.Column));
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

        public void OnCellClick(DataGridCellMouseEventArgs<Liga> args)
        {
            if (type == "Click")
            {
                Select(args);
            }
        }

        public void OnCellDoubleClick(DataGridCellMouseEventArgs<Liga> args)
        {
            if (type != "Click")
            {
                Select(args);
            }
        }

        public void OnCellRender(DataGridCellRenderEventArgs<Liga> args)
        {
            if (selectedCellData.Any(i => i.Item1 == args.Data && i.Item2 == args.Column))
            {
                args.Attributes.Add("style", $"background-color: var(--rz-secondary-lighter);");
            }
        }
        public void OnFilter(DataGridColumnFilterEventArgs<Liga> args)
        {
            //
        }

    }
}

