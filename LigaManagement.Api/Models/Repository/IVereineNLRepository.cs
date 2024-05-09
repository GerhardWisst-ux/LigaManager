
using LigaManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface IVereineNLRepository
    {
        Task<IEnumerable<VereinAUS>> GetVereine();
        Task<VereinAUS> GetVerein(int Id);
        Task<VereinAUS> AddVerein(VereinAUS Verein);
        Task<VereinAUS> UpdateVerein(VereinAUS Verein);
        Task<VereinAUS> DeleteVerein(int VereinId);
        Task<IEnumerable<VereinAktSaisonAUS>> GetVereineSaison();
        Task<List<VereineSaison>> AddVereineSaison(List<VereineSaison> Vereine);
    }
}
