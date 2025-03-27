using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ISpielplaeneRepository
    {
        Task<IEnumerable<Spielplan>> GetSpielplaene();        
        Task<IEnumerable<Spielplan>> GetSpielplaeneL3();
               
        Task<Spielplan> GetSpielplan(int SpielplanId);
        Task<Spielplan> GetSpielplanL3(int SpielplanId);

        Task<Spielplan> AddSpielplan(Spielplan Spielplan);

        Task<Spielplan> AddSpielplanL3(Spielplan Spielplan);

        Task<Spielplan> UpdateSpielplan(Spielplan Spielplan);

        Task<Spielplan> UpdateSpielplanL3(Spielplan Spielplan);

        Task<Spielplan> DeleteSpielplan(int SpielplanId);

        Task<Spielplan> DeleteSpielplanL3(int SpielplanId);
    }
}
