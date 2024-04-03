using LigaManagement.Models;
using LigaManagement.Web.Models;
using Ligamanager.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<IEnumerable<Tabelle>> BerechneTabelle(ISpieltagService spieltagService, bool Abgeschlossen, IEnumerable<Verein> vereine, int currentspieltag, string currentSaison,int tabart);
        Task<IEnumerable<Spielergebnisse>> VereinGegenVerein(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<IEnumerable<Spielergebnisse>> StatistikVerein(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<IEnumerable<Tabelle>> BerechneTabelleEwig(ISpieltagService spieltagService, ISaisonenService saisonenservice, IEnumerable<Verein> vereine, int currentspieltag, string currentSaison, int ewigeTabelle);
        Task<Spielstatistik> VereinGegenVereinSum(ISpieltagService spieltagService, Spielergebnisse spiel);
        Task<Spielstatistik> VereinSum(ISpieltagService spieltagService, Spielergebnisse spiel);
    }
}
