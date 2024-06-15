using LigaManagement.Models;
using LigaManagement.Web.Models;
using LigaManagement.Web.Pages;
using Ligamanager.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static LigaManagement.Web.Pages.ChartData;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ITabelleService
    {
        Task<IEnumerable<Tabelle>> GetTabellen();
        Task<Tabelle> GetTabelle(int id);        
        Task<Tabelle> UpdateTabelle(Tabelle updatedTabelle);
        Task<Tabelle> CreateTabelle(Tabelle newTabelle);
        Task DeleteTabelle(int? id);
        Task<DateTime> GetAktSpieltag(ISpieltagService spieltagService);               

        Task<IEnumerable<Spielergebnisse>> VereinGegenVerein(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<IEnumerable<Spielergebnisse>> StatistikVerein(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<IEnumerable<Tabelle>> BerechneTabelleEwig(ISpieltagService spieltagService, ISaisonenService saisonenservice, IEnumerable<Verein> vereine, int currentspieltag, int ewigeTabelle);
        Task<Spielstatistik> VereinGegenVereinSum(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<Spielstatistik> VereinSum(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<List<int?>> CreateChart(ISpieltagService spieltagService, IEnumerable<Verein> vereine,int ivereinnr, int currentspieltag);

        Task<IEnumerable<Tabelle>> BerechneTabelleDE(ISpieltagService spieltagService, bool Abgeschlossen, List<VereineSaison> vereineAktSaison, IEnumerable<Verein> vereine, int currentspieltag, int tabart);
        Task<IEnumerable<Tabelle>> BerechneTabellePL(ISpieltageENService spieltagENService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereine, int count, int tabart);        
        Task<IEnumerable<Tabelle>> BerechneTabelleIT(ISpieltageITService spieltagITService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, int tabart);
        Task<IEnumerable<Tabelle>> BerechneTabelleFR(ISpieltageFRService spieltagFRService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, int tabart);
        Task<IEnumerable<Tabelle>> BerechneTabelleES(ISpieltageESService spieltagESService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, int tabart);
        Task<IEnumerable<Tabelle>> BerechneTabelleNL(ISpieltageNLService spieltagNLService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count,  int tabart);
        Task<IEnumerable<Tabelle>> BerechneTabellePT(ISpieltagePTService spieltagNLService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, int tabart);
        Task<IEnumerable<Tabelle>> BerechneTabelleTU(ISpieltageTUService spieltagTUService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, int tabart);
        Task<IEnumerable<Tabelle>> BerechneTabelleBE(ISpieltageBEService spieltagTUService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, int tabart);

        Task<IEnumerable<Tabelle>> BerechneTabelleCL(ISpieltageCLService spieltagService, int GroupID, int BisSpieltag);
        Task<IEnumerable<Tabelle>> BerechneTabelleDEL3(ISpieltagService spieltagService, bool bAbgeschlossen, List<VereinAktSaison> verList, int iSpieltage, int gesamt);
        Task<IEnumerable<Tabelle>> BerechneTabelleEMWM(ISpieltageEMWMService spieltagService, int count, int bisSpieltag);
    }
}
