﻿using LigaManagement.Models;
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
        public RadzenDataGrid<PokalergebnisCL_EM_WMSpieltag> grid;
        public Density Density = Density.Compact;
        public string Titel { get; set; }
        protected string DisplayErrorRunde = "none";
        protected string DisplayErrorSaison = "none";
        protected string FontWeight = "font-weight:normal";

        protected string VisibleTableABCD = "inline-block";
        protected string VisibleTableEF = "none";
        protected string VisibleTableGH = "none";        
        protected string VisibleTableWM = "none";

        public List<DisplayRunde> RundeList;

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
        public IEnumerable<Tabelle> TabellenI { get; set; }
        public IEnumerable<Tabelle> TabellenII { get; set; }

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

        public IEnumerable<PokalergebnisCL_EM_WMSpieltag> EMWMSpieltage { get; set; }

        public IEnumerable<PokalergebnisCL_EM_WMSpieltag> EMWMSpieltageFinale { get; set; }

        [Inject]
        public IStringLocalizer<EM_WMListbase> Localizer { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Saison saison;
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

                Saisonen = (await SaisonenEMWMService.GetSaisonen()).Where(x => x.Saisonname.StartsWith("WM") || x.Saisonname.StartsWith("EM")).ToList().OrderByDescending(x => x.Saisonname.Substring(x.Saisonname.Length - 4));

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname, columns.Aktuell));
                }

                if (Globals.EMWMSaisonID == 0)
                    saison = await SaisonenEMWMService.GetSaison(Convert.ToInt32(SaisonenList[0].SaisonID));
                else
                    saison = await SaisonenEMWMService.GetSaison(Globals.EMWMSaisonID);

                if (saison != null)
                {
                    Globals.currentEMWMSaison = saison.Saisonname;
                    Globals.EMWMSaisonID = saison.SaisonID;
                    Globals.EMMWMLigaId = saison.LigaID;

                    SaisonChoosed = Globals.EMWMSaisonID;

                    OnClickHandler();
                }

                ShowGroupTables();

                if (Globals.currentEMWMRunde == null)
                    Globals.currentEMWMRunde = "1";

                if (Globals.currentEMWMRunde != null)
                {
                    EMWMSpieltage = await SpieltageEMWMService.GetSpielergebnisse();
                    //if (EMWMSpieltage == null)
                    //{
                    //    return;
                    //}

                    EMWMSpieltageFinale = EMWMSpieltage.ToList().Where(x => x.Runde == "F");

                    if (Globals.currentEMWMRunde == null)
                        RundeChoosed = "G1";
                    else
                        RundeChoosed = Globals.currentEMWMRunde;

                    EMWMSpieltage = EMWMSpieltage.ToList().Where(x => x.SaisonID == Globals.EMWMSaisonID).Where(x => x.Runde == RundeChoosed).OrderBy(x => x.Datum);
                }


                //if (EMWMSpieltage.Count() > 0)
                //{
                //    System.Threading.Thread.Sleep(2000);
                //}
                //var result = await GetDataFromOpenLgaDB();

                Globals.bVisibleNavMenuElements = true;

                ShowRunden();

                VisibleBtnNew = "hidden";
                                
                OnClickHandler();

                DisplayErrorRunde = "none";
                DisplayErrorSaison = "none";

                TableVisible();
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
                        VisibleTableEF = "inline-block;";
                    else if (Globals.currentEMWMSaison.IndexOf("2024") > -1 || Globals.currentEMWMSaison.IndexOf("2022") > -1 || Globals.currentEMWMSaison.IndexOf("2020") > -1 ||
                         Globals.currentEMWMSaison.IndexOf("2018") > -1 || Globals.currentEMWMSaison.IndexOf("2016") > -1 || Globals.currentEMWMSaison.IndexOf("2014") > -1 ||
                         Globals.currentEMWMSaison.IndexOf("2012") > -1 || Globals.currentEMWMSaison.IndexOf("2010") > -1)
                        VisibleTableEF = "inline-block;";
                    else
                        VisibleTableEF = "none;";

                    if (Globals.currentEMWMSaison.StartsWith("WM"))
                    {
                        if (Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1990")
                            VisibleTableABCD = "inline-block;";
                        else
                            VisibleTableGH = "none;";

                    }
                    else if (Globals.currentEMWMSaison.IndexOf("1980") > -1 || Globals.currentEMWMSaison.IndexOf("1984") > -1 || Globals.currentEMWMSaison.IndexOf("1988") > -1 ||
                            Globals.currentEMWMSaison.IndexOf("1992") > -1 || Globals.currentEMWMSaison.IndexOf("1996") > -1)
                        VisibleTableGH = "none;";
                    else
                        VisibleTableGH = "inline-block;";
                }

                ShowRunden();
                OnClickHandler();
            }
        }

        public void CellRender(DataGridCellRenderEventArgs<PokalergebnisCL_EM_WMSpieltag> args)
        {
            if (args.Column.Property == "Verein1")
            {
                args.Attributes.Add("style", $"font-weight: {(args.Data.Tore1_Nr > args.Data.Tore2_Nr && args.Data.GroupID == 0 ? "800" : "normal")};");
            }

            if (args.Column.Property == "Verein2")
            {
                args.Attributes.Add("style", $"font-weight: {(args.Data.Tore1_Nr < args.Data.Tore2_Nr && args.Data.GroupID == 0 ? "800" : "normal")};");
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
            if (e.Value != null)
            {
                RundeChoosed = e.Value.ToString();
                Globals.currentEMWMRunde = RundeChoosed;

                EMWMSpieltage = await SpieltageEMWMService.GetSpielergebnisse();

                if (EMWMSpieltage == null)
                    return;

                EMWMSpieltage = EMWMSpieltage.ToList().Where(x => x.Saison == Globals.currentEMWMSaison).Where(x => x.Runde == RundeChoosed).OrderBy(x => x.Datum);

                VisibleBtnNew = NewButtonVisible();

                TableVisible();
                OnClickHandler();
            }
        }

        public async void OnClickHandler()
        {
            try
            {
                if (SaisonChoosed == 0 && RundeChoosed == "0")
                {
                    DisplayErrorSaison = "block";
                    DisplayErrorRunde = "block";
                    StateHasChanged();
                    return;
                }

                if (SaisonChoosed == 0)
                {
                    DisplayErrorSaison = "block";
                    DisplayErrorRunde = "none";
                    StateHasChanged();
                    return;
                }

                if (RundeChoosed == null)
                {
                    DisplayErrorRunde = "block";
                    DisplayErrorSaison = "none";
                    StateHasChanged();
                    return;
                }

                ShowGroupTables();

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

        private async void TableVisible()
        {
            if (RundeChoosed == "2G1" || RundeChoosed == "2G2" || RundeChoosed == "2G3")
            {
                if (RundeChoosed == "2G1")
                    BisSpieltag = 1;
                else if (RundeChoosed == "2G2")
                    BisSpieltag = 2;
                else if (RundeChoosed == "2G3")
                    BisSpieltag = 3;

                TabellenI = await TabelleService.BerechneTabelleEMWM(SpieltagService, 9, BisSpieltag);
                TabellenII = await TabelleService.BerechneTabelleEMWM(SpieltagService, 10, BisSpieltag);

                VisibleTableABCD = "none;";

            }
            else if (RundeChoosed == "G1" || RundeChoosed == "G2" || RundeChoosed == "G3")
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
                    Globals.currentEMWMSaison.IndexOf("2010") > -1 || Globals.currentEMWMSaison.IndexOf("2006") > -1 || Globals.currentEMWMSaison.IndexOf("2002") > -1 ||
                    Globals.currentEMWMSaison.IndexOf("1998") > -1 || Globals.currentEMWMSaison.IndexOf("1994") > -1 || Globals.currentEMWMSaison.IndexOf("1990") > -1 ||
                    Globals.currentEMWMSaison.IndexOf("1986") > -1 || Globals.currentEMWMSaison.IndexOf("1982") > -1)
                {
                    TabellenE = await TabelleService.BerechneTabelleEMWM(SpieltagService, 5, BisSpieltag);
                    TabellenF = await TabelleService.BerechneTabelleEMWM(SpieltagService, 6, BisSpieltag);
                    VisibleTableEF = "inline-block;";
                }
                if (Globals.currentEMWMSaison.StartsWith("WM"))
                {
                    if (Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1990" && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1986"
                        && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1982")
                    {
                        TabellenG = await TabelleService.BerechneTabelleEMWM(SpieltagService, 7, BisSpieltag);
                        TabellenH = await TabelleService.BerechneTabelleEMWM(SpieltagService, 8, BisSpieltag);
                    }
                }

                VisibleTableABCD = "inline-block;";
            }
            StateHasChanged();
        }

        private void ShowRunden()
        {
            if (Globals.currentEMWMSaison.IndexOf("1974") > -1 || Globals.currentEMWMSaison.IndexOf("1978") > -1)
            {
                RundeList = new List<DisplayRunde>
                    {
                        new DisplayRunde("G1", Localizer["Gruppenphase Spieltag"].Value + " " + 1),
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

        private void ShowGroupTables()
        {
            if (Globals.currentEMWMSaison.StartsWith("WM") && (Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1990"
                     && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1986" && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1982"
                     && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1978" && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1974"
                     && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1970"
                     && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1966" && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1962"
                     && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1958" && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) != "1954"))
            {
                VisibleTableABCD = "inline-block;";
                VisibleTableEF = "inline-block;";
                VisibleTableWM = "inline-block;";
            }
            else if (Globals.currentEMWMSaison.StartsWith("WM") && (Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1990"
                && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1986" && Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1982"))
            {
                VisibleTableEF = "inline-block;";
                VisibleTableGH = "inline-block;";
                VisibleTableEF = "inline-block;";
                VisibleTableWM = "none;";
            }
            else if (Globals.currentEMWMSaison.StartsWith("WM")
                && (Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1978")
                || (Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1974")
                || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1970"
                || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1966"
                || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1962"
                || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1958"
                || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1954"
                || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1954"
                || Globals.currentEMWMSaison.Substring(Globals.currentEMWMSaison.Length - 4) == "1930")
            {
                VisibleTableEF = "inline-block;";
                VisibleTableGH = "inline-block;";
                VisibleTableEF = "none;";
                VisibleTableWM = "none;";
            }
            else if (Globals.currentEMWMSaison.IndexOf("1980") > -1 || Globals.currentEMWMSaison.IndexOf("1984") > -1 || Globals.currentEMWMSaison.IndexOf("1988") > -1 ||
                    Globals.currentEMWMSaison.IndexOf("1992") > -1)
            {
                VisibleTableEF = "inline-block;";
                VisibleTableGH = "none;";
                VisibleTableEF = "none;";
                VisibleTableWM = "none;";
            }
            else if (Globals.currentEMWMSaison.IndexOf("1996") > -1 || Globals.currentEMWMSaison.IndexOf("2000") > -1 || Globals.currentEMWMSaison.IndexOf("2004") > -1
               || Globals.currentEMWMSaison.IndexOf("2008") > -1 || Globals.currentEMWMSaison.IndexOf("2012") > -1)
            {
                VisibleTableEF = "inline-block;";
                VisibleTableGH = "inline-block;";
                VisibleTableEF = "none;";
                VisibleTableWM = "none;";
            }
            else if (Globals.currentEMWMSaison.IndexOf("1972") > -1 || Globals.currentEMWMSaison.IndexOf("1968") > -1 || Globals.currentEMWMSaison.IndexOf("1964") > -1
               || Globals.currentEMWMSaison.IndexOf("1960") > -1)
            {
                VisibleTableEF = "none;";
                VisibleTableGH = "none;";
                VisibleTableEF = "none;";
                VisibleTableWM = "none;";
            }
            else
            {
                VisibleTableEF = "inline-block;";
                VisibleTableGH = "inline-block;";
                VisibleTableEF = "inline-block;";
                VisibleTableWM = "none;";
            }
        }

        private string NewButtonVisible()
        {
            string sButtonVisible = "";
            if (SaisonChoosed > 0 && RundeChoosed != "F")
                sButtonVisible = "block";

            //var PokalergebnisseSpieltage = SpieltageEMWMService.GetSpielergebnisse();

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
    }

    [Bind]
    public class DisplaySaison
    {
        public DisplaySaison(int saisonID, string saisonname, bool aktuell)
        {
            SaisonID = saisonID;
            Saisonname = saisonname;
            Aktuell = aktuell;
        }
        public int SaisonID { get; set; }
        public string Saisonname { get; set; }

        public bool Aktuell { get; set; }
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
}




