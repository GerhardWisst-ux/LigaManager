using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using static LigaManagement.Web.Pages.EinstiegListBase;

namespace LigaManagerManagement.Web.Pages
{
    public class CreateSaisonListBase : ComponentBase
    {
        protected string DisplayErrorLiga = "display:none;";
        public string Liganame = "Bundesliga";
        public int LandID = 0;
        protected int LigaID = 0;
        public Density Density = Density.Compact;
        public string searchfeld = string.Empty;

        [Parameter]
        public string SaisonId { get; set; }

        public Saison Saison { get; set; } = new Saison();

        [Inject]
        public ISaisonenService SaisonenService { get; set; }
        public IEnumerable<Saison> SaisonenList { get; set; }


        [Inject]
        public ILandService LandService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        [Inject]
        public ILandService LaenderService { get; set; }

        [Inject]
        public ILigaService LigenService { get; set; }

        public List<DisplayLiga> LigenList;
        public bool IsLoading = false;
        public List<Verein> Vereine { get; set; }

        public IEnumerable<Liga> Ligen { get; set; }
        public Verein Verein { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();
        public List<DisplayVerein> VereineSaisonList = new List<DisplayVerein>();

        public RadzenDataGrid<Saison> grid;
        IList<Tuple<Saison, RadzenDataGridColumn<Saison>>> selectedCellData = new List<Tuple<Saison, RadzenDataGridColumn<Saison>>>();

        public List<Verein> vereinesaison = new List<Verein>();

        public List<Verein> vereinesaisonSelected = new List<Verein>();

        // Liste der ausgewählten Vereine
        public List<DisplayVerein> SelectedVereine { get; set; } = new List<DisplayVerein>();


        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<EditSaison> Localizer { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {

                var authenticationState = await authenticationStateTask;

                if (authenticationState.User.Identity == null)
                {
                    return;
                }

                if (!authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/Ligamanager");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                }

                IsLoading = true;
                LigenList = new List<DisplayLiga>();
                Ligen = (await LigaService.GetLigen()).ToList();

                for (int i = 0; i < Ligen.Count(); i++)
                {
                    var columns = Ligen.ElementAt(i);
                    LigenList.Add(new DisplayLiga(columns.Aktiv, columns.Id, columns.Id, columns.Liganame, columns.EMWM));
                }

                SaisonenList = (await SaisonenService.GetSaisonen()).ToList();

                VereineList = new List<DisplayVerein>();

                Vereine = (await VereineService.GetVereine()).ToList();



                foreach (var item in Vereine)
                {
                    if (SaisonId == "0")
                        VereineList.Add(new DisplayVerein(item.VereinNr.ToString(), item.Vereinsname1, false));
                    else
                    {
                        VereineList.Add(new DisplayVerein(item.VereinNr.ToString(), item.Vereinsname1, true));
                    }
                }


                DisplayErrorLiga = "display:none;";

                Globals.bVisibleNavMenuElements = true;

                LigaChange(new ChangeEventArgs());

                IsLoading = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

                IsLoading = false;
            }
        }

        [Bind]
        public class DisplayVerein
        {
            public DisplayVerein(string vereinID, string vereinname, bool vereinchecked)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
                VereinChecked = vereinchecked;
            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }
            public bool VereinChecked { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is DisplayVerein other)
                {
                    return VereinID == other.VereinID; // Oder eine andere Eigenschaft, die Objekte eindeutig macht
                }
                return false;
            }

            public override int GetHashCode()
            {
                return VereinID.GetHashCode(); // Konsistenter HashCode basierend auf der eindeutigen Eigenschaft
            }
        }

        // Funktion zum Hinzufügen oder Entfernen eines Vereins aus der Auswahl
        protected void ToggleSelection(DisplayVerein verein)
        {
            if (SelectedVereine.Contains(verein))
            {
                SelectedVereine.Remove(verein);
            }
            else if (SelectedVereine.Count < Saison.AnzahlVereine)
            {
                SelectedVereine.Add(verein);
            }
        }

        // Fix for the CS1503 error in the GetSelectedClass method
        protected string GetSelectedClass(DisplayVerein verein)
        {
            return SelectedVereine.Contains(verein) ? "selected" : "";
        }

        private DisplayVerein _currentVerein;

        public void SetSelectedClass(DisplayVerein verein)
        {
            if (verein != null)
            {
                _currentVerein = verein;

                // Optional: Logic to update SelectedVereine or trigger UI updates
                if (!SelectedVereine.Contains(verein))
                {
                    SelectedVereine.Add(verein);
                }
            }
        }

        public async void CheckboxClicked(string aSelectedId, object aChecked)
        {
            try
            {
                Verein = await VereineService.GetVerein(Convert.ToInt32(aSelectedId));

                var isVereinInList = vereinesaisonSelected.FirstOrDefault(x => x.Vereinsname1 == Verein.Vereinsname1);

                if (vereinesaisonSelected == null)
                    throw new Exception("vereinesaisonSelected null");

                if ((bool)aChecked)
                {
                    if (isVereinInList == null)
                        vereinesaisonSelected.Add(Verein);
                }
                else
                {
                    if (isVereinInList != null)
                        vereinesaisonSelected.Remove(isVereinInList);
                }

                StateHasChanged();
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

            }
        }


        public async void LigaChange(ChangeEventArgs e)
        {
            try
            {
                if (e.Value != null)
                {
                    LigaID = Convert.ToInt32(e.Value);

                    var liga = await LigaService.GetLiga(LigaID);
                    Liganame = liga.Liganame;
                    LandID = liga.LandID;

                    if (LigaID == 0)
                        DisplayErrorLiga = "display:block;";
                    else
                        DisplayErrorLiga = "display:none;";


                }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

            }
        }
    }
}

