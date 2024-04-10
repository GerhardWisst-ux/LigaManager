using System.Collections.Generic;
using System.Threading.Tasks;
using LigaManagement.Models;

namespace ToremanagerManagement.Api.Models.Repository
{
    public interface IPokalergebnisseRepository
    {
        Task<IEnumerable<PokalergebnisSpieltag>> GetPokalergebnisse();
        Task<PokalergebnisSpieltag> GetPokalergebnis(int SpieltagID);
        Task<PokalergebnisSpieltag> CreatePokalergebnis(PokalergebnisSpieltag SpieltagID);
        Task<PokalergebnisSpieltag> UpdatePokalergebnis(PokalergebnisSpieltag SpieltagID);
        Task<PokalergebnisSpieltag> DeletePokalergebnis(int SpieltagID);
    }
}
