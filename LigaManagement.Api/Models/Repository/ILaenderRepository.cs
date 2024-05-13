using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ILaenderRepository
    {
        Task<IEnumerable<Land>> GetLaender();
        Task<Land> GetLand(int landId);
        Task<Land> AddLand(Land landId);
        Task<Land> UpdateLand(Land landId);
        Task<Land> DeleteLand(int landId);
    }
}
