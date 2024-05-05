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
        Task<IEnumerable<Tabelle>> BerechneTabelle(ISpieltagService spieltagService, bool Abgeschlossen, IEnumerable<Verein> vereine, int currentspieltag, string currentSaison,int LigaId, int tabart);
        Task<IEnumerable<Spielergebnisse>> VereinGegenVerein(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<IEnumerable<Spielergebnisse>> StatistikVerein(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<IEnumerable<Tabelle>> BerechneTabelleEwig(ISpieltagService spieltagService, ISaisonenService saisonenservice, IEnumerable<Verein> vereine, int currentspieltag, string currentSaison, int ewigeTabelle);
        Task<Spielstatistik> VereinGegenVereinSum(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<Spielstatistik> VereinSum(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<List<int?>> CreateChart(ISpieltagService spieltagService, IEnumerable<Verein> vereine,int ivereinnr, int currentspieltag);
        Task<IEnumerable<Tabelle>> BerechneTabellePL(ISpieltagPLService spieltagService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereine, int count, string currentSaison, int ligaID, int tabart);
        Task<IEnumerable<Tabelle>> BerechneTabelleAus(ISpieltagAusService spieltagService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereine, int count, string currentSaison, int ligaID, int tabart);
        Task<IEnumerable<Tabelle>> BerechneTabelleIT(ISpieltagAusService spieltagPLService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, string currentSaison, int ligaID, int v);
        Task<IEnumerable<Tabelle>> BerechneTabelleFR(ISpieltagFRService spieltagPLService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, string currentSaison, int ligaID, int v);
        Task<IEnumerable<Tabelle>> BerechneTabelleES(ISpieltagESService spieltagPLService, bool bAbgeschlossen, IEnumerable<VereinAUS> vereineAus, int count, string currentSaison, int ligaID, int v);
    }
}
