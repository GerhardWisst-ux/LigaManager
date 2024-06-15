using LigaManagerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ISaisonenCLRepository
    {
        Task<IEnumerable<Saison>> GetSaisonen();
        Task<Saison> GetSaison(int SaisonId);

        Task<Saison> GetSaisonEMWM(int SaisonId);

        Task<Saison> GetSaisonID(string saison);
        Task<Saison> AddSaison(Saison Saison);
        Task<Saison> UpdateSaison(Saison Saison);
        Task<Saison> DeleteSaison(int SaisonId);
    }
}
