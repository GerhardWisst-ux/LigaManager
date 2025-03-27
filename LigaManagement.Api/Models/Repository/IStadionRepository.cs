using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface IStadionRepository
    {
        Task<IEnumerable<Stadion>> GetStadien();
        Task<Stadion> GetStadion(int StadionIdId);
        Task<Stadion> AddStadion(Stadion StadionId);
        Task<Stadion> UpdateStadion(Stadion StadionId);
        Task<Stadion> DeleteStadion(int StadionIdId);
    }
}
