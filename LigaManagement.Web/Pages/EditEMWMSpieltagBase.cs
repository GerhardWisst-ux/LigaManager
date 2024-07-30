using LigaManagement.Models;
using LigaManagement.Web.Classes;
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
using static LigaManagement.Web.Pages.EM_WMListbase;

namespace LigamanagerManagement.Web.Pages
{
    public class EditEMWMSpieltagBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Runde { get; set; }

        [Parameter]
        public string Gruppe { get; set; }

        public Int32 currentspieltag = Globals.Spieltag;
        protected string DisplayErrorRunde = "none;";
        public string RundeChoosed;
        public int GruppeChoosed;
        protected string GroupVisible = "none;";
        
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

        [Inject]
        public IStringLocalizer<EditEMWMSpieltag> Localizer { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        NotificationService NotificationService = new NotificationService();

        public bool Collapsed = true;

        public bool bDeleteButtonVisible = true;

        public string sGroupABCDVisible = "block;";
        public string sGroupGHVisible = "none;";
        public string sGroupEFVisible = "none;";

        protected async override Task OnInitializedAsync()
        {
            try
            {
                List<VereinAktSaison> verList = new List<VereinAktSaison>();
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

                var saison = (await SaisonenEMWMService.GetSaisonen()).ToList().Where(x => x.Saisonname == Globals.currentEMWMSaison).First();

                var vereineSaison = await VereineService.GetVereineEMWM();

                if (saison.Saisonname.ToString() == "EM 2024")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2024 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 2022")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2022 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 2020")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2020 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 2018")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2018 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 2016")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2016 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 2014")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2014 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 2012")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2012 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 2010")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2010 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 2008")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2008 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 2006")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2006 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 2004")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2004 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 2002")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2002 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 2000")
                    verList = vereineSaison.ToList().Where(x => x.GroupID2000 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1998")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1998 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 1996")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1996 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1994")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1994 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 1992")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1992 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1990")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1990 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 1988")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1988 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1986")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1986 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 1984")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1984 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1982")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1982 > 0).ToList();
                else if (saison.Saisonname.ToString() == "EM 1980")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1980 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1978")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1978 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1974")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1974 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1970")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1970 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1966")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1966 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1962")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1962 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1958")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1958 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1954")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1954 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1950")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1950 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1938")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1938 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1934")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1934 > 0).ToList();
                else if (saison.Saisonname.ToString() == "WM 1930")
                    verList = vereineSaison.ToList().Where(x => x.GroupID1930 > 0).ToList();

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


                if (Convert.ToInt32(Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4)) > 1938)
                {
                    RundeList = new List<DisplayRunde>
                    {

                if (Convert.ToInt32(Id) == 0)
                {
                    Runde = Globals.currentEMWMRunde;
                    RundeChoosed = Runde;
                    GruppeChoosed = 1;
                }
                else
                {
                    RundeChoosed = Spiel.Runde;
                    Gruppe = Convert.ToInt32(Spiel.GroupID).ToString();
                    GruppeChoosed = Convert.ToInt32(Spiel.GroupID);
                    Runde = RundeChoosed;
                    Globals.currentEMWMRunde = RundeChoosed;
                    Spiel.Runde = Runde;
                }

                if (RundeChoosed == "G1" || RundeChoosed == "G2" || RundeChoosed == "G3")
                    GroupVisible = "inline-block;";
                else
                    GroupVisible = "none;";


                if (Globals.currentEMWMSaison.StartsWith("WM") && (Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1990"
                  && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1986"
                  && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1982"
                  && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1970")
                  && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1966"
                  && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1962"
                  && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1958"
                  && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1954"
                  && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1950")
                    sGroupGHVisible = "inline-block;";
                else
                    sGroupGHVisible = "none;";

                if (Globals.currentEMWMSaison.StartsWith("WM")
                    && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1978"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1974"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1970"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1966"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1962"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1958"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1954"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1950"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1938"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1934"
                    || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1930")
                    sGroupEFVisible = "none;";
                else
                    sGroupEFVisible = "inline-block;";

            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        private void ShowRunden()
        {
            if (Globals.currentEMWMSaison.IndexOf("1974") > -1 || Globals.currentEMWMSaison.IndexOf("1978") > -1)
            {
                RundeList = new List<DisplayRunde>
                    {
                        new DisplayRunde("G1",Localizer["Gruppenphase Spieltag"].Value + " " + 1),
                        new DisplayRunde("G2", Localizer["Gruppenphase Spieltag"].Value + " " + 2),
                        new DisplayRunde("G3", Localizer["Gruppenphase Spieltag"].Value + " " + 3),
                        new DisplayRunde("2G1",Localizer["2.Finalrunde Spieltag"].Value + " " + 1),
                        new DisplayRunde("2G2",Localizer["2.Finalrunde Spieltag"].Value + " " + 2),
                        new DisplayRunde("2G3",Localizer["2.Finalrunde Spieltag"].Value + " " + 3),
                        new DisplayRunde("F3",Localizer["Spiel um Platz 3"].Value),
                        new DisplayRunde("F", Localizer["Finale"].Value),
                    };
            }
            else if (Globals.currentEMWMSaison.IndexOf("1950") > -1)
            {
                RundeList = new List<DisplayRunde>
                    {
                        new DisplayRunde("G1",Localizer["Gruppenphase Spieltag"].Value + " " + 1),
                        new DisplayRunde("G2", Localizer["Gruppenphase Spieltag"].Value + " " + 2),
                        new DisplayRunde("G3", Localizer["Gruppenphase Spieltag"].Value + " " + 3),
                        new DisplayRunde("F",Localizer["Finalrunde"].Value + " " + 1),
                    };
            }
            else
            {
                RundeList = new List<DisplayRunde>
                    {
                        new DisplayRunde("G1",Localizer["Gruppenphase Spieltag"].Value + " " + 1),
                        new DisplayRunde("G2", Localizer["Gruppenphase Spieltag"].Value + " " + 2),
                        new DisplayRunde("G3", Localizer["Gruppenphase Spieltag"].Value + " " + 3),
                        new DisplayRunde("AF", Localizer["Achtelfinale"].Value),
                        new DisplayRunde("VF", Localizer["Viertelfinale"].Value),
                        new DisplayRunde("HF", Localizer["Halbfinale"].Value),
                        new DisplayRunde("F3",Localizer["Spiel um Platz 3"].Value),
                        new DisplayRunde("F", Localizer["Finale"].Value),
                    };
            }
        }

        public async void Verein1Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var verein = await VereineService.GetVereinEMWM(Convert.ToInt32(e.Value.ToString()));
                Spiel.Verein1 = verein.Vereinsname1;
                Spiel.Verein1_Nr = int.Parse(e.Value.ToString());
                //Spiel.Ort = verein.Stadion;
                Spiel.Land1_Nr = 57;
                Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);

                StateHasChanged();
            }
            
        }

        public async void Verein2Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var verein = await VereineService.GetVereinEMWM(Convert.ToInt32(e.Value.ToString()));
                Spiel.Verein2 = verein.Vereinsname1;
                Spiel.Land2_Nr = 57;
                Spiel.Verein2_Nr = int.Parse(e.Value.ToString());

                StateHasChanged();
            }
            
        }


        public async void RundeChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                RundeChoosed = e.Value.ToString();

                Globals.currentEMWMRunde = RundeChoosed;
                Runde = RundeChoosed;

                if (RundeChoosed == "G1" || RundeChoosed == "G2" || RundeChoosed == "G3" || RundeChoosed == "2G1" || RundeChoosed == "2G2" || RundeChoosed == "2G3")
                    GroupVisible = "inline-block;";
                else
                    GroupVisible = "none;";


                StateHasChanged();
            }
        }

        public async void GruppeChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                GruppeChoosed = Convert.ToInt32(e.Value.ToString());
                Gruppe = e.Value.ToString();

                StateHasChanged();
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
                await SpieltagService.DeleteSpieltag(Convert.ToInt32(Id));

                //NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Info, Summary = Localizer["Löschen Spiel"].Value, Detail = Localizer["Gelöscht"].Value });
                message = "Spiel wurde gelöscht";
                await JSRuntime.InvokeVoidAsync("alert", message);
            }

            return result;
        }
    }
}
