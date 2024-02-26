using AutoMapper;
using LigaManagement.Models;
using LigaManagement.Web.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using LigaManagerManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LigamanagerManagement.Web.Pages
{
    public class EditSpieltagBase : ComponentBase
    {
        public Int32 currentspieltag = Globals.Spieltag;

        public string Vereinsname1;

        public string Vereinsname2;

        public string Spielername;

        public DateTime? Time { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ISpielerSpieltagService SpielerSpieltagService { get; set; }
        

        [Inject]
        public IKaderService KaderService { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();

        public List<DisplaySpieler> KaderList1 = new List<DisplaySpieler>();
        public List<DisplaySpieler> KaderList2 = new List<DisplaySpieler>();
        public List<DisplaySpieler> SpielerList1 = new List<DisplaySpieler>();
        public List<DisplaySpieler> SpielerList2 = new List<DisplaySpieler>();

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


        public bool Collapsed = true;

        protected async override Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            List<Spieltag> spiele2;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/editSpieltag/{Id}");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            if (Id != null)
                Spiel = await SpieltagService.GetSpieltag(Convert.ToInt32(Id));

            var spiele = await SpieltagService.GetSpieltage();

            if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
            {
                var vereineSaison = await VereineService.GetVereineSaison(Globals.currentSaison);

                spiele2 = spiele.OrderBy(y => y.SpieltagNr).Where(x => x.Saison == Globals.currentSaison).Where(y => y.SpieltagNr.ToString() == "1").Take(8).ToList();
            }
            else if (Globals.currentSaison == "1991/92")
            {
                spiele2 = spiele.OrderBy(y => y.SpieltagNr).Where(x => x.Saison == Globals.currentSaison).Where(y => y.SpieltagNr.ToString() == "1").Take(10).ToList();
            }
            else
            {
                spiele2 = spiele.OrderBy(y => y.SpieltagNr).Where(x => x.Saison == Globals.currentSaison).Where(y => y.SpieltagNr.ToString() == "1").Take(9).ToList();
            }

            Vereine = (await VereineService.GetVereine()).ToList();
            VereineList = new List<DisplayVerein>();

            int iAnzahl = spiele2.Count() * 2;

            for (int i = 0; i < spiele2.Count(); i++)
            {
                VereineList.Add(new DisplayVerein(spiele2[i].Verein1_Nr, spiele2[i].Verein1));
                VereineList.Add(new DisplayVerein(spiele2[i].Verein2_Nr, spiele2[i].Verein2));
            }

            if (Id == null)
            {
                Vereinsname1 = Spiel.Verein1;
                Vereinsname2 = Spiel.Verein2;
            }
            else
            {
                Vereinsname1 = null;
                Vereinsname2 = null;
            }

            List<Kader> SpielerSpiel = (await KaderService.GetAllSpieler()).Where(x => x.VereinID == Convert.ToInt32(Spiel.Verein1_Nr)).ToList();

            KaderList1 = new List<DisplaySpieler>();

            for (int i = 0; i < SpielerSpiel.Count(); i++)
            {
                KaderList1.Add(new DisplaySpieler(SpielerSpiel[i].Id, (SpielerSpiel[i].SpielerName + ", " + SpielerSpiel[i].Vorname)));
            }

            KaderList2 = new List<DisplaySpieler>();

            SpielerSpiel = (await KaderService.GetAllSpieler()).Where(x => x.VereinID == Convert.ToInt32(Spiel.Verein2_Nr)).ToList();
            for (int i = 0; i < SpielerSpiel.Count(); i++)
            {
                KaderList2.Add(new DisplaySpieler(SpielerSpiel[i].Id, (SpielerSpiel[i].SpielerName + ", " + SpielerSpiel[i].Vorname)));
            }

            SpieltagNr = Globals.Spieltag.ToString();

            if (Id == null)
                Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, 15, 30, 0, DateTimeKind.Utc);
            else
                Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, Spiel.Datum.Hour, Spiel.Datum.Minute, 0, DateTimeKind.Utc);
        }

        public async void SpeichernSpieler()
        {
            for (int i = 0; i < SpielerList1.Count(); i++)
            {
                SpielerSpieltag spielerspieltag = new SpielerSpieltag();
                spielerspieltag.KaderId = SpielerList1[i].SpielerID;
                spielerspieltag.SaisonId = Globals.SaisonID;
                spielerspieltag.SpieltagId = Globals.Spieltag;
                spielerspieltag.Tore = 0;
                spielerspieltag.Einsatz = 1;
                spielerspieltag.Tore = 0;
                spielerspieltag.Spielminuten = 90;
                spielerspieltag.Eingewechselt = false;
                spielerspieltag.Ausgewechselt = false;
                var erg = SpielerSpieltagService.CreateSpieler(spielerspieltag);
            }

            for (int i = 0; i < SpielerList2.Count(); i++)
            {
                SpielerSpieltag spielerspieltag = new SpielerSpieltag();
                spielerspieltag.KaderId = SpielerList2[i].SpielerID;
                spielerspieltag.SaisonId = Globals.SaisonID;
                spielerspieltag.SpieltagId = Globals.Spieltag;
                spielerspieltag.Tore = 0;
                spielerspieltag.Einsatz = 1;
                spielerspieltag.Tore = 0;
                spielerspieltag.Spielminuten = 90;
                spielerspieltag.Eingewechselt = false;
                spielerspieltag.Ausgewechselt = false;
                SpielerSpieltag spieler = await SpielerSpieltagService.CreateSpieler(spielerspieltag);
            }
        }

        public void Change(object value, string name, string action)
        {
            Console.WriteLine($"{name} item with index {value} {action}");
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

        public async void KaderChange1(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                SpielerList1.Add(new DisplaySpieler(Convert.ToInt32(e.Value), Spieler.SpielerName + ", " + Spieler.Vorname)); ;

                //SpielerList1.Verein2_Nr = e.Value.ToString();
                //int index = VereineList.FindIndex(x => x.VereinID == Spiel.Verein2_Nr);
                //Spiel.Verein2 = VereineList[index].Vereinname1;
            }
            StateHasChanged();
        }

        public async void KaderChange2(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                SpielerList2.Add(new DisplaySpieler(Convert.ToInt32(e.Value), Spieler.SpielerName + ", " + Spieler.Vorname)); ;
            }
            StateHasChanged();
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

        [Bind]
        public class DisplaySpieler
        {
            public DisplaySpieler(int spielerID, string spielername)
            {
                SpielerID = spielerID;
                Spielername = spielername;
            }
            public int SpielerID { get; set; }
            public string Spielername { get; set; }
        }

        protected Ligamanager.Components.ConfirmBase DeleteConfirmation { get; set; }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }
    }
}
