using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using static LigamanagerManagement.Web.Pages.EditPokalspieltagBase;

namespace LigaManagement.Web.Pages
{
    public class EM_WMListbase : ComponentBase
    {
        public RadzenDataGrid<Tabelle> gridTabelle;
        public bool allowVirtualization;
        static HttpClient client = new HttpClient();
        public RadzenDataGrid<PokalergebnisCLSpieltag> grid;
        public Density Density = Density.Compact;
        public string Titel { get; set; }
        protected string DisplayErrorRunde = "none";
        protected string DisplayErrorSaison = "none";

        protected string VisibleTable = "none";
        protected string VisibleTableWM = "none";

        public List<DisplayCLRunde> RundeList;

        public int SaisonChoosed = 0;
        int BisSpieltag = 3;
        public string RundeChoosed;

        public IEnumerable<Tabelle> TabellenA { get; set; }
        public IEnumerable<Tabelle> TabellenB { get; set; }
        public IEnumerable<Tabelle> TabellenC { get; set; }
        public IEnumerable<Tabelle> TabellenD { get; set; }
        public IEnumerable<Tabelle> TabellenE { get; set; }
        public IEnumerable<Tabelle> TabellenF { get; set; }
        public IEnumerable<Tabelle> TabellenG { get; set; }
        public IEnumerable<Tabelle> TabellenH { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public ISaisonenCLService SaisonenEMWMService { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }
        [Inject]
        public ISpieltageEMWMService SpieltagService { get; set; }

        public List<DisplaySaison> SaisonenList;

        public string VisibleBtnNew { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ISpieltageEMWMService SpieltageEMWMService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        public IEnumerable<PokalergebnisCLSpieltag> EMWMSpieltage { get; set; }

        public IEnumerable<PokalergebnisCLSpieltag> EMWMSpieltageFinale { get; set; }

        [Inject]
        public IStringLocalizer<EditEMWMSpieltag> Localizer { get; set; }
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
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }

                SaisonenList = new List<DisplaySaison>();


                Saisonen = (await SaisonenEMWMService.GetSaisonen()).Where(x => x.Saisonname.StartsWith("WM") || x.Saisonname.StartsWith("EM")).ToList().OrderByDescending(x => x.Saisonname.Substring(x.Saisonname.Length - 4));

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                var saison = await SaisonenEMWMService.GetSaison(Convert.ToInt32(SaisonenList[0].SaisonID));

                if (saison != null)
                {
                    Globals.currentEMWMSaison = saison.Saisonname;
                    Globals.EMWMSaisonID = saison.SaisonID;
                    Globals.EMMWMLigaId = saison.LigaID;
                    
                    SaisonChoosed = Globals.EMWMSaisonID;

                    OnClickHandler();
                }
                

                EMWMSpieltage = await SpieltageEMWMService.GetSpielergebnisse();
                if (EMWMSpieltage == null)
                {
                    return;
                }

                EMWMSpieltageFinale = EMWMSpieltage.ToList().Where(x => x.Runde == "F");

                if (Globals.currentEMWMRunde == null)
                    RundeChoosed = "F";
                else
                    RundeChoosed = Globals.currentEMWMRunde;

                EMWMSpieltage = EMWMSpieltage.ToList().Where(x => x.SaisonID == Globals.EMWMSaisonID).Where(x => x.Runde == RundeChoosed).OrderBy(x => x.Datum);

                //if (EMWMSpieltage.Count() > 0)
                //{
                //    System.Threading.Thread.Sleep(2000);
                //}
                //var result = await GetDataFromOpenLgaDB();

             

                Globals.bVisibleNavMenuElements = true;

                RundeList = new List<DisplayCLRunde>
                {
                    new DisplayCLRunde("G1",Localizer["Gruppenphase Spieltag"].Value + 1),
                    new DisplayCLRunde("G2", Localizer["Gruppenphase Spieltag"].Value + 2),
                    new DisplayCLRunde("G3", Localizer["Gruppenphase Spieltag"].Value + 3),
                    new DisplayCLRunde("AF", Localizer["Achtelfinale"].Value),
                    new DisplayCLRunde("VF", Localizer["Viertelfinale"].Value),
                    new DisplayCLRunde("HF", Localizer["Halbfinale"].Value),
                    new DisplayCLRunde("F", Localizer["Finale"].Value),
                };

                VisibleBtnNew = "hidden";

             
                if (Globals.currentEMWMSaison.StartsWith("WM"))
                    VisibleTable = "block";
                else if (Globals.currentEMWMSaison.IndexOf("2024") > -1 || Globals.currentEMWMSaison.IndexOf("2022") > -1 || Globals.currentEMWMSaison.IndexOf("2020") > -1 ||
                        Globals.currentEMWMSaison.IndexOf("2018") > -1 || Globals.currentEMWMSaison.IndexOf("2016") > -1 || Globals.currentEMWMSaison.IndexOf("2014") > -1 ||
                        Globals.currentEMWMSaison.IndexOf("2012") > -1 || Globals.currentEMWMSaison.IndexOf("2010") > -1 || Globals.currentEMWMSaison.IndexOf("2008") > -1)
                    VisibleTable = "block";
                else
                    VisibleTable = "none";

                if (Globals.currentEMWMRunde != null)
                    OnClickHandler();

                if (RundeChoosed == "G1" || RundeChoosed == "G2" || RundeChoosed == "G3")
                {
                    if (RundeChoosed == "G1")
                        BisSpieltag = 1;
                    else if (RundeChoosed == "G2")
                        BisSpieltag = 2;
                    else if (RundeChoosed == "G3")
                        BisSpieltag = 3;

                    TabellenA = await TabelleService.BerechneTabelleEMWM(SpieltagService, 1, BisSpieltag);
                    TabellenB = await TabelleService.BerechneTabelleEMWM(SpieltagService, 2, BisSpieltag);
                    TabellenC = await TabelleService.BerechneTabelleEMWM(SpieltagService, 3, BisSpieltag);
                    TabellenD = await TabelleService.BerechneTabelleEMWM(SpieltagService, 4, BisSpieltag);

                    if (Globals.currentEMWMSaison.IndexOf("2024") > -1 || Globals.currentEMWMSaison.IndexOf("2022") > -1 || Globals.currentEMWMSaison.IndexOf("2020") > -1 ||
                        Globals.currentEMWMSaison.IndexOf("2018") > -1 || Globals.currentEMWMSaison.IndexOf("2016") > -1 || Globals.currentEMWMSaison.IndexOf("2014") > -1 ||
                        Globals.currentEMWMSaison.IndexOf("2012") > -1 || Globals.currentEMWMSaison.IndexOf("2010") > -1 || Globals.currentEMWMSaison.IndexOf("2008") > -1)
                    {
                        TabellenE = await TabelleService.BerechneTabelleEMWM(SpieltagService, 5, BisSpieltag);
                        TabellenF = await TabelleService.BerechneTabelleEMWM(SpieltagService, 6, BisSpieltag);
                    }
                    if (Globals.currentEMWMSaison.StartsWith("WM"))
                    {
                        TabellenG = await TabelleService.BerechneTabelleEMWM(SpieltagService, 7, BisSpieltag);
                        TabellenH = await TabelleService.BerechneTabelleEMWM(SpieltagService, 8, BisSpieltag);
                    }
                }

                DisplayErrorRunde = "none";
                DisplayErrorSaison = "none";

                Globals.bVisibleNavMenuElements = true;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }


        public async void SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {

                if (e.Value.ToString() == "")
                    return;

                SaisonChoosed = Convert.ToInt32(e.Value);

                var saison = await SaisonenEMWMService.GetSaison(Convert.ToInt32(SaisonChoosed));

                if (saison != null)
                {
                    Globals.currentEMWMSaison = saison.Saisonname;
                    Globals.EMWMSaisonID = saison.SaisonID;
                    Globals.EMMWMLigaId = saison.LigaID;

                    if (Globals.currentEMWMSaison.StartsWith("WM"))
                        VisibleTable = "block";
                    else if (Globals.currentEMWMSaison.IndexOf("2024") > -1 || Globals.currentEMWMSaison.IndexOf("2022") > -1 || Globals.currentEMWMSaison.IndexOf("2020") > -1 ||
                            Globals.currentEMWMSaison.IndexOf("2018") > -1 || Globals.currentEMWMSaison.IndexOf("2016") > -1 || Globals.currentEMWMSaison.IndexOf("2014") > -1 ||
                            Globals.currentEMWMSaison.IndexOf("2012") > -1 || Globals.currentEMWMSaison.IndexOf("2010") > -1 || Globals.currentEMWMSaison.IndexOf("2008") > -1)
                        VisibleTable = "block";
                    else
                        VisibleTable = "none";

                    OnClickHandler();
                }
            }
        }

        protected async Task<int> GetDataFromOpenLgaDB()
        {
            int ret = 0;
            client.BaseAddress = new Uri("https://api.openligadb.de/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            do
            {
                try
                {
                    //PokalergebnisseCLSpieltage = PokalergebnisseCLSpieltage.ToList().Where(x => x.SaisonID == Globals.EMWMSaisonID).Where(x => x.Runde == RundeChoosed);

                    //if (PokalergebnisseCLSpieltage.Count() > 0)
                    //    return ret;

                    var matches = await GetMatchesAsync("getmatchdata/UEFA EM 2016/2016");

                    if (matches == null)
                        return ret;

                    foreach (var match in matches)
                    {
                        Debug.Print(string.Concat(match.MatchDateTime, ": ", match.Team1.TeamName, " : ", match.Team2.TeamName, match.Team2.TeamName));

                        var matchDetail = await GetMatchAsync("getmatchdata/" + match.MatchID + "");

                        //Debug.Print(string.Concat(matchDetail.LeagueName, ": ", matchDetail.matchResults[1].PointsTeam1, " : ", matchDetail.matchResults[1].PointsTeam2));

                        if (matches.Count() <= 125)
                            SaveImportDataToDatabase(match, matchDetail);
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return ret;

                }

            } while (true);
        }

        private void SaveImportDataToDatabase(LigaManagement.Models.Match match, MatchDetail matchdetail)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Globals.connstring))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO spieltageEMWM(Saison,SaisonID,Verein1,Verein2,Verein1_Nr,Verein2_Nr, Tore1_Nr,Tore2_Nr, Ort,Datum,LigaID,Zuschauer,Schiedrichter,Runde,RundeDetail,Gruppe,Abgeschlossen,Land1_Nr, Land2_Nr,TeamIconUrl1,TeamIconUrl2,Verlängerung,Elfmeterschiessen,GroupID) " +
                                                      "VALUES (@Saison,@SaisonID,@Verein1,@Verein2,@Verein1_Nr,@Verein2_Nr,@Tore1_Nr,@Tore2_Nr,@Ort,@Datum,@LigaID,@Zuschauer,@Schiedrichter,@Runde,@RundeDetail,@Gruppe,@Abgeschlossen,@Land1_Nr,@Land2_Nr,@TeamIconUrl1,@TeamIconUrl2,@Verlängerung,@Elfmeterschiessen,@GroupID)", conn);

                    cmd.Parameters.AddWithValue("@Saison", "EM 2016");
                    cmd.Parameters.AddWithValue("@SaisonID", 33);
                    cmd.Parameters.AddWithValue("@LigaID", 31);
                    cmd.Parameters.AddWithValue("@Verein1", match.Team1.TeamName);
                    cmd.Parameters.AddWithValue("@Verein2", match.Team2.TeamName);
                    cmd.Parameters.AddWithValue("@Verein1_Nr", match.Team1.TeamId);
                    cmd.Parameters.AddWithValue("@Verein2_Nr", match.Team2.TeamId);
                    cmd.Parameters.AddWithValue("@Land1_Nr", 0);
                    cmd.Parameters.AddWithValue("@Land2_Nr", 0);

                    cmd.Parameters.AddWithValue("@Verlängerung", 0);
                    cmd.Parameters.AddWithValue("@Elfmeterschiessen", 0);

                    //cmd.Parameters.AddWithValue("@Tore1_Nr", matchdetail.MatchResults[1].PointsTeam1);
                    //cmd.Parameters.AddWithValue("@Tore2_Nr", matchdetail.MatchResults[1].PointsTeam2);

                    cmd.Parameters.AddWithValue("@Tore1_Nr", 0);
                    cmd.Parameters.AddWithValue("@Tore2_Nr", 0);

                    cmd.Parameters.AddWithValue("@Ort", "k.A.");
                    cmd.Parameters.AddWithValue("@Zuschauer", 0);
                    cmd.Parameters.AddWithValue("@TeamIconUrl1", match.Team1.TeamIconUrl);
                    cmd.Parameters.AddWithValue("@TeamIconUrl2", match.Team2.TeamIconUrl);
                    cmd.Parameters.AddWithValue("@Schiedrichter", "k.A.");

                    //cmd.Parameters.AddWithValue("@Runde", match.Group.GroupName);
                    cmd.Parameters.AddWithValue("@Gruppe", 0);
                    cmd.Parameters.AddWithValue("@GroupID", 0);

                    if (match.Group.GroupName == "Achtelfinale")
                    {
                        cmd.Parameters.AddWithValue("@Runde", "AF");
                        cmd.Parameters.AddWithValue("@RundeDetail", "AF");
                    }
                    else if (match.Group.GroupName == "Viertelfinale")
                    {
                        cmd.Parameters.AddWithValue("@Runde", "VF");
                        cmd.Parameters.AddWithValue("@RundeDetail", "VF");
                    }
                    else if (match.Group.GroupName == "Halbfinale")
                    {
                        cmd.Parameters.AddWithValue("@Runde", "HF");
                        cmd.Parameters.AddWithValue("@RundeDetail", "HF");
                    }

                    else if (match.Group.GroupName == "Finale")
                    {
                        cmd.Parameters.AddWithValue("@Runde", "F");
                        cmd.Parameters.AddWithValue("@RundeDetail", "F");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Runde", match.Group.GroupName);
                        cmd.Parameters.AddWithValue("@RundeDetail", match.Group.GroupName);
                    }

                    cmd.Parameters.AddWithValue("@Datum", match.MatchDateTime);
                    cmd.Parameters.AddWithValue("@Abgeschlossen", true);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        static async Task<List<Match>> GetMatchesAsync(string path)
        {
            try
            {
                List<LigaManagement.Models.Match> matches = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {

                    string matchstring = await response.Content.ReadAsStringAsync();
                    matches = JsonConvert.DeserializeObject<List<LigaManagement.Models.Match>>(matchstring);

                    //matches = await response.Content.ReadFromJsonAsync<List<LigaManagement.Models.Match>>();
                }
                return matches;
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
                MatchDetail match = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    string matchstring = await response.Content.ReadAsStringAsync();
                    match = JsonConvert.DeserializeObject<MatchDetail>(matchstring);

                    //match = await response.Content.ReadFromJsonAsync<MatchDetail>();
                }
                return match;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async void RundeChange(ChangeEventArgs e)
        {
            int BisSpieltag = 3;
            if (e.Value != null)
            {
                RundeChoosed = e.Value.ToString();
                Globals.currentEMWMRunde = RundeChoosed;


                EMWMSpieltage = await SpieltageEMWMService.GetSpielergebnisse();

                if (EMWMSpieltage == null)
                    return;

                EMWMSpieltage = EMWMSpieltage.ToList().Where(x => x.Saison == Globals.currentEMWMSaison).Where(x => x.Runde == RundeChoosed).OrderBy(x => x.Datum);

                VisibleBtnNew = NewButtonVisible();

                if (RundeChoosed == "G1" || RundeChoosed == "G2" || RundeChoosed == "G3")
                {
                    if (RundeChoosed == "G1")
                        BisSpieltag = 1;
                    else if (RundeChoosed == "G2")
                        BisSpieltag = 2;
                    else if (RundeChoosed == "G3")
                        BisSpieltag = 3;

                    TabellenA = await TabelleService.BerechneTabelleEMWM(SpieltagService, 1, BisSpieltag);
                    TabellenB = await TabelleService.BerechneTabelleEMWM(SpieltagService, 2, BisSpieltag);
                    TabellenC = await TabelleService.BerechneTabelleEMWM(SpieltagService, 3, BisSpieltag);
                    TabellenD = await TabelleService.BerechneTabelleEMWM(SpieltagService, 4, BisSpieltag);
                    if (Globals.currentEMWMSaison.IndexOf("2024") > -1 || Globals.currentEMWMSaison.IndexOf("2022") > -1 || Globals.currentEMWMSaison.IndexOf("2020") > -1 ||
                        Globals.currentEMWMSaison.IndexOf("2018") > -1 || Globals.currentEMWMSaison.IndexOf("2016") > -1 || Globals.currentEMWMSaison.IndexOf("2014") > -1 ||
                        Globals.currentEMWMSaison.IndexOf("2012") > -1 || Globals.currentEMWMSaison.IndexOf("2010") > -1 || Globals.currentEMWMSaison.IndexOf("2008") > -1)
                    {
                        TabellenE = await TabelleService.BerechneTabelleEMWM(SpieltagService, 5, BisSpieltag);
                        TabellenF = await TabelleService.BerechneTabelleEMWM(SpieltagService, 6, BisSpieltag);
                    }

                    if (Globals.currentEMWMSaison.StartsWith("WM"))
                    {
                        TabellenG = await TabelleService.BerechneTabelleEMWM(SpieltagService, 7, BisSpieltag);
                        TabellenH = await TabelleService.BerechneTabelleEMWM(SpieltagService, 8, BisSpieltag);
                    }
                }

                StateHasChanged();
            }
        }

        public async void OnClickHandler()
        {
            try
            {
                if (SaisonChoosed == 0 && RundeChoosed == null)
                {
                    DisplayErrorSaison = "block";
                    DisplayErrorRunde = "block";
                    return;
                }

                if (SaisonChoosed == 0)
                {
                    DisplayErrorSaison = "block";
                    DisplayErrorRunde = "none";
                    return;
                }

                if (RundeChoosed == null)
                {
                    DisplayErrorRunde = "block";
                    DisplayErrorSaison = "none";
                    return;
                }

                if (Globals.currentEMWMSaison.StartsWith("WM"))
                    VisibleTableWM = "block";
                else
                    VisibleTableWM = "none";



                DisplayErrorSaison = "none";
                DisplayErrorRunde = "none";

                EMWMSpieltage = await SpieltageEMWMService.GetSpielergebnisse();

                EMWMSpieltage = EMWMSpieltage.ToList().Where(x => x.SaisonID == Globals.EMWMSaisonID).Where(x => x.Runde == RundeChoosed).OrderBy(x => x.Datum);

                VisibleBtnNew = NewButtonVisible();

                Globals.bVisibleNavMenuElements = true;

                StateHasChanged();
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        private string NewButtonVisible()
        {
            string sButtonVisible = "";
            if (SaisonChoosed > 0 && RundeChoosed != "F")
                sButtonVisible = "block";

            //var PokalergebnisseSpieltage = SpieltageCLService.GetSpielergebnisse();

            //if (PokalergebnisseSpieltage == null)
            //    return sButtonVisible;

            //PokalergebnisseSpieltage = PokalergebnisseSpieltage.ToList().Where(x => x.SaisonID == SaisonChoosed);
            //if (RundeChoosed == "F" && PokalergebnisseSpieltage.Count() >= 1)
            //    sButtonVisible = "hidden";
            //else if (RundeChoosed == "HF" && PokalergebnisseSpieltage.Count() >= 2)
            //    sButtonVisible = "hidden";
            //else if (RundeChoosed == "VF" && PokalergebnisseSpieltage.Count() >= 4)
            //    sButtonVisible = "hidden";
            //else if (RundeChoosed == "AF" && PokalergebnisseSpieltage.Count() >= 8)
            //    sButtonVisible = "hidden";
            //else if (RundeChoosed == "G" && PokalergebnisseSpieltage.Count() >= 32)
            //    sButtonVisible = "hidden";
            //else
            //    sButtonVisible = "visible";

            return sButtonVisible;

        }

        [Bind]
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
        public class DisplayCLRunde
        {
            public DisplayCLRunde(string rundeKurzbezeichung, string rundename)
            {
                RundeKurzbezeichung = rundeKurzbezeichung;
                Rundename = rundename;
            }
            public string RundeKurzbezeichung { get; set; }
            public string Rundename { get; set; }
        }
    }
}



