using LigaManagement.Api.Migrations;
using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using LigaManagerManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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

        [Inject]
        public IVereineService VereineService { get; set; }

        public List<Verein> Vereine { get; set; }
        public Verein Verein { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();
        public List<DisplayVerein> VereineSaisonList = new List<DisplayVerein>();

        public RadzenDataGrid<Saison> grid;
        IList<Tuple<Saison, RadzenDataGridColumn<Saison>>> selectedCellData = new List<Tuple<Saison, RadzenDataGridColumn<Saison>>>();

        List<Verein> vereinesaison = new List<Verein>();

        string type = "Click";
        bool multiple = true;


        protected override async Task OnInitializedAsync()
        {
            SaisonenList = (await SaisonenService.GetSaisonen()).ToList();

            VereineList = new List<DisplayVerein>();

            Vereine = (await VereineService.GetVereine()).ToList();

            for (int i = 0; i < Vereine.Count(); i++)
            {
                VereineList.Add(new DisplayVerein(Vereine[i].VereinNr.ToString(), Vereine[i].Vereinsname1));
            }



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
        [Bind]
        public class DisplayVerein
        {
            public DisplayVerein(string vereinID, string vereinname)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }
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


        public async void CheckboxClicked(string aSelectedId, object aChecked)
        {
            Verein = await VereineService.GetVerein(Convert.ToInt32(aSelectedId));

            if ((bool)aChecked)
            {
                if (!vereinesaison.Contains(Verein))
                    vereinesaison.Add(Verein);
            }
            else
            {
                if (vereinesaison.Contains(Verein))
                    vereinesaison.Remove(Verein);
            }

            if (vereinesaison.Count() == 5)
                Zuordnen();

            StateHasChanged();
        }

        public async void Zuordnen()
        {

            //for (int i = 0; i < VereineSaisonList.Count(); i++)
            //{
            //    Verein ver = new Verein();
            //    ver.VereinNr = Convert.ToInt32(VereineSaisonList[i].VereinID);
            //    ver.Vereinsname1 = VereineSaisonList[i].Vereinname1;

            //    vereinesaison.Add(ver);
            //}

            var vereine = await VereineService.CreateVereineSaison(vereinesaison);

            StateHasChanged();
        }

    }
}

