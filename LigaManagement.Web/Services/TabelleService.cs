using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class TabelleService : ITabelleService
    {
        public IEnumerable<Spieltag> Spieltag { get; set; }

        public IEnumerable<Spielergebnisse> Spielergebnisse { get; set; }

        public IEnumerable<Verein> Verein { get; set; }

        private readonly HttpClient httpClient;

        public TabelleService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<Tabelle> CreateTabelle(Tabelle newTabelle)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTabelle(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tabelle> GetTabelle(int id)
        {
            return await httpClient.GetJsonAsync<Tabelle>($"api/Tabellen/{id}");
        }

        public async Task<IEnumerable<Tabelle>> GetTabellen()
        {
            return await httpClient.GetJsonAsync<Tabelle[]>("api/Tabellen");
        }

        public async Task<Tabelle> UpdateTabelle(Tabelle updatedTabelle)
        {
            return await httpClient.PutJsonAsync<Tabelle>("api/Tabellen", updatedTabelle);
        }

        public async Task<DateTime> GetAktSpieltag(ISpieltagService spieltagService)
        {
            var alleSpieltage = (await spieltagService.GetSpieltage());

            var Spieltag = (alleSpieltage).Where(st => st.Saison == Ligamanager.Components.Globals.currentSaison).OrderByDescending(o => o.SpieltagNr + 0).Take(1).ToList().ToArray();

            return Spieltag[0].Datum;
        }

        public async Task<IEnumerable<Tabelle>> BerechneTabelle(ISpieltagService spieltagService,
                                                IEnumerable<Verein> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;

            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int VonSpieltag = 1;
            int BisSpieltag = 34;  //Globals.MaxSpieltag(Globals.SaisonID);
            var alleSpieltage = (await spieltagService.GetSpieltage());

            if (Tabart == 4)
                BisSpieltag = 17;

            if (Tabart == 5)
            {
                VonSpieltag = 18;
                BisSpieltag = 34;
            }

            for (int i = VonSpieltag; i <= BisSpieltag; i++)
            {

                this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.SpieltagNr == i.ToString()).ToList();


                foreach (var item in this.Spieltag)
                {
                    int Saison = 0;

                    Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                    Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                    if (i == 1 || (Tabart == 5 && i == 18))
                    {

                        tabelleneintrag1 = new Tabelle();
                        tabelleneintrag2 = new Tabelle();
                        if (item.Tore1_Nr > item.Tore2_Nr)
                        {
                            tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                            tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;
                            tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                            tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                            tabelleneintrag1.Spiele = 1;

                            int.TryParse(item.Saison.Substring(0, 4), out Saison);

                            if (Saison > 1994)
                                tabelleneintrag1.Punkte = 3;
                            else
                                tabelleneintrag1.Punkte = 2;

                            tabelleneintrag1.Gewonnen = 1;
                            tabelleneintrag1.Untentschieden = 0;
                            tabelleneintrag1.Verloren = 0;

                            tabelleneintrag1.Platz = 0;
                            tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                            tabelleneintrag1.Liga = Globals.currentLiga;

                            tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                            tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;

                            tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                            tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                            tabelleneintrag2.Spiele = 1;
                            tabelleneintrag2.Punkte = 0;
                            tabelleneintrag2.Gewonnen = 0;
                            tabelleneintrag2.Untentschieden = 0;
                            tabelleneintrag2.Verloren = 1;

                            tabelleneintrag2.Platz = 0;
                            tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                            tabelleneintrag2.Liga = Globals.currentLiga;

                        }
                        else if (item.Tore1_Nr == item.Tore2_Nr)
                        {
                            tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                            tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;
                            tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                            tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                            tabelleneintrag1.Spiele = 1;
                            tabelleneintrag1.Punkte = 1;
                            tabelleneintrag1.Gewonnen = 0;
                            tabelleneintrag1.Untentschieden = 1;
                            tabelleneintrag1.Verloren = 0;
                            tabelleneintrag1.Platz = 0;
                            tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                            tabelleneintrag1.Liga = Globals.currentLiga;

                            tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                            tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;

                            tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                            tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                            tabelleneintrag2.Spiele = 1;
                            tabelleneintrag2.Punkte = 1;
                            tabelleneintrag2.Gewonnen = 0;
                            tabelleneintrag2.Untentschieden = 1;
                            tabelleneintrag2.Verloren = 0;

                            tabelleneintrag2.Platz = 0;
                            tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                            tabelleneintrag2.Liga = Globals.currentLiga;
                        }
                        else if (item.Tore1_Nr < item.Tore2_Nr)
                        {
                            tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                            tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;

                            tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                            tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                            tabelleneintrag1.Spiele = 1;
                            tabelleneintrag1.Punkte = 0;
                            tabelleneintrag1.Gewonnen = 0;
                            tabelleneintrag1.Untentschieden = 0;
                            tabelleneintrag1.Verloren = 1;

                            tabelleneintrag1.Platz = 0;
                            tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                            tabelleneintrag1.Liga = Globals.currentLiga;

                            tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                            tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;
                            tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                            tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                            tabelleneintrag2.Spiele = 1;

                            int.TryParse(item.Saison.Substring(0, 4), out Saison);

                            if (Saison > 1994)
                                tabelleneintrag2.Punkte = 3;
                            else
                                tabelleneintrag2.Punkte = 2;

                            tabelleneintrag2.Gewonnen = 1;
                            tabelleneintrag2.Untentschieden = 0;
                            tabelleneintrag2.Verloren = 0;
                            tabelleneintrag2.Platz = 0;
                            tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                            tabelleneintrag2.Liga = Globals.currentLiga;
                        }
                        paarung++;

                        if (Tabart == 3)
                        {
                            tabelleneintrag1.Spiele = 0;
                            tabelleneintrag1.TorePlus = 0;
                            tabelleneintrag1.ToreMinus = 0;
                            tabelleneintrag1.Gewonnen = 0;
                            tabelleneintrag1.Untentschieden = 0;
                            tabelleneintrag1.Verloren = 0;
                            tabelleneintrag1.Punkte = 0;
                        }
                        TabSaisonSorted.Add(tabelleneintrag1);
                        if (Tabart == 2)
                        {
                            tabelleneintrag2.Spiele = 0;
                            tabelleneintrag2.TorePlus = 0;
                            tabelleneintrag2.ToreMinus = 0;
                            tabelleneintrag2.Untentschieden = 0;
                            tabelleneintrag2.Verloren = 0;
                            tabelleneintrag2.Gewonnen = 0;
                            tabelleneintrag2.Punkte = 0;

                        }
                        TabSaisonSorted.Add(tabelleneintrag2);

                    }
                    else
                    {
                        if (Tabart == 5 && i <= 18)
                            continue;

                        tabelleneintrag1 = new Tabelle();
                        tabelleneintrag2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                tabelleneintrag1.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                tabelleneintrag1.Spiele = tabelleneintragF.Spiele + 1;
                                tabelleneintrag1.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                tabelleneintrag1.Untentschieden = tabelleneintragF.Untentschieden;
                                tabelleneintrag1.Verloren = tabelleneintragF.Verloren;
                                tabelleneintrag1.Punkte = tabelleneintragF.Punkte + 3;
                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                tabelleneintrag2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                tabelleneintrag2.Spiele = tabelleneintragF2.Spiele + 1;
                                tabelleneintrag2.Gewonnen = tabelleneintragF2.Gewonnen;
                                tabelleneintrag2.Untentschieden = tabelleneintragF2.Untentschieden;
                                tabelleneintrag2.Verloren = tabelleneintragF2.Verloren + 1;
                                tabelleneintrag2.Punkte = tabelleneintragF2.Punkte;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;

                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                tabelleneintrag1.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                tabelleneintrag1.Spiele = tabelleneintragF.Spiele + 1;
                                tabelleneintrag1.Gewonnen = tabelleneintragF.Gewonnen;
                                tabelleneintrag1.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                tabelleneintrag1.Verloren = tabelleneintragF.Verloren;
                                tabelleneintrag1.Punkte = tabelleneintragF.Punkte + 1;
                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                tabelleneintrag2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                tabelleneintrag2.Spiele = tabelleneintragF2.Spiele + 1;
                                tabelleneintrag2.Gewonnen = tabelleneintragF2.Gewonnen;
                                tabelleneintrag2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                tabelleneintrag2.Verloren = tabelleneintragF2.Verloren;
                                tabelleneintrag2.Punkte = tabelleneintragF2.Punkte + 1;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                tabelleneintrag1.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                tabelleneintrag1.Spiele = tabelleneintragF.Spiele + 1;
                                tabelleneintrag1.Gewonnen = tabelleneintragF.Gewonnen;
                                tabelleneintrag1.Untentschieden = tabelleneintragF.Untentschieden;
                                tabelleneintrag1.Verloren = tabelleneintragF.Verloren + 1;
                                tabelleneintrag1.Punkte = tabelleneintragF.Punkte;
                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                tabelleneintrag2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                tabelleneintrag2.Spiele = tabelleneintragF2.Spiele + 1;
                                tabelleneintrag2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                tabelleneintrag2.Untentschieden = tabelleneintragF2.Untentschieden;
                                tabelleneintrag2.Verloren = tabelleneintragF2.Verloren;
                                tabelleneintrag2.Punkte = tabelleneintragF2.Punkte + 3;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;
                            }

                            var item1 = TabSaisonSorted.Find(r => r.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                            var item2 = TabSaisonSorted.Find(r => r.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                            TabSaisonSorted.Remove(item1);
                            TabSaisonSorted.Remove(item2);

                            if (Tabart == 3)
                            {
                                tabelleneintrag1.Spiele = tabelleneintragF.Spiele;
                                tabelleneintrag1.TorePlus = tabelleneintragF.TorePlus;
                                tabelleneintrag1.ToreMinus = tabelleneintragF.ToreMinus;
                                tabelleneintrag1.Gewonnen = tabelleneintragF.Gewonnen;
                                tabelleneintrag1.Verloren = tabelleneintragF.Verloren;
                                tabelleneintrag1.Untentschieden = tabelleneintragF.Untentschieden;
                                tabelleneintrag1.Punkte = tabelleneintragF.Punkte;

                            }
                            TabSaisonSorted.Add(tabelleneintrag1);
                            if (Tabart == 2)
                            {
                                tabelleneintrag2.Spiele = tabelleneintragF2.Spiele;
                                tabelleneintrag2.TorePlus = tabelleneintragF2.TorePlus;
                                tabelleneintrag2.ToreMinus = tabelleneintragF2.ToreMinus;
                                tabelleneintrag2.Gewonnen = tabelleneintragF2.Gewonnen;
                                tabelleneintrag2.Verloren = tabelleneintragF2.Verloren;
                                tabelleneintrag2.Untentschieden = tabelleneintragF2.Untentschieden;
                                tabelleneintrag2.Punkte = tabelleneintragF2.Punkte;
                            }
                            TabSaisonSorted.Add(tabelleneintrag2);
                        }
                        else
                        {
                            Debug.Print("null");
                        }
                    }
                }

                TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.TorePlus - o.ToreMinus).OrderByDescending(o => o.Punkte).ToList();

                for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                {
                    TabSaisonSorted[ii].Platz = ii + 1;
                }
            }

            return TabSaisonSorted;
        }

        public async Task<IEnumerable<Spielergebnisse>> VereinGegenVerein(ISpieltagService spieltagService, Spielergebnisse spiel)
        {
            var TabSaisonSorted = new List<Spielergebnisse>();

            var alleSpieltage = (await spieltagService.GetSpielergebnisse());
            var Spielergebnisse = (alleSpieltage).Where(st => st.Verein1 == spiel.Verein1 && st.Verein2 == spiel.Verein2 || st.Verein1 == spiel.Verein2 && st.Verein2 == spiel.Verein1).ToList();



            return Spielergebnisse;
        }


        public async Task<IEnumerable<Tabelle>> BerechneTabelleEwig(ISpieltagService spieltagService,
                                                IEnumerable<Verein> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;

            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int VonSpieltag = 1;
            int BisSpieltag = 34;
            var alleSpieltage = (await spieltagService.GetSpieltage());

            if (Tabart == 4)
                BisSpieltag = 17;

            if (Tabart == 5)
            {
                VonSpieltag = 18;
                BisSpieltag = 34;
            }

            for (int j = 1; j <= 23; j++)
            {
                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {
                    this.Spieltag = (alleSpieltage).Where(st => st.SaisonID == j && st.SpieltagNr == i.ToString()).ToList();

                    foreach (var item in this.Spieltag)
                    {
                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        if (tabelleneintragF == null)
                        {
                            tabelleneintrag1 = new Tabelle();                            

                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag1.Spiele = 1;

                                tabelleneintrag1.Gewonnen = 1;
                                tabelleneintrag1.Untentschieden = 0;
                                tabelleneintrag1.Verloren = 0;

                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;                                

                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag1.Spiele = 1;
                                tabelleneintrag1.Punkte = 1;
                                tabelleneintrag1.Gewonnen = 0;
                                tabelleneintrag1.Untentschieden = 1;
                                tabelleneintrag1.Verloren = 0;
                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;                             
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;

                                tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag1.Spiele = 1;
                                tabelleneintrag1.Punkte = 0;
                                tabelleneintrag1.Gewonnen = 0;
                                tabelleneintrag1.Untentschieden = 0;
                                tabelleneintrag1.Verloren = 1;

                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;                               
                            }
                            paarung++;
                                                   
                            TabSaisonSorted.Add(tabelleneintrag1);
                        }

                        if (tabelleneintragF2 == null)
                        {
                            
                            tabelleneintrag2 = new Tabelle();

                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {                              

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;

                                tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag2.Spiele = 1;
                                tabelleneintrag2.Punkte = 0;
                                tabelleneintrag2.Gewonnen = 0;
                                tabelleneintrag2.Untentschieden = 0;
                                tabelleneintrag2.Verloren = 1;

                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;

                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {                               

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;

                                tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag2.Spiele = 1;
                                tabelleneintrag2.Punkte = 1;
                                tabelleneintrag2.Gewonnen = 0;
                                tabelleneintrag2.Untentschieden = 1;
                                tabelleneintrag2.Verloren = 0;

                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {                               

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag2.Spiele = 1;

                                tabelleneintrag2.Gewonnen = 1;
                                tabelleneintrag2.Untentschieden = 0;
                                tabelleneintrag2.Verloren = 0;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;
                            }
                            paarung++;
                                                      
                            TabSaisonSorted.Add(tabelleneintrag2);
                        }

                        tabelleneintrag1 = new Tabelle();
                        tabelleneintrag2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                tabelleneintrag1.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                tabelleneintrag1.Spiele = tabelleneintragF.Spiele + 1;
                                tabelleneintrag1.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                tabelleneintrag1.Untentschieden = tabelleneintragF.Untentschieden;
                                tabelleneintrag1.Verloren = tabelleneintragF.Verloren;
                                tabelleneintrag1.Punkte = tabelleneintragF.Punkte + 3;
                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                tabelleneintrag2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                tabelleneintrag2.Spiele = tabelleneintragF2.Spiele + 1;
                                tabelleneintrag2.Gewonnen = tabelleneintragF2.Gewonnen;
                                tabelleneintrag2.Untentschieden = tabelleneintragF2.Untentschieden;
                                tabelleneintrag2.Verloren = tabelleneintragF2.Verloren + 1;
                                tabelleneintrag2.Punkte = tabelleneintragF2.Punkte;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;

                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                tabelleneintrag1.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                tabelleneintrag1.Spiele = tabelleneintragF.Spiele + 1;
                                tabelleneintrag1.Gewonnen = tabelleneintragF.Gewonnen;
                                tabelleneintrag1.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                tabelleneintrag1.Verloren = tabelleneintragF.Verloren;
                                tabelleneintrag1.Punkte = tabelleneintragF.Punkte + 1;
                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                tabelleneintrag2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                tabelleneintrag2.Spiele = tabelleneintragF2.Spiele + 1;
                                tabelleneintrag2.Gewonnen = tabelleneintragF2.Gewonnen;
                                tabelleneintrag2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                tabelleneintrag2.Verloren = tabelleneintragF2.Verloren;
                                tabelleneintrag2.Punkte = tabelleneintragF2.Punkte + 1;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                tabelleneintrag1.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                tabelleneintrag1.Spiele = tabelleneintragF.Spiele + 1;
                                tabelleneintrag1.Gewonnen = tabelleneintragF.Gewonnen;
                                tabelleneintrag1.Untentschieden = tabelleneintragF.Untentschieden;
                                tabelleneintrag1.Verloren = tabelleneintragF.Verloren + 1;
                                tabelleneintrag1.Punkte = tabelleneintragF.Punkte;
                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                tabelleneintrag2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                tabelleneintrag2.Spiele = tabelleneintragF2.Spiele + 1;
                                tabelleneintrag2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                tabelleneintrag2.Untentschieden = tabelleneintragF2.Untentschieden;
                                tabelleneintrag2.Verloren = tabelleneintragF2.Verloren;
                                tabelleneintrag2.Punkte = tabelleneintragF2.Punkte + 3;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;
                            }

                            var item1 = TabSaisonSorted.Find(r => r.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                            var item2 = TabSaisonSorted.Find(r => r.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                            TabSaisonSorted.Remove(item1);
                            TabSaisonSorted.Remove(item2);

                            if (Tabart == 3)
                            {
                                tabelleneintrag1.Spiele = tabelleneintragF.Spiele;
                                tabelleneintrag1.TorePlus = tabelleneintragF.TorePlus;
                                tabelleneintrag1.ToreMinus = tabelleneintragF.ToreMinus;
                                tabelleneintrag1.Gewonnen = tabelleneintragF.Gewonnen;
                                tabelleneintrag1.Verloren = tabelleneintragF.Verloren;
                                tabelleneintrag1.Untentschieden = tabelleneintragF.Untentschieden;
                                tabelleneintrag1.Punkte = tabelleneintragF.Punkte;

                            }
                            TabSaisonSorted.Add(tabelleneintrag1);
                            if (Tabart == 2)
                            {
                                tabelleneintrag2.Spiele = tabelleneintragF2.Spiele;
                                tabelleneintrag2.TorePlus = tabelleneintragF2.TorePlus;
                                tabelleneintrag2.ToreMinus = tabelleneintragF2.ToreMinus;
                                tabelleneintrag2.Gewonnen = tabelleneintragF2.Gewonnen;
                                tabelleneintrag2.Verloren = tabelleneintragF2.Verloren;
                                tabelleneintrag2.Untentschieden = tabelleneintragF2.Untentschieden;
                                tabelleneintrag2.Punkte = tabelleneintragF2.Punkte;
                            }
                            TabSaisonSorted.Add(tabelleneintrag2);
                        }
                        else
                        {
                            Debug.Print("null");
                        }

                    }

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.TorePlus - o.ToreMinus).OrderByDescending(o => o.Punkte).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        TabSaisonSorted[ii].Platz = ii + 1;
                    }
                }
            }


            return TabSaisonSorted;
        }
    }
}
