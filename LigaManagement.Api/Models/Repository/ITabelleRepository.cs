using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ITabelleRepository
    {
        Task<IEnumerable<Tabelle>> GetTabellen();
        Task<Tabelle> GetTabelle(int TabelleId);
        Task<Tabelle> AddTabelle(Tabelle Tabelle);
        Task<Tabelle> UpdateTabelle(Tabelle Tabelle);
        Task<Tabelle> DeleteTabelle(int TabelleId);
    }
}
