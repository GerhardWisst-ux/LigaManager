
using LigaManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface IVereineSaisonRepository
    {
        Task<IEnumerable<VereineSaison>> GetVereineSaison();
        Task<IEnumerable<VereinAktSaison>> GetVereineAktSaison();
        //Task<Verein> GetVerein(int Id);
        //Task<Verein> AddVerein(Verein Verein);
        //Task<Verein> UpdateVerein(Verein Verein);
        Task<VereineSaison> DeleteVereineSaison(int saisonId);
        Task<List<VereineSaison>> AddVereineSaison(List<VereineSaison> Vereine);
    }
}
