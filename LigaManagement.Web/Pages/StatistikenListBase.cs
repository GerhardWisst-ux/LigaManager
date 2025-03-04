using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class StatistikenListBase : ComponentBase
    {
        [Parameter]
        public string SpieltagNr { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        public bool allowVirtualization;
        public int selectedIndex = 0;

        protected string DisplayElements = "none";
        protected string Statistik = "";

        public int VereinNr1;
        public int VereinNr2;

        public bool IsLoading = false;

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IKaderService KaderService { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IToreService ToreService { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();
        public Spielergebnisse Spiel { get; set; } = new Spielergebnisse();
        public List<Verein> Vereine { get; set; }
        public IEnumerable<Spielergebnisse> Spielergebnisse = new List<Spielergebnisse>();
        public IEnumerable<Spieltag> Spieltage { get; set; }
        public List<Torjaeger> TorjaegerList = new List<Torjaeger>();
        public List<ToreProSaison> ToreProSaisonList = new List<ToreProSaison>();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<Statistiken> Localizer { get; set; }

        private List<Tore> torelist;

        public void OnChange(int index)
        {
            if (index > 0)
            {
                StateHasChanged();
            }
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;
                var authenticationState = await authenticationStateTask;

                if (authenticationState.User.Identity == null || !authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/statistiken");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                    return;
                }

                await LoadDataAsync();

                IsLoading = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        private async Task LoadDataAsync()
        {
            Vereine = (await VereineService.GetVereine()).ToList();
            Spieltage = (await SpieltagService.GetSpieltage()).Where(x => x.LigaID == 1);
            torelist = (await ToreService.GetTore()).ToList();

            await Task.WhenAll(GetVereineList(), GetTorjaegerList(), GetToreProSaisonList());

            StateHasChanged();
        }

        public async Task<List<ToreProSaison>> GetToreProSaisonList()
        {
            try
            {
                ToreProSaisonList = await TabelleService.ToreProSaison();
                return ToreProSaisonList;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<List<DisplayVerein>> GetVereineList()
        {
            VereineList = Vereine
                .Where(v => v.Bundesliga)
                .Select(v => new DisplayVerein(v.VereinNr.ToString(), v.Vereinsname1, v.Bundesliga))
                .ToList();
            return VereineList;
        }

        public async Task<List<Torjaeger>> GetTorjaegerList()
        {
            IsLoading = true;
            var tasks = torelist.Select(async tor =>
            {
                var kaderspieler = await KaderService.GetSpieler(tor.SpielerID);
                var verein = await VereineService.GetVerein(kaderspieler.VereinID);
                var torjaeger = TorjaegerList.FirstOrDefault(t => t.Id == tor.SpielerID) ?? new Torjaeger
                {
                    LigaID = Globals.currentLiga,
                    Id = tor.SpielerID,
                    SaisonID = Globals.SaisonID,
                    Tore = 0,
                    Spielername = kaderspieler.SpielerName,
                    Vereinsname = verein.Vereinsname1
                };

                torjaeger.Tore++;
                if (!TorjaegerList.Contains(torjaeger))
                {
                    TorjaegerList.Add(torjaeger);
                }
            });

            await Task.WhenAll(tasks);
            IsLoading = false;
            return TorjaegerList;
        }

        public async void Verein1Change(ChangeEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != Localizer["Verein auswählen"].Value)
            {
                IsLoading = true;
                Spiel.Verein1_Nr = e.Value.ToString();
                int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein1_Nr);
                Spiel.Verein1 = VereineList[index].Vereinname1;

                Spielergebnisse = await TabelleService.VereinGegenVerein(SpieltagService, Spiel);
                IsLoading = false;
                StateHasChanged();
            }
        }

        public async void Verein2Change(ChangeEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != Localizer["Verein auswählen"].Value)
            {
                IsLoading = true;
                Spiel.Verein2_Nr = e.Value.ToString();
                int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein2_Nr);
                Spiel.Verein2 = VereineList[index].Vereinname1;

                if (Spiel.Verein1_Nr.ToString() != "0")
                {
                    Spielergebnisse = await TabelleService.VereinGegenVerein(SpieltagService, Spiel);
                    var stat = await TabelleService.VereinGegenVereinSum(SpieltagService, Spiel);

                    Statistik = $"{Localizer["Gewonnen"].Value}: {stat.Gewonnen}, {Localizer["Untentschieden"].Value}: {stat.Unentschieden}, {Localizer["Verloren"].Value}: {stat.Verloren}";
                    DisplayElements = "block";
                }

                IsLoading = false;
                StateHasChanged();
            }
        }

        public async void EinzelVereinChange(ChangeEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != Localizer["Verein auswählen"].Value)
            {
                IsLoading = true;
                Spiel.Verein1_Nr = e.Value.ToString();
                int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein1_Nr);
                Spiel.Verein1 = VereineList[index].Vereinname1;

                if (Spiel.Verein1_Nr != null && Spiel.Verein1_Nr.ToString() != "0")
                {
                    Spielergebnisse = await TabelleService.StatistikVerein(SpieltagService, Spiel);
                    var stat = await TabelleService.VereinSum(SpieltagService, Spiel);

                    Statistik = $"{Localizer["Gewonnen"].Value}: {stat.Gewonnen}, {Localizer["Untentschieden"].Value}: {stat.Unentschieden}, {Localizer["Verloren"].Value}: {stat.Verloren}";
                    DisplayElements = "block";
                }

                IsLoading = false;
                StateHasChanged();
            }
        }

        [Bind]
        public class DisplayTorjaeger
        {
            public DisplayTorjaeger(int spielerid, string spielername, int? tore)
            {
                Spielerid = spielerid;
                Spielername = spielername;
                Tore = tore;
            }
            public int Spielerid { get; set; }
            public string Spielername { get; set; }
            public int? Tore { get; set; }
        }

        [Bind]
        public class DisplayVerein
        {
            public DisplayVerein(string vereinID, string vereinname, bool bundesliga)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
                Bundesliga = bundesliga;
            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }
            public bool Bundesliga { get; set; }
        }
    }

    public class FeedItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
