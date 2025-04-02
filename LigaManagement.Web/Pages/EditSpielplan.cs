using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace LigamanagerManagement.Web.Pages
{
    public class EditSpielplanBase : ComponentBase
    {
        [Parameter]
        public string SaisonID { get; set; }
        [Parameter]
        public string SpielplanID { get; set; }

        public string Titel;
        public bool IsLoading = true;
        [Parameter]
        public string SpieltagNr { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        DialogService DialogService { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        public bool popup;
        public bool allowVirtualization;
        public int currentspieltag = 1;
        public int currentliganummer = 1;

        public string currentsaison;
        protected bool isDropdownDisabledSaison = true;
        public List<DisplaySpieltag> SpieltagPlanList;
        private int iSpieltage;

        public string Vereinsname1;
        public string Vereinsname2;

        public string Stadionname { get; set; }
        public IEnumerable<Stadion> StadionList { get; set; }

        [Inject]
        public IStadionService StadionService { get; set; }
              
        public DateTime? Time { get; set; }

        [Inject]
        public ISpielplanService SpielplanService { get; set; }


        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        [Inject]
        public ISpielerSpieltagService SpielerSpieltagService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<EditSpieltag> Localizer { get; set; }

        LigaManagerManagement.Web.Services.LigaManagerAuthenticationStateProvider _LigaManagerAuthenticationStateProvider { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();
        public List<DisplaySaison> SaisonenList { get; set; } = new List<DisplaySaison>();
        public IEnumerable<Saison> Saisonen { get; set; }
        
        public IEnumerable<Spieltag> spieltage { get; set; }

        public EditSpieltagModel EditSpieltagModel { get; set; } =
            new EditSpieltagModel();

        public Spielplan Spiel { get; set; } = new Spielplan();

        public IEnumerable<Verein> Vereine { get; set; }

        private NotificationService NotificationService = new NotificationService();

        public bool bAbgeschlossen = true;

        public bool bDeleteButtonVisible = true;
        Saison saison;

        protected async override Task OnInitializedAsync()
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
                    string returnUrl = WebUtility.UrlEncode($"/editSpielplan/{SaisonID}/{SpielplanID}");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                }

                IsLoading = true;

                saison = await SaisonenService.GetSaison(Convert.ToInt32(SaisonID));

                if (Convert.ToInt32(SpielplanID) > 0)
                {
                    Spiel = await SpielplanService.GetSpielplan(Convert.ToInt32(SpielplanID));
                    if (Spiel != null)
                    {                        
                        currentsaison = String.Concat(Localizer["Spielplan"].Value, " ", Spiel.SpieltagNr, ".", Localizer["Spieltag"].Value, " ", saison.Saisonname);
                        currentspieltag = Convert.ToInt32(Spiel.SpieltagNr);
                        currentliganummer = Convert.ToInt32(Spiel.LigaID);

                        Spiel.Saison = currentsaison;
                        Spiel.SaisonID = Convert.ToInt32(SaisonID);
                        Spiel.SpieltagNr = SpieltagNr;
                        
                        Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, Spiel.Datum.Hour, Spiel.Datum.Minute, 0, DateTimeKind.Utc);
                    }
                    else
                    {
                        currentsaison = String.Concat(Localizer["Spielplan"].Value, " ", "1.", Localizer["Spieltag"].Value, " ", saison.Saisonname);
                        currentspieltag = 1;
                        currentliganummer = saison.LigaID;                    
                    }
                }
                else
                {
                    currentsaison = String.Concat(Localizer["Spielplan"].Value, " ", "1.", Localizer["Spieltag"].Value, " ", saison.Saisonname);
                    currentspieltag = Convert.ToInt32(SpieltagNr); 
                    
                    currentliganummer = saison.LigaID;                    
                }

                SaisonenList = new List<DisplaySaison>();
                Saisonen = (await SaisonenService.GetSaisonen()).Where(x => x.LigaID == Globals.LigaID && x.LandID == Globals.LandID).ToList();
                if (currentliganummer == 0)
                {
                    SaisonenList.Clear();
                    isDropdownDisabledSaison = false;
                }
                else
                {
                    for (int i = 0; i < Saisonen.Count(); i++)
                    {
                        var columns = Saisonen.ElementAt(i);                        
                        SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname, true));
                    }

                    isDropdownDisabledSaison = true;
                }

                SpieltagPlanList = new List<DisplaySpieltag>();

                int iSpieltage = 34;
                if (currentliganummer == 1)
                {
                    if (currentsaison.Substring(0, 4) == "1963" || currentsaison.Substring(0, 4) == "1964")
                        iSpieltage = 30;
                    else if (currentsaison.Substring(0, 4) == "1991")
                        iSpieltage = 38;
                    else
                        iSpieltage = 34;
                }
                else if (currentliganummer == 2)
                {
                    if (currentsaison.Substring(0, 4) == "1993")
                        iSpieltage = 38;
                    else
                        iSpieltage = 34;
                }
                else if (currentliganummer == 3)
                {
                    iSpieltage = 38;
                }

                for (int i = 1; i <= iSpieltage; i++)
                {
                    SpieltagPlanList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + "." + Localizer["Spieltag"].Value));
                }

                if (currentliganummer < 3)
                {
                 
                    var vereineSaison = await VereineSaisonService.GetVereineSaison();
                    List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == Convert.ToInt32(SaisonID)).ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineService.GetVerein(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname2, verein.Stadion));
                    }

                    SpieltagNr = currentspieltag.ToString();
                }
                if (currentliganummer == 3 || currentliganummer == 20 || currentliganummer == 21)
                {                    

                    var vereineSaison = await SpielplanService.GetVereineL3();
                    List<VereinAktSaison> verList = vereineSaison.Where(x => x.SaisonID == Convert.ToInt32(SaisonID)).ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineService.GetVereinL3(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }

                    SpieltagNr = currentspieltag.ToString();
                }
               

                if (SpielplanID == "0")
                {
                    Time = new DateTime(Convert.ToInt32(saison.Saisonname.Substring(0, 4)), 8, 20, 15, 30, 0, DateTimeKind.Utc);
                }
                else
                {
                    if (SpielplanID == null)
                        Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, Spiel.Datum.Hour, Spiel.Datum.Minute, 0, DateTimeKind.Utc);

                    Stadion stadion = null;
                    if (currentliganummer < 4)
                    {
                        stadion = await StadionService.GetStadion(Convert.ToInt32(Spiel.StadionID));
                        Stadionname = stadion?.Stadionname;
                        Spiel.StadionID = stadion?.Id;
                        Spiel.Ort = stadion?.Stadionname;
                        SpielplanID = Spiel.SpieltagId.ToString();
                    }                   
                }

                Titel = Localizer["Spielplan"].Value + " " + saison.Saisonname + ":" + " " + SpieltagNr + ". " + Localizer["Spieltag"].Value;

                IsLoading = false;

                StateHasChanged();
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                IsLoading = false;
            }
        }

        public void SpieltagChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                IsLoading = true;
                currentspieltag = Convert.ToInt32(e.Value);
                SpieltagNr = e.Value.ToString();

                Titel = Localizer["Spielplan"].Value + " " + saison.Saisonname + ":" + " " + SpieltagNr + ". " + Localizer["Spieltag"].Value;
                IsLoading = false;
                StateHasChanged();
            }
        }

      
        public async void Verein1Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (Globals.LigaID < 3)
                {
                    var verein = await VereineService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein1 = verein.Vereinsname1;
                    Spiel.Verein1_Nr = e.Value.ToString();
                    Spiel.Ort = verein.Stadion;
                    Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);

                    var stadien = await StadionService.GetStadien();
                    var stadion = stadien.Where(x => x.VereinNr == Convert.ToInt32(Spiel.Verein1_Nr?.ToString()) && x.JahrVonDate < Spiel.Datum && x.JahrBisDate > Spiel.Datum).ToList();
                    if (stadion.Count == 1)  // ein Stadion gefunden
                    {
                        Stadionname = stadion[0].Stadionname;
                        Spiel.StadionID = stadion[0].Id;
                    }
                    else
                    {
                        Stadionname = "kein Stadion gefunden";
                        Spiel.StadionID =0;
                    }
                }
                if (Globals.LigaID == 3)
                {
                    var verein = await VereineService.GetVereinL3(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein1 = verein.Vereinsname1;
                    Spiel.Verein1_Nr = e.Value.ToString();
                    Spiel.Ort = verein.Stadion;
                    Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);
                }

            }
            StateHasChanged();
        }

        public async void Verein2Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (Globals.LigaID < 3)
                {
                    var verein = await VereineService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();

                }
                if (Globals.LigaID == 3)
                {
                    var verein = await VereineService.GetVereinL3(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();

                }

            }
            StateHasChanged();
        }
        protected void StadionChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                int index = VereineList.FindIndex(x => x.VereinID == e.Value.ToString());
                Spiel.Ort = VereineList[index].Ort;
            }

            StateHasChanged();
        }


        protected void DatumChange(ChangeEventArgs args)
        {

        }



        [Bind]
        public class DisplaySpieltag(string nummer, string name)
        {
            public string Nummer { get; set; } = nummer;
            public string Name { get; set; } = name;
        }

        [Bind]
        public class DisplayVerein
        {
            public DisplayVerein(string vereinID, string vereinname, string ort)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
                Ort = ort;
            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }
            public string Ort { get; set; }
        }

        protected ConfirmBase DeleteConfirmation { get; set; }

        public async Task<bool> OnDeleteClick()
        {
            try
            {
                string message;
                if (Globals.CurrentRole == "USER" || Globals.CurrentRole == "GUEST")
                {
                    message = "Sie können dieses Spiel nicht löschen";
                    await JSRuntime.InvokeVoidAsync("alert", message);
                    return false;
                }

                var result = await DialogService.Confirm("Möchten Sie dieses Spiel tatsächlich löschen?", "Löschen Spiel",
                    new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });

                if (result.HasValue && result.Value)
                {
                    // Die Bestätigung war positiv, führe die Lösch-Logik aus
                    DeleteItem();
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return false;
            }
        }
        private void DeleteItem()
        {
            // Deine Logik zum Löschen
            Console.WriteLine("Das Spiel wurde gelöscht.");
        }
        protected async Task<bool> Confirm()
        {
            string message;

            if (Globals.CurrentRole == "USER" || Globals.CurrentRole == "GUEST")
            {
                message = "Sie können dieses Spiel nicht löschen";
                await JSRuntime.InvokeVoidAsync("alert", message);

                //NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Sie können diese Spiel nicht löschen", Detail = "Löschen" });
                return false;
            }

            message = Localizer["Möchten Sie dieses Spiel tatsächlich löschen?"].Value;

            var result = await JSRuntime.InvokeAsync<bool>("confirm", new[] { message });

            if (result)
            {

                await SpielplanService.DeleteSpielplan(Convert.ToInt32(SpielplanID));

                //NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Info, Summary = Localizer["Löschen Spiel"].Value, Detail = Localizer["Gelöscht"].Value });
                message = "Spiel wurde gelöscht";
                await JSRuntime.InvokeVoidAsync("alert", message);
            }

            return result;
        }
    }


}
