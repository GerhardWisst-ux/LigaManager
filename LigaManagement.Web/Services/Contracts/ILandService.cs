using LigaManagement.Models;
using LigaManagerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ILandService
    {
        Task<IEnumerable<Land>> GetLaender();
        Task<Land> GetLand(int id);        
        Task<Land> UpdateLand(Land updatedLand);
        Task<Land> CreateLand(Land newLand);
        Task DeleteLand(int id);
        
    }
}
