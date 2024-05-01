using LigaManagement.Models;
using LigamanagerManagement.Web.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IVereineServicePL
    {
        Task<IEnumerable<VereinPL>> GetVereine();
        Task<VereinPL> GetVerein(int id);        
        Task<VereinPL> UpdateVerein(VereinPL updatedVerein);
        Task<VereinPL> CreateVerein(VereinPL newVerein);
        Task DeleteVerein(int id);
        Task<IEnumerable<VereinAktSaisonPL>> GetVereineSaison();
        Task<List<VereineSaison>> CreateVereineSaison(List<VereineSaison> vereine);        
    }
}
