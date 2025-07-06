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
    public interface IStatistikService
    {
        Task<List<Spielergebnisse>> AnzahlSiegeSaison(int SaisonId, int VereinNr);
        Task<List<Spielergebnisse>> AnzahlUnentschiedenSaison(int SaisonId, int VereinNr);
        Task<List<Spielergebnisse>> AnzahlNiederlagenSaison(int SaisonId, int VereinNr);

    }
}
