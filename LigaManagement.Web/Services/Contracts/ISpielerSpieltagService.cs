using LigaManagerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{

    public interface ISpielerSpieltagService
    {
        Task<IEnumerable<SpielerSpieltag>> GetAllSpieler();
        Task<SpielerSpieltag> GetSpieler(int id);
        Task<SpielerSpieltag> UpdateSpieler(SpielerSpieltag updatedSpieler);
        Task<SpielerSpieltag> CreateSpieler(SpielerSpieltag newSpieler);
        Task DeleteSpieler(int id);

    }

}
