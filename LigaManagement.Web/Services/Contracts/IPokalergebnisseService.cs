using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IPokalergebnisseService
    {
        Task<IEnumerable<PokalergebnisSpieltag>> GetPokalergebnisseSpieltag();
        Task<IEnumerable<PokalHistorieStatistik>> GetPokalergebnisseHistorie(string vereinid);
        Task<IEnumerable<PokalergebnisStatistik>> GetPokalergebnisseStatistik(bool all);
        Task<PokalergebnisSpieltag> GetPokalergebnisSpieltag(int id);        
        Task<PokalergebnisSpieltag> UpdatePokalergebnisSpieltag(PokalergebnisSpieltag updatedTor);
        Task<PokalergebnisSpieltag> CreatePokalergebnisSpieltag(PokalergebnisSpieltag newPokalergbnis);
        Task DeletePokalergebnis(int id);
        
    }
}
