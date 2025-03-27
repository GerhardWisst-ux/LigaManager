
using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IStadionService
    {
        Task<IEnumerable<Stadion>> GetStadien();
        Task<Stadion> GetStadion(int id);        
        Task<Stadion> UpdateStadion(Stadion updateStadion);
        Task<Stadion> CreateStadion(Stadion newStadion);
        Task DeleteStadion(int id);
        
    }
}
