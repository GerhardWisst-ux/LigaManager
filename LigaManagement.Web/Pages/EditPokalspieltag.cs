﻿using AutoMapper;
using LigaManagement.Api.Migrations;
using LigaManagement.Models;
using LigaManagement.Web.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
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
    public class EditPokalspieltagBase : ComponentBase
    {
        public bool allowVirtualization;
        public Int32 currentspieltag = Globals.Spieltag;
        protected string DisplayErrorRunde = "none";
        public string Vereinsname1;

        public string Vereinsname2;

        public string Stadion;

        public string Spielername;
        
        public string RundeChoosed;

        public DateTime? Time { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public IPokalergebnisseService PokalergebnisseService { get; set; }

        [Inject]
        public IToreService ToreService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        [Inject]
        public ISpielerSpieltagService SpielerSpieltagService { get; set; }


        [Inject]
        public IKaderService KaderService { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();

        public List<DisplaySpieler> KaderList1 = new List<DisplaySpieler>();
        public List<DisplaySpieler> KaderList2 = new List<DisplaySpieler>();
        public List<DisplaySpieler> SpielerList1 = new List<DisplaySpieler>();
        public List<DisplaySpieler> SpielerList2 = new List<DisplaySpieler>();

        public List<DisplayTore> ToreList = new List<DisplayTore>();
                
        public IEnumerable<Spieltag> spieltage { get; set; }

        public EditSpieltagModel EditSpieltagModel { get; set; } =
            new EditSpieltagModel();

        public PokalergebnisSpieltag Spiel { get; set; } = new PokalergebnisSpieltag();

        public Tore Tor { get; set; } = new Tore();

        public PokalergebnisSpieltag SpielCombo { get; set; } = new PokalergebnisSpieltag();

        public IEnumerable<Verein> Vereine { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Runde { get; set; }

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
                Spiel = await PokalergebnisseService.GetPokalergebnisSpieltag(Convert.ToInt32(Id));

            var spiele = await PokalergebnisseService.GetPokalergebnisseSpieltag();

            var saison = (await SaisonenService.GetSaisonen()).ToList().Where(x => x.Saisonname == Globals.currentSaison).First();
                        
            var vereineSaison = await VereineService.GetVereine();
            List<Verein> verList = vereineSaison.ToList();

            for (int i = 0; i < verList.Count(); i++)
            {
                var verein = await VereineService.GetVerein(verList[i].VereinNr);
                VereineList.Add(new DisplayVerein(verList[i].VereinNr.ToString(), verein.Vereinsname1, verein.Stadion));
            }

            var allkader = (await KaderService.GetAllSpieler());
            List<Kader> SpielerSpiel = allkader.Where(x => x.VereinID == Convert.ToInt32(Spiel.Verein1_Nr)).ToList();

            KaderList1 = new List<DisplaySpieler>();

            for (int i = 0; i < SpielerSpiel.Count(); i++)
            {
                KaderList1.Add(new DisplaySpieler(SpielerSpiel[i].Id, (SpielerSpiel[i].SpielerName + ", " + SpielerSpiel[i].Vorname)));
            }

            KaderList2 = new List<DisplaySpieler>();

            SpielerSpiel = allkader.Where(x => x.VereinID == Convert.ToInt32(Spiel.Verein2_Nr)).ToList();
            for (int i = 0; i < SpielerSpiel.Count(); i++)
            {
                KaderList2.Add(new DisplaySpieler(SpielerSpiel[i].Id, (SpielerSpiel[i].SpielerName + ", " + SpielerSpiel[i].Vorname)));
            }
            
            if (Id == null)
                Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, 15, 30, 0, DateTimeKind.Utc);
            else
                Time = new DateTime(Spiel.Datum.Year, Spiel.Datum.Month, Spiel.Datum.Day, Spiel.Datum.Hour, Spiel.Datum.Minute, 0, DateTimeKind.Utc);

            Spiel.Saison = Globals.currentSaison;
            Spiel.SaisonID = Globals.SaisonID;
            //Spiel.SpieltagNr = SpieltagNr;

            var tore = await ToreService.GetTore();

            List<Tore> torlist = tore.ToList();

            try
            {
                for (int i = 0; i < torlist.Count(); i++)
                {
                    var kaderspieler = await KaderService.GetSpieler(torlist[i].SpielerID);
                    if (Spiel.SpieltagId == torlist[i].SpieltagId)
                    {
                        if (kaderspieler.Vorname == "")
                            ToreList.Add(new DisplayTore(torlist[i].SpielerID, kaderspieler.SpielerName, torlist[i].Spielstand, torlist[i].Spielminute,
                            Convert.ToInt32(torlist[i].SpieltagId), torlist[i].Eigentor));
                        else
                            ToreList.Add(new DisplayTore(torlist[i].SpielerID, kaderspieler.SpielerName + ", " + kaderspieler.Vorname, torlist[i].Spielstand, torlist[i].Spielminute,
                            Convert.ToInt32(torlist[i].SpieltagId), torlist[i].Eigentor));
                    }
                        
                }
            }
            catch (Exception ex )
            {

                throw ex;
            }
                       
        }
        protected async override void OnAfterRender(bool firstRender)
        {

            //if (Id != null)
            //{
            //    SpielCombo = await SpieltagService.GetSpieltag(Convert.ToInt32(Id));
            //    Vereinsname1 = SpielCombo.Verein1;
            //    Vereinsname2 = SpielCombo.Verein2;
            //    Stadion = SpielCombo.Ort;
            //    //VereineList.Add(new DisplayVerein("0", "Verein wählen", ""));
            //}
            //else
            //{
            //    Vereinsname1 = null;
            //    Vereinsname2 = null;
            //}
        }

        public void Change(object value, string name, string action)
        {
            Console.WriteLine($"{name} item with index {value} {action}");
        }

        public async void Verein1Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var verein = await VereineService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                Spiel.Verein1 = verein.Vereinsname1;
                Spiel.Verein1_Nr = int.Parse(e.Value.ToString());
            }
            StateHasChanged();
        }

        public async void Verein2Change(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var verein = await VereineService.GetVerein(Convert.ToInt32(e.Value.ToString()));
                Spiel.Verein2 = verein.Vereinsname1;
                Spiel.Verein2_Nr = int.Parse(e.Value.ToString());
            }
            StateHasChanged();
        }
        public void StadionChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                int index = VereineList.FindIndex(x => x.VereinID == e.Value.ToString());
                Spiel.Ort = VereineList[index].Ort;
            }
            StateHasChanged();
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
       
        public async void ToreChange1(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                Tor.SpielerID = Spieler.Id;
            }
            StateHasChanged();
        }

        public async void btnSpeichernTorV2_Click(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                Tor.SpielerID = Spieler.Id;
            }
            StateHasChanged();
        }

        public async void ToreChange2(ChangeEventArgs e)
        {
            if (e.Value != null)            {
                
                var Spieler = await KaderService.GetSpieler(Convert.ToInt32(e.Value));
                Tor.SpielerID = Spieler.Id;
            }
            StateHasChanged();
        }

        public async void RundeChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                RundeChoosed = e.Value.ToString();

                //if (RundeChoosed == "2")
                //    Titel = "Pokalspiel Neuanlage 2. Runde";
                //else if (RundeChoosed == "AF")
                //    Titel = "Pokalspiel Neuanlage Achtelfinale";
                //else if (RundeChoosed == "VF")
                //    Titel = "Pokalspiel Neuanlage Viertelfinale";
                //else if (RundeChoosed == "HF")
                //    Titel = "Pokalspiel Neuanlage Halbfinale";
                //else
                //    Titel = "Pokalspiel Neuanlage Finale";
            }
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
            public DisplayVerein(string vereinID, string vereinname, string ort)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
                Ort = ort;
            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }

            public string Ort { get; set; }
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

        public class DisplayTore
        {
            public DisplayTore(int spielerid, string spieler, string spielstand, int spielminute, int spieltagId, bool eigentor)
            {
                Spieler = spieler;
                Spielstand = spielstand;
                Spielerid = spielerid;
                Spielminute = spielminute;
                SpieltagId = spieltagId;
                Eigentor = eigentor;
            }
            public int Spielerid { get; set; }
            public int Spielminute { get; set; }
            public string Spieler { get; set; }
            public string Spielstand { get; set; }
            public int SpieltagId { get; set; }
            public bool Eigentor { get; set; }
        }


        protected Ligamanager.Components.ConfirmBase DeleteConfirmation { get; set; }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }
    }
}