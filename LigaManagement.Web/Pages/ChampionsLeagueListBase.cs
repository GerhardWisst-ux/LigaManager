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
        private static readonly HttpClient client = new HttpClient();
        public RadzenDataGrid<PokalergebnisCL_EM_WMSpieltag> grid;
        public Density Density = Density.Compact;
        public string Titel { get; set; }
        protected string DisplayErrorRunde = "none";
        protected string DisplayErrorSaison = "none";

        public List<DisplayCLRunde> RundeList;

        public bool IsLoading = false;
        public int SaisonChoosed = 0;
        public string RundeChoosed;

        public IEnumerable<Tabelle> TabellenALL { get; set; }
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

        public IEnumerable<PokalergebnisCL_EM_WMSpieltag> ErgebnisseCLSpieltage { get; set; }

        public IEnumerable<PokalergebnisCL_EM_WMSpieltag> PokalergebnisseCLSpieltageFinale { get; set; }

        [Inject]
        public IStringLocalizer<ChampionsLeague> Localizer { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authenticationState = await authenticationStateTask.ConfigureAwait(false);

                if (authenticationState.User.Identity == null || !authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/Ligamanager");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                    return;
                }

                IsLoading = true;
                SaisonenList = new List<DisplaySaison>();

                Saisonen = (await SaisonenCLService.GetSaisonen().ConfigureAwait(false))
                    .Where(x => x.Liganame == "Champions League")
                    .ToList();

                foreach (var saison in Saisonen)
                {
                    SaisonenList.Add(new DisplaySaison(saison.SaisonID, saison.Saisonname));
                }

                SaisonChoosed = Globals.CLSaisonID;

                ErgebnisseCLSpieltage = await SpieltageCLService.GetSpielergebnisse().ConfigureAwait(false);
                if (ErgebnisseCLSpieltage == null)
                {
                    return;
                }

                PokalergebnisseCLSpieltageFinale = ErgebnisseCLSpieltage
                    .Where(x => x.Runde == "F")
                    .ToList();

                Globals.CLPokalSaisonID = Globals.SaisonID;

                RundeChoosed = Globals.currentClRunde ?? "F";

                ErgebnisseCLSpieltage = ErgebnisseCLSpieltage
                    .Where(x => x.SaisonID == Globals.CLPokalSaisonID && x.Runde == RundeChoosed)
                    .ToList();

                DisplayErrorRunde = "none";
                DisplayErrorSaison = "none";

                Globals.bVisibleNavMenuElements = true;

                RundeList = GetRundeList();

                if (Globals.currentClRunde != null)
                {
                    OnClickHandler();
                }

                VisibleBtnNew = "hidden";
                IsLoading = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        private List<DisplayCLRunde> GetRundeList()
        {
            if (Convert.ToInt32(Globals.currentCLSaison.Substring(0, 4)) > 2023)
            {
                return new List<DisplayCLRunde>
            {
                new DisplayCLRunde("G1", Localizer["Gruppenphase Spieltag"].Value + 1),
                new DisplayCLRunde("G2", Localizer["Gruppenphase Spieltag"].Value + 2),
                new DisplayCLRunde("G3", Localizer["Gruppenphase Spieltag"].Value + 3),
                new DisplayCLRunde("G4", Localizer["Gruppenphase Spieltag"].Value + 4),
                new DisplayCLRunde("G5", Localizer["Gruppenphase Spieltag"].Value + 5),
                new DisplayCLRunde("G6", Localizer["Gruppenphase Spieltag"].Value + 6),
                new DisplayCLRunde("G7", Localizer["Gruppenphase Spieltag"].Value + 7),
                new DisplayCLRunde("G8", Localizer["Gruppenphase Spieltag"].Value + 8),
                new DisplayCLRunde("Zw", Localizer["Zwischenrunde"].Value),
                new DisplayCLRunde("AF", Localizer["Achtelfinale"].Value),
                new DisplayCLRunde("VF", Localizer["Viertelfinale"].Value),
                new DisplayCLRunde("HF", Localizer["Halbfinale"].Value),
                new DisplayCLRunde("F", Localizer["Finale"].Value),
            };
            }
            else
            {
                return new List<DisplayCLRunde>
            {
                new DisplayCLRunde("G1", Localizer["Gruppenphase Spieltag"].Value + 1),
                new DisplayCLRunde("G2", Localizer["Gruppenphase Spieltag"].Value + 2),
                new DisplayCLRunde("G3", Localizer["Gruppenphase Spieltag"].Value + 3),
                new DisplayCLRunde("G4", Localizer["Gruppenphase Spieltag"].Value + 4),
                new DisplayCLRunde("G5", Localizer["Gruppenphase Spieltag"].Value + 5),
                new DisplayCLRunde("G6", Localizer["Gruppenphase Spieltag"].Value + 6),
                new DisplayCLRunde("AF", Localizer["Achtelfinale"].Value),
                new DisplayCLRunde("VF", Localizer["Viertelfinale"].Value),
                new DisplayCLRunde("HF", Localizer["Halbfinale"].Value),
                new DisplayCLRunde("F", Localizer["Finale"].Value),
            };
            }
        }

        public void CellRender(DataGridCellRenderEventArgs<PokalergebnisCL_EM_WMSpieltag> args)
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

        public async Task SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                SaisonChoosed = Convert.ToInt32(e.Value);

                var saison = await SaisonenCLService.GetSaison(SaisonChoosed).ConfigureAwait(false);

                if (saison != null)
                {
                    Globals.currentCLSaison = saison.Saisonname;
                    Globals.CLPokalSaisonID = saison.SaisonID;
                }

                TabellenALL = null;
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
                    var matches = await GetMatchesAsync("getmatchdata/uefacl22/2022").ConfigureAwait(false);

                    if (matches == null)
                    {
                        return ret;
                    }

                    foreach (var match in matches)
                    {
                        Debug.Print($"{match.MatchDateTime}: {match.Team1.TeamName} : {match.Team2.TeamName}");

                        var matchDetail = await GetMatchAsync($"getmatchdata/{match.MatchID}").ConfigureAwait(false);

                        if (matches.Count <= 125)
                        {
                            SaveImportDataToDatabase(match, matchDetail);
                        }
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

        private void SaveImportDataToDatabase(LigaManagement.Models.Match match, MatchDetail matchdetail)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Globals.connstring))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO spieltageCL(Saison,SaisonID,Verein1,Verein2,Verein1_Nr,Verein2_Nr, Tore1_Nr,Tore2_Nr, Ort,Datum,LigaID,Zuschauer,Schiedrichter,Runde,RundeDetail,Gruppe,Abgeschlossen,Land1_Nr, Land2_Nr,TeamIconUrl1,TeamIconUrl2,Verlängerung,Elfmeterschiessen,GroupID) " +
                                                      "VALUES (@Saison, @SaisonID,@Verein1,@Verein2,@Verein1_Nr,@Verein2_Nr,@Tore1_Nr,@Tore2_Nr,@Ort,@Datum,@LigaID,@Zuschauer,@Schiedrichter,@Runde,@RundeDetail,@Gruppe,@Abgeschlossen,@Land1_Nr,@Land2_Nr,@TeamIconUrl1,@TeamIconUrl2,@Verlängerung,@Elfmeterschiessen,@GroupID)", conn);

                    cmd.Parameters.AddWithValue("@Saison", matchdetail.LeagueSeason + (Convert.ToInt32(matchdetail.LeagueSeason.ToString().Substring(2, 2)) + 1));
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
                    cmd.Parameters.AddWithValue("@Ort", "k.A.");
                    cmd.Parameters.AddWithValue("@Zuschauer", 0);
                    cmd.Parameters.AddWithValue("@TeamIconUrl1", match.Team1.TeamIconUrl);
                    cmd.Parameters.AddWithValue("@TeamIconUrl2", match.Team2.TeamIconUrl);
                    cmd.Parameters.AddWithValue("@Schiedrichter", "k.A.");

                    if (match.Group.GroupName.Contains("Gruppenspieltag"))
                    {
                        cmd.Parameters.AddWithValue("@Runde", match.Group.GroupName switch
                        {
                            "1. Gruppenspieltag" => "G1",
                            "2. Gruppenspieltag" => "G2",
                            "3. Gruppenspieltag" => "G3",
                            "4. Gruppenspieltag" => "G4",
                            "5. Gruppenspieltag" => "G5",
                            "6. Gruppenspieltag" => "G6",
                            _ => match.Group.GroupName
                        });

                        cmd.Parameters.AddWithValue("@GroupID", match.Team1.TeamName switch
                        {
                            "Galatasaray Istanbul" or "FC Bayern München" or "Manchester United FC" or "FC Kopenhagen" => 1,
                            "Feyenoord Rotterdam" or "Celtic Glasgow" or "Lazio Rom" => 5,
                            "Benfica Lissabon" or "Real Sociedad" => 4,
                            "RB Leipzig" or "BSC Young Boys" or "Manchester City" or "Roter Stern Belgrad" => 7,
                            "Royal Antwerpen FC" or "Schachtar Donezk" or "Barcelona F. C" or "FC Porto" => 8,
                            _ => 0
                        });

                        cmd.Parameters.AddWithValue("@Gruppe", true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Runde", match.Group.GroupName switch
                        {
                            "Achtelfinale - Hinspiel" or "Achtelfinale - Rückspiel" => "AF",
                            "Viertelfinale - Hinspiel" or "Viertelfinale - Rückspiel" => "VF",
                            "Halbfinale - Hinspiel" or "Halbfinale - Rückspiel" => "HF",
                            "Finalspiel" => "F",
                            _ => match.Group.GroupName
                        });

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

        public async void RundeChange(ChangeEventArgs e)
        {            

            if (e.Value != null)
            {              

                RundeChoosed = e.Value.ToString();
                Globals.currentClRunde = RundeChoosed;

                

                OnClickHandler();
                StateHasChanged();
            }
        }

        public async void OnClickHandler()
        {
            try
            {
                int BisSpieltag = 6;

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

                if (ErgebnisseCLSpieltage.Count() == 0)
                {
                    TabellenB = null;
                    TabellenC = null;
                    TabellenD = null;
                    TabellenE = null;
                    TabellenF = null;
                    TabellenG = null;
                    TabellenH = null;

                    VisibleBtnNew = await NewButtonVisible();

                    Globals.bVisibleNavMenuElements = true;
                    StateHasChanged();
                    return;
                }

                if (Convert.ToInt32(Globals.currentCLSaison.Substring(0, 4)) < 2024)
                {
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

                        StateHasChanged();
                    }
                }
                else
                {
                    if (RundeChoosed == "G1" || RundeChoosed == "G2" || RundeChoosed == "G3" || RundeChoosed == "G4" || RundeChoosed == "G5" || RundeChoosed == "G6" || RundeChoosed == "G7" || RundeChoosed == "G8")
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
                        else if (RundeChoosed == "G7")
                            BisSpieltag = 7;
                        else if (RundeChoosed == "G8")
                            BisSpieltag = 8;

                        TabellenALL = await TabelleService.BerechneTabelleCL36(SpieltagService, BisSpieltag);

                        StateHasChanged();

                    }
                }

                VisibleBtnNew = await NewButtonVisible();

                Globals.bVisibleNavMenuElements = true;
                StateHasChanged();
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        private async Task<string> NewButtonVisible()
        {
            string sButtonVisible = "hidden";

            var PokalergebnisseSpieltage = await SpieltageCLService.GetSpielergebnisse();

            if (PokalergebnisseSpieltage == null)
                return sButtonVisible;

            PokalergebnisseSpieltage = PokalergebnisseSpieltage.ToList().Where(x => x.SaisonID == SaisonChoosed);
            if (RundeChoosed == "F" && PokalergebnisseSpieltage.Count() >= 1)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "HF" && PokalergebnisseSpieltage.Count() >= 2)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "VF" && PokalergebnisseSpieltage.Count() >= 4)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "AF" && PokalergebnisseSpieltage.Count() >= 8)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "G" && PokalergebnisseSpieltage.Count() >= 32)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "0")
                sButtonVisible = "hidden";
            else
                sButtonVisible = "visible";

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



