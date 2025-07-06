using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using static LigaManagement.Web.Pages.ChartData;
using static Ligamanager.Components.Globals;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LigaManagerManagement.Web.Services
{
    public class StatistikService : IStatistikService
    {
        private readonly ISpieltagService _spieltagService;

        public StatistikService(ISpieltagService spieltagService)
        {
            _spieltagService = spieltagService;
        }

        public async Task<List<Spielergebnisse>> AnzahlNiederlagenSaison(int SaisonId, int VereinNr)
        {
            var TabSaisonSorted = new List<Spielergebnisse>();

            var alleSpieltage = (await _spieltagService.GetSpielergebnisse());

            if (alleSpieltage == null)
                return null;

            alleSpieltage = (IEnumerable<Spielergebnisse>)alleSpieltage.Where(s => ((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr < s.Tore2_Nr) || (s.Verein2_Nr == VereinNr.ToString() && s.Tore1_Nr > s.Tore2_Nr) &&
            s.LigaID == 1).GroupBy(s => s.Saison).
            Select(group => new
            {
                Saison = group.Key,
                Unentschieden = group.Count()
            })
            .OrderByDescending(x => x.Unentschieden)
            .ThenByDescending(x => x.Saison)
            .ToList();

            return (List<Spielergebnisse>)alleSpieltage;
        }

        public async Task<List<Spielergebnisse>> AnzahlSiegeSaison(int SaisonId, int VereinNr)
        {
            var TabSaisonSorted = new List<Spielergebnisse>();

            var alleSpieltage = (await _spieltagService.GetSpielergebnisse());

            if (alleSpieltage == null)
                return null;

            alleSpieltage = (IEnumerable<Spielergebnisse>)alleSpieltage.Where(s => ((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr == s.Tore2_Nr) || (s.Verein2_Nr == VereinNr.ToString() && s.Tore1_Nr == s.Tore2_Nr) &&
            s.LigaID == 1).GroupBy(s => s.Saison).
            Select(group => new
            {
                Saison = group.Key,
                Unentschieden = group.Count()
            })
            .OrderByDescending(x => x.Unentschieden)
            .ThenByDescending(x => x.Saison)
            .ToList();

            return (List<Spielergebnisse>)alleSpieltage;
        }

        public async Task<List<Spielergebnisse>> AnzahlUnentschiedenSaison(int SaisonId, int VereinNr)
        {
            var TabSaisonSorted = new List<Spielergebnisse>();

            var alleSpieltage = (await _spieltagService.GetSpielergebnisse());

            if (alleSpieltage == null)
                return null;

            alleSpieltage = (IEnumerable<Spielergebnisse>)alleSpieltage.Where(s => ((s.Verein1_Nr == VereinNr.ToString()) && s.Tore1_Nr > s.Tore2_Nr) || (s.Verein2_Nr == VereinNr.ToString() && s.Tore1_Nr < s.Tore2_Nr) &&
            s.LigaID == 1).GroupBy(s => s.Saison).
            Select(group => new
            {
                Saison = group.Key,
                Unentschieden = group.Count()
            })
            .OrderByDescending(x => x.Unentschieden)
            .ThenByDescending(x => x.Saison)
            .ToList();

            return (List<Spielergebnisse>)alleSpieltage;
        }
    }
}

    
