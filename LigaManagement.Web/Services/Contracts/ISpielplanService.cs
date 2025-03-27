using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ISpielplanService
    {
        Task<IEnumerable<Spielplan>> GetSpielplaene();
        Task<IEnumerable<Spielplan>> GetSpielplaeneL3();
        Task<IEnumerable<VereinAktSaison>> GetVereineL3();
        Task<IEnumerable<Spielergebnisse>> GetSpielergebnisse();
        Task<Spielplan> GetSpielplan(int id);
        Task<Spielplan> GetSpielplanL3(int id);
        Task<Spielplan> UpdateSpielplan(Spielplan updatedSpielplan);
        Task<Spielplan> CreateSpielplan(Spielplan newSpielplan);
        Task<Spielplan> UpdateSpielplanL3(Spielplan updatedSpielplan);
        Task<Spielplan> CreateSpielplanL3(Spielplan newSpielplan);
        Task DeleteSpielplan(int? id);        
    }
}
