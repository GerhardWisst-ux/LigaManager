using LigaManagerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ISaisonenService
    {
        Task<IEnumerable<Saison>> GetSaisonen();
        Task<Saison> GetSaison(int id);
        Task<Saison> UpdateSaison(Saison updatedSaison);
        Task<Saison> CreateSaison(Saison newSaison);
        Task DeleteSaison(int id);        
    }
}
