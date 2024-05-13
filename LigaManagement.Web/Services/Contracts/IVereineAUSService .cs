using LigaManagement.Models;
using LigaManagerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IVereineAusService
    {
        Task<IEnumerable<VereinAUS>> GetVereinePL();
        Task<IEnumerable<VereinAUS>> GetVereineIT();
        Task<IEnumerable<VereinAUS>> GetVereineFR();
        Task<IEnumerable<VereinAUS>> GetVereineES();
        Task<IEnumerable<VereinAUS>> GetVereineNL();
        Task<IEnumerable<VereinAUS>> GetVereinePT();
        Task<IEnumerable<VereinAUS>> GetVereineTU();
        Task<VereinAUS> GetVereinPL(int id);        
        Task<VereinAUS> GetVereinFR(int id);        
        Task<VereinAUS> GetVereinIT(int id);
        Task<VereinAUS> GetVereinES(int id);
        Task<VereinAUS> GetVereinNL(int id);
        Task<VereinAUS> GetVereinTU(int id);
        Task<VereinAUS> GetVereinPT(int id);
        Task<VereinAUS> UpdateVerein(VereinAUS updatedVerein);
        Task<VereinAUS> CreateVerein(VereinAUS newVerein);
        Task DeleteVerein(int id);
        Task<IEnumerable<VereinAktSaisonAUS>> GetVereineSaison(int saisonid);
        Task<List<VereineSaison>> CreateVereineSaison(List<VereineSaison> vereine);
        
    }
}
