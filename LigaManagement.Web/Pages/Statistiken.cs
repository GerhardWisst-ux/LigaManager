using AutoMapper;
using LigaManagement.Models;
using LigaManagement.Web.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class StatistikenListBase : ComponentBase
    {
        public Int32 currentspieltag = Globals.Spieltag;
                
        protected string DisplayElements = "none";

        protected string Statistik = "";

        public int VereinNr1;

        public int VereinNr2;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();

        public string PageHeaderText { get; set; }

        public IEnumerable<Spieltag> spieltage { get; set; }

        public EditSpieltagModel EditSpieltagModel { get; set; } =
            new EditSpieltagModel();

        public Spielergebnisse Spiel { get; set; } = new Spielergebnisse();

        public IEnumerable<Verein> Vereine { get; set; }
                
        [Parameter]
        public string SpieltagNr { get; set; }

        public IEnumerable<Spielergebnisse> Spielergebnisse = new List<Spielergebnisse>();

        public IEnumerable<Spieltag> Spieltage { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        
        protected async override Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/statistiken");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }                        

            var spiele = await SpieltagService.GetSpieltage();
            List<Spieltag> spiele2 = spiele.Where(x => x.Saison == Globals.currentSaison).Where(y => y.SpieltagNr.ToString() == "1").Take(9).ToList();

            Vereine = (await VereineService.GetVereine()).ToList();
            VereineList = new List<DisplayVerein>();

            int iAnzahl = spiele2.Count() * 2;

            for (int i = 0; i < spiele2.Count(); i++)
            {
                VereineList.Add(new DisplayVerein(spiele2[i].Verein1_Nr, spiele2[i].Verein1));
                VereineList.Add(new DisplayVerein(spiele2[i].Verein2_Nr, spiele2[i].Verein2));
            }

            DisplayElements = "none";

            Spieltage = (await SpieltagService.GetSpieltage());
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
    }
}
