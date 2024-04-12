using LigaManagement.Api.Migrations;
using LigaManagement.Models;
using LigaManagement.Web.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class StatistikenListBase : ComponentBase
    {
        public bool allowVirtualization;
        public Int32 currentspieltag = Globals.Spieltag;
        public int selectedIndex = 0;

        protected string DisplayElements = "none";

        protected string Statistik = "";

        public int VereinNr1;

        public int VereinNr2;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

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

        public string PageHeaderText { get; set; }

        public IEnumerable<Spieltag> spieltage { get; set; }

        public EditSpieltagModel EditSpieltagModel { get; set; } =
            new EditSpieltagModel();

        public Spielergebnisse Spiel { get; set; } = new Spielergebnisse();

        public List<Verein> Vereine { get; set; }

        [Parameter]
        public string SpieltagNr { get; set; }

        public IEnumerable<Spielergebnisse> Spielergebnisse = new List<Spielergebnisse>();

        public IEnumerable<Spieltag> Spieltage { get; set; }

        public List<Torjaeger> TorjaegerList = new List<Torjaeger>();

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public void OnChange(int index)
        {
            if (index > 0)
            {
                //Spielergebnisse = null;

                StateHasChanged();
            }
        }
        protected async override Task OnInitializedAsync()
        {
            List<Spieltag> spiele2;
            var authenticationState = await authenticationStateTask;

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/statistiken");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            var spiele = await SpieltagService.GetSpieltage();

            if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
            {
                spiele2 = spiele.Where(x => x.Saison == Globals.currentSaison).Where(y => y.SpieltagNr.ToString() == "1").Take(8).ToList();
            }
            else if (Globals.currentSaison == "1991/92")
            {
                spiele2 = spiele.Where(x => x.Saison == Globals.currentSaison).Where(y => y.SpieltagNr.ToString() == "1").Take(10).ToList();
            }
            else
            {
                spiele2 = spiele.Where(x => x.Saison == Globals.currentSaison).Where(y => y.SpieltagNr.ToString() == "1").Take(9).ToList();
            }


            Vereine = (await VereineService.GetVereine()).ToList();
            
            VereineList = new List<DisplayVerein>();

            
            for (int i = 0; i < Vereine.Count() - 1; i++)
            {
                VereineList.Add(new DisplayVerein(Vereine[i].VereinNr.ToString(), Vereine[i].Vereinsname1));                
            }

            DisplayElements = "none";

            Spieltage = (await SpieltagService.GetSpieltage());

            var tore = await ToreService.GetTore();

            List<Tore> torelist = tore.ToList();                        

            try
            {
                TorjaegerList.Clear();
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
            }
            catch (Exception ex)
            {

                throw ex;
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
            public DisplayVerein(string vereinID, string vereinname)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }
        }

        public async void Verein1Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == "Verein auswählen")
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
                if (e.Value.ToString() == "Verein auswählen")
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
                if (e.Value.ToString() == "Verein auswählen")
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

                Statistik = String.Concat("Gewonnen:", stat.Gewonnen, " Unentschieden: ", stat.Unentschieden, " Verloren: ", stat.Verloren);
                DisplayElements = "block";
                StateHasChanged();
            }
        }
    }
}
