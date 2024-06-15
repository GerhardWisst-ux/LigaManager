using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace LigamanagerManagement.Web.Pages
{
    public class EditEMWMSpieltagBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Runde { get; set; }

        public Int32 currentspieltag = Globals.Spieltag;
        protected string DisplayErrorRunde = "none";
        public string RundeChoosed;

        public List<DisplayRunde> RundeList;

        public DateTime? Time { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public ISpieltageEMWMService SpieltagService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ISaisonenCLService SaisonenEMWMService { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();

        public IEnumerable<Spieltag> spieltage { get; set; }

        public PokalergebnisCLSpieltag Spiel { get; set; } = new PokalergebnisCLSpieltag();

        public IEnumerable<Verein> Vereine { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public bool Collapsed = true;

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
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }

                var saison = (await SaisonenEMWMService.GetSaisonen()).ToList().Where(x => x.Saisonname == Globals.currentEMWMSaison).First();

                var vereineSaison = await VereineService.GetVereineEMWM();
                var verList = vereineSaison.ToList(); //.Where(x => x.SaisonID == Globals.EMWMSaisonID).ToList();
                
                for (int i = 0; i < verList.Count(); i++)
                {
                    var verein = await VereineService.GetVereinEMWM(verList[i].VereinNr);

                    if (!VereineList.Contains(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion)))
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                }

                if (Convert.ToInt32(Id) > 0)
                {
                    Spiel = await SpieltagService.GetSpieltag(Convert.ToInt32(Id));
                    Spiel.Saison = Globals.currentPokalSaison;
                    Spiel.SaisonID = Globals.CLSaisonID;
                }

                if (Convert.ToInt32(Id) == 0)
                    Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, 0, 0, 0, DateTimeKind.Utc);
                else
                    Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, Spiel.Datum.Hour, Spiel.Datum.Minute, 0, DateTimeKind.Utc);


                RundeList = new List<DisplayRunde>
                {
                    new DisplayRunde("G1", "Gruppenphase Spieltag 1"),
                    new DisplayRunde("G2", "Gruppenphase Spieltag 2"),
                    new DisplayRunde("G3", "Gruppenphase Spieltag 3"),
                    new DisplayRunde("G4", "Gruppenphase Spieltag 4"),
                    new DisplayRunde("G5", "Gruppenphase Spieltag 5"),
                    new DisplayRunde("G6", "Gruppenphase Spieltag 6"),
                    new DisplayRunde("AF", "Achtelfinale"),
                    new DisplayRunde("VF", "Viertelfinale"),
                    new DisplayRunde("HF", "Halbfinale"),
                    new DisplayRunde("F", "Finale")
                };

                if (Convert.ToInt32(Id) == 0)
                {
                    Runde = Globals.currentClRunde;
                    RundeChoosed = Runde;

                    StateHasChanged();
                }
                else
                {
                    RundeChoosed = Spiel.Runde;
                    Runde = RundeChoosed;
                    Globals.currentClRunde = RundeChoosed;
                    Spiel.Runde = Runde;

                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }




        }
        protected async override void OnAfterRender(bool firstRender)
        {


        }

        public void Change(object value, string name, string action)
        {
            Console.WriteLine($"{name} item with index {value} {action}");
        }

        public async void Verein1Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var verein = await VereineService.GetVereinCL(Convert.ToInt32(e.Value.ToString()));
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
                var verein = await VereineService.GetVereinCL(Convert.ToInt32(e.Value.ToString()));
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
        public class DisplayRunde
        {
            public DisplayRunde(string rundeiD, string rundename)
            {
                RundeID = rundeiD;
                Rundename = rundename;
            }
            public string RundeID { get; set; }
            public string Rundename { get; set; }
        }

        protected Ligamanager.Components.ConfirmBase DeleteConfirmation { get; set; }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }
    }
}
