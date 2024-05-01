
using LigaManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface IVereinRepositoryPL
    {
        Task<IEnumerable<VereinPL>> GetVereine();
        Task<VereinPL> GetVerein(int Id);
        Task<VereinPL> AddVerein(VereinPL Verein);
        Task<VereinPL> UpdateVerein(VereinPL Verein);
        Task<VereinPL> DeleteVerein(int VereinId);
        Task<IEnumerable<VereinAktSaisonPL>> GetVereineSaison();
        Task<List<VereineSaison>> AddVereineSaison(List<VereineSaison> Vereine);
    }
}
