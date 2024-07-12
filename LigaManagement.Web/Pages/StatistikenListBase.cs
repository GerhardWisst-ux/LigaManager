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
using System.Diagnostics;
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

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<Statistiken> Localizer { get; set; }

        List<Tore> torelist;
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
                var authenticationState = await authenticationStateTask;

                if (authenticationState.User.Identity == null)
                {
                    return;
                }

                if (!authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/statistiken");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                }

               
                Vereine = (await VereineService.GetVereine()).ToList();

                VereineList = new List<DisplayVerein>();

                DisplayElements = "none";

                Spieltage = (await SpieltagService.GetSpieltage());

                var tore = await ToreService.GetTore();

                torelist = tore.ToList();

                await GetVereineList();

                TorjaegerList.Clear();

                await GetTorjaegerList();

                OnChange(1);
                OnChange(0);
                base.StateHasChanged();
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);

            }
        }

        public async Task<List<DisplayVerein>> GetVereineList()
        {
            for (int i = 0; i < Vereine.Count() - 1; i++)
            {

                if (Vereine[i].Bundesliga == true)
                    VereineList.Add(new DisplayVerein(Vereine[i].VereinNr.ToString(), Vereine[i].Vereinsname1, Vereine[i].Bundesliga));
            }
            return VereineList;
        }

            public async Task<List<Torjaeger>> GetTorjaegerList()
        {

            for (int i = 0; i < torelist.Count(); i++)
            {
                var kaderspieler = await KaderService.GetSpieler(torelist[i].SpielerID);

                var verein = await VereineService.GetVerein(kaderspieler.VereinID);
                Torjaeger torjaeger = new Torjaeger();

                var result = TorjaegerList.FindIndex(x => x.Id == torelist[i].SpielerID);

                if (result == -1)
                {
                    torjaeger.LigaID = Globals.currentLiga;
                    torjaeger.Id = torelist[i].SpielerID;
                    torjaeger.SaisonID = Globals.SaisonID;
                    torjaeger.Tore = 1;
                    torjaeger.Spielername = kaderspieler.SpielerName;
                    torjaeger.Vereinsname = verein.Vereinsname1;
                    TorjaegerList.Add(torjaeger);
                }
                else
                {
                    var found = TorjaegerList.Find(x => x.Id == torelist[i].SpielerID);

                    if (found != null)
                    {
                        found.Tore = found.Tore + 1;
                    }

                }
            }
            return TorjaegerList;
        }


        public string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        public async void Verein1Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == Localizer["Verein auswählen"].Value)
                    return;

                Spiel.Verein1_Nr = e.Value.ToString();
                int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein1_Nr);
                Spiel.Verein1 = VereineList[index].Vereinname1;

                Spielergebnisse = await TabelleService.VereinGegenVerein(SpieltagService, Spiel);
                StateHasChanged();
            }
        }

        public async void Verein2Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == Localizer["Verein auswählen"].Value)
                    return;

                Spiel.Verein2_Nr = e.Value.ToString();
                int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein2_Nr);
                Spiel.Verein2 = VereineList[index].Vereinname1;

                if (Spiel.Verein1_Nr.ToString() == "0")
                    return;

                Spielergebnisse = await TabelleService.VereinGegenVerein(SpieltagService, Spiel);

                var stat = await TabelleService.VereinGegenVereinSum(SpieltagService, Spiel);

                Statistik = String.Concat("Gewonnen:", stat.Gewonnen, " Unentschieden: ", stat.Unentschieden, " Verloren: ", stat.Verloren);

                DisplayElements = "block";

                StateHasChanged();
            }
        }

        public async void EinzelVereinChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == Localizer["Verein auswählen"].Value)
                    return;

                Spiel.Verein1_Nr = e.Value.ToString();
                int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein1_Nr);
                Spiel.Verein1 = VereineList[index].Vereinname1;

                if (Spiel.Verein1_Nr == null)
                    return;

                if (Spiel.Verein1_Nr.ToString() == "0")
                    return;

                Spielergebnisse = await TabelleService.StatistikVerein(SpieltagService, Spiel);

                var stat = await TabelleService.VereinSum(SpieltagService, Spiel);

                Statistik = String.Concat(Localizer["Gewonnen"].Value, stat.Gewonnen, " " + Localizer["Untentschieden"].Value + " ", stat.Unentschieden, " " + Localizer["Verloren"].Value + "", stat.Verloren);
                DisplayElements = "block";
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
        public string Title
        {
            get;
            set;
        }
        public string Link
        {
            get;
            set;
        }
    }
}

