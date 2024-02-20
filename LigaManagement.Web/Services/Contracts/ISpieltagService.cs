using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ISpieltagService
    {
        Task<IEnumerable<Spieltag>> GetSpieltage();
        Task<IEnumerable<Spielergebnisse>> GetSpielergebnisse();
        Task<Spieltag> GetSpieltag(int id);
        Task<Spieltag> UpdateSpieltag(Spieltag updatedSpieltag);
        Task<Spieltag> CreateSpieltag(Spieltag newSpieltag);
        Task DeleteSpieltag(int id);        
    }
}
