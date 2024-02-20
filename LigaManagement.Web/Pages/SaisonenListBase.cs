using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class SaisonenListBase : ComponentBase
    {
        [Inject]
        public ISaisonenService SaisonenService { get; set; }
        public IEnumerable<Saison> SaisonenList { get; set; }
        public Saison Saison { get; set; } = new Saison();

        public RadzenDataGrid<Saison> grid;
        IList<Tuple<Saison, RadzenDataGridColumn<Saison>>> selectedCellData = new List<Tuple<Saison, RadzenDataGridColumn<Saison>>>();

        string type = "Click";
        bool multiple = true;

        protected override async Task OnInitializedAsync()
        {
            SaisonenList = (await SaisonenService.GetSaisonen()).ToList();
        }
        public void Select(DataGridCellMouseEventArgs<Saison> args)
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
                selectedCellData.Add(new Tuple<Saison, RadzenDataGridColumn<Saison>>(args.Data, args.Column));
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

        public void OnCellClick(DataGridCellMouseEventArgs<Saison> args)
        {
            if (type == "Click")
            {
                Select(args);
            }
        }

        public void OnCellDoubleClick(DataGridCellMouseEventArgs<Saison> args)
        {
            if (type != "Click")
            {
                Select(args);
            }
        }

        public void OnCellRender(DataGridCellRenderEventArgs<Saison> args)
        {
            if (selectedCellData.Any(i => i.Item1 == args.Data && i.Item2 == args.Column))
            {
                args.Attributes.Add("style", $"background-color: var(--rz-secondary-lighter);");
            }
        }
        public void OnFilter(DataGridColumnFilterEventArgs<Saison> args)
        {
            //
        }

    }
}

