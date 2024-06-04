using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ISpieltageCLRepository
    {
        Task<IEnumerable<PokalergebnisCLSpieltag>> GetSpieltage();
        Task<List<Verein>> GetVereine(int GroupID);
        Task<PokalergebnisCLSpieltag> GetSpieltag(int spieltagId);        
        Task<PokalergebnisCLSpieltag> AddSpieltag(PokalergebnisCLSpieltag Spieltag);
        Task<PokalergebnisCLSpieltag> UpdateSpieltag(PokalergebnisCLSpieltag Spieltag);
        Task<PokalergebnisCLSpieltag> DeleteSpieltag(int SpieltagId);
    }
}
