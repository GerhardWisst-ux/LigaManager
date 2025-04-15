using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
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
    public class EditPokalspieltagBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Runde { get; set; }
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        public bool allowVirtualization;
        public bool DisabledRunde = false;
        public int currentspieltag = Globals.Spieltag;
        protected string DisplayErrorRunde = "none";
        

        public string RundeChoosed;
        public bool IsLoading = false;
        public bool Collapsed = true;
        public bool bDeleteButtonVisible = true;
        public string Stadion;

        public string Spielername;
        public string Vereinsname1;
        public string Vereinsname2;       
        public string Titel { get; set; }

        public List<DisplayRunde> RundeList;

        public DateTime? Time { get; set; }

        [Inject]
        public IPokalergebnisseService PokalergebnisseService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        [Inject]
        public ISpielerSpieltagService SpielerSpieltagService { get; set; }


        public List<DisplayVerein> VereineList = new List<DisplayVerein>();


        public List<DisplaySpieler> SpielerList1 = new List<DisplaySpieler>();
        public List<DisplaySpieler> SpielerList2 = new List<DisplaySpieler>();

        public IEnumerable<Spieltag> spieltage { get; set; }

        public EditSpieltagModel EditSpieltagModel { get; set; } =
            new EditSpieltagModel();

        public PokalergebnisSpieltag Spiel { get; set; } = new PokalergebnisSpieltag();

        public Tore Tor { get; set; } = new Tore();

        public PokalergebnisSpieltag SpielCombo { get; set; } = new PokalergebnisSpieltag();

        public IEnumerable<Verein> Vereine { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<EditPokalspieltag> Localizer { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        NotificationService NotificationService = new NotificationService();

       
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
                    string returnUrl = WebUtility.UrlEncode($"/Ligamanager");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                }
                IsLoading = true;
                await InitializeData();
                IsLoading = false;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                IsLoading = false;
            }

        }
        private void SetTitelBasedOnRunde()
        {
            if (Id == "0" || Id is null)
            {
                Titel = Runde switch
                {
                    "2" => "Pokalspiel Neuanlage 2. Runde",
                    "AF" => "Pokalspiel Neuanlage Achtelfinale",
                    "VF" => "Pokalspiel Neuanlage Viertelfinale",
                    "HF" => "Pokalspiel Neuanlage Halbfinale",
                    _ => "Pokalspiel Neuanlage Finale"
                };
            }
            else
            {
                Titel = "Pokal Bearbeiten";
            }
        }

        private async Task InitializeData()
        {          

            SetTitelBasedOnRunde();

            
            IsLoading = true;

            if (!(Id == "0" || Id is null))
            {             
                Spiel = await PokalergebnisseService.GetPokalergebnisSpieltag(Convert.ToInt32(Id));
                Spiel.Saison = Globals.currentPokalSaison;
                Spiel.SaisonID = Globals.CLSaisonID;            
            }

            var saison = (await SaisonenService.GetSaisonen()).ToList().Where(x => x.Saisonname == Globals.currentSaison).First();

            var vereineSaison = await VereineService.GetVereine();
            List<Verein> verList = vereineSaison.ToList();

            for (int i = 0; i < verList.Count(); i++)
            {
                var verein = await VereineService.GetVerein(verList[i].VereinNr);
                VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
            }

            if (Convert.ToInt32(Id) == 0)
                Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, 0, 0, 0, DateTimeKind.Utc);
            else
                Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, Spiel.Datum.Hour, Spiel.Datum.Minute, 0, DateTimeKind.Utc);

            RundeList = new List<DisplayRunde>
                {
                    new DisplayRunde(PokalRunden.Runde2, Localizer["2. Runde"].Value),
                    new DisplayRunde(PokalRunden.Achtelfinale, Localizer["Achtelfinale"].Value),
                    new DisplayRunde(PokalRunden.Viertelfinale, Localizer["Viertelfinale"].Value),
                    new DisplayRunde(PokalRunden.Halbfinale, Localizer["Halbfinale"].Value),
                    new DisplayRunde(PokalRunden.Finale, Localizer["Finale"].Value),
                };

            

            if (Convert.ToInt32(Id) == 0)
            {
                Runde = Globals.currentPokalRunde;
                RundeChoosed = Runde;
            }
            else
            {
                RundeChoosed = Spiel.Runde;
                Runde = RundeChoosed;
                Spiel.Runde = Runde;
            }

            IsLoading = false;

            if (Id == "0" || Id is null)
                DisabledRunde = false;
            else
                DisabledRunde = true;

            StateHasChanged();
        }


        public async Task Verein1Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var verein = await VereineService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                Spiel.Verein1 = verein.Vereinsname1;
                Spiel.Verein1_Nr = int.Parse(e.Value.ToString());
                Spiel.Ort = verein.Stadion;
                Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);
            }
            StateHasChanged();
        }

        public async void Verein2Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var verein = await VereineService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                Spiel.Verein2 = verein.Vereinsname1;
                Spiel.Verein2_Nr = int.Parse(e.Value.ToString());
            }
            StateHasChanged();
        }
        public void StadionChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                int index = VereineList.FindIndex(x => x.VereinID == e.Value.ToString());
                Spiel.Ort = VereineList[index].Ort;
            }


            StateHasChanged();
        }


        public async void RundeChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                RundeChoosed = e.Value.ToString();

                Globals.currentPokalRunde = RundeChoosed;

             
            }
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

        [Bind]
        public class DisplaySpieler
        {
            public DisplaySpieler(int spielerID, string spielername)
            {
                SpielerID = spielerID;
                Spielername = spielername;
            }
            public int SpielerID { get; set; }
            public string Spielername { get; set; }
        }

        [Bind]
        public class DisplayRunde
        {
            public DisplayRunde(string rundeKurzbezeichung, string rundename)
            {
                RundeKurzbezeichung = rundeKurzbezeichung;
                Rundename = rundename;
            }
            public string RundeKurzbezeichung { get; set; }
            public string Rundename { get; set; }
        }

        public static class PokalRunden
        {
            public const string Runde2 = "2";
            public const string Achtelfinale = "AF";
            public const string Viertelfinale = "VF";
            public const string Halbfinale = "HF";
            public const string Finale = "F";
        }

        protected ConfirmBase DeleteConfirmation { get; set; }

        protected async Task<bool> Confirm()
        {
            string message;

            if (Globals.CurrentRole == "USER" || Globals.CurrentRole == "GUEST")
            {
                message = "Sie können dieses Pokalspiel nicht löschen";
                await JSRuntime.InvokeVoidAsync("alert", message);

                //NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Sie können dieses Pokalspiel nicht löschen", Detail = "Löschen" });
                return false;
            }

            message = Localizer["Möchten Sie dieses Pokalspiel tatsächlich löschen?"].Value;
            var result = await JSRuntime.InvokeAsync<bool>("confirm", new[] { message });

            if (result)
            {
                await PokalergebnisseService.DeletePokalergebnis(Convert.ToInt32(Id));

                //NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Info, Summary = Localizer["Löschen Pokalspiel"].Value, Detail = Localizer["Gelöscht"].Value });
            }
            message = "Pokalspiel wurde gelöscht";
            await JSRuntime.InvokeVoidAsync("alert", message);

            return result;
        }
    }


}
