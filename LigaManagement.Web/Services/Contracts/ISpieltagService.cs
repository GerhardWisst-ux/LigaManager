using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ISpieltagService
    {
        Task<IEnumerable<Spieltag>> GetSpieltage();
        Task<IEnumerable<Spieltag>> GetSpieltageL3();
        Task<IEnumerable<VereinAktSaison>> GetVereineL3();
        Task<IEnumerable<Spielergebnisse>> GetSpielergebnisse();
        Task<Spieltag> GetSpieltag(int id);
        Task<Spieltag> GetSpieltagL3(int id);
        Task<Spieltag> UpdateSpieltag(Spieltag updatedSpieltag);
        Task<Spieltag> CreateSpieltag(Spieltag newSpieltag);
        Task DeleteSpieltag(int? id);        
    }
}
