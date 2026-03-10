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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;


namespace LigaManagement.Web.Pages
{
    public class PokalergebnisseListBase : ComponentBase
    {
        public RadzenDataGrid<PokalergebnisSpieltag> grid;
        public RadzenDataGrid<PokalergebnisStatistik> gridstat;
        private static readonly HttpClient client = new HttpClient();
        public Density Density = Density.Compact;
        public bool allowVirtualization;
        public string Titel { get; set; }
        protected string DisplayErrorRunde = "none";
        protected string DisplayErrorSaison = "none";

        public bool IsLoading = false;
        public int SaisonChoosed = 0;
        public string RundeChoosed;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        public List<DisplayRunde> RundeList;

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public List<DisplaySaison> SaisonenList;

        public string VisibleBtnNew { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IPokalergebnisseService PokalergebnisseService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        public IEnumerable<PokalergebnisSpieltag> PokalergebnisseSpieltage { get; set; }

        public IEnumerable<PokalergebnisSpieltag> PokalergebnisseSpieltageFinale { get; set; }

        public IEnumerable<PokalergebnisSpieltag> SupercupEndspiele { get; set; }

        public IEnumerable<PokalergebnisStatistik> PokalergebnisseSpieltageStatistik { get; set; }

        [Inject]
        public IStringLocalizer<Pokalergebnisse> Localizer { get; set; }

     

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

                IsLoading = true;
                SaisonenList = new List<DisplaySaison>();


                Saisonen = (await SaisonenService.GetSaisonen()).ToList().Where(x => x.LigaID == 1);

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                PokalergebnisseSpieltageFinale = await PokalergebnisseService.GetPokalergebnisseSpieltag();

                if (PokalergebnisseSpieltageFinale == null)
                    return;

                PokalergebnisseSpieltageFinale = PokalergebnisseSpieltageFinale.ToList().Where(x => x.Runde == "F" && x.Supercup == false).OrderByDescending(x => x.Datum);

                var supercup = await PokalergebnisseService.GetPokalergebnisseSpieltag();

                SupercupEndspiele = supercup.Where(x => x.Runde == "F" && x.Supercup == true).OrderByDescending(x => x.Datum);

                PokalergebnisseSpieltageStatistik = await PokalergebnisseService.GetPokalergebnisseStatistik(true);

                if (PokalergebnisseSpieltageStatistik == null)
                    return;

                
                DisplayErrorRunde = "none";
                DisplayErrorSaison = "none";

                VisibleBtnNew = "hidden";

                if (Globals.currentPokalRunde == null)
                    RundeChoosed = null;
                else
                    RundeChoosed = Globals.currentPokalRunde;

                SaisonChoosed = Globals.CLPokalSaisonID;

                if (Globals.currentPokalRunde != null)
                    OnClickHandler();

                //await GetDataFromOpenLgaDB();

                RundeList = new List<DisplayRunde>
                {
                    new DisplayRunde("1",Localizer["1. Runde"].Value),
                    new DisplayRunde("2",Localizer["2. Runde"].Value),
                    new DisplayRunde("AF", Localizer["Achtelfinale"].Value),
                    new DisplayRunde("VF", Localizer["Viertelfinale"].Value),
                    new DisplayRunde("HF", Localizer["Halbfinale"].Value),
                    new DisplayRunde("F", Localizer["Finale"].Value),
                };

                Globals.bVisibleNavMenuElements = true;

                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }        
        public async Task RundeZurueck()
        {
            IsLoading = true;

            var currentIndex = RundeList.FindIndex(r => r.RundeKurzbezeichung == RundeChoosed);
            DisplayRunde previous = currentIndex > 0 ? RundeList[currentIndex - 1] : null;
            if (previous == null)
            {
                IsLoading = false;
                return;
            }

            Globals.currentPokalRunde = previous?.RundeKurzbezeichung;
            RundeChoosed = previous?.RundeKurzbezeichung;            

            PokalergebnisseSpieltage = await PokalergebnisseService.GetPokalergebnisseSpieltag();

            if (PokalergebnisseSpieltage == null)
                return;

            PokalergebnisseSpieltage = PokalergebnisseSpieltage.ToList();
            PokalergebnisseSpieltage = PokalergebnisseSpieltage.Where(x => x.SaisonID == SaisonChoosed && x.Runde == RundeChoosed).OrderBy(x => x.Datum);

            VisibleBtnNew = NewButtonVisible();

            OnClickHandler();
            IsLoading = false;
            StateHasChanged();
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
                    var matches = await GetMatchesAsync("getmatchdata/dfb/2023").ConfigureAwait(false);

                    if (matches == null)
                    {
                        return ret;
                    }

                    int ii = 0;
                    foreach (var match in matches)
                    {
                        int mod = ii % 10;

                        
                        Debug.Print($"{match.MatchDateTime}: {match.Team1.TeamName} : {match.Team2.TeamName}");

                        var matchDetail = await GetMatchAsync($"getmatchdata/{match.MatchID}").ConfigureAwait(false);

                       
                        if (match.MatchResults.Count() == 0)
                            return 1;

                        if (matchDetail.Group.GroupName == "1. Runde")
                            SaveImportDataToDatabase(match, matchDetail, matchDetail.Group.GroupID);

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
        private async void SaveImportDataToDatabase(LigaManagement.Models.Match match, MatchDetail matchdetail, string Runde)
        {
            try
            {
                if (match.MatchResults == null)
                    return;
                            

                SqlConnection conn = new SqlConnection(Globals.connstring);
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Pokalergebnisse ([Runde], [Saison],[SaisonID],[Verein1_Nr],[Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum],[Ort],[Schiedrichter],[Zuschauer])" +
                    " VALUES(@Runde,@Saison,@SaisonID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Zuschauer)";

                cmd.Parameters.AddWithValue("@Runde", 1);
                cmd.Parameters.AddWithValue("@SaisonID", 1);
                cmd.Parameters.AddWithValue("@Saison", "2023/24");
                //cmd.Parameters.AddWithValue("@StadionID", 0);
                
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
                cmd.Parameters.AddWithValue("@Ort", "k.A.");
                cmd.Parameters.AddWithValue("@Schiedrichter", "SR");
                //cmd.Parameters.AddWithValue("@Abgeschlossen", 1);
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

        public async Task RundeVor()
        {
            IsLoading = true;

            var currentIndex = RundeList.FindIndex(r => r.RundeKurzbezeichung == RundeChoosed);
            DisplayRunde next = currentIndex < RundeList.Count - 1 ? RundeList[currentIndex + 1] : null;
            if (next == null)
            {
                IsLoading = false;
                return;
            }

            Globals.currentPokalRunde = next?.RundeKurzbezeichung;
            RundeChoosed = next?.RundeKurzbezeichung;
            
                

            PokalergebnisseSpieltage = await PokalergebnisseService.GetPokalergebnisseSpieltag();

            if (PokalergebnisseSpieltage == null)
                return;

            PokalergebnisseSpieltage = PokalergebnisseSpieltage.ToList();
            PokalergebnisseSpieltage = PokalergebnisseSpieltage.Where(x => x.SaisonID == SaisonChoosed && x.Runde == RundeChoosed).OrderBy(x => x.Datum);

            VisibleBtnNew = NewButtonVisible();
                        
            OnClickHandler();

            IsLoading = false;

            StateHasChanged();

        }
        public void CellRender(DataGridCellRenderEventArgs<PokalergebnisSpieltag> args)
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

                Globals.CLPokalSaisonID = SaisonChoosed;

                var saison = await SaisonenService.GetSaison(Convert.ToInt32(SaisonChoosed));

                Globals.currentPokalSaison = saison.Saisonname;
                Globals.CLPokalSaisonID = saison.SaisonID;
                OnClickHandler();
                StateHasChanged();
            }
        }
        public async void RundeChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                RundeChoosed = e.Value.ToString();
                Globals.currentPokalRunde = RundeChoosed;

                PokalergebnisseSpieltage = await PokalergebnisseService.GetPokalergebnisseSpieltag();

                if (PokalergebnisseSpieltage == null)
                    return;

                PokalergebnisseSpieltage = PokalergebnisseSpieltage.ToList();
                PokalergebnisseSpieltage = PokalergebnisseSpieltage.Where(x => x.SaisonID == SaisonChoosed && x.Runde == RundeChoosed).OrderBy(x => x.Datum);

                VisibleBtnNew = NewButtonVisible();

                OnClickHandler();
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

                PokalergebnisseSpieltage = await PokalergebnisseService.GetPokalergebnisseSpieltag();
                            

                if (PokalergebnisseSpieltage == null)
                    return;

                PokalergebnisseSpieltage = PokalergebnisseSpieltage.Where(x => x.SaisonID == SaisonChoosed && x.Runde == RundeChoosed).OrderBy(x => x.Datum);
                
                VisibleBtnNew = NewButtonVisible();

                Globals.bVisibleNavMenuElements = true;
                StateHasChanged();
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        public string EntscheidungsSymbol(PokalergebnisSpieltag spiel)
        {
            if (spiel.Elfmeterschiessen == true)
                return "🎯";
            else if (spiel.Verlängerung == true)
                return "🕒";
            else
                return "";
        }
        public string EntscheidungsText(PokalergebnisSpieltag spiel)
        {
            if (spiel.Elfmeterschiessen == true)
                return "Entschieden im Elfmeterschießen";
            else if (spiel.Verlängerung == true)
                return "Entschieden in der Verlängerung";
            else
                return "";
        }

        private string NewButtonVisible()
        {
            string sButtonVisible = "hidden";

            if (RundeChoosed == "F" && PokalergebnisseSpieltage.Count() >= 1)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "HF" && PokalergebnisseSpieltage.Count() >= 2)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "VF" && PokalergebnisseSpieltage.Count() >= 4)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "AF" && PokalergebnisseSpieltage.Count() >= 8)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "2" && PokalergebnisseSpieltage.Count() >= 16)
                sButtonVisible = "hidden";
            else if (RundeChoosed == "1" && PokalergebnisseSpieltage.Count() >= 32)
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
    }
}



