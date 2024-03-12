using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace LigamanagerManagement.Web.Pages
{
    public class TabellenListBase : ComponentBase
    {
        public Int32 currentspieltag;
        public string saison;
        public string Liganame;

        protected string DisplayElements = "none";

        public List<DisplaySpieltag> SpieltagList;

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public List<DisplaySaison> SaisonenList;

        protected string selectedspieltagID;

        protected string SelectedspieltagID
        {
            get => selectedspieltagID;
            set { selectedspieltagID = value; }
        }

        [Inject]
        public ITabelleService TabelleService { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        public IEnumerable<Tabelle> Tabellen { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        int iMaxSpieltag = 0;
        private AppDbContext appDbContext;

        bool bAbgeschlossen;
        protected override async Task OnInitializedAsync()
        {
            int iSpieltage;
            try
            {
                SpieltagList = new List<DisplaySpieltag>();
                SaisonenList = new List<DisplaySaison>();

                if (Globals.currentSaison.Substring(0, 4) == "1963" || Globals.currentSaison.Substring(0, 4) == "1964")
                    iSpieltage = 30;
                else if (Globals.currentSaison.Substring(0, 4) == "1991")
                    iSpieltage = 38;
                else
                    iSpieltage = 34;

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                for (int i = 1; i <= iSpieltage; i++)
                {
                    SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
                }

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                SpieltagRepository rep = new SpieltagRepository(appDbContext);

                Globals.SaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).SaisonID;

                currentspieltag = rep.AktSpieltag(Globals.SaisonID);

                saison = Globals.currentSaison;
                
                Vereine = await VereineService.GetVereine();

                bool bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

                Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, SpieltagList.Count, Ligamanager.Components.Globals.currentSaison, 1);

                DateTime dt = await TabelleService.GetAktSpieltag(SpieltagService);

                DisplayElements = "none";

                var liga = await LigaService.GetLiga(Convert.ToInt32(Globals.currentLiga));

                Liganame = liga.Liganame;
            }
            catch (Exception ex)
            {

                Debug.Print(ex.Message);
            }
        }

        public async Task SaisonChange(ChangeEventArgs e)
        {
            int iSpieltage;

            if (e.Value != null)
            {
                saison = e.Value.ToString();
                Globals.currentSaison = saison;
                SpieltagList = new List<DisplaySpieltag>();
                SaisonenList = new List<DisplaySaison>();

                if (Globals.currentSaison.Substring(0, 4) == "1963" || Globals.currentSaison.Substring(0, 4) == "1964")
                    iSpieltage = 30;
                else if (Globals.currentSaison.Substring(0, 4) == "1991")
                    iSpieltage = 38;
                else
                    iSpieltage = 34;

                Globals.maxSpieltag = iSpieltage;

                Saisonen = (await SaisonenService.GetSaisonen()).ToList();
                for (int i = 1; i <= iSpieltage; i++)
                {
                    SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
                }

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }

                saison = Globals.currentSaison;
                currentspieltag = SpieltagList.Count;
                Vereine = await VereineService.GetVereine();
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
                Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, SpieltagList.Count, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);

                DisplayElements = "block";
                StateHasChanged();

            }
        }
        public async Task SpieltagChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                currentspieltag = Convert.ToInt32(e.Value);
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
                Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);
                DisplayElements = "block";               
                StateHasChanged();
            }
        }

        public async Task SpieltagZurueck()
        {
            if (currentspieltag > 1)
                currentspieltag = currentspieltag - 1;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
            Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);
            DisplayElements = "block";
            StateHasChanged();

        }

        public async Task SpieltagVor()
        {
            if (currentspieltag < Globals.maxSpieltag)
                currentspieltag = currentspieltag + 1;

            bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
            Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);
            DisplayElements = "block";
            StateHasChanged();
        }
        public async Task TabArtChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                int TabArt = Convert.ToInt32(e.Value);
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;
                if (TabArt == 1)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Gesamt);
                else if (TabArt == 2)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Heim);
                else if (TabArt == 3)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Auswärts);
                else if (TabArt == 4)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen, Vereine, 17, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Vorrunde);
                else if (TabArt == 5)
                    Tabellen = await TabelleService.BerechneTabelle(SpieltagService, bAbgeschlossen,Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.Rückrunde);
                else if (TabArt == 6)
                    Tabellen = await TabelleService.BerechneTabelleEwig(SpieltagService, Vereine, currentspieltag, Ligamanager.Components.Globals.currentSaison, (int)Globals.Tabart.EwigeTabelle);

                DisplayElements = "block";
                StateHasChanged();
            }
        }


        public class DisplaySpieltag
        {
            public DisplaySpieltag(string nummer, string name)
            {
                Nummer = nummer;
                Name = name;
            }
            public string Nummer { get; set; }
            public string Name { get; set; }

        }

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

