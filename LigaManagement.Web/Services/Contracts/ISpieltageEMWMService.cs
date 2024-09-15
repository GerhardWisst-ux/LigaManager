using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ISpieltageEMWMService
    {
        Task<IEnumerable<PokalergebnisCL_EM_WMSpieltag>> GetSpieltage();
        Task<IEnumerable<PokalergebnisCL_EM_WMSpieltag>> GetSpielergebnisse();
        Task<PokalergebnisCL_EM_WMSpieltag> GetSpieltag(int id);
        Task<PokalergebnisCL_EM_WMSpieltag> UpdateSpieltag(PokalergebnisCL_EM_WMSpieltag updatedSpieltag);
        Task<PokalergebnisCL_EM_WMSpieltag> CreateSpieltag(PokalergebnisCL_EM_WMSpieltag newSpieltag);
        Task DeleteSpieltag(int? id);        
    }
}
