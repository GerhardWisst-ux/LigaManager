using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class KaderBase : ComponentBase
    {
        protected bool bShowSpieler;
        protected string DisplayErrorSaison = "none";
        protected string DisplayErrorVerein = "none";

        public string DisplayTopButton = "none";

        [Inject]
        public IKaderService KaderService { get; set; }
        public IEnumerable<Kader> SpielerList { get; set; }

        public Int32 currentspieltag = 1;
        public string saison;
        public string VisibleAdd = "none";
        public List<DisplaySaison> SaisonenList = new List<DisplaySaison>();

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public Kader Spieler { get; set; } = new Kader();

        public IEnumerable<Saison> Saisonen { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();

        private bool bChangedSaison;
        private bool bChangedVerein;

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        public RadzenDataGrid<Kader> grid;
        IList<Tuple<Kader, RadzenDataGridColumn<Kader>>> selectedCellData = new List<Tuple<Kader, RadzenDataGridColumn<Kader>>>();

        string type = "Click";
        bool multiple = true;

        protected override async Task OnInitializedAsync()
        {            
            SaisonenList = new List<DisplaySaison>();
            Saisonen = (await SaisonenService.GetSaisonen()).ToList();

            for (int i = 0; i < Saisonen.Count(); i++)
            {
                var columns = Saisonen.ElementAt(i);
                SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
            }

            saison = Globals.currentSaison;
            SpielerList = (await KaderService.GetAllSpieler()).Where(x => x.SaisonId == 23).ToList();

            var spiele = await SpieltagService.GetSpieltage();
            List<Spieltag> spiele2 = spiele.Where(x => x.Saison == Globals.currentSaison && x.SpieltagNr == "1").Take(9).ToList();

            Vereine = (await VereineService.GetVereine()).ToList();
            VereineList = new List<DisplayVerein>();

            int iAnzahl = spiele2.Count() * 2;

            for (int i = 0; i < spiele2.Count(); i++)
            {
                VereineList.Add(new DisplayVerein(spiele2[i].Verein1_Nr, spiele2[i].Verein1));
                VereineList.Add(new DisplayVerein(spiele2[i].Verein2_Nr, spiele2[i].Verein2));
            }

            SpielerList = SpielerList.OrderByDescending(x => x.Tore);

            DisplayErrorVerein = "none";
            DisplayErrorSaison = "none";
            VisibleAdd = "none";

            bChangedVerein = false;
            bShowSpieler = false;

            DisplayTopButton = "none";
        }

        public void SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Globals.currentSaison = e.Value.ToString();
                Globals.SaisonID = 1;
                bChangedSaison = true;             
            }
        }

        public void VereinChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Globals.currentVereinID = Convert.ToInt32(e.Value);
                bChangedVerein=true;
                VisibleAdd = "block";
            }
        }

        public async void OnClickHandler()
        {
            if (Globals.currentSaison == null & Globals.currentLiga == null)
            {
                DisplayErrorVerein = "block";
                DisplayErrorSaison = "block";
                return;
            }

            if (Globals.currentSaison == null)
            {
                DisplayErrorSaison = "block";
                return;
            }

            if (Globals.currentLiga == null)
            {
                DisplayErrorVerein = "block";
                return;
            }

            DisplayErrorVerein = "none";
            DisplayErrorSaison = "none";

            bShowSpieler = true;
            VisibleAdd = "block";

            DisplayTopButton = "block";
            SpielerList = (await KaderService.GetAllSpieler()).Where(x => x.VereinID == Globals.currentVereinID).ToList();

            StateHasChanged();
        }

        public void Select(DataGridCellMouseEventArgs<Kader> args)
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
                selectedCellData.Add(new Tuple<Kader, RadzenDataGridColumn<Kader>>(args.Data, args.Column));
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

        public void OnCellClick(DataGridCellMouseEventArgs<Kader> args)
        {
            if (type == "Click")
            {
                Select(args);
            }
        }

        public void OnCellDoubleClick(DataGridCellMouseEventArgs<Kader> args)
        {
            if (type != "Click")
            {
                Select(args);
            }
        }

        public void OnCellRender(DataGridCellRenderEventArgs<Kader> args)
        {
            if (selectedCellData.Any(i => i.Item1 == args.Data && i.Item2 == args.Column))
            {
                args.Attributes.Add("style", $"background-color: var(--rz-secondary-lighter);");
            }
        }
        public void OnFilter(DataGridColumnFilterEventArgs<Kader> args)
        {
            //
        }
    }


    public class DisplaySaison
    {
        public DisplaySaison(int saisonID, string saisonname)
        {
            SaisonID = saisonID;
            Saisonname = saisonname;
        }
        public int SaisonID { get; set; }
        public string Saisonname { get; set; }
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

}
