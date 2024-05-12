using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using static LigaManagement.Web.Pages.ChartData;
using static Ligamanager.Components.Globals;

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

        public Task DeleteTabelle(int? id)
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
            try
            {
                var alleSpieltage = (await spieltagService.GetSpieltage());

                var Spieltag = (alleSpieltage).Where(st => st.Saison == Ligamanager.Components.Globals.currentSaison).OrderByDescending(o => o.SpieltagNr + 0).Take(1).ToList().ToArray();

                return Spieltag[0].Datum;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return DateTime.Now;
            }
        }


        public async Task<IEnumerable<Tabelle>> BerechneTabelle(ISpieltagService spieltagService,
                                                bool bAbgeschlossen,
                                                IEnumerable<Verein> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;
            int BisSpieltag;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int VonSpieltag = 1;

            DateTime dtGrenze_2_3 = DateTime.Parse("01/07/1995");


            try
            {
                if (bAbgeschlossen)
                    BisSpieltag = Spieltag;
                else
                {
                    if (Spieltag < rep.AktSpieltag(Globals.SaisonID, Globals.LigaID))
                        BisSpieltag = Spieltag;
                    else
                        BisSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                }


                var alleSpieltage = (await spieltagService.GetSpieltage());

                if (Tabart == 4)
                    BisSpieltag = 17;

                if (Tabart == 5)
                {
                    VonSpieltag = 18;

                    int iAktSpieltag = Globals.maxSpieltag;
                    BisSpieltag = iAktSpieltag;
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();


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

                                if (item.Datum < dtGrenze_2_3.Date)
                                    tabelleneintrag1.Punkte = 2;
                                else
                                    tabelleneintrag1.Punkte = 3;

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

                                if (item.Datum < dtGrenze_2_3.Date)
                                    tabelleneintrag2.Punkte = 2;
                                else
                                    tabelleneintrag2.Punkte = 3;

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

                                    if (item.Datum < dtGrenze_2_3.Date)
                                        tabelleneintrag1.Punkte = tabelleneintragF.Punkte + 2;
                                    else
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

                                    if (item.Datum < dtGrenze_2_3.Date)
                                        tabelleneintrag2.Punkte = tabelleneintragF2.Punkte + 2;
                                    else
                                        tabelleneintrag2.Punkte = tabelleneintragF2.Punkte + 3;

                                    tabelleneintrag2.Platz = 0;
                                    tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintrag2.Liga = Globals.currentLiga;
                                }

                                if (Globals.SaisonID == 44 && tabelleneintrag1.Verein == "Arminia Bielefeld")
                                {
                                    tabelleneintrag1.TorePlus = 0;
                                    tabelleneintrag1.Platz = 18;
                                    tabelleneintrag1.Punkte = 0;
                                    tabelleneintrag1.TorePlus = 0;
                                    tabelleneintrag1.ToreMinus = 0;

                                }

                                if (Globals.SaisonID == 44 && tabelleneintrag2.Verein == "Arminia Bielefeld")
                                {
                                    tabelleneintrag2.TorePlus = 0;
                                    tabelleneintrag2.Platz = 18;
                                    tabelleneintrag2.Punkte = 0;
                                    tabelleneintrag2.TorePlus = 0;
                                    tabelleneintrag2.ToreMinus = 0;
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

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        TabSaisonSorted[ii].Platz = ii + 1;
                        TabSaisonSorted[ii].Tore = TabSaisonSorted[ii].TorePlus + ":" + TabSaisonSorted[ii].ToreMinus;
                        TabSaisonSorted[ii].Diff = TabSaisonSorted[ii].TorePlus - TabSaisonSorted[ii].ToreMinus;
                    }
                }

                return TabSaisonSorted;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Spielergebnisse>> VereinGegenVerein(ISpieltagService spieltagService, Spielergebnisse spiel)
        {
            var TabSaisonSorted = new List<Spielergebnisse>();

            var alleSpieltage = (await spieltagService.GetSpielergebnisse());
            var Spielergebnisse = (alleSpieltage).Where(st => st.Verein1 == spiel.Verein1 && st.Verein2 == spiel.Verein2 || st.Verein1 == spiel.Verein2 && st.Verein2 == spiel.Verein1).ToList();



            return Spielergebnisse;
        }
        public async Task<Spielstatistik> VereinGegenVereinSum(ISpieltagService spieltagService, Spielergebnisse spiel)
        {
            try
            {
                int Gewonnen = 0, Unentschieden = 0, Verloren = 0;
                var TabSaisonSorted = new List<Spielergebnisse>();

                var alleSpieltage = (await spieltagService.GetSpielergebnisse());
                var Spielergebnisse = (alleSpieltage).Where(st => st.Verein1 == spiel.Verein1 && st.Verein2 == spiel.Verein2 || st.Verein1 == spiel.Verein2 && st.Verein2 == spiel.Verein1).ToList();

                int GewonnenH = (Spielergebnisse).Where(st => st.Tore1_Nr > st.Tore2_Nr && st.Verein1 == spiel.Verein1 && st.Verein2 == spiel.Verein2).ToList().Count();
                int UnentschiedenH = (Spielergebnisse).Where(st => st.Tore1_Nr == st.Tore2_Nr && st.Verein1 == spiel.Verein1 && st.Verein2 == spiel.Verein2).ToList().Count();
                int VerlorenH = (Spielergebnisse).Where(st => st.Tore1_Nr < st.Tore2_Nr && st.Verein1 == spiel.Verein1 && st.Verein2 == spiel.Verein2).ToList().Count();

                int GewonnenA = (Spielergebnisse).Where(st => st.Tore2_Nr > st.Tore1_Nr && st.Verein1 == spiel.Verein2 && st.Verein2 == spiel.Verein1).ToList().Count();
                int UnentschiedenA = (Spielergebnisse).Where(st => st.Tore2_Nr == st.Tore1_Nr && st.Verein1 == spiel.Verein2 && st.Verein2 == spiel.Verein1).ToList().Count();
                int VerlorenA = (Spielergebnisse).Where(st => st.Tore2_Nr < st.Tore1_Nr && st.Verein1 == spiel.Verein2 && st.Verein2 == spiel.Verein1).ToList().Count();

                Gewonnen = GewonnenH + GewonnenA;
                Unentschieden = UnentschiedenH + UnentschiedenA;
                Verloren = VerlorenH + VerlorenA;

                Spielstatistik stat = new Spielstatistik();
                stat.Gewonnen = Gewonnen;
                stat.Unentschieden = Unentschieden;
                stat.Verloren = Verloren;

                return stat;

            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Tabelle>> BerechneTabelleEwig(ISpieltagService spieltagService, ISaisonenService saisonenService,
                                                IEnumerable<Verein> Vereine,
                                                int iSpieltag,
                                                string sSaison,
                                                int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonEwigSorted = new List<Tabelle>();
            int VonSpieltag = 1;
            int BisSpieltag = 34;

            try
            {

                foreach (var item in Vereine)
                {
                    tabelleneintrag1 = new Tabelle();

                    tabelleneintrag1.VereinNr = item.VereinNr;
                    tabelleneintrag1.Verein = item.Vereinsname1;
                    tabelleneintrag1.TorePlus = 0;
                    tabelleneintrag1.ToreMinus = 0;
                    tabelleneintrag1.Spiele = 0;

                    tabelleneintrag1.Punkte = 0;

                    tabelleneintrag1.Gewonnen = 0;
                    tabelleneintrag1.Untentschieden = 0;
                    tabelleneintrag1.Verloren = 0;

                    tabelleneintrag1.Platz = 0;
                    tabelleneintrag1.Tab_Sai_Id = 999; //saisonnen[j].SaisonID;
                    tabelleneintrag1.Liga = Globals.currentLiga;

                    TabSaisonEwigSorted.Add(tabelleneintrag1);

                    tabelleneintrag2 = new Tabelle();

                    tabelleneintrag2.VereinNr = item.VereinNr;
                    tabelleneintrag2.Verein = item.Vereinsname1;
                    tabelleneintrag2.TorePlus = 0;
                    tabelleneintrag2.ToreMinus = 0;
                    tabelleneintrag2.Spiele = 0;

                    tabelleneintrag2.Punkte = 0;

                    tabelleneintrag2.Gewonnen = 0;
                    tabelleneintrag2.Untentschieden = 0;
                    tabelleneintrag2.Verloren = 0;

                    tabelleneintrag2.Platz = 0;
                    tabelleneintrag2.Tab_Sai_Id = 999; //saisonnen[j].SaisonID;
                    tabelleneintrag2.Liga = Globals.currentLiga;
                }


                var alleSpieltage = (await spieltagService.GetSpieltage());
                var saisonnen = (await saisonenService.GetSaisonen()).OrderBy(x => x.Saisonname).ToList();

                for (int j = 0; j < saisonnen.Count(); j++)
                {
                    if (saisonnen[j].Saisonname == "1963/64" || saisonnen[j].Saisonname == "1964/65")
                        BisSpieltag = 30;
                    else if (saisonnen[j].Saisonname == "1991/92")
                        BisSpieltag = 38;
                    else
                        BisSpieltag = 34;



                    if (saisonnen[j].Aktuell)
                    {
                        //if (Spieltag < rep.AktSpieltag(Globals.SaisonID))
                        //    BisSpieltag = Spieltag;
                        //else
                        //    BisSpieltag = rep.AktSpieltag(Globals.SaisonID);
                        BisSpieltag = iSpieltag;
                    }


                    for (int i = VonSpieltag; i <= BisSpieltag; i++)
                    {

                        this.Spieltag = (alleSpieltage).Where(st => st.Saison == saisonnen[j].Saisonname && st.SpieltagNr == i.ToString()).ToList();

                        foreach (var item in this.Spieltag)
                        {
                            tabelleneintrag1 = TabSaisonEwigSorted.Find(x => x.VereinNr == Convert.ToInt32(item.Verein1_Nr));

                            tabelleneintrag2 = TabSaisonEwigSorted.Find(x => x.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                            //if (saisonnen[j].Saisonname == "1971/72" && tabelleneintrag1.Verein == "Arminia Bielefeld")
                            //    continue;

                            //if (saisonnen[j].Saisonname == "1971/72" && tabelleneintrag2.Verein == "Arminia Bielefeld")
                            //    continue;

                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag1.VereinNr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = tabelleneintrag1.TorePlus + item.Tore1_Nr;
                                tabelleneintrag1.ToreMinus = tabelleneintrag1.ToreMinus + item.Tore2_Nr;
                                tabelleneintrag1.Spiele = tabelleneintrag1.Spiele + 1;
                                tabelleneintrag1.Gewonnen = tabelleneintrag1.Gewonnen + 1;
                                tabelleneintrag1.Untentschieden = tabelleneintrag1.Untentschieden;
                                tabelleneintrag1.Verloren = tabelleneintrag1.Verloren;


                                tabelleneintrag1.Punkte = tabelleneintrag1.Punkte + 3;

                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = 99;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag2.VereinNr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = tabelleneintrag2.TorePlus + item.Tore2_Nr;
                                tabelleneintrag2.ToreMinus = tabelleneintrag2.ToreMinus + item.Tore1_Nr;
                                tabelleneintrag2.Spiele = tabelleneintrag2.Spiele + 1;
                                tabelleneintrag2.Gewonnen = tabelleneintrag2.Gewonnen;
                                tabelleneintrag2.Untentschieden = tabelleneintrag2.Untentschieden;
                                tabelleneintrag2.Verloren = tabelleneintrag2.Verloren + 1;
                                tabelleneintrag2.Punkte = tabelleneintrag2.Punkte;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = 99;
                                tabelleneintrag2.Liga = Globals.currentLiga;

                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = tabelleneintrag1.VereinNr;
                                tabelleneintrag1.Verein = tabelleneintrag1.Verein;
                                tabelleneintrag1.TorePlus = tabelleneintrag1.TorePlus + item.Tore1_Nr;
                                tabelleneintrag1.ToreMinus = tabelleneintrag1.ToreMinus + item.Tore2_Nr;
                                tabelleneintrag1.Spiele = tabelleneintrag1.Spiele + 1;
                                tabelleneintrag1.Gewonnen = tabelleneintrag1.Gewonnen;
                                tabelleneintrag1.Untentschieden = tabelleneintrag1.Untentschieden + 1;
                                tabelleneintrag1.Verloren = tabelleneintrag1.Verloren;
                                tabelleneintrag1.Punkte = tabelleneintrag1.Punkte + 1;
                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = tabelleneintrag2.VereinNr;
                                tabelleneintrag2.Verein = tabelleneintrag2.Verein;

                                tabelleneintrag2.TorePlus = tabelleneintrag2.TorePlus + item.Tore2_Nr;
                                tabelleneintrag2.ToreMinus = tabelleneintrag2.ToreMinus + item.Tore1_Nr;
                                tabelleneintrag2.Spiele = tabelleneintrag2.Spiele + 1;
                                tabelleneintrag2.Gewonnen = tabelleneintrag2.Gewonnen;
                                tabelleneintrag2.Untentschieden = tabelleneintrag2.Untentschieden + 1;
                                tabelleneintrag2.Verloren = tabelleneintrag2.Verloren;
                                tabelleneintrag2.Punkte = tabelleneintrag2.Punkte + 1;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                tabelleneintrag1.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                tabelleneintrag1.Verein = tabelleneintrag1.Verein;
                                tabelleneintrag1.TorePlus = tabelleneintrag1.TorePlus + item.Tore1_Nr;
                                tabelleneintrag1.ToreMinus = tabelleneintrag1.ToreMinus + item.Tore2_Nr;
                                tabelleneintrag1.Spiele = tabelleneintrag1.Spiele + 1;
                                tabelleneintrag1.Gewonnen = tabelleneintrag1.Gewonnen;
                                tabelleneintrag1.Untentschieden = tabelleneintrag1.Untentschieden;
                                tabelleneintrag1.Verloren = tabelleneintrag1.Verloren + 1;
                                tabelleneintrag1.Punkte = tabelleneintrag1.Punkte;
                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = tabelleneintrag2.Verein;
                                tabelleneintrag2.TorePlus = tabelleneintrag2.TorePlus + item.Tore2_Nr;
                                tabelleneintrag2.ToreMinus = tabelleneintrag2.ToreMinus + item.Tore1_Nr;
                                tabelleneintrag2.Spiele = tabelleneintrag2.Spiele + 1;
                                tabelleneintrag2.Gewonnen = tabelleneintrag2.Gewonnen + 1;
                                tabelleneintrag2.Untentschieden = tabelleneintrag2.Untentschieden;
                                tabelleneintrag2.Verloren = tabelleneintrag2.Verloren;

                                tabelleneintrag2.Punkte = tabelleneintrag2.Punkte + 3;

                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;
                            }
                        }
                    }

                    TabSaisonEwigSorted = TabSaisonEwigSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonEwigSorted.Count; ii++)
                    {
                        TabSaisonEwigSorted[ii].Platz = ii + 1;
                        TabSaisonEwigSorted[ii].Tore = TabSaisonEwigSorted[ii].TorePlus + ":" + TabSaisonEwigSorted[ii].ToreMinus;
                        TabSaisonEwigSorted[ii].Diff = TabSaisonEwigSorted[ii].TorePlus - TabSaisonEwigSorted[ii].ToreMinus;
                    }
                }
                return TabSaisonEwigSorted;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Spielergebnisse>> StatistikVerein(ISpieltagService spieltagService, Spielergebnisse spiel)
        {
            var TabSaisonSorted = new List<Spielergebnisse>();

            var alleSpieltage = (await spieltagService.GetSpielergebnisse());
            var Spielergebnisse = (alleSpieltage).Where(st => st.Verein1 == spiel.Verein1 || st.Verein2 == spiel.Verein1).OrderByDescending(sais => sais.SaisonID).ThenBy(sais => sais.SpieltagNr).ToList();

            return Spielergebnisse;
        }

        public async Task<Spielstatistik> VereinSum(ISpieltagService spieltagService, Spielergebnisse spiel)
        {
            try
            {
                int Gewonnen = 0, Unentschieden = 0, Verloren = 0;
                var TabSaisonSorted = new List<Spielergebnisse>();

                var alleSpieltage = (await spieltagService.GetSpielergebnisse());
                var SpielergebnisseVereinH = (alleSpieltage).Where(st => st.Verein1 == spiel.Verein1).ToList();

                var SpielergebnisseVereinA = (alleSpieltage).Where(st => st.Verein2 == spiel.Verein1).ToList();

                int GewonnenH = (SpielergebnisseVereinH).Where(st => st.Tore1_Nr > st.Tore2_Nr).ToList().Count();
                int UnentschiedenH = (SpielergebnisseVereinH).Where(st => st.Tore1_Nr == st.Tore2_Nr).ToList().Count();
                int VerlorenH = (SpielergebnisseVereinH).Where(st => st.Tore1_Nr < st.Tore2_Nr).ToList().Count();

                int GewonnenA = (SpielergebnisseVereinA).Where(st => st.Tore2_Nr > st.Tore1_Nr).ToList().Count();
                int UnentschiedenA = (SpielergebnisseVereinA).Where(st => st.Tore2_Nr == st.Tore1_Nr).ToList().Count();
                int VerlorenA = (SpielergebnisseVereinA).Where(st => st.Tore2_Nr < st.Tore1_Nr).ToList().Count();

                Gewonnen = GewonnenH + GewonnenA;
                Unentschieden = UnentschiedenH + UnentschiedenA;
                Verloren = VerlorenH + VerlorenA;

                Spielstatistik stat = new Spielstatistik();
                stat.Gewonnen = Gewonnen;
                stat.Unentschieden = Unentschieden;
                stat.Verloren = Verloren;

                return stat;
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }


        public async Task<List<int?>> CreateChart(ISpieltagService spieltagService, IEnumerable<Verein> Vereine, int vereinnr, int BisSpieltag)
        {
            try
            {
                Tabelle tabelleneintrag1;
                Tabelle tabelleneintrag2;

                SpieltageRepository rep = new SpieltageRepository();
                var TabSaisonSorted = new List<Tabelle>();
                int paarung = 1;
                int VonSpieltag = 1;


                var alleSpieltage = (await spieltagService.GetSpieltage());

                ChartPlaetze ChartPlaetze = new ChartPlaetze();

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == Globals.currentSaison && st.SpieltagNr == i.ToString()).ToList().OrderBy(x => x.Datum);


                    foreach (var item in this.Spieltag)
                    {

                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        if (i == 1)
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

                                tabelleneintrag1.Punkte = 3;

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

                                tabelleneintrag2.Punkte = 3;

                                tabelleneintrag2.Gewonnen = 1;
                                tabelleneintrag2.Untentschieden = 0;
                                tabelleneintrag2.Verloren = 0;
                                tabelleneintrag2.Platz = 0;
                                tabelleneintrag2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag2.Liga = Globals.currentLiga;
                            }
                            paarung++;

                            TabSaisonSorted.Add(tabelleneintrag1);

                            TabSaisonSorted.Add(tabelleneintrag2);

                        }
                        else
                        {

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

                                TabSaisonSorted.Add(tabelleneintrag1);
                                TabSaisonSorted.Add(tabelleneintrag2);                                


                            }
                            else
                            {
                                Debug.Print("null");
                            }
                        }

                    }

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        if (TabSaisonSorted[ii].VereinNr == vereinnr)
                        {
                            ChartPlaetze.punkte.Add(TabSaisonSorted[ii].Punkte);
                            break;
                        }                           
                    }
                }

                return ChartPlaetze.punkte;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<IEnumerable<Tabelle>> BerechneTabellePL(ISpieltageENService spieltagPLService,
                                                bool bAbgeschlossen,
                                                IEnumerable<VereinAUS> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;
            int BisSpieltag;
            SpieltageENRepository rep = new SpieltageENRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int VonSpieltag = 1;
                      

            try
            {
                if (bAbgeschlossen)
                    BisSpieltag = Spieltag;
                else
                {
                    if (Spieltag < rep.AktSpieltag(Globals.SaisonID))
                        BisSpieltag = Spieltag;
                    else
                        BisSpieltag = rep.AktSpieltag(Globals.SaisonID);
                }


                var alleSpieltage = (await spieltagPLService.GetSpieltage());

                if (Tabart == 4)
                    BisSpieltag = 17;

                if (Tabart == 5)
                {
                    VonSpieltag = 18;

                    int iAktSpieltag = Globals.maxSpieltag;
                    BisSpieltag = iAktSpieltag;
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();


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
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname2;
                                tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag1.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag1.Punkte = 3;

                                tabelleneintrag1.Gewonnen = 1;
                                tabelleneintrag1.Untentschieden = 0;
                                tabelleneintrag1.Verloren = 0;

                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname2;

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
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname2;
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
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname2;

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
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname2;

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
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname2;
                                tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag2.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag2.Punkte = 3;

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
                                    tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
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

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        TabSaisonSorted[ii].Platz = ii + 1;
                        TabSaisonSorted[ii].Tore = TabSaisonSorted[ii].TorePlus + ":" + TabSaisonSorted[ii].ToreMinus;
                        TabSaisonSorted[ii].Diff = TabSaisonSorted[ii].TorePlus - TabSaisonSorted[ii].ToreMinus;
                    }
                }

                return TabSaisonSorted;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Tabelle>> BerechneTabelleAus(ISpieltagAusService spieltagAusService,
                                                bool bAbgeschlossen,
                                                IEnumerable<VereinAUS> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;            
            SpieltageITRepository rep = new SpieltageITRepository();
            rep = new SpieltageITRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int BisSpieltag;
            int VonSpieltag = 1;


            try
            {
                if (bAbgeschlossen)
                    BisSpieltag = Spieltag;
                else
                {
                    if (Spieltag < rep.AktSpieltag(Globals.SaisonID))
                        BisSpieltag = Spieltag;
                    else
                        BisSpieltag = rep.AktSpieltag(Globals.SaisonID);
                }


                var alleSpieltage = (await spieltagAusService.GetSpieltage());

                if (Tabart == 4)
                    BisSpieltag = 17;

                if (Tabart == 5)
                {
                    VonSpieltag = 18;

                    int iAktSpieltag = Globals.maxSpieltag;
                    BisSpieltag = iAktSpieltag;
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();


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
                                tabelleneintrag1.Punkte = 3;

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
                                tabelleneintrag2.Punkte = 3;

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

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        TabSaisonSorted[ii].Platz = ii + 1;
                        TabSaisonSorted[ii].Tore = TabSaisonSorted[ii].TorePlus + ":" + TabSaisonSorted[ii].ToreMinus;
                        TabSaisonSorted[ii].Diff = TabSaisonSorted[ii].TorePlus - TabSaisonSorted[ii].ToreMinus;
                    }
                }

                return TabSaisonSorted;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Tabelle>> BerechneTabelleIT(ISpieltageITService spieltagITService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, string currentSaison, int ligaID, int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;
            SpieltageITRepository rep = new SpieltageITRepository();
            rep = new SpieltageITRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int BisSpieltag = 38;
            int VonSpieltag = 1;


            try
            {
                if (bAbgeschlossen)
                    BisSpieltag = count;
                else
                {
                    if (count < rep.AktSpieltag(Globals.SaisonID))
                        BisSpieltag = count;
                    else
                        BisSpieltag = rep.AktSpieltag(Globals.SaisonID);
                }


                var alleSpieltage = (await spieltagITService.GetSpieltage());

                if (Tabart == 4)
                    BisSpieltag = 17;

                if (Tabart == 5)
                {
                    VonSpieltag = 18;

                    int iAktSpieltag = Globals.maxSpieltag;
                    BisSpieltag = iAktSpieltag;
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == currentSaison && st.LigaID == ligaID && st.SpieltagNr == i.ToString()).ToList();


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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag1.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag1.Punkte = 3;

                                tabelleneintrag1.Gewonnen = 1;
                                tabelleneintrag1.Untentschieden = 0;
                                tabelleneintrag1.Verloren = 0;

                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;
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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;

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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag2.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag2.Punkte = 3;

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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
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

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        TabSaisonSorted[ii].Platz = ii + 1;
                        TabSaisonSorted[ii].Tore = TabSaisonSorted[ii].TorePlus + ":" + TabSaisonSorted[ii].ToreMinus;
                        TabSaisonSorted[ii].Diff = TabSaisonSorted[ii].TorePlus - TabSaisonSorted[ii].ToreMinus;
                    }
                }

                return TabSaisonSorted;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
   

        public async Task<IEnumerable<Tabelle>> BerechneTabelleFR(ISpieltageFRService spieltagFRService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, string currentSaison, int ligaID, int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;
            SpieltageFRRepository rep = new SpieltageFRRepository();
            rep = new SpieltageFRRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int BisSpieltag = 38;
            int VonSpieltag = 1;


            try
            {
                if (bAbgeschlossen)
                    BisSpieltag = count;
                else
                {
                    if (count < rep.AktSpieltag(Globals.SaisonID))
                        BisSpieltag = count;
                    else
                        BisSpieltag = rep.AktSpieltag(Globals.SaisonID);
                }


                var alleSpieltage = (await spieltagFRService.GetSpieltage());

                if (Tabart == 4)
                    BisSpieltag = 17;

                if (Tabart == 5)
                {
                    VonSpieltag = 18;

                    int iAktSpieltag = Globals.maxSpieltag;
                    BisSpieltag = iAktSpieltag;
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == currentSaison && st.LigaID == ligaID && st.SpieltagNr == i.ToString()).ToList();


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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;
                                tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag1.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag1.Punkte = 3;

                                tabelleneintrag1.Gewonnen = 1;
                                tabelleneintrag1.Untentschieden = 0;
                                tabelleneintrag1.Verloren = 0;

                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;
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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1;

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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1;
                                tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag2.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag2.Punkte = 3;

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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
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

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        TabSaisonSorted[ii].Platz = ii + 1;
                        TabSaisonSorted[ii].Tore = TabSaisonSorted[ii].TorePlus + ":" + TabSaisonSorted[ii].ToreMinus;
                        TabSaisonSorted[ii].Diff = TabSaisonSorted[ii].TorePlus - TabSaisonSorted[ii].ToreMinus;
                    }
                }

                return TabSaisonSorted;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Tabelle>> BerechneTabelleES(ISpieltageESService spieltagESService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, string currentSaison, int ligaID, int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;
            SpieltageESRepository rep = new SpieltageESRepository();
            rep = new SpieltageESRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int BisSpieltag = 38;
            int VonSpieltag = 1;


            try
            {
                if (bAbgeschlossen)
                    BisSpieltag = count;
                else
                {
                    if (count < rep.AktSpieltag(Globals.SaisonID))
                        BisSpieltag = count;
                    else
                        BisSpieltag = rep.AktSpieltag(Globals.SaisonID);
                }


                var alleSpieltage = (await spieltagESService.GetSpieltage());

                if (Tabart == 4)
                    BisSpieltag = 17;

                if (Tabart == 5)
                {
                    VonSpieltag = 18;

                    int iAktSpieltag = Globals.maxSpieltag;
                    BisSpieltag = iAktSpieltag;
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == currentSaison && st.LigaID == ligaID && st.SpieltagNr == i.ToString()).ToList();


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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname2;
                                tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag1.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag1.Punkte = 3;

                                tabelleneintrag1.Gewonnen = 1;
                                tabelleneintrag1.Untentschieden = 0;
                                tabelleneintrag1.Verloren = 0;

                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname2;

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname2;
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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname2;

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname2;

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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname2;
                                tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag2.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag2.Punkte = 3;

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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
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

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        TabSaisonSorted[ii].Platz = ii + 1;
                        TabSaisonSorted[ii].Tore = TabSaisonSorted[ii].TorePlus + ":" + TabSaisonSorted[ii].ToreMinus;
                        TabSaisonSorted[ii].Diff = TabSaisonSorted[ii].TorePlus - TabSaisonSorted[ii].ToreMinus;
                    }
                }

                return TabSaisonSorted;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Tabelle>> BerechneTabelleNL(ISpieltageNLService spieltagNLService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, string currentSaison, int ligaID, int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;
            SpieltageNLRepository rep = new SpieltageNLRepository();
            rep = new SpieltageNLRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int BisSpieltag = 38;
            int VonSpieltag = 1;


            try
            {
                if (bAbgeschlossen)
                    BisSpieltag = count;
                else
                {
                    if (count < rep.AktSpieltag(Globals.SaisonID))
                        BisSpieltag = count;
                    else
                        BisSpieltag = rep.AktSpieltag(Globals.SaisonID);
                }


                var alleSpieltage = (await spieltagNLService.GetSpieltage());

                if (Tabart == 4)
                    BisSpieltag = 17;

                if (Tabart == 5)
                {
                    VonSpieltag = 18;

                    int iAktSpieltag = Globals.maxSpieltag;
                    BisSpieltag = iAktSpieltag;
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == currentSaison && st.LigaID == ligaID && st.SpieltagNr == i.ToString()).ToList();


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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1.Trim();
                                tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag1.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag1.Punkte = 3;

                                tabelleneintrag1.Gewonnen = 1;
                                tabelleneintrag1.Untentschieden = 0;
                                tabelleneintrag1.Verloren = 0;

                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1.Trim();

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1.Trim()  ;
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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1.Trim();

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1.Trim();

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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1.Trim();
                                tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag2.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag2.Punkte = 3;

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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1.Trim(); 
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1.Trim(); 
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1.Trim();
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1.Trim();
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1.Trim();
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1.Trim();
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

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        TabSaisonSorted[ii].Platz = ii + 1;
                        TabSaisonSorted[ii].Tore = TabSaisonSorted[ii].TorePlus + ":" + TabSaisonSorted[ii].ToreMinus;
                        TabSaisonSorted[ii].Diff = TabSaisonSorted[ii].TorePlus - TabSaisonSorted[ii].ToreMinus;
                    }
                }

                return TabSaisonSorted;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<IEnumerable<Tabelle>> BerechneTabellePT(ISpieltagePTService spieltagPTService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, string currentSaison, int ligaID, int Tabart)
        {
            Tabelle tabelleneintrag1;
            Tabelle tabelleneintrag2;
            SpieltagePTRepository rep = new SpieltagePTRepository();
            rep = new SpieltagePTRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int paarung = 1;
            int BisSpieltag = 34;
            int VonSpieltag = 1;


            try
            {
                if (bAbgeschlossen)
                    BisSpieltag = count;
                else
                {
                    if (count < rep.AktSpieltag(Globals.SaisonID))
                        BisSpieltag = count;
                    else
                        BisSpieltag = rep.AktSpieltag(Globals.SaisonID);
                }


                var alleSpieltage = (await spieltagPTService.GetSpieltage());

                if (Tabart == 4)
                    BisSpieltag = 15;

                if (Tabart == 5)
                {
                    VonSpieltag = 18;

                    int iAktSpieltag = Globals.maxSpieltag;
                    BisSpieltag = iAktSpieltag;
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == currentSaison && st.LigaID == ligaID && st.SpieltagNr == i.ToString()).ToList();


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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1.Trim();
                                tabelleneintrag1.TorePlus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag1.ToreMinus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag1.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag1.Punkte = 3;

                                tabelleneintrag1.Gewonnen = 1;
                                tabelleneintrag1.Untentschieden = 0;
                                tabelleneintrag1.Verloren = 0;

                                tabelleneintrag1.Platz = 0;
                                tabelleneintrag1.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintrag1.Liga = Globals.currentLiga;

                                tabelleneintrag2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1.Trim();

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1.Trim();
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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1.Trim();

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
                                tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein1_Nr)).Vereinsname1.Trim();

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
                                tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(item.Verein2_Nr)).Vereinsname1.Trim();
                                tabelleneintrag2.TorePlus = Convert.ToInt32(item.Tore2_Nr);
                                tabelleneintrag2.ToreMinus = Convert.ToInt32(item.Tore1_Nr);
                                tabelleneintrag2.Spiele = 1;

                                int.TryParse(item.Saison.Substring(0, 4), out Saison);
                                tabelleneintrag2.Punkte = 3;

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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1.Trim();
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1.Trim();
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1.Trim();
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1.Trim();
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
                                    tabelleneintrag1.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1.Trim();
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
                                    tabelleneintrag2.Verein = vereineAus.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1.Trim();
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

                    TabSaisonSorted = TabSaisonSorted.OrderByDescending(o => o.Punkte).ThenByDescending(o => o.TorePlus - o.ToreMinus).ThenByDescending(o => o.TorePlus).ToList();

                    for (int ii = 0; ii < TabSaisonSorted.Count; ii++)
                    {
                        TabSaisonSorted[ii].Platz = ii + 1;
                        TabSaisonSorted[ii].Tore = TabSaisonSorted[ii].TorePlus + ":" + TabSaisonSorted[ii].ToreMinus;
                        TabSaisonSorted[ii].Diff = TabSaisonSorted[ii].TorePlus - TabSaisonSorted[ii].ToreMinus;
                    }
                }

                return TabSaisonSorted;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
    }
}
