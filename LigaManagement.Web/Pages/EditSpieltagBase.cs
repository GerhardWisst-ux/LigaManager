using AutoMapper;
using LigaManagement.Models;
using LigaManagement.Web.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LigamanagerManagement.Web.Pages
{
    public class EditSpieltagBase : ComponentBase
    {
        public Int32 currentspieltag = Globals.Spieltag;


        public int VereinNr1;

        public int VereinNr2;

        public DateTime? Time { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();

        public string PageHeaderText { get; set; }

        public IEnumerable<Spieltag> spieltage { get; set; }

        public EditSpieltagModel EditSpieltagModel { get; set; } =
            new EditSpieltagModel();

        public Spieltag Spiel { get; set; } = new Spieltag();

        public IEnumerable<Verein> Vereine { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string SpieltagNr { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected async override Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/editSpieltag/{Id}");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            if (Id != null)
                Spiel = await SpieltagService.GetSpieltag(Convert.ToInt32(Id));


            var spiele = await SpieltagService.GetSpieltage();
            List<Spieltag> spiele2 = spiele.Where(x => x.Saison == Globals.currentSaison && x.SpieltagNr == "1").Take(9).ToList();

            Vereine = (await VereineService.GetVereine()).ToList();
            VereineList = new List<DisplayVerein>();

            int iAnzahl = spiele2.Count() * 2;

            for (int i = 0; i < spiele2.Count(); i++)
            {
                VereineList.Add(new DisplayVerein(spiele2[i].Verein1_Nr, spiele2[i].Verein1));
                VereineList.Add(new DisplayVerein(spiele2[i].Verein2_Nr, spiele2[i].Verein2));
            }

            SpieltagNr = Globals.Spieltag.ToString();

            if (Id == null)
                Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, 15, 30, 0, DateTimeKind.Utc);
            else
                Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, Spiel.Datum.Hour, Spiel.Datum.Minute, 0, DateTimeKind.Utc);
        }

        public void Verein1Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Spiel.Verein1_Nr = e.Value.ToString();
                int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein1_Nr);
                Spiel.Verein1 = VereineList[index].Vereinname1;
            }
        }

        public void Verein2Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Spiel.Verein2_Nr = e.Value.ToString();
                int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein2_Nr);
                Spiel.Verein2 = VereineList[index].Vereinname1;
            }
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

        protected Ligamanager.Components.ConfirmBase DeleteConfirmation { get; set; }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }
    }
}
