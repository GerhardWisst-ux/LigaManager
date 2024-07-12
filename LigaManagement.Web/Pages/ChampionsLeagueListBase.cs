using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
using Microsoft.Extensions.Localization;
using static LigamanagerManagement.Web.Pages.EditPokalspieltagBase;

namespace LigaManagement.Web.Pages
{
    public class ChampionsLeagueListBase : ComponentBase
    {
        public RadzenDataGrid<Tabelle> gridTabelle;
        public bool allowVirtualization;
        static HttpClient client = new HttpClient();
        public RadzenDataGrid<PokalergebnisCLSpieltag> grid;
        public Density Density = Density.Compact;
        public string Titel { get; set; }
        protected string DisplayErrorRunde = "none";
        protected string DisplayErrorSaison = "none";

        public List<DisplayCLRunde> RundeList;

        public int SaisonChoosed = 0;
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
        public ISaisonenCLService SaisonenCLService { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }
        [Inject]
        public ISpieltageCLService SpieltagService { get; set; }

        public List<DisplaySaison> SaisonenList;

        public string VisibleBtnNew { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ISpieltageCLService SpieltageCLService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        public IEnumerable<PokalergebnisCLSpieltag> ErgebnisseCLSpieltage { get; set; }

        public IEnumerable<PokalergebnisCLSpieltag> PokalergebnisseCLSpieltageFinale { get; set; }

        [Inject]
        public IStringLocalizer<ChampionsLeague> Localizer { get; set; }

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

                SaisonenList = new List<DisplaySaison>();


                Saisonen = (await SaisonenCLService.GetSaisonen()).ToList().Where(x => x.Liganame == "Champions League");
                
                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }
                               
                SaisonChoosed = Globals.CLSaisonID;

                ErgebnisseCLSpieltage = await SpieltageCLService.GetSpielergebnisse();
                if (ErgebnisseCLSpieltage == null)
                {
                    return;
                }

                PokalergebnisseCLSpieltageFinale = ErgebnisseCLSpieltage.ToList().Where(x => x.Runde == "F");

                Globals.CLPokalSaisonID = Globals.SaisonID;
                
                if (Globals.currentClRunde == null)
                    RundeChoosed = "F";
                else
                    RundeChoosed = Globals.currentClRunde;


                ErgebnisseCLSpieltage = ErgebnisseCLSpieltage.ToList().Where(x => x.SaisonID == Globals.CLPokalSaisonID).Where(x => x.Runde == RundeChoosed);

                //if (PokalergebnisseCLSpieltage.Count() == 0)
                //{

                //    System.Threading.Thread.Sleep(2000);
                //}
                // var result = await GetDataFromOpenLgaDB();

                DisplayErrorRunde = "none";
                DisplayErrorSaison = "none";

                Globals.bVisibleNavMenuElements = true;

                RundeList = new List<DisplayCLRunde>
                {
                    new DisplayCLRunde("G1",Localizer["Gruppenphase Spieltag"].Value + 1),
                    new DisplayCLRunde("G2", Localizer["Gruppenphase Spieltag"].Value + 2),
                    new DisplayCLRunde("G3", Localizer["Gruppenphase Spieltag"].Value + 3),
                    new DisplayCLRunde("G4",Localizer["Gruppenphase Spieltag"].Value + 4),
                    new DisplayCLRunde("G5", Localizer["Gruppenphase Spieltag"].Value + 5),
                    new DisplayCLRunde("G6", Localizer["Gruppenphase Spieltag"].Value + 6),
                    new DisplayCLRunde("AF", Localizer["Achtelfinale"].Value),
                    new DisplayCLRunde("VF", Localizer["Viertelfinale"].Value),
                    new DisplayCLRunde("HF", Localizer["Halbfinale"].Value),
                    new DisplayCLRunde("F", Localizer["Finale"].Value),
                };


                if (Globals.currentClRunde != null)
                    OnClickHandler();

                VisibleBtnNew = "hidden";

                Globals.bVisibleNavMenuElements = true;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }
        public void CellRender(DataGridCellRenderEventArgs<PokalergebnisCLSpieltag> args)
        {
            if (args.Column.Property == "Verein1")
            {
                args.Attributes.Add("style", $"font-weight: {(args.Data.Tore1_Nr > args.Data.Tore2_Nr ? "800" : "normal")};");
            }

            if (args.Column.Property == "Verein2")
            {
                args.Attributes.Add("style", $"font-weight: {(args.Data.Tore1_Nr < args.Data.Tore2_Nr ? "800" : "normal")};");
            }

        }

        public async void SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                SaisonChoosed = Convert.ToInt32(e.Value);

                var saison = await SaisonenCLService.GetSaison(Convert.ToInt32(SaisonChoosed));

                if (saison != null)
                {
                    Globals.currentCLSaison = saison.Saisonname;
                    Globals.CLPokalSaisonID = saison.SaisonID;

                    TabellenA = null;
                    TabellenB = null;
                    TabellenC = null;
                    TabellenD = null;
                    TabellenE = null;
                    TabellenF = null;
                    TabellenG = null;
                    TabellenH = null;

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
                    //PokalergebnisseCLSpieltage = PokalergebnisseCLSpieltage.ToList().Where(x => x.SaisonID == Globals.CLPokalSaisonID).Where(x => x.Runde == RundeChoosed);

                    //if (PokalergebnisseCLSpieltage.Count() > 0)
                    //    return ret;

                    var matches = await GetMatchesAsync("getmatchdata/uefacl22/2022");

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

                    SqlCommand cmd = new SqlCommand("INSERT INTO spieltageCL(Saison,SaisonID,Verein1,Verein2,Verein1_Nr,Verein2_Nr, Tore1_Nr,Tore2_Nr, Ort,Datum,LigaID,Zuschauer,Schiedrichter,Runde,RundeDetail,Gruppe,Abgeschlossen,Land1_Nr, Land2_Nr,TeamIconUrl1,TeamIconUrl2,Verlängerung,Elfmeterschiessen,GroupID) " +
                                                      "VALUES (@Saison, @SaisonID,@Verein1,@Verein2,@Verein1_Nr,@Verein2_Nr,@Tore1_Nr,@Tore2_Nr,@Ort,@Datum,@LigaID,@Zuschauer,@Schiedrichter,@Runde,@RundeDetail,@Gruppe,@Abgeschlossen,@Land1_Nr,@Land2_Nr,@TeamIconUrl1,@TeamIconUrl2,@Verlängerung,@Elfmeterschiessen,@GroupID)", conn);

                    cmd.Parameters.AddWithValue("@Saison", matchdetail.LeagueSeason +  + (Convert.ToInt32(matchdetail.LeagueSeason.ToString().Substring(2, 2)) + 1));
                    cmd.Parameters.AddWithValue("@SaisonID", Globals.CLPokalSaisonID);
                    cmd.Parameters.AddWithValue("@LigaID", 13);
                    cmd.Parameters.AddWithValue("@Verein1", match.Team1.TeamName);
                    cmd.Parameters.AddWithValue("@Verein2", match.Team2.TeamName);
                    cmd.Parameters.AddWithValue("@Verein1_Nr", match.Team1.TeamId);
                    cmd.Parameters.AddWithValue("@Verein2_Nr", match.Team2.TeamId);
                    cmd.Parameters.AddWithValue("@Land1_Nr", 0);
                    cmd.Parameters.AddWithValue("@Land2_Nr", 0);


                    cmd.Parameters.AddWithValue("@Verlängerung", 0);
                    cmd.Parameters.AddWithValue("@Elfmeterschiessen", 0);

                    //cmd.Parameters.AddWithValue("@Tore1_Nr", matchdetail.matchResults[1].PointsTeam1);
                    //cmd.Parameters.AddWithValue("@Tore2_Nr", matchdetail.matchResults[1].PointsTeam2);
                    cmd.Parameters.AddWithValue("@Ort", "k.A.");
                    cmd.Parameters.AddWithValue("@Zuschauer", 0);
                    cmd.Parameters.AddWithValue("@TeamIconUrl1", match.Team1.TeamIconUrl);
                    cmd.Parameters.AddWithValue("@TeamIconUrl2", match.Team2.TeamIconUrl);
                    cmd.Parameters.AddWithValue("@Schiedrichter", "k.A.");


                    if (match.Group.GroupName.IndexOf("Gruppenspieltag") > -1)
                    {
                        if (match.Group.GroupName == "1. Gruppenspieltag")
                            cmd.Parameters.AddWithValue("@Runde", "G1");
                        else if (match.Group.GroupName == "2. Gruppenspieltag")
                            cmd.Parameters.AddWithValue("@Runde", "G2");
                        else if (match.Group.GroupName == "3. Gruppenspieltag")
                            cmd.Parameters.AddWithValue("@Runde", "G3");
                        else if (match.Group.GroupName == "4. Gruppenspieltag")
                            cmd.Parameters.AddWithValue("@Runde", "G4");
                        else if (match.Group.GroupName == "5. Gruppenspieltag")
                            cmd.Parameters.AddWithValue("@Runde", "G5");
                        else if (match.Group.GroupName == "6. Gruppenspieltag")
                            cmd.Parameters.AddWithValue("@Runde", "G6");
                        else
                            cmd.Parameters.AddWithValue("@Runde", match.Group.GroupName);


                        if (match.Team1.TeamName == "Galatasaray Istanbul" || match.Team2.TeamName == "Galatasaray Istanbul")
                            cmd.Parameters.AddWithValue("@GroupID", 1);
                        else if (match.Team1.TeamName == "FC Bayern München" || match.Team2.TeamName == "FC Bayern München")
                            cmd.Parameters.AddWithValue("@GroupID", 1);
                        else if (match.Team1.TeamName == "Manchester United FC" || match.Team2.TeamName == "Manchester United FC")
                            cmd.Parameters.AddWithValue("@GroupID", 1);
                        else if (match.Team1.TeamName == "FC Kopenhagen" || match.Team2.TeamName == "FC Kopenhagen")
                            cmd.Parameters.AddWithValue("@GroupID", 1);
                        else if (match.Team1.TeamName == "Feyenoord Rotterdam" || match.Team2.TeamName == "Feyenoord Rotterdam")
                            cmd.Parameters.AddWithValue("@GroupID", 5);
                        else if (match.Team1.TeamName == "Celtic Glasgow" || match.Team2.TeamName == "Celtic Glasgow")
                            cmd.Parameters.AddWithValue("@GroupID", 5);
                        else if (match.Team1.TeamName == "Lazio Rom" || match.Team2.TeamName == "Lazio Rom")
                            cmd.Parameters.AddWithValue("@GroupID", 5);
                        else if (match.Team1.TeamName == "Lazio Rom" || match.Team2.TeamName == "Lazio Rom")
                            cmd.Parameters.AddWithValue("@GroupID", 5);
                        else if (match.Team1.TeamName == "Feyenoord Rotterdam" || match.Team2.TeamName == "Feyenoord Rotterdam")
                            cmd.Parameters.AddWithValue("@GroupID", 4);
                        else if (match.Team1.TeamName == "Benfica Lissabon" || match.Team2.TeamName == "Benfica Lissabon")
                            cmd.Parameters.AddWithValue("@GroupID", 4);
                        else if (match.Team1.TeamName == "Celtic Glasgow" || match.Team2.TeamName == "Celtic Glasgow")
                            cmd.Parameters.AddWithValue("@GroupID", 4);
                        else if (match.Team1.TeamName == "Real Sociedad" || match.Team2.TeamName == "Real Sociedad")
                            cmd.Parameters.AddWithValue("@GroupID", 4);
                        else if (match.Team1.TeamName == "RB Leipzig" || match.Team2.TeamName == "RB Leipzig")
                            cmd.Parameters.AddWithValue("@GroupID", 7);
                        else if (match.Team1.TeamName == "BSC Young Boys" || match.Team2.TeamName == "BSC Young Boys")
                            cmd.Parameters.AddWithValue("@GroupID", 7);
                        else if (match.Team1.TeamName == "Manchester City" || match.Team2.TeamName == "Manchester City")
                            cmd.Parameters.AddWithValue("@GroupID", 7);
                        else if (match.Team1.TeamName == "Roter Stern Belgrad" || match.Team2.TeamName == "Roter Stern Belgrad")
                            cmd.Parameters.AddWithValue("@GroupID", 7);
                        else if (match.Team1.TeamName == "Royal Antwerpen FC" || match.Team2.TeamName == "Royal Antwerpen FC")
                            cmd.Parameters.AddWithValue("@GroupID", 8);
                        else if (match.Team1.TeamName == "Schachtar Donezk" || match.Team2.TeamName == "Schachtar Donezk")
                            cmd.Parameters.AddWithValue("@GroupID", 8);
                        else if (match.Team1.TeamName == "Barcelona F. C" || match.Team2.TeamName == "Barcelona F. C")
                            cmd.Parameters.AddWithValue("@GroupID", 8);
                        else if (match.Team1.TeamName == "FC Porto" || match.Team2.TeamName == "FC Porto")
                            cmd.Parameters.AddWithValue("@GroupID", 8);

                        else
                            cmd.Parameters.AddWithValue("@GroupID", 0);

                        cmd.Parameters.AddWithValue("@Gruppe", true);

                    }
                    else
                    {
                        if (match.Group.GroupName == "Achtelfinale - Hinspiel")
                            cmd.Parameters.AddWithValue("@Runde", "AF");
                        else if (match.Group.GroupName == "Achtelfinale - Rückspiel")
                            cmd.Parameters.AddWithValue("@Runde", "AF");
                        else if (match.Group.GroupName == "Viertelfinale - Hinspiel")
                            cmd.Parameters.AddWithValue("@Runde", "VF");
                        else if (match.Group.GroupName == "Viertelfinale - Rückspiel")
                            cmd.Parameters.AddWithValue("@Runde", "VF");
                        else if (match.Group.GroupName == "Halbfinale - Hinspiel")
                            cmd.Parameters.AddWithValue("@Runde", "HF");
                        else if (match.Group.GroupName == "Halbfinale - Rückspiel")
                            cmd.Parameters.AddWithValue("@Runde", "HF");
                        else if (match.Group.GroupName == "Finalspiel")
                            cmd.Parameters.AddWithValue("@Runde", "F");
                        else
                            cmd.Parameters.AddWithValue("@Runde", match.Group.GroupName);

                        cmd.Parameters.AddWithValue("@Gruppe", false);
                        cmd.Parameters.AddWithValue("@GroupID", 0);
                    }

                    cmd.Parameters.AddWithValue("@RundeDetail", match.Group.GroupName);
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

        static async Task<List<LigaManagement.Models.Match>> GetMatchesAsync(string path)
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
            int BisSpieltag = 6;
            if (e.Value != null)
            {
                RundeChoosed = e.Value.ToString();
                Globals.currentClRunde = RundeChoosed;

                ErgebnisseCLSpieltage = await SpieltageCLService.GetSpielergebnisse();

                if (ErgebnisseCLSpieltage == null)
                    return;

                ErgebnisseCLSpieltage = ErgebnisseCLSpieltage.ToList().Where(x => x.Saison == Globals.currentSaison).Where(x => x.Runde == RundeChoosed);

                VisibleBtnNew = NewButtonVisible();

                if (RundeChoosed == "G1" || RundeChoosed == "G2" || RundeChoosed == "G3" || RundeChoosed == "G4" || RundeChoosed == "G5" || RundeChoosed == "G6")
                {
                    if (RundeChoosed == "G1")
                        BisSpieltag = 1;
                    else if (RundeChoosed == "G2")
                        BisSpieltag = 2;
                    else if (RundeChoosed == "G3")
                        BisSpieltag = 3;
                    else if (RundeChoosed == "G4")
                        BisSpieltag = 4;
                    else if (RundeChoosed == "G5")
                        BisSpieltag = 5;
                    else if (RundeChoosed == "G6")
                        BisSpieltag = 6;

                    TabellenA = await TabelleService.BerechneTabelleCL(SpieltagService, 1, BisSpieltag);
                    TabellenB = await TabelleService.BerechneTabelleCL(SpieltagService, 2, BisSpieltag);
                    TabellenC = await TabelleService.BerechneTabelleCL(SpieltagService, 3, BisSpieltag);
                    TabellenD = await TabelleService.BerechneTabelleCL(SpieltagService, 4, BisSpieltag);
                    TabellenE = await TabelleService.BerechneTabelleCL(SpieltagService, 5, BisSpieltag);
                    TabellenF = await TabelleService.BerechneTabelleCL(SpieltagService, 6, BisSpieltag);
                    TabellenG = await TabelleService.BerechneTabelleCL(SpieltagService, 7, BisSpieltag);
                    TabellenH = await TabelleService.BerechneTabelleCL(SpieltagService, 8, BisSpieltag);
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

                DisplayErrorSaison = "none";
                DisplayErrorRunde = "none";

                ErgebnisseCLSpieltage = await SpieltageCLService.GetSpielergebnisse();

                ErgebnisseCLSpieltage = ErgebnisseCLSpieltage.ToList().Where(x => x.Saison == Globals.currentCLSaison).Where(x => x.Runde == RundeChoosed);

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
            string sButtonVisible = "hidden";

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



