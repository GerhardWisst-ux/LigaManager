using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IVereineBEService
    {
        Task<IEnumerable<VereinAUS>> GetVereine();
        Task<VereinAUS> GetVerein(int id);        
        Task<VereinAUS> UpdateVerein(VereinAUS updatedVerein);
        Task<VereinAUS> CreateVerein(VereinAUS newVerein);
        Task DeleteVerein(int id);
        Task<IEnumerable<VereinAktSaisonAUS>> GetVereineSaison();
        Task<List<VereineSaison>> CreateVereineSaison(List<VereineSaison> vereine);        
    }
}
