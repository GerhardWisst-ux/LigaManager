using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ISpieltageCLService
    {
        Task<IEnumerable<PokalergebnisCLSpieltag>> GetSpieltage();
        Task<IEnumerable<PokalergebnisCLSpieltag>> GetSpielergebnisse();
        Task<PokalergebnisCLSpieltag> GetSpieltag(int id);
        Task<PokalergebnisCLSpieltag> UpdateSpieltag(PokalergebnisCLSpieltag updatedSpieltag);
        Task<PokalergebnisCLSpieltag> CreateSpieltag(PokalergebnisCLSpieltag newSpieltag);
        Task DeleteSpieltag(int? id);        
    }
}
