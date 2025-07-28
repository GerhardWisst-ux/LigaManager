using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LigaManagerManagement.Web.Pages
{
    public class SpieltagListBase : ComponentBase
    {
        [Parameter]
        public string CurrentligaUrl { get; set; }

        [Parameter]
        public string SpieltagNr { get; set; }

        public bool VisibleSpielplan { get; set; }
        public bool VisibleBtnNew { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        private static readonly HttpClient client = new HttpClient();

        public RadzenDataGrid<Spieltag> spieltageGrid;
        
        public string Liganame;
        public string curentsaison;
        public Density Density = Density.Default;
        public List<string> DensityValues = new List<string> { "Standard", "Kompakt" };

        public int iSpieltage;
        public bool IsLoading = false;
        public List<DisplaySaison> SaisonenList;

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public RadzenDataGrid<Spieltag> grid;
        IList<Tuple<Spieltag, RadzenDataGridColumn<Spieltag>>> selectedCellData = new List<Tuple<Spieltag, RadzenDataGridColumn<Spieltag>>>();

        public Int32 currentspieltag;
        public IEnumerable<Saison> Saisonen { get; set; }
        public bool VisibleVorZurueck { get; set; }

        public List<DisplaySpieltag> SpieltagList;

        public IEnumerable<Tabelle> Tabellen { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public ISpielplanService SpielplanService { get; set; }

        [Inject]
        public IVereinePLService VereinePLService { get; set; }

        [Inject]
        public ISpieltageBEService SpieltagBEService { get; set; }

        [Inject]
        public ISpieltageENService SpieltageENService { get; set; }

        [Inject]
        public ISpieltageITService SpieltagITService { get; set; }

        [Inject]
        public ISpieltageESService SpieltagESService { get; set; }

        [Inject]
        public ISpieltageNLService SpieltagNLService { get; set; }

        [Inject]
        public ISpieltageTUService SpieltagTUService { get; set; }

        [Inject]
        public ISpieltagePTService SpieltagPTService { get; set; }

        [Inject]
        public ISpieltageFRService SpieltagFRService { get; set; }

        public IEnumerable<VereinAUS> VereineAus = new List<VereinAUS>();

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereinePLService VereineServicePL { get; set; }

        [Inject]
        public IVereineAusService VereineAusService { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        [Inject]
        public IStadionService StadionService { get; set; }

        [Inject]
        public IStringLocalizer<SpieltageList> Localizer { get; set; }
        public IEnumerable<Spieltag> Spieltage { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }
        public NavigationManager NavigationManager { get; set; }

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

                if (SpieltagNr == "0")
                    SpieltagNr = Globals.maxSpieltag.ToString();

                IsLoading = true;
                await DisplaySpieltagAkt();

                var liga = await LigaService.GetLiga(Globals.LigaID);
                Liganame = liga.Liganame + " " + Globals.currentSaison;

                SaisonenList = new List<DisplaySaison>();

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                if (Globals.LigaID < 4)
                {
                    var spielplanVorhanden = await SpielplanService.GetSpielplaene();

                    spielplanVorhanden = spielplanVorhanden.Where(st => st.SaisonID == Globals.SaisonID && st.LigaID == Globals.LigaID).ToList();

                    if (spielplanVorhanden.Count() == 0)
                        VisibleSpielplan = false;
                    else
                        VisibleSpielplan = true;                    
                }

                IsLoading = false;

            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                IsLoading = false;
            }
        }

        private async Task DisplaySpieltagAkt()
        {
            if (Globals.LigaNummer == 1)
            {
                if (Globals.currentSaison.Substring(0, 4) == "1963" || Globals.currentSaison.Substring(0, 4) == "1964")
                    iSpieltage = 30;
                else if (Globals.currentSaison.Substring(0, 4) == "1991")
                    iSpieltage = 38;
                else
                    iSpieltage = 34;
            }
            else if (Globals.LigaNummer == 2)
            {
                if (Globals.currentSaison.Substring(0, 4) == "1993")
                    iSpieltage = 38;
                else
                    iSpieltage = 34;
            }
            else if (Globals.LigaNummer == 3)
            {
                iSpieltage = 38;

            }
            else if (Globals.LigaNummer == 4)
            {
                if (Globals.currentSaison.StartsWith("1993") || Globals.currentSaison.StartsWith("1994"))
                    iSpieltage = 42;
                else
                    iSpieltage = 38;
            }
            else if (Globals.LigaNummer == 5)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2003)
                    iSpieltage = 38;
                else
                    iSpieltage = 34;
            }
            else if (Globals.LigaNummer == 6)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 1996 && (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) < 2002))
                    iSpieltage = 34;
                else
                    iSpieltage = 38;
            }
            else if (Globals.LigaNummer == 7)
            {
                if (Globals.currentSaison.StartsWith("1993") || Globals.currentSaison.StartsWith("1994"))
                    iSpieltage = 42;
                else
                    iSpieltage = 38;
            }
            else if (Globals.LigaNummer == 8)
                iSpieltage = 38;

            else if (Globals.LigaNummer == 9)
            {
                iSpieltage = 34;
            }
            else if (Globals.LigaNummer == 10)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2019)
                    iSpieltage = 38;
                else
                    iSpieltage = 34;
            }
            else if (Globals.LigaNummer == 11)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2022)
                    iSpieltage = 30;
                else if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2020)
                    iSpieltage = 34;
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2008)
                    iSpieltage = 30;
                else
                    iSpieltage = 34;
            }
            else if (Globals.LigaNummer == 12)
                iSpieltage = 46;
            else if (Globals.LigaNummer == 20 || Globals.LigaNummer == 21)
                iSpieltage = 34;


            //await GetDataFromOpenLgaDB();

            SpieltagList = new List<DisplaySpieltag>();

            for (int i = 1; i <= iSpieltage; i++)
            {
                SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + "." + Localizer["Spieltag"].Value));
            }

            if (Globals.LigaNummer < 3)
            {
                Vereine = await VereineService.GetVereine();
                if (Vereine == null)
                    throw new Exception("Vereine sind null");

                Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID)?.ToList();
                Spieltage = Spieltage?.OrderBy(o => o.Datum);
                for (int i = 0; i < Spieltage?.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = columns.Verein1;
                    columns.Verein2Anzeige = columns.Verein2;
                    columns.Doppelpunkt = ":";
                }
            }
            else if (Globals.LigaNummer == 3)
            {
                Vereine = await VereineService.GetVereine();

                Spieltage = (await SpieltagService.GetSpieltageL3()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID)?.ToList();
                Spieltage = Spieltage?.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage?.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (Vereine == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = columns.Verein1;
                    columns.Verein2Anzeige = columns.Verein2;
                    columns.Doppelpunkt = ":";
                }
            }
            else if (Globals.LigaNummer == 3 || Globals.LigaNummer == 20 || Globals.LigaNummer == 21)
            {
                var vereineSaison = await SpieltagService.GetVereineL3();

                Spieltage = (await SpieltagService.GetSpieltageL3()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (vereineSaison == null)
                        throw new Exception("VereineSaison sind null");

                    if (columns.Verein1 != "" && columns.Verein2 != "")
                    {
                        columns.Verein1Anzeige = columns.Verein1;
                        columns.Verein2Anzeige = columns.Verein2;
                    }
                    else
                    {
                        columns.Verein1 = vereineSaison.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                        columns.Verein2 = vereineSaison.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                        columns.Verein1Anzeige = columns.Verein1;
                        columns.Verein2Anzeige = columns.Verein2;
                    }

                }
            }
            else if (Globals.LigaNummer == 4 || Globals.LigaNummer == 12)
            {
                VereineAus = await VereineAusService.GetVereinePL();

                Spieltage = (await SpieltageENService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (VereineAus == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname2;
                    columns.Verein2Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname2;
                    columns.Doppelpunkt = ":";
                }
            }

            else if (Globals.LigaNummer == 5)
            {
                VereineAus = await VereineAusService.GetVereineIT();

                Spieltage = (await SpieltagITService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (VereineAus == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname2;
                    columns.Verein2Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname2;
                    columns.Doppelpunkt = ":";
                }
            }

            else if (Globals.LigaNummer == 6)
            {
                VereineAus = await VereineAusService.GetVereineFR();

                Spieltage = (await SpieltagFRService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (VereineAus == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname2;
                    columns.Verein2Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname2;
                    columns.Doppelpunkt = ":";
                }
            }
            else if (Globals.LigaNummer == 7)
            {
                VereineAus = await VereineAusService.GetVereineES();

                Spieltage = (await SpieltagESService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (VereineAus == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname2;
                    columns.Verein2Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname2;
                    columns.Doppelpunkt = ":";
                }
            }
            else if (Globals.LigaNummer == 8)
            {

                VereineAus = await VereineAusService.GetVereineNL();

                Spieltage = (await SpieltagNLService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (VereineAus == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname2;
                    columns.Verein2Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname2;
                    columns.Doppelpunkt = ":";
                }
            }
            else if (Globals.LigaNummer == 9)
            {

                VereineAus = await VereineAusService.GetVereinePT();

                Spieltage = (await SpieltagPTService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (VereineAus == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname2;
                    columns.Verein2Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname2;
                    columns.Doppelpunkt = ":";
                }
            }
            else if (Globals.LigaNummer == 10)
            {

                VereineAus = await VereineAusService.GetVereineTU();

                Spieltage = (await SpieltagTUService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (VereineAus == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname2;
                    columns.Verein2Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname2;
                    columns.Doppelpunkt = ":";
                }
            }
            else if (Globals.LigaNummer == 11)
            {

                VereineAus = await VereineAusService.GetVereineBE();

                Spieltage = (await SpieltagBEService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);

                    if (VereineAus == null)
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname2;
                    columns.Verein2Anzeige = VereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname2;
                    columns.Doppelpunkt = ":";
                }
            }

            SpieltagNr = Globals.Spieltag.ToString();


            if (Globals.LigaNummer == 1)
            {
                if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
                {
                    if (Spieltage.Count() >= 8)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else if (Globals.currentSaison == "1991/92")
                {
                    if (Spieltage.Count() >= 10)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else
                {
                    if (Spieltage.Count() >= 9)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
            }
            else if (Globals.LigaNummer == 2)
            {
                if (Globals.currentSaison == "1993/94")
                {
                    if (Spieltage.Count() >= 10)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else
                {
                    if (Spieltage.Count() >= 9)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
            }
            else if (Globals.LigaNummer == 3)
            {
                if (Spieltage.Count() >= 10)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }

            else if (Globals.LigaNummer == 4)
            {

                if (Spieltage.Count() >= 10)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;

            }
            else if (Globals.LigaNummer == 5)
            {

                if (Spieltage.Count() >= 10)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;

            }
            else if (Globals.LigaNummer == 6)
            {

                if (Spieltage.Count() >= 9)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;

            }
            else if (Globals.LigaNummer == 7)
            {

                if (Spieltage.Count() >= 10)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;

            }
            else if (Globals.LigaNummer == 8)
            {

                if (Spieltage.Count() >= 9)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;

            }
            else if (Globals.LigaNummer == 9)
            {

                if (Spieltage.Count() >= 9)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;

            }
            else if (Globals.LigaNummer == 10)
            {

                if (Spieltage.Count() >= 9)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }
            else if (Globals.LigaNummer == 11)
            {
                if (Spieltage.Count() >= 9)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }
            else if (Globals.LigaNummer == 12)
            {
                if (Spieltage.Count() >= 12)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }
            else if (Globals.LigaNummer == 20 || Globals.LigaNummer == 21)
            {
                if (Spieltage.Count() >= 9)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }
            else
                VisibleBtnNew = true;

            if (Spieltage.Count() == 0)
            {
                VisibleVorZurueck = false;
            }
            else
            {
                VisibleVorZurueck = true;
            }
        }
        static async Task<List<LigaManagement.Models.Match>> GetMatchesAsync(string path)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string matchstring = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<List<Match>>(matchstring);
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
        static async Task<MatchDetail> GetMatchAsync(string path)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string matchstring = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<MatchDetail>(matchstring);
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        protected async Task<int> GetDataFromOpenLgaDB()
        {
            int ret = 0;
            client.BaseAddress = new Uri("https://api.openligadb.de/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                try
                {
                    var matches = await GetMatchesAsync("getmatchdata/bl3/2024").ConfigureAwait(false);

                    if (matches == null)
                    {
                        return ret;
                    }

                    int SpieltagNr = 0;
                    int ii = 0;
                    foreach (var match in matches)
                    {
                        int mod = ii % 10;

                        if (mod == 0)
                            SpieltagNr = SpieltagNr + 1;

                        Debug.Print($"{match.MatchDateTime}: {match.Team1.TeamName} : {match.Team2.TeamName}");

                        var matchDetail = await GetMatchAsync($"getmatchdata/{match.MatchID}").ConfigureAwait(false);

                        //if (ii == 209)
                        //{
                        //    Debug.Print("Halt");
                        //}
                        SaveImportDataToDatabase(match, matchDetail, SpieltagNr);

                        ii++;

                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return ret;
                }
            }
        }

        private async void SaveImportDataToDatabase(LigaManagement.Models.Match match, MatchDetail matchdetail, int SpieltagNr)
        {
            try
            {
                if (match.MatchResults == null)
                    return;

                if (match.MatchResults.Count() == 0)
                    return;

                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Spielplaene ([SpieltagNr], [Saison],[SaisonID],[LigaID],[Verein1_Nr],[Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum],[DatumString],[Ort],[Schiedrichter],[Abgeschlossen],[Zuschauer],[StadionID])" +
                    " VALUES(@SpieltagNr,@Saison,@SaisonID,@LigaID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@DatumString,@Ort,@Schiedrichter,@Abgeschlossen,@Zuschauer,@StadionID)";

                cmd.Parameters.AddWithValue("@SpieltagNr", SpieltagNr);
                cmd.Parameters.AddWithValue("@SaisonID", 418);
                cmd.Parameters.AddWithValue("@Saison", "2019/20");
                cmd.Parameters.AddWithValue("@StadionID", 0);
                cmd.Parameters.AddWithValue("@LigaID", 3);
                try
                {
                    cmd.Parameters.AddWithValue("@Verein1_Nr", match.Team1.TeamId);
                    cmd.Parameters.AddWithValue("@Verein2_Nr", match.Team2.TeamId);
                }
                catch (Exception)
                {

                    cmd.Parameters.AddWithValue("@Verein1", "kein Verein gefunden");
                    cmd.Parameters.AddWithValue("@Verein2", "kein Verein gefunden");
                }

                try
                {
                    cmd.Parameters.AddWithValue("@Verein1", match.Team1.TeamName);
                    cmd.Parameters.AddWithValue("@Verein2", match.Team2.TeamName);
                }
                catch (Exception)
                {

                    cmd.Parameters.AddWithValue("@Verein1", "kein Verein-Nr gefunden");
                    cmd.Parameters.AddWithValue("@Verein2", "kein Verein-Nr gefunden");
                }
                try
                {
                    cmd.Parameters.AddWithValue("@Tore1_Nr", match.MatchResults[1].PointsTeam1);
                    cmd.Parameters.AddWithValue("@Tore2_Nr", match.MatchResults[1].PointsTeam2);
                }
                catch (Exception)
                {

                    cmd.Parameters.AddWithValue("@Tore1_Nr", 0);
                    cmd.Parameters.AddWithValue("@Tore2_Nr", 0);
                }
                cmd.Parameters.AddWithValue("@Datum", match.MatchDateTime);
                cmd.Parameters.AddWithValue("@DatumString", match.MatchDateTime.ToString());
                cmd.Parameters.AddWithValue("@Ort", "k.A.");
                cmd.Parameters.AddWithValue("@Schiedrichter", "SR");
                cmd.Parameters.AddWithValue("@Abgeschlossen", 1);
                if (match.NumberOfViewers != null)
                    cmd.Parameters.AddWithValue("@Zuschauer", match.NumberOfViewers);
                else
                    cmd.Parameters.AddWithValue("@Zuschauer", 0);
                await cmd.ExecuteNonQueryAsync();

                conn.Close();

            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

            }
        }
        public async Task SpieltagChange(ChangeEventArgs e)
        {
            try
            {
                if (e.Value != null)
                {
                    IsLoading = true;
                    SpieltagNr = e.Value.ToString();

                    int SpieltagNr2 = Convert.ToInt32(e.Value);
                    Globals.Spieltag = SpieltagNr2;

                    await DisplaySpieltagAkt();

                    if (Spieltage.Any())
                        VisibleVorZurueck = true;
                    else
                        VisibleVorZurueck = false;

                    currentspieltag = Convert.ToInt32(e.Value);

                    IsLoading = false;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                IsLoading = false;
            }
        }

        public async Task SpieltagZurueck()
        {
            IsLoading = true;
            if (Convert.ToInt32(SpieltagNr) > 1)
                SpieltagNr = (Convert.ToInt32(SpieltagNr) - 1).ToString();
            else
                return;

            Globals.Spieltag = Convert.ToInt32(SpieltagNr);

            await DisplaySpieltagAkt();

            if (Spieltage.Any())
                VisibleVorZurueck = true;
            else
                VisibleVorZurueck = false;

            IsLoading = false;
            StateHasChanged();
        }

        public async Task SpieltagVor()
        {
            IsLoading = true;
            if (Convert.ToInt32(SpieltagNr) < Globals.maxSpieltag)
                SpieltagNr = (Convert.ToInt32(SpieltagNr) + 1).ToString();

            Globals.Spieltag = Convert.ToInt32(SpieltagNr);

            await DisplaySpieltagAkt();


            if (Spieltage.Any())
                VisibleVorZurueck = true;
            else
                VisibleVorZurueck = false;

            IsLoading = false;

            StateHasChanged();

        }
        public class DisplaySpieltag
        {
            public DisplaySpieltag(string nummer, string name)
            {
                Nummer = nummer;
                Name = name;
            }
            public string Nummer { get; set; }
            public string Name { get; set; }
        }
    }

}
