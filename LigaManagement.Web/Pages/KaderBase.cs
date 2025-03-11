using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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
        protected bool busy;
        protected string DisplayErrorSaison = "none";
        protected string DisplayErrorVerein = "none";
        public string DisplayTopButton = "none";

        [Inject]
        public IKaderService KaderService { get; set; }
        public IEnumerable<Kader> SpielerList { get; set; }

        public bool IsLoading = false;
        public string saison;
        public bool VisibleAdd;
        public List<DisplaySaison> SaisonenList = new List<DisplaySaison>();

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public Kader Spieler { get; set; } = new Kader();

        public IEnumerable<Saison> Saisonen { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();

        private bool bChangedSaison;
        private bool bChangedVerein;

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        public RadzenDataGrid<Kader> grid;
        IList<Tuple<Kader, RadzenDataGridColumn<Kader>>> selectedCellData = new List<Tuple<Kader, RadzenDataGridColumn<Kader>>>();

        [Inject]
        public IStringLocalizer<KaderList> Localizer { get; set; }

        private int VereinNr;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            try
            {
                await LoadInitialDataAsync();
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine($"Fehler beim Laden der Daten: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadInitialDataAsync()
        {
            SaisonenList = new List<DisplaySaison>();
            Saisonen = (await SaisonenService.GetSaisonen()).OrderBy(x => x.Saisonname).ToList();

            foreach (var saison in Saisonen)
            {
                if (saison.LigaID ==1) // Bundesliga
                {
                    SaisonenList.Add(new DisplaySaison(saison.SaisonID, saison.Saisonname));
                }
            }

            SpielerList = (await KaderService.GetAllSpieler()).Where(x => x.SaisonId == Globals.KaderSaisonID).ToList();

            var aktuelleSaison = Saisonen.FirstOrDefault(x => x.SaisonID == Globals.KaderSaisonID);
            if (aktuelleSaison != null)
            {
                var vereineSaison = await VereineSaisonService.GetVereineSaison();
                var verList = vereineSaison.Where(x => x.SaisonID == aktuelleSaison.SaisonID).ToList();

                foreach (var ver in verList)
                {
                    var verein = await VereineService.GetVerein(ver.VereinNr);
                    VereineList.Add(new DisplayVerein(ver.VereinNr.ToString(), verein.Vereinsname1));
                }
            }

            SpielerList = SpielerList.OrderByDescending(x => x.PositionsNr).ToList();

            DisplayErrorVerein = "none";
            DisplayErrorSaison = "none";
            VisibleAdd = false;

            bChangedVerein = false;
            bShowSpieler = false;

            DisplayTopButton = "none";

            if (Globals.KaderVereinNr > 0)
            {
                await OnClickHandlerAsync();
            }

            VereinNr = Globals.KaderVereinNr;
            Globals.bVisibleNavMenuElements = true;
        }

        public void SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Globals.KaderSaisonID = Convert.ToInt32(e.Value);
                bChangedSaison = true;
            }
        }

        public void VereinChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Value = 0;
                }

                Globals.KaderVereinNr = Convert.ToInt32(e.Value);
                VereinNr = Convert.ToInt32(e.Value);
                bChangedVerein = true;

                if (VereinNr > 0)
                {
                    VisibleAdd = false;
                }
            }
        }

        public async Task OnClickHandlerAsync()
        {
            IsLoading = true;
            busy = true;
            try
            {
                if (Globals.currentSaison == null && Globals.currentLiga == null)
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

                if (Globals.KaderVereinNr == 0)
                {
                    DisplayErrorVerein = "block";
                    return;
                }

                DisplayErrorVerein = "none";
                DisplayErrorSaison = "none";

                bShowSpieler = true;
                VisibleAdd = true;

                DisplayTopButton = "block";
                SpielerList = (await KaderService.GetAllSpieler())
                    .OrderBy(x => x.PositionsNr).ThenByDescending(x => x.Einsaetze)
                    .Where(x => x.SaisonId == Globals.KaderSaisonID && x.VereinID == Globals.KaderVereinNr)                    
                    .ToList();

                IsLoading = false;
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine($"Fehler beim Laden des Spielers: {ex.Message}");
            }
            finally
            {
                busy = false;
                IsLoading = false;
                StateHasChanged();
            }
        }

        public void OnFilter(DataGridColumnFilterEventArgs<Kader> args)
        {
            // Filterlogik hier implementieren
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
