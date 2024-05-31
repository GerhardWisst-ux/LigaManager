using AutoMapper;
using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Models;
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
    public class EditPokalspieltagBase : ComponentBase
    {
        public bool allowVirtualization;
        public Int32 currentspieltag = Globals.Spieltag;
        protected string DisplayErrorRunde = "none";
        public string Vereinsname1;

        public string Vereinsname2;

        public string Stadion;

        public string Spielername;

        public string RundeChoosed;

        public List<DisplayRunde> RundeList;

        public DateTime? Time { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

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

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Runde { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

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
                    string returnUrl = WebUtility.UrlEncode($"/");
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }


                var saison = (await SaisonenService.GetSaisonen()).ToList().Where(x => x.Saisonname == Globals.currentSaison).First();

                var vereineSaison = await VereineService.GetVereine();
                List<Verein> verList = vereineSaison.ToList();

                for (int i = 0; i < verList.Count(); i++)
                {
                    var verein = await VereineService.GetVerein(verList[i].VereinNr);
                    VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                }

                if (Convert.ToInt32(Id) > 0)
                {
                    Spiel = await PokalergebnisseService.GetPokalergebnisSpieltag(Convert.ToInt32(Id));
                    Spiel.Saison = Globals.currentPokalSaison;
                    Spiel.SaisonID = Globals.PokalSaisonID;
                    //Spiel.SpieltagNr = SpieltagNr;
                }

                // var spiele = await PokalergebnisseService.GetPokalergebnisseSpieltag();


                if (Convert.ToInt32(Id) == 0)
                    Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, 0, 0, 0, DateTimeKind.Utc);
                else
                    Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, Spiel.Datum.Hour, Spiel.Datum.Minute, 0, DateTimeKind.Utc);


                RundeList = new List<DisplayRunde>
                {
                    new DisplayRunde("2", "2. Runde"),
                      new DisplayRunde("AF", "Achtelfinale"),
                       new DisplayRunde("VF", "Viertelfinale"),
                       new DisplayRunde("HF", "Halbfinale"),
                        new DisplayRunde("F", "Finale")
                };
                              
                if (Convert.ToInt32(Id) == 0)
                {
                    Runde = Globals.currentPokalRunde;
                    RundeChoosed = Runde;

                    StateHasChanged();
                }
                else
                {
                    RundeChoosed = Spiel.Runde;
                    Runde = RundeChoosed;
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

            //if (Id != null)
            //{
            //    SpielCombo = await SpieltagService.GetSpieltag(Convert.ToInt32(Id));
            //    Vereinsname1 = SpielCombo.Verein1;
            //    Vereinsname2 = SpielCombo.Verein2;
            //    Stadion = SpielCombo.Ort;
            //    //VereineList.Add(new DisplayVerein("0", "Verein wählen", ""));
            //}
            //else
            //{
            //    Vereinsname1 = null;
            //    Vereinsname2 = null;
            //}
        }

        public void Change(object value, string name, string action)
        {
            Console.WriteLine($"{name} item with index {value} {action}");
        }

        public async void Verein1Change(ChangeEventArgs e)
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

                //if (RundeChoosed == "2")
                //    Titel = "Pokalspiel Neuanlage 2. Runde";
                //else if (RundeChoosed == "AF")
                //    Titel = "Pokalspiel Neuanlage Achtelfinale";
                //else if (RundeChoosed == "VF")
                //    Titel = "Pokalspiel Neuanlage Viertelfinale";
                //else if (RundeChoosed == "HF")
                //    Titel = "Pokalspiel Neuanlage Halbfinale";
                //else
                //    Titel = "Pokalspiel Neuanlage Finale";
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
