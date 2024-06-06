
using LigaManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface IVereinRepository
    {
        Task<IEnumerable<Verein>> GetVereine();
        Task<IEnumerable<VereinAktSaison>> GetVereineL3();
        Task<Verein> GetVerein(int Id);
        Task<Verein> GetVereinL3(int Id);
        Task<Verein> AddVerein(Verein Verein);
        Task<Verein> UpdateVerein(Verein Verein);
        Task<Verein> DeleteVerein(int VereinId);
        Task<IEnumerable<VereinAktSaison>> GetVereineSaison();
        Task<List<VereineSaison>> AddVereineSaison(List<VereineSaison> Vereine);
    }
}
