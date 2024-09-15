using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ISpieltageEMWMRepository
    {
        Task<IEnumerable<PokalergebnisCL_EM_WMSpieltag>> GetSpieltage();
        Task<List<VereinEMWM>> GetVereine(int GroupID);
        Task<PokalergebnisCL_EM_WMSpieltag> GetSpieltag(int spieltagId);        
        Task<PokalergebnisCL_EM_WMSpieltag> AddSpieltag(PokalergebnisCL_EM_WMSpieltag Spieltag);
        Task<PokalergebnisCL_EM_WMSpieltag> UpdateSpieltag(PokalergebnisCL_EM_WMSpieltag Spieltag);
        Task<PokalergebnisCL_EM_WMSpieltag> DeleteSpieltag(int SpieltagId);
    }
}
