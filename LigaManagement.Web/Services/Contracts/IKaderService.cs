using LigaManagerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{

    public interface IKaderService
    {
        Task<IEnumerable<Kader>> GetAllSpieler();
        Task<Kader> GetSpieler(int id);
        Task<Kader> UpdateSpieler(Kader updatedSpieler);
        Task<Kader> CreateSpieler(Kader newSpieler);
        Task DeleteSpieler(int id);

    }

}
