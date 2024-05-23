
using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface IVereineSaisonAusRepository
    {
        Task<List<VereineSaisonAus>> AddVereineSaison(int LigaID, int SaisonID);
        Task<IEnumerable<VereineSaisonAus>> GetVereineSaison(int saisonid);
        Task<bool> DeleteVereineSaison(int LigaID, int SaisonID);
        Task<bool> DeleteVereineAll();
    }
}
