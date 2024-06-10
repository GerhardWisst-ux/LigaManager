using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class SpieltagListBase : ComponentBase
    {
        [Parameter]
        public string CurrentligaUrl { get; set; }

        [Parameter]
        public string SpieltagNr { get; set; }
        public bool VisibleFooter;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        public RadzenDataGrid<Spieltag> spieltageGrid;
        public IList<Spieltag> orders;

        public string Liganame;
        public string curentsaison;
        public Density Density = Density.Default;
        public List<string> DensityValues = new List<string> { "Standard", "Kompakt" };

        public int iSpieltage;
        public List<DisplaySaison> SaisonenList;

        [Inject]
        public ISaisonenService SaisonenService { get; set; }
        public bool VisibleBtnNew { get; set; }

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
                    string returnUrl = WebUtility.UrlEncode($"/");
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }

                if (SpieltagNr == "0")
                    SpieltagNr = Globals.maxSpieltag.ToString();

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

             
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

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

           
            SpieltagList = new List<DisplaySpieltag>();

            for (int i = 1; i <= iSpieltage; i++)
            {
                SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
            }

            if (Globals.LigaNummer < 3)
            {
                Vereine = await VereineService.GetVereine();

                Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString() && st.SaisonID == Globals.SaisonID).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
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
                        throw new Exception("Vereine sind null");

                    columns.Verein1 = vereineSaison.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr))?.Vereinsname1;
                    columns.Verein2 = vereineSaison.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr))?.Vereinsname1;
                    columns.Verein1Anzeige = columns.Verein1;
                    columns.Verein2Anzeige = columns.Verein2;
                    columns.Doppelpunkt = ":";
                }
            }
            else if (Globals.LigaNummer == 4 || Globals.LigaNummer == 15)
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
            else if (Globals.LigaNummer == 20  || Globals.LigaNummer == 21)
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
                VisibleFooter = false;
            }
            else
            {
                VisibleFooter = true;
                VisibleVorZurueck = true;
            }

        }
        public async Task SpieltagChange(ChangeEventArgs e)
        {
            try
            {
                if (e.Value != null)
                {
                    SpieltagNr = e.Value.ToString();

                    int SpieltagNr2 = Convert.ToInt32(e.Value);
                    Globals.Spieltag = SpieltagNr2;

                    await DisplaySpieltagAkt();

                    if (Spieltage.Any())
                        VisibleVorZurueck = true;
                    else
                        VisibleVorZurueck = false;

                    currentspieltag = Convert.ToInt32(e.Value);

                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

            }
        }

        public async Task SpieltagZurueck()
        {
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

            StateHasChanged();
        }

        public async Task SpieltagVor()
        {
            if (Convert.ToInt32(SpieltagNr) < Globals.maxSpieltag)
                SpieltagNr = (Convert.ToInt32(SpieltagNr) + 1).ToString();

            Globals.Spieltag = Convert.ToInt32(SpieltagNr);

            await DisplaySpieltagAkt();

            if (Spieltage.Any())
                VisibleVorZurueck = true;
            else
                VisibleVorZurueck = false;

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

        public void OnCellRender(DataGridCellRenderEventArgs<Spieltag> args)
        {
            if (selectedCellData.Any(i => i.Item1 == args.Data && i.Item2 == args.Column))
            {
                args.Attributes.Add("style", $"background-color: var(--rz-secondary-lighter);");
            }
        }

        protected async Task Delete_Click()
        {
            //DeleteConfirmation.Show();

            //await SpieltagService.DeleteSpieltag(Spieltag.SpieltagId);
            //await OnSpieltagDeleted.InvokeAsync((int)Spieltag.SpieltagId);

            //NavigationManager.NavigateTo(($"/spieltage?spieltag={Globals.Spieltag}"));
        }

        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            //if (deleteConfirmed)
            //{
            //    await SpieltagService.DeleteSpieltag(Spieltag.SpieltagId);
            //    await OnSpieltagDeleted.InvokeAsync((int)Spieltag.SpieltagId);
            //}

            //NavigationManager.NavigateTo(($"/spieltage?spieltag={Globals.Spieltag}"));
        }

        public bool doesImageExistRemotely(string uriToImage)
        {
            if (uriToImage == "")
                return false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriToImage);
            request.Method = "HEAD";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }

}
