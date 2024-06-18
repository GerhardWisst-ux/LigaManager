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
    public class EditSpieltagBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string SpieltagNr { get; set; }

        public string Torart { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        protected bool SpielverlaufVisible = false;
        protected bool AufstellungenVisible = false;
        public bool popup;
        public bool allowVirtualization;
        public Int32 currentspieltag = Globals.Spieltag;

        public string Vereinsname1;

        public string Vereinsname2;

        public string Stadion;

        public string Spielername;

        public DateTime? Time { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public ISpieltageBEService SpieltagBEService { get; set; }
        [Inject]
        public ISpieltageENService SpieltagENService { get; set; }

        [Inject]
        public ISpieltageITService SpieltagITService { get; set; }

        [Inject]
        public ISpieltagAusService SpieltagAusService { get; set; }

        [Inject]
        public ISpieltageESService SpieltagESService { get; set; }

        [Inject]
        public ISpieltageFRService SpieltagFRService { get; set; }

        [Inject]
        public ISpieltageNLService SpieltagNLService { get; set; }

        [Inject]
        public ISpieltagePTService SpieltagPTService { get; set; }

        [Inject]
        public ISpieltageTUService SpieltagTUService { get; set; }


        [Inject]
        public IToreService ToreService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereinePLService VereinePLService { get; set; }

        [Inject]
        public IVereineAusService VereineAusService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        [Inject]
        public ISpielerSpieltagService SpielerSpieltagService { get; set; }

        [Inject]
        public IKaderService KaderService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<EditSpieltag> Localizer { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();

        public List<DisplaySpieler> KaderList1 = new List<DisplaySpieler>();
        public List<DisplaySpieler> KaderList2 = new List<DisplaySpieler>();
        public List<DisplaySpieler> SpielerList1 = new List<DisplaySpieler>();
        public List<DisplaySpieler> SpielerList2 = new List<DisplaySpieler>();

        public List<DisplayTore> ToreList = new List<DisplayTore>();

        public string PageHeaderText { get; set; }

        public IEnumerable<Spieltag> spieltage { get; set; }

        public EditSpieltagModel EditSpieltagModel { get; set; } =
            new EditSpieltagModel();

        public Spieltag Spiel { get; set; } = new Spieltag();

        public Tore Tor { get; set; } = new Tore();

        public Spieltag SpielCombo { get; set; } = new Spieltag();

        public IEnumerable<Verein> Vereine { get; set; }

        NotificationService NotificationService = new NotificationService();

        public bool bAbgeschlossen = true;

        public bool bDeleteButtonVisible = true;

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
                    string returnUrl = WebUtility.UrlEncode($"/editSpieltag/{Id}");
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }

                if (Globals.LigaNummer < 3)
                {
                    if (Id != null)
                        Spiel = await SpieltagService.GetSpieltag(Convert.ToInt32(Id));

                    var vereineSaison = await VereineSaisonService.GetVereineSaison();
                    List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == Globals.SaisonID).ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineService.GetVerein(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }

                    var allkader = await KaderService.GetAllSpieler();
                    List<Kader> SpielerSpiel = allkader.Where(x => x.SaisonId == Globals.SaisonID && x.VereinID == Convert.ToInt32(Spiel.Verein1_Nr)).ToList();

                    KaderList1 = new List<DisplaySpieler>();

                    for (int i = 0; i < SpielerSpiel.Count(); i++)
                    {
                        KaderList1.Add(new DisplaySpieler(SpielerSpiel[i].Id, (SpielerSpiel[i].SpielerName + ", " + SpielerSpiel[i].Vorname)));
                    }

                    KaderList2 = new List<DisplaySpieler>();

                    SpielerSpiel = allkader.Where(x => x.SaisonId == Globals.SaisonID && x.VereinID == Convert.ToInt32(Spiel.Verein2_Nr)).ToList();
                    for (int i = 0; i < SpielerSpiel.Count(); i++)
                    {
                        KaderList2.Add(new DisplaySpieler(SpielerSpiel[i].Id, (SpielerSpiel[i].SpielerName + ", " + SpielerSpiel[i].Vorname)));
                    }

                    SpieltagNr = Globals.Spieltag.ToString();
                }
                if (Globals.LigaNummer == 3 || Globals.LigaNummer == 20 || Globals.LigaNummer == 21)
                {
                    if (Id != null)
                        Spiel = await SpieltagService.GetSpieltagL3(Convert.ToInt32(Id));

                    var vereineSaison = await SpieltagService.GetVereineL3();
                    List<VereinAktSaison> verList = vereineSaison.Where(x => x.SaisonID == Globals.SaisonID).ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineService.GetVereinL3(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }

                    SpieltagNr = Globals.Spieltag.ToString();
                }
                else if (Globals.LigaNummer == 4 || Globals.LigaNummer == 12)
                {
                    if (Id != null)
                        Spiel = await SpieltagENService.GetSpieltag(Convert.ToInt32(Id));

                    var vereineSaison = await VereineAusService.GetVereineSaison(Globals.SaisonID);
                    List<VereinAktSaisonAUS> verList = vereineSaison.ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereinePLService.GetVerein(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }
                    SpieltagNr = Globals.Spieltag.ToString();
                }
                else if (Globals.LigaNummer == 5)
                {
                    if (Id != null)
                        Spiel = await SpieltagITService.GetSpieltag(Convert.ToInt32(Id));

                    var vereineSaison = await VereineAusService.GetVereineSaison(Globals.SaisonID);
                    List<VereinAktSaisonAUS> verList = vereineSaison.ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineAusService.GetVereinIT(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }
                    SpieltagNr = Globals.Spieltag.ToString();
                }

                else if (Globals.LigaNummer == 6)
                {

                    if (Id != null)
                        Spiel = await SpieltagFRService.GetSpieltag(Convert.ToInt32(Id));

                    var vereineSaison = await VereineAusService.GetVereineSaison(Globals.SaisonID);
                    List<VereinAktSaisonAUS> verList = vereineSaison.ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineAusService.GetVereinFR(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }

                    SpieltagNr = Globals.Spieltag.ToString();
                }
                else if (Globals.LigaNummer == 7)
                {
                    if (Id != null)
                        Spiel = await SpieltagESService.GetSpieltag(Convert.ToInt32(Id));

                    var vereineSaison = await VereineAusService.GetVereineSaison(Globals.SaisonID);
                    List<VereinAktSaisonAUS> verList = vereineSaison.ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineAusService.GetVereinES(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }
                    SpieltagNr = Globals.Spieltag.ToString();
                }
                else if (Globals.LigaNummer == 8)
                {
                    if (Id != null)
                        Spiel = await SpieltagNLService.GetSpieltag(Convert.ToInt32(Id));

                    var vereineSaison = await VereineAusService.GetVereineSaison(Globals.SaisonID);
                    List<VereinAktSaisonAUS> verList = vereineSaison.ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineAusService.GetVereinNL(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }
                    SpieltagNr = Globals.Spieltag.ToString();
                }
                else if (Globals.LigaNummer == 9)
                {
                    if (Id != null)
                        Spiel = await SpieltagPTService.GetSpieltag(Convert.ToInt32(Id));

                    var vereineSaison = await VereineAusService.GetVereineSaison(Globals.SaisonID);
                    List<VereinAktSaisonAUS> verList = vereineSaison.ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineAusService.GetVereinPT(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }
                    SpieltagNr = Globals.Spieltag.ToString();
                }
                else if (Globals.LigaNummer == 10)
                {
                    if (Id != null)
                        Spiel = await SpieltagTUService.GetSpieltag(Convert.ToInt32(Id));

                    var vereineSaison = await VereineAusService.GetVereineSaison(Globals.SaisonID);
                    List<VereinAktSaisonAUS> verList = vereineSaison.ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineAusService.GetVereinTU(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }
                    SpieltagNr = Globals.Spieltag.ToString();
                }

                else if (Globals.LigaNummer == 11)

                {
                    if (Id != null)
                        Spiel = await SpieltagBEService.GetSpieltag(Convert.ToInt32(Id));

                    var vereineSaison = await VereineAusService.GetVereineSaison(Globals.SaisonID);
                    List<VereinAktSaisonAUS> verList = vereineSaison.ToList();

                    for (int i = 0; i < verList.Count(); i++)
                    {
                        var verein = await VereineAusService.GetVereinBE(verList[i].VereinNr);
                        VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
                    }
                    SpieltagNr = Globals.Spieltag.ToString();
                }


                if (Id == null)
                    Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, 15, 30, 0, DateTimeKind.Utc);
                else
                    Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, Spiel.Datum.Hour, Spiel.Datum.Minute, 0, DateTimeKind.Utc);

                Spiel.Saison = Globals.currentSaison;
                Spiel.SaisonID = Globals.SaisonID;
                Spiel.SpieltagNr = SpieltagNr;

                if (Globals.LigaNummer == 1)
                {
                    var tore = await ToreService.GetTore();
                    List<Tore> torlist = tore.Where(x => x.SpieltagId == Spiel.SpieltagId).ToList();

                    for (int i = 0; i < torlist.Count; i++)
                    {
                        var kaderspieler = await KaderService.GetSpieler(torlist[i].SpielerID);
                        if (Spiel.SpieltagId == torlist[i].SpieltagId)
                        {
                            if (kaderspieler.Vorname == "")
                                ToreList.Add(new DisplayTore(torlist[i].SpielerID, kaderspieler.SpielerName, torlist[i].Spielstand, torlist[i].Spielminute,
                                Convert.ToInt32(torlist[i].SpieltagId), torlist[i].Eigentor, torlist[i].Elfmeter, torlist[i].Torart));
                            else
                                ToreList.Add(new DisplayTore(torlist[i].SpielerID, kaderspieler.SpielerName + ", " + kaderspieler.Vorname, torlist[i].Spielstand, torlist[i].Spielminute,
                                Convert.ToInt32(torlist[i].SpieltagId), torlist[i].Eigentor, torlist[i].Elfmeter, torlist[i].Torart));
                        }

                    }
                    ToreList.Sort("Spielstand", BootstrapBlazor.Components.SortOrder.Asc).Sort("Spielminute", BootstrapBlazor.Components.SortOrder.Asc);
                }

                var saison = await SaisonenService.GetSaison(Globals.SaisonID);

                if (saison == null)
                {
                    if (saison.Abgeschlossen)
                        bAbgeschlossen = true;
                    else
                        bAbgeschlossen = false;
                }

                if (bAbgeschlossen)
                    bDeleteButtonVisible = false;
                else
                    bDeleteButtonVisible = true;


                if (LMSettings.GetSpielverlaufVisible() == false)
                    SpielverlaufVisible = false;
                else if (LMSettings.GetSpielverlaufVisible() == true)
                    SpielverlaufVisible = true;

                if (LMSettings.GetAufstellungenVisible() == false)
                    AufstellungenVisible = false;
                else if (LMSettings.GetAufstellungenVisible() == true)
                    AufstellungenVisible = true;


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

        public void TorartChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Torart = e.Value.ToString();

                if (Torart == "1")
                {
                    Tor.Torart = "Linksschuß";
                }
                else if (Torart == "2")
                {
                    Tor.Torart = "Rechtsschuß";
                }
                else if (Torart == "3")
                {
                    Tor.Torart = "Kopfball";
                }
                else if (Torart == "4")
                {
                    Tor.Torart = "Elfmeter";
                }
                else
                {
                    Tor.Torart = "";
                }

                StateHasChanged();
            }
        }
        public void Change(object value, string name, string action)
        {
            Console.WriteLine($"{name} item with index {value} {action}");
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
                }
                if (Globals.LigaID == 3)
                {
                    var verein = await VereineService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein1 = verein.Vereinsname1;
                    Spiel.Verein1_Nr = e.Value.ToString();
                    Spiel.Ort = verein.Stadion;
                    Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);
                }
                else if (Globals.LigaNummer == 4)
                {
                    var verein = await VereinePLService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein1 = verein.Vereinsname1;
                    Spiel.Verein1_Nr = e.Value.ToString();
                    Spiel.Ort = verein.Stadion;
                    Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);
                }
                else if (Globals.LigaNummer == 5)
                {
                    var verein = await VereineAusService.GetVereinIT(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein1 = verein.Vereinsname1;
                    Spiel.Verein1_Nr = e.Value.ToString();
                    Spiel.Ort = verein.Stadion;
                    Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);
                }
                else if (Globals.LigaNummer == 6)
                {
                    var verein = await VereineAusService.GetVereinFR(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein1 = verein.Vereinsname1;
                    Spiel.Verein1_Nr = e.Value.ToString();
                    Spiel.Ort = verein.Stadion;
                    Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);
                }
                else if (Globals.LigaNummer == 7)
                {
                    var verein = await VereineAusService.GetVereinES(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein1 = verein.Vereinsname1;
                    Spiel.Verein1_Nr = e.Value.ToString();
                    Spiel.Ort = verein.Stadion;
                    Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);
                }
                else if (Globals.LigaNummer == 8)
                {
                    var verein = await VereineAusService.GetVereinNL(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein1 = verein.Vereinsname1;
                    Spiel.Verein1_Nr = e.Value.ToString();
                    Spiel.Ort = verein.Stadion;
                    Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);
                }
                else if (Globals.LigaNummer == 9)
                {
                    var verein = await VereineAusService.GetVereinPT(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein1 = verein.Vereinsname1;
                    Spiel.Verein1_Nr = e.Value.ToString();
                    Spiel.Ort = verein.Stadion;
                    Spiel.Zuschauer = Convert.ToInt32(verein.Fassungsvermoegen);
                }
                else if (Globals.LigaNummer == 10)
                {
                    var verein = await VereineAusService.GetVereinTU(Convert.ToInt32(e.Value.ToString()));
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
                if (Globals.LigaID < 4)
                {
                    var verein = await VereineService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();

                }
                else if (Globals.LigaNummer == 4)
                {
                    var verein = await VereinePLService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();
                }
                else if (Globals.LigaNummer == 5)
                {
                    var verein = await VereineAusService.GetVereinIT(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();

                }
                else if (Globals.LigaNummer == 6)
                {
                    var verein = await VereineAusService.GetVereinFR(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();
                }
                else if (Globals.LigaNummer == 7)
                {
                    var verein = await VereineAusService.GetVereinES(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();
                }
                else if (Globals.LigaNummer == 8)
                {
                    var verein = await VereineAusService.GetVereinNL(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();
                }
                else if (Globals.LigaNummer == 9)
                {
                    var verein = await VereineAusService.GetVereinPT(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();
                }
                else if (Globals.LigaNummer == 10)
                {
                    var verein = await VereineAusService.GetVereinTU(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();
                }
                else if (Globals.LigaNummer == 11)
                {
                    var verein = await VereineAusService.GetVereinBE(Convert.ToInt32(e.Value.ToString()));
                    Spiel.Verein2 = verein.Vereinsname1;
                    Spiel.Verein2_Nr = e.Value.ToString();
                }
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

        public async void KaderChange1(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                SpielerList1.Add(new DisplaySpieler(Convert.ToInt32(e.Value), Spieler.SpielerName + ", " + Spieler.Vorname)); ;

                //SpielerList1.Verein2_Nr = e.Value.ToString();
                //int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein2_Nr);
                //Spiel.Verein2 = VereineList[index].Vereinname1;
            }
            StateHasChanged();
        }

        public async void ToreChange1(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                Tor.SpielerID = Spieler.Id;
            }
            StateHasChanged();
        }

        public async void btnSpeichernTorV2_Click(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                Tor.SpielerID = Spieler.Id;
            }
            StateHasChanged();
        }

        public async void ToreChange2(ChangeEventArgs e)
        {
            if (e.Value != null)
            {

                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                Tor.SpielerID = Spieler.Id;
            }
            StateHasChanged();
        }

        public async void KaderChange2(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                SpielerList2.Add(new DisplaySpieler(Convert.ToInt32(e.Value), Spieler.SpielerName + ", " + Spieler.Vorname)); ;
            }
            StateHasChanged();
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

        public class DisplayTore
        {
            public DisplayTore(int spielerId, string spieler, string spielstand, int spielminute, int spieltagId, bool eigentor, bool elfmeter, string torart)
            {
                Spieler = spieler;
                Spielstand = spielstand;
                SpielerId = spielerId;
                Spielminute = spielminute;
                SpieltagId = spieltagId;
                Eigentor = eigentor;
                Elfmeter = elfmeter;
                Torart = torart;
            }
            public int SpielerId { get; set; }
            public int Spielminute { get; set; }
            public string Spieler { get; set; }
            public string Spielstand { get; set; }
            public int SpieltagId { get; set; }
            public bool Eigentor { get; set; }
            public bool Elfmeter { get; set; }
            public string Torart { get; set; }
        }

        protected ConfirmBase DeleteConfirmation { get; set; }

        protected async Task<bool> Confirm()
        {
            string message = "Möchten Sie dieses Spiel tatsächlich löschen?";
            var result = await JSRuntime.InvokeAsync<bool>("confirm", new[] { message });

            if (result)
            {
                await SpieltagService.DeleteSpieltag(Convert.ToInt32(Id));

                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Info, Summary = "Löschen Spiel", Detail = "Gelöscht" });
            }
            

            return result;
        }


    }


}
