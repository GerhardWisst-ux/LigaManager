using LigaManagement.Models;
using LigaManagerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IVereineAusService
    {
<<<<<<< HEAD
        Task<IEnumerable<VereinAUS>> GetVereineBE();
=======
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
        Task<IEnumerable<VereinAUS>> GetVereinePL();
        Task<IEnumerable<VereinAUS>> GetVereineIT();
        Task<IEnumerable<VereinAUS>> GetVereineFR();
        Task<IEnumerable<VereinAUS>> GetVereineES();
        Task<IEnumerable<VereinAUS>> GetVereineNL();
        Task<IEnumerable<VereinAUS>> GetVereinePT();
        Task<IEnumerable<VereinAUS>> GetVereineTU();
<<<<<<< HEAD
        Task<VereinAUS> GetVereinBE(int id);
        Task<VereinAUS> GetVereinPL(int id);
=======
        Task<IEnumerable<VereinAUS>> GetVereineBE();
        Task<VereinAUS> GetVereinPL(int id);        
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
        Task<VereinAUS> GetVereinFR(int id);        
        Task<VereinAUS> GetVereinIT(int id);
        Task<VereinAUS> GetVereinES(int id);
        Task<VereinAUS> GetVereinNL(int id);
        Task<VereinAUS> GetVereinTU(int id);
        Task<VereinAUS> GetVereinPT(int id);
<<<<<<< HEAD
=======
        Task<VereinAUS> GetVereinBE(int id);
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
        Task<VereinAUS> UpdateVerein(VereinAUS updatedVerein);
        Task<VereinAUS> CreateVerein(VereinAUS newVerein);
        Task DeleteVerein(int id);
        Task<IEnumerable<VereinAktSaisonAUS>> GetVereineSaison(int saisonid);
        Task<List<VereineSaison>> CreateVereineSaison(List<VereineSaison> vereine);
        
    }
}
