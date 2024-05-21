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

        public async Task<IEnumerable<Tabelle>> BerechneTabelleDE(ISpieltagService spieltagService,
                                             bool bAbgeschlossen,
                                             List<VereineSaison> VereineSaison,
                                             IEnumerable<Verein> Vereine,
                                             int Spieltag,
                                             string sSaison,
                                             int LigaId,
                                             int Tabart)
        {
            Tabelle tabelleneintragV1;
            Tabelle tabelleneintragV2;
            DateTime dtGrenze_2_3 = DateTime.Parse("01/07/1995");

            int BisSpieltag;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonSorted = new List<Tabelle>();            
            int VonSpieltag = 1;

            

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

                if (Tabart == (int)Globals.Tabart.Vorrunde) 
                {
                    if (sSaison == "1963/64" || sSaison == "1964/65")
                        BisSpieltag = 15;
                    else if (sSaison == "1991/92")
                        BisSpieltag = 19;
                    else
                        BisSpieltag = 17;
                }                    

                if (Tabart == (int)Globals.Tabart.Rückrunde)
                {
                    if (sSaison == "1963/64" || sSaison == "1964/65")
                        VonSpieltag = 16;
                    else if (sSaison == "1991/92")
                        VonSpieltag = 20;
                    else
                        VonSpieltag = 18;                                       
                    
                    int iAktSpieltag = 0;
                    if (bAbgeschlossen)
                    {
                        iAktSpieltag = Globals.maxSpieltag;
                    }
                    else
                    {
                        iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);                        
                    }
                    BisSpieltag = iAktSpieltag;
                }

                // Grundtabelle erzeugen
                foreach (VereineSaison verein in VereineSaison)
                {

                    tabelleneintragV1 = new Tabelle();

                    tabelleneintragV1.VereinNr = verein.VereinNr;
                    tabelleneintragV1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname1;
                    tabelleneintragV1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname2;
                    tabelleneintragV1.TorePlus = 0;
                    tabelleneintragV1.ToreMinus = 0;
                    tabelleneintragV1.Spiele = 0;
                    tabelleneintragV1.Punkte = 0;
                    tabelleneintragV1.Gewonnen = 0;
                    tabelleneintragV1.Untentschieden = 0;
                    tabelleneintragV1.Verloren = 0;
                    tabelleneintragV1.Platz = 0;
                    tabelleneintragV1.Tore = "0";
                    tabelleneintragV1.Diff = 0;
                    tabelleneintragV1.Tab_Sai_Id = Globals.SaisonID;
                    tabelleneintragV1.Tab_Lig_Id = Globals.LigaID;
                    tabelleneintragV1.Liga = Globals.currentLiga;

                    TabSaisonSorted.Add(tabelleneintragV1);
                }


                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();

                    foreach (var item in this.Spieltag)
                    {                        
                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        tabelleneintragV1 = new Tabelle();
                        tabelleneintragV2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;

                                    if (item.Datum < dtGrenze_2_3.Date)
                                        tabelleneintragF.Punkte = tabelleneintragF.Punkte + 2;
                                    else
                                        tabelleneintragF.Punkte = tabelleneintragF.Punkte + 3;

                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren + 1;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 1;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 1;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren + 1;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }
                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;

                                    if (item.Datum < dtGrenze_2_3.Date)
                                        tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 2;
                                    else
                                        tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 3;
                                }

                                tabelleneintragF2.Platz = 0;
                                tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintragF2.Liga = Globals.currentLiga;
                            }                           

                            if (Globals.SaisonID == 44 && tabelleneintragV2.Verein == "Arminia Bielefeld")
                            {
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.Platz = 18;
                                tabelleneintragF2.Punkte = 0;
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.ToreMinus = 0;
                            }
                        }
                        else
                        {
                            ErrorLogger.WriteToErrorLog("Fehler", "Fehler", Assembly.GetExecutingAssembly().FullName);
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

                var alleSpieltage = (await spieltagService.GetSpieltage()).Where(x => x.LigaID == Globals.LigaID);
                var saisonen = (await saisonenService.GetSaisonen()).Where(x => x.LigaID == Globals.LigaID).OrderBy(x => x.Saisonname).ToList();

                for (int j = 0; j < saisonen.Count(); j++)
                {
                    if (saisonen[j].Saisonname == "1963/64" || saisonen[j].Saisonname == "1964/65")
                        BisSpieltag = 30;
                    else if (saisonen[j].Saisonname == "1991/92")
                        BisSpieltag = 38;
                    else
                        BisSpieltag = 34;


                    if (saisonen[j].Aktuell)
                    {
                        //if (Spieltag < rep.AktSpieltag(Globals.SaisonID))
                        //    BisSpieltag = Spieltag;
                        //else
                        //    BisSpieltag = rep.AktSpieltag(Globals.SaisonID);
                        BisSpieltag = iSpieltag;
                    }


                    for (int i = VonSpieltag; i <= BisSpieltag; i++)
                    {

                        this.Spieltag = (alleSpieltage).Where(st => st.Saison == saisonen[j].Saisonname && st.SpieltagNr == i.ToString()).ToList();

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
                                tabelleneintrag1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag1.VereinNr)).Vereinsname2;
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
                                tabelleneintrag2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag2.VereinNr)).Vereinsname2;
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
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag1.VereinNr)).Vereinsname1;
                                tabelleneintrag1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag1.VereinNr)).Vereinsname2;
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
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag2.VereinNr)).Vereinsname1;
                                tabelleneintrag2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag2.VereinNr)).Vereinsname2;
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
                                tabelleneintrag1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag1.VereinNr)).Vereinsname1;
                                tabelleneintrag1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag1.VereinNr)).Vereinsname2;
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
                                tabelleneintrag2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag2.VereinNr)).Vereinsname1;
                                tabelleneintrag2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintrag2.VereinNr)).Vereinsname2;
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
            Tabelle tabelleneintragV1;
            Tabelle tabelleneintragV2;

            int BisSpieltag;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int VonSpieltag = 1;

            VereineSaisonAusRepository repAus = new VereineSaisonAusRepository();
            var retDel = await repAus.DeleteVereineSaison(Globals.LigaID, Globals.SaisonID);
            var retAdd = await repAus.AddVereineSaison(Globals.LigaID, Globals.SaisonID);

            if ((retDel == false) || (retAdd.Count() == 0))
                return null;

            var VereineSaison = await repAus.GetVereineSaison(Globals.SaisonID);

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

                var alleSpieltage = (await spieltagPLService.GetSpieltage());

                if (Tabart == (int)Globals.Tabart.Vorrunde)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1993" || Globals.currentSaison.Substring(0, 4) == "1994")
                        BisSpieltag = 21;
                    else
                        BisSpieltag = 19;
                }

                if (Tabart == (int)Globals.Tabart.Rückrunde)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2019)
                        VonSpieltag = 22;
                    else
                        VonSpieltag = 20;


                    int iAktSpieltag = 0;
                    if (bAbgeschlossen)
                    {
                        iAktSpieltag = Globals.maxSpieltag;
                    }
                    else
                    {
                        iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                    }
                    BisSpieltag = iAktSpieltag;
                }

                // Grundtabelle erzeugen
                foreach (VereineSaisonAus verein in VereineSaison)
                {

                    tabelleneintragV1 = new Tabelle();

                    tabelleneintragV1.VereinNr = verein.VereinNr;
                    tabelleneintragV1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname1;
                    tabelleneintragV1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname2;
                    //tabelleneintragV1.Hyperlink = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Hyperlink;
                    tabelleneintragV1.TorePlus = 0;
                    tabelleneintragV1.ToreMinus = 0;
                    tabelleneintragV1.Spiele = 0;
                    tabelleneintragV1.Punkte = 0;
                    tabelleneintragV1.Gewonnen = 0;
                    tabelleneintragV1.Untentschieden = 0;
                    tabelleneintragV1.Verloren = 0;
                    tabelleneintragV1.Platz = 0;
                    tabelleneintragV1.Tore = "0";
                    tabelleneintragV1.Diff = 0;
                    tabelleneintragV1.Tab_Sai_Id = Globals.SaisonID;
                    tabelleneintragV1.Tab_Lig_Id = Globals.LigaID;
                    tabelleneintragV1.Liga = Globals.currentLiga;

                    TabSaisonSorted.Add(tabelleneintragV1);
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();

                    foreach (var item in this.Spieltag)
                    {
                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        tabelleneintragV1 = new Tabelle();
                        tabelleneintragV2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;

                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 3;


                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren + 1;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 1;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 1;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren + 1;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }
                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;

                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 3;
                                }

                                tabelleneintragF2.Platz = 0;
                                tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintragF2.Liga = Globals.currentLiga;
                            }

                            if (Globals.SaisonID == 44 && tabelleneintragV2.Verein == "Arminia Bielefeld")
                            {
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.Platz = 18;
                                tabelleneintragF2.Punkte = 0;
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.ToreMinus = 0;
                            }
                        }
                        else
                        {
                            ErrorLogger.WriteToErrorLog("Fehler", "Fehler", Assembly.GetExecutingAssembly().FullName);
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
      

        public async Task<IEnumerable<Tabelle>> BerechneTabelleIT(ISpieltageITService spieltagITService,
                                                bool bAbgeschlossen,
                                                IEnumerable<VereinAUS> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
            Tabelle tabelleneintragV1;
            Tabelle tabelleneintragV2;

            int BisSpieltag;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int VonSpieltag = 1;

            VereineSaisonAusRepository repAus = new VereineSaisonAusRepository();

            var VereineSaison = await repAus.GetVereineSaison(Globals.SaisonID);

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

                var alleSpieltage = (await spieltagITService.GetSpieltage());

                if (Tabart == (int)Globals.Tabart.Vorrunde)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2003)
                        BisSpieltag = 19;
                    else
                        BisSpieltag = 17;
                }

                if (Tabart == (int)Globals.Tabart.Rückrunde)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2003)
                        VonSpieltag = 20;
                    else
                        VonSpieltag = 18;

                    int iAktSpieltag = 0;
                    if (bAbgeschlossen)
                    {
                        iAktSpieltag = Globals.maxSpieltag;
                    }
                    else
                    {
                        iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                    }
                    BisSpieltag = iAktSpieltag;
                }

                // Grundtabelle erzeugen
                foreach (VereineSaisonAus verein in VereineSaison)
                {

                    tabelleneintragV1 = new Tabelle();

                    tabelleneintragV1.VereinNr = verein.VereinNr;
                    tabelleneintragV1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname1;
                    tabelleneintragV1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname2;
                    tabelleneintragV1.TorePlus = 0;
                    tabelleneintragV1.ToreMinus = 0;
                    tabelleneintragV1.Spiele = 0;
                    tabelleneintragV1.Punkte = 0;
                    tabelleneintragV1.Gewonnen = 0;
                    tabelleneintragV1.Untentschieden = 0;
                    tabelleneintragV1.Verloren = 0;
                    tabelleneintragV1.Platz = 0;
                    tabelleneintragV1.Tore = "0";
                    tabelleneintragV1.Diff = 0;
                    tabelleneintragV1.Tab_Sai_Id = Globals.SaisonID;
                    tabelleneintragV1.Tab_Lig_Id = Globals.LigaID;
                    tabelleneintragV1.Liga = Globals.currentLiga;

                    TabSaisonSorted.Add(tabelleneintragV1);
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();

                    foreach (var item in this.Spieltag)
                    {
                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        tabelleneintragV1 = new Tabelle();
                        tabelleneintragV2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;

                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 3;


                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren + 1;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 1;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 1;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren + 1;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }
                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;

                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 3;
                                }

                                tabelleneintragF2.Platz = 0;
                                tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintragF2.Liga = Globals.currentLiga;
                            }

                            if (Globals.SaisonID == 44 && tabelleneintragV2.Verein == "Arminia Bielefeld")
                            {
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.Platz = 18;
                                tabelleneintragF2.Punkte = 0;
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.ToreMinus = 0;
                            }
                        }
                        else
                        {
                            ErrorLogger.WriteToErrorLog("Fehler", "Fehler", Assembly.GetExecutingAssembly().FullName);
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


        public async Task<IEnumerable<Tabelle>> BerechneTabelleFR(ISpieltageFRService spieltagITService,
                                                bool bAbgeschlossen,
                                                IEnumerable<VereinAUS> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
            Tabelle tabelleneintragV1;
            Tabelle tabelleneintragV2;

            int BisSpieltag;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int VonSpieltag = 1;

            VereineSaisonAusRepository repAus = new VereineSaisonAusRepository();

            var VereineSaison = await repAus.GetVereineSaison(Globals.SaisonID);

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

                var alleSpieltage = (await spieltagITService.GetSpieltage());

                if (Tabart == (int)Globals.Tabart.Vorrunde)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1993" || Globals.currentSaison.Substring(0, 4) == "1994")
                        BisSpieltag = 21;
                    else
                        BisSpieltag = 19;
                }

                if (Tabart == (int)Globals.Tabart.Rückrunde)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1993" || Globals.currentSaison.Substring(0, 4) == "1994")
                        VonSpieltag = 22;
                    else
                        VonSpieltag = 20;

                    int iAktSpieltag = 0;
                    if (bAbgeschlossen)
                    {
                        iAktSpieltag = Globals.maxSpieltag;
                    }
                    else
                    {
                        iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                    }
                    BisSpieltag = iAktSpieltag;
                }

                // Grundtabelle erzeugen
                foreach (VereineSaisonAus verein in VereineSaison)
                {

                    tabelleneintragV1 = new Tabelle();

                    tabelleneintragV1.VereinNr = verein.VereinNr;
                    tabelleneintragV1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname1;
                    tabelleneintragV1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname2;
                    tabelleneintragV1.TorePlus = 0;
                    tabelleneintragV1.ToreMinus = 0;
                    tabelleneintragV1.Spiele = 0;
                    tabelleneintragV1.Punkte = 0;
                    tabelleneintragV1.Gewonnen = 0;
                    tabelleneintragV1.Untentschieden = 0;
                    tabelleneintragV1.Verloren = 0;
                    tabelleneintragV1.Platz = 0;
                    tabelleneintragV1.Tore = "0";
                    tabelleneintragV1.Diff = 0;
                    tabelleneintragV1.Tab_Sai_Id = Globals.SaisonID;
                    tabelleneintragV1.Tab_Lig_Id = Globals.LigaID;
                    tabelleneintragV1.Liga = Globals.currentLiga;

                    TabSaisonSorted.Add(tabelleneintragV1);
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();

                    foreach (var item in this.Spieltag)
                    {
                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        tabelleneintragV1 = new Tabelle();
                        tabelleneintragV2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;

                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 3;


                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren + 1;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 1;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 1;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren + 1;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }
                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;

                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 3;
                                }

                                tabelleneintragF2.Platz = 0;
                                tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintragF2.Liga = Globals.currentLiga;
                            }

                            if (Globals.SaisonID == 44 && tabelleneintragV2.Verein == "Arminia Bielefeld")
                            {
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.Platz = 18;
                                tabelleneintragF2.Punkte = 0;
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.ToreMinus = 0;
                            }
                        }
                        else
                        {
                            ErrorLogger.WriteToErrorLog("Fehler", "Fehler", Assembly.GetExecutingAssembly().FullName);
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

        public async Task<IEnumerable<Tabelle>> BerechneTabelleES(ISpieltageESService spieltagPLService,
                                                bool bAbgeschlossen,
                                                IEnumerable<VereinAUS> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
            Tabelle tabelleneintragV1;
            Tabelle tabelleneintragV2;

            int BisSpieltag;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int VonSpieltag = 1;

            VereineSaisonAusRepository repAus = new VereineSaisonAusRepository();
            var retDel = await repAus.DeleteVereineSaison(Globals.LigaID, Globals.SaisonID);
            var retAdd = await repAus.AddVereineSaison(Globals.LigaID, Globals.SaisonID);

            if ((retDel == false) || (retAdd.Count() == 0))
                return null;

            var VereineSaison = await repAus.GetVereineSaison(Globals.SaisonID);

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

                var alleSpieltage = (await spieltagPLService.GetSpieltage());

                if (Tabart == (int)Globals.Tabart.Vorrunde)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1995" || Globals.currentSaison.Substring(0, 4) == "1996")
                        BisSpieltag = 21;
                    else
                        BisSpieltag = 19;
                }

                if (Tabart == (int)Globals.Tabart.Rückrunde)
                {
                    if (Globals.currentSaison.Substring(0, 4) == "1995" || Globals.currentSaison.Substring(0, 4) == "1996")
                        VonSpieltag = 22;
                    else
                        VonSpieltag = 20;

                    int iAktSpieltag = 0;
                    if (bAbgeschlossen)
                    {
                        iAktSpieltag = Globals.maxSpieltag;
                    }
                    else
                    {
                        iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                    }
                    BisSpieltag = iAktSpieltag;
                }

                // Grundtabelle erzeugen
                foreach (VereineSaisonAus verein in VereineSaison)
                {

                    tabelleneintragV1 = new Tabelle();

                    tabelleneintragV1.VereinNr = verein.VereinNr;
                    tabelleneintragV1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname1;
                    tabelleneintragV1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname2;
                    tabelleneintragV1.TorePlus = 0;
                    tabelleneintragV1.ToreMinus = 0;
                    tabelleneintragV1.Spiele = 0;
                    tabelleneintragV1.Punkte = 0;
                    tabelleneintragV1.Gewonnen = 0;
                    tabelleneintragV1.Untentschieden = 0;
                    tabelleneintragV1.Verloren = 0;
                    tabelleneintragV1.Platz = 0;
                    tabelleneintragV1.Tore = "0";
                    tabelleneintragV1.Diff = 0;
                    tabelleneintragV1.Tab_Sai_Id = Globals.SaisonID;
                    tabelleneintragV1.Tab_Lig_Id = Globals.LigaID;
                    tabelleneintragV1.Liga = Globals.currentLiga;

                    TabSaisonSorted.Add(tabelleneintragV1);
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();

                    foreach (var item in this.Spieltag)
                    {
                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        tabelleneintragV1 = new Tabelle();
                        tabelleneintragV2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;

                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 3;


                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren + 1;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 1;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 1;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren + 1;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }
                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;

                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 3;
                                }

                                tabelleneintragF2.Platz = 0;
                                tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintragF2.Liga = Globals.currentLiga;
                            }

                            if (Globals.SaisonID == 44 && tabelleneintragV2.Verein == "Arminia Bielefeld")
                            {
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.Platz = 18;
                                tabelleneintragF2.Punkte = 0;
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.ToreMinus = 0;
                            }
                        }
                        else
                        {
                            ErrorLogger.WriteToErrorLog("Fehler", "Fehler", Assembly.GetExecutingAssembly().FullName);
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

        public async Task<IEnumerable<Tabelle>> BerechneTabelleNL(ISpieltageNLService spieltagNLService,
                                                bool bAbgeschlossen,
                                                IEnumerable<VereinAUS> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
            Tabelle tabelleneintragV1;
            Tabelle tabelleneintragV2;

            int BisSpieltag;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int VonSpieltag = 1;

            VereineSaisonAusRepository repAus = new VereineSaisonAusRepository();
            var retDel = await repAus.DeleteVereineSaison(Globals.LigaID, Globals.SaisonID);
            var retAdd = await repAus.AddVereineSaison(Globals.LigaID, Globals.SaisonID);

            if ((retDel == false) || (retAdd.Count() == 0))
                return null;

            var VereineSaison = await repAus.GetVereineSaison(Globals.SaisonID);

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

                var alleSpieltage = (await spieltagNLService.GetSpieltage());

                if (Tabart == (int)Globals.Tabart.Vorrunde)
                {                    
                    BisSpieltag = 17;
                }

                if (Tabart == (int)Globals.Tabart.Rückrunde)
                {
                     VonSpieltag = 18;

                    int iAktSpieltag = 0;
                    if (bAbgeschlossen)
                    {
                        iAktSpieltag = Globals.maxSpieltag;
                    }
                    else
                    {
                        iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                    }
                    BisSpieltag = iAktSpieltag;
                }

                // Grundtabelle erzeugen
                foreach (VereineSaisonAus verein in VereineSaison)
                {

                    tabelleneintragV1 = new Tabelle();

                    tabelleneintragV1.VereinNr = verein.VereinNr;
                    tabelleneintragV1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname1;
                    tabelleneintragV1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname2;
                    tabelleneintragV1.TorePlus = 0;
                    tabelleneintragV1.ToreMinus = 0;
                    tabelleneintragV1.Spiele = 0;
                    tabelleneintragV1.Punkte = 0;
                    tabelleneintragV1.Gewonnen = 0;
                    tabelleneintragV1.Untentschieden = 0;
                    tabelleneintragV1.Verloren = 0;
                    tabelleneintragV1.Platz = 0;
                    tabelleneintragV1.Tore = "0";
                    tabelleneintragV1.Diff = 0;
                    tabelleneintragV1.Tab_Sai_Id = Globals.SaisonID;
                    tabelleneintragV1.Tab_Lig_Id = Globals.LigaID;
                    tabelleneintragV1.Liga = Globals.currentLiga;

                    TabSaisonSorted.Add(tabelleneintragV1);
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();

                    foreach (var item in this.Spieltag)
                    {
                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        tabelleneintragV1 = new Tabelle();
                        tabelleneintragV2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;

                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 3;


                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren + 1;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 1;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 1;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren + 1;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }
                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;

                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 3;
                                }

                                tabelleneintragF2.Platz = 0;
                                tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintragF2.Liga = Globals.currentLiga;
                            }

                            if (Globals.SaisonID == 44 && tabelleneintragV2.Verein == "Arminia Bielefeld")
                            {
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.Platz = 18;
                                tabelleneintragF2.Punkte = 0;
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.ToreMinus = 0;
                            }
                        }
                        else
                        {
                            ErrorLogger.WriteToErrorLog("Fehler", "Fehler", Assembly.GetExecutingAssembly().FullName);
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

        public async Task<IEnumerable<Tabelle>> BerechneTabellePT(ISpieltagePTService spieltagPTService,
                                                bool bAbgeschlossen,
                                                IEnumerable<VereinAUS> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
            Tabelle tabelleneintragV1;
            Tabelle tabelleneintragV2;

            int BisSpieltag;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int VonSpieltag = 1;

            VereineSaisonAusRepository repAus = new VereineSaisonAusRepository();
            var retDel = await repAus.DeleteVereineSaison(Globals.LigaID, Globals.SaisonID);
            var retAdd= await repAus.AddVereineSaison(Globals.LigaID, Globals.SaisonID);

            if ((retDel == false) || (retAdd.Count() == 0))
                return null;

            var VereineSaison = await repAus.GetVereineSaison(Globals.SaisonID);

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

                var alleSpieltage = (await spieltagPTService.GetSpieltage());

                if (Tabart == (int)Globals.Tabart.Vorrunde)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2013)
                        BisSpieltag = 17;
                    else
                        BisSpieltag = 15;
                }

                if (Tabart == (int)Globals.Tabart.Rückrunde)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2013)
                        VonSpieltag = 18;
                    else
                        VonSpieltag = 16;

                    int iAktSpieltag = 0;
                    if (bAbgeschlossen)
                    {
                        iAktSpieltag = Globals.maxSpieltag;
                    }
                    else
                    {
                        iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                    }
                    BisSpieltag = iAktSpieltag;
                }

                // Grundtabelle erzeugen
                foreach (VereineSaisonAus verein in VereineSaison)
                {

                    tabelleneintragV1 = new Tabelle();

                    tabelleneintragV1.VereinNr = verein.VereinNr;
                    tabelleneintragV1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname1;
                    tabelleneintragV1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname2;
                    tabelleneintragV1.TorePlus = 0;
                    tabelleneintragV1.ToreMinus = 0;
                    tabelleneintragV1.Spiele = 0;
                    tabelleneintragV1.Punkte = 0;
                    tabelleneintragV1.Gewonnen = 0;
                    tabelleneintragV1.Untentschieden = 0;
                    tabelleneintragV1.Verloren = 0;
                    tabelleneintragV1.Platz = 0;
                    tabelleneintragV1.Tore = "0";
                    tabelleneintragV1.Diff = 0;
                    tabelleneintragV1.Tab_Sai_Id = Globals.SaisonID;
                    tabelleneintragV1.Tab_Lig_Id = Globals.LigaID;
                    tabelleneintragV1.Liga = Globals.currentLiga;

                    TabSaisonSorted.Add(tabelleneintragV1);
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();

                    foreach (var item in this.Spieltag)
                    {
                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        tabelleneintragV1 = new Tabelle();
                        tabelleneintragV2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;

                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 3;


                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren + 1;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 1;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 1;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren + 1;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }
                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;

                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 3;
                                }

                                tabelleneintragF2.Platz = 0;
                                tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintragF2.Liga = Globals.currentLiga;
                            }

                            if (Globals.SaisonID == 44 && tabelleneintragV2.Verein == "Arminia Bielefeld")
                            {
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.Platz = 18;
                                tabelleneintragF2.Punkte = 0;
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.ToreMinus = 0;
                            }
                        }
                        else
                        {
                            ErrorLogger.WriteToErrorLog("Fehler", "Fehler", Assembly.GetExecutingAssembly().FullName);
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

        public async Task<IEnumerable<Tabelle>> BerechneTabelleTU(ISpieltageTUService spieltagTUService,
                                                bool bAbgeschlossen,
                                                IEnumerable<VereinAUS> Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
            Tabelle tabelleneintragV1;
            Tabelle tabelleneintragV2;

            int BisSpieltag;
            SpieltageRepository rep = new SpieltageRepository();
            var TabSaisonSorted = new List<Tabelle>();
            int VonSpieltag = 1;

            VereineSaisonAusRepository repAus = new VereineSaisonAusRepository();
            var retDel = await repAus.DeleteVereineSaison(Globals.LigaID, Globals.SaisonID);
            var retAdd = await repAus.AddVereineSaison(Globals.LigaID, Globals.SaisonID);

            if ((retDel == false) || (retAdd.Count() == 0))
                return null;

            var VereineSaison = await repAus.GetVereineSaison(Globals.SaisonID);

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

                var alleSpieltage = (await spieltagTUService.GetSpieltage());

                if (Tabart == (int)Globals.Tabart.Vorrunde)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2019)
                        BisSpieltag = 19;
                    else
                        BisSpieltag = 17;
                }

                if (Tabart == (int)Globals.Tabart.Rückrunde)
                {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2019)
                        VonSpieltag = 20;
                    else
                        VonSpieltag = 18;


                    int iAktSpieltag = 0;
                    if (bAbgeschlossen)
                    {
                        iAktSpieltag = Globals.maxSpieltag;
                    }
                    else
                    {
                        iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                    }
                    BisSpieltag = iAktSpieltag;
                }

                // Grundtabelle erzeugen
                foreach (VereineSaisonAus verein in VereineSaison)
                {

                    tabelleneintragV1 = new Tabelle();

                    tabelleneintragV1.VereinNr = verein.VereinNr;
                    tabelleneintragV1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname1;
                    tabelleneintragV1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname2;
                    tabelleneintragV1.TorePlus = 0;
                    tabelleneintragV1.ToreMinus = 0;
                    tabelleneintragV1.Spiele = 0;
                    tabelleneintragV1.Punkte = 0;
                    tabelleneintragV1.Gewonnen = 0;
                    tabelleneintragV1.Untentschieden = 0;
                    tabelleneintragV1.Verloren = 0;
                    tabelleneintragV1.Platz = 0;
                    tabelleneintragV1.Tore = "0";
                    tabelleneintragV1.Diff = 0;
                    tabelleneintragV1.Tab_Sai_Id = Globals.SaisonID;
                    tabelleneintragV1.Tab_Lig_Id = Globals.LigaID;
                    tabelleneintragV1.Liga = Globals.currentLiga;

                    TabSaisonSorted.Add(tabelleneintragV1);
                }

                for (int i = VonSpieltag; i <= BisSpieltag; i++)
                {

                    this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();

                    foreach (var item in this.Spieltag)
                    {
                        Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                        Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                        tabelleneintragV1 = new Tabelle();
                        tabelleneintragV2 = new Tabelle();

                        if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                        {
                            if (item.Tore1_Nr > item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;

                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 3;


                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren + 1;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr == item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte + 1;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }

                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;
                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 1;
                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }
                            }
                            else if (item.Tore1_Nr < item.Tore2_Nr)
                            {
                                if (Tabart != (int)Globals.Tabart.Auswärts)
                                {
                                    tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                    tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                    tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                    tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                    tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                    tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                    tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                    tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                    tabelleneintragF.Verloren = tabelleneintragF.Verloren + 1;
                                    tabelleneintragF.Punkte = tabelleneintragF.Punkte;
                                    tabelleneintragF.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF.Liga = Globals.currentLiga;
                                }
                                if (Tabart != (int)Globals.Tabart.Heim)
                                {
                                    tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                    tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                    tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                    tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                    tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                    tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                    tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                    tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                    tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;

                                    tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 3;
                                }

                                tabelleneintragF2.Platz = 0;
                                tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                tabelleneintragF2.Liga = Globals.currentLiga;
                            }

                            if (Globals.SaisonID == 44 && tabelleneintragV2.Verein == "Arminia Bielefeld")
                            {
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.Platz = 18;
                                tabelleneintragF2.Punkte = 0;
                                tabelleneintragF2.TorePlus = 0;
                                tabelleneintragF2.ToreMinus = 0;
                            }
                        }
                        else
                        {
                            ErrorLogger.WriteToErrorLog("Fehler", "Fehler", Assembly.GetExecutingAssembly().FullName);
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

        public async Task<IEnumerable<Tabelle>> BerechneTabelleBE(ISpieltageBEService spieltagBEService,
                                                bool bAbgeschlossen,
                                                IEnumerable< VereinAUS > Vereine,
                                                int Spieltag,
                                                string sSaison,
                                                int LigaId,
                                                int Tabart)
        {
                Tabelle tabelleneintragV1;
                Tabelle tabelleneintragV2;

                int BisSpieltag;
                SpieltageRepository rep = new SpieltageRepository();
                var TabSaisonSorted = new List<Tabelle>();
                int VonSpieltag = 1;

                VereineSaisonAusRepository repAus = new VereineSaisonAusRepository();
                var retDel = await repAus.DeleteVereineSaison(Globals.LigaID, Globals.SaisonID);
                var retAdd = await repAus.AddVereineSaison(Globals.LigaID, Globals.SaisonID);

                if ((retDel == false) || (retAdd.Count() == 0))
                    return null;

                var VereineSaison = await repAus.GetVereineSaison(Globals.SaisonID);

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

                    var alleSpieltage = (await spieltagBEService.GetSpieltage());

                    if (Tabart == (int)Globals.Tabart.Vorrunde)
                    {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) < 2020 || Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2023)
                        BisSpieltag = 17;
                        else
                            BisSpieltag = 15;
                    }

                    if (Tabart == (int)Globals.Tabart.Rückrunde)
                    {
                    if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) < 2020 || Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2023)
                        VonSpieltag = 18;
                        else
                            VonSpieltag = 16;


                        int iAktSpieltag = 0;
                        if (bAbgeschlossen)
                        {
                            iAktSpieltag = Globals.maxSpieltag;
                        }
                        else
                        {
                            iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                        }
                        BisSpieltag = iAktSpieltag;
                    }

                    // Grundtabelle erzeugen
                    foreach (VereineSaisonAus verein in VereineSaison)
                    {

                        tabelleneintragV1 = new Tabelle();

                        tabelleneintragV1.VereinNr = verein.VereinNr;
                        tabelleneintragV1.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname1;
                        tabelleneintragV1.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(verein.VereinNr)).Vereinsname2;
                        tabelleneintragV1.TorePlus = 0;
                        tabelleneintragV1.ToreMinus = 0;
                        tabelleneintragV1.Spiele = 0;
                        tabelleneintragV1.Punkte = 0;
                        tabelleneintragV1.Gewonnen = 0;
                        tabelleneintragV1.Untentschieden = 0;
                        tabelleneintragV1.Verloren = 0;
                        tabelleneintragV1.Platz = 0;
                        tabelleneintragV1.Tore = "0";
                        tabelleneintragV1.Diff = 0;
                        tabelleneintragV1.Tab_Sai_Id = Globals.SaisonID;
                        tabelleneintragV1.Tab_Lig_Id = Globals.LigaID;
                        tabelleneintragV1.Liga = Globals.currentLiga;

                        TabSaisonSorted.Add(tabelleneintragV1);
                    }

                    for (int i = VonSpieltag; i <= BisSpieltag; i++)
                    {

                        this.Spieltag = (alleSpieltage).Where(st => st.Saison == sSaison && st.LigaID == LigaId && st.SpieltagNr == i.ToString()).ToList();

                        foreach (var item in this.Spieltag)
                        {
                            Tabelle tabelleneintragF = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein1_Nr));
                            Tabelle tabelleneintragF2 = TabSaisonSorted.FirstOrDefault(element => element.VereinNr == Convert.ToInt32(item.Verein2_Nr));

                            tabelleneintragV1 = new Tabelle();
                            tabelleneintragV2 = new Tabelle();

                            if ((tabelleneintragF != null) && (tabelleneintragF2 != null))
                            {
                                if (item.Tore1_Nr > item.Tore2_Nr)
                                {
                                    if (Tabart != (int)Globals.Tabart.Auswärts)
                                    {
                                        tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                        tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                        tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                        tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                        tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                        tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                        tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen + 1;
                                        tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                        tabelleneintragF.Verloren = tabelleneintragF.Verloren;

                                        tabelleneintragF.Punkte = tabelleneintragF.Punkte + 3;


                                        tabelleneintragF.Platz = 0;
                                        tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                        tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                        tabelleneintragF.Liga = Globals.currentLiga;
                                    }

                                    if (Tabart != (int)Globals.Tabart.Heim)
                                    {
                                        tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                        tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                        tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                        tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                        tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                        tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                        tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                        tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                        tabelleneintragF2.Verloren = tabelleneintragF2.Verloren + 1;
                                        tabelleneintragF2.Punkte = tabelleneintragF2.Punkte;
                                        tabelleneintragF2.Platz = 0;
                                        tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                        tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                        tabelleneintragF2.Liga = Globals.currentLiga;
                                    }
                                }
                                else if (item.Tore1_Nr == item.Tore2_Nr)
                                {
                                    if (Tabart != (int)Globals.Tabart.Auswärts)
                                    {
                                        tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                        tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                        tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                        tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                        tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                        tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                        tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                        tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden + 1;
                                        tabelleneintragF.Verloren = tabelleneintragF.Verloren;
                                        tabelleneintragF.Punkte = tabelleneintragF.Punkte + 1;
                                        tabelleneintragF.Platz = 0;
                                        tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                        tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                        tabelleneintragF.Liga = Globals.currentLiga;
                                    }

                                    if (Tabart != (int)Globals.Tabart.Heim)
                                    {
                                        tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                        tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                        tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                        tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                        tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                        tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                        tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen;
                                        tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden + 1;
                                        tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;
                                        tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 1;
                                        tabelleneintragF2.Platz = 0;
                                        tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                        tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                        tabelleneintragF2.Liga = Globals.currentLiga;
                                    }
                                }
                                else if (item.Tore1_Nr < item.Tore2_Nr)
                                {
                                    if (Tabart != (int)Globals.Tabart.Auswärts)
                                    {
                                        tabelleneintragF.VereinNr = Convert.ToInt32(item.Verein1_Nr);
                                        tabelleneintragF.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname1;
                                        tabelleneintragF.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF.VereinNr)).Vereinsname2;
                                        tabelleneintragF.TorePlus = tabelleneintragF.TorePlus + item.Tore1_Nr;
                                        tabelleneintragF.ToreMinus = tabelleneintragF.ToreMinus + item.Tore2_Nr;
                                        tabelleneintragF.Spiele = tabelleneintragF.Spiele + 1;
                                        tabelleneintragF.Gewonnen = tabelleneintragF.Gewonnen;
                                        tabelleneintragF.Untentschieden = tabelleneintragF.Untentschieden;
                                        tabelleneintragF.Verloren = tabelleneintragF.Verloren + 1;
                                        tabelleneintragF.Punkte = tabelleneintragF.Punkte;
                                        tabelleneintragF.Platz = 0;
                                        tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                        tabelleneintragF.Tab_Sai_Id = Globals.SaisonID;
                                        tabelleneintragF.Liga = Globals.currentLiga;
                                    }
                                    if (Tabart != (int)Globals.Tabart.Heim)
                                    {
                                        tabelleneintragF2.VereinNr = Convert.ToInt32(item.Verein2_Nr);
                                        tabelleneintragF2.Verein = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname1;
                                        tabelleneintragF2.Anzeigename = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(tabelleneintragF2.VereinNr)).Vereinsname2;
                                        tabelleneintragF2.TorePlus = tabelleneintragF2.TorePlus + item.Tore2_Nr;
                                        tabelleneintragF2.ToreMinus = tabelleneintragF2.ToreMinus + item.Tore1_Nr;
                                        tabelleneintragF2.Spiele = tabelleneintragF2.Spiele + 1;
                                        tabelleneintragF2.Gewonnen = tabelleneintragF2.Gewonnen + 1;
                                        tabelleneintragF2.Untentschieden = tabelleneintragF2.Untentschieden;
                                        tabelleneintragF2.Verloren = tabelleneintragF2.Verloren;

                                        tabelleneintragF2.Punkte = tabelleneintragF2.Punkte + 3;
                                    }

                                    tabelleneintragF2.Platz = 0;
                                    tabelleneintragF2.Tab_Lig_Id = Globals.LigaID;
                                    tabelleneintragF2.Tab_Sai_Id = Globals.SaisonID;
                                    tabelleneintragF2.Liga = Globals.currentLiga;
                                }

                                if (Globals.SaisonID == 44 && tabelleneintragV2.Verein == "Arminia Bielefeld")
                                {
                                    tabelleneintragF2.TorePlus = 0;
                                    tabelleneintragF2.Platz = 18;
                                    tabelleneintragF2.Punkte = 0;
                                    tabelleneintragF2.TorePlus = 0;
                                    tabelleneintragF2.ToreMinus = 0;
                                }
                            }
                            else
                            {
                                ErrorLogger.WriteToErrorLog("Fehler", "Fehler", Assembly.GetExecutingAssembly().FullName);
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
