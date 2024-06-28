using System.Collections.Generic;
using System.Threading.Tasks;
using LigaManagement.Models;

namespace ToremanagerManagement.Api.Models.Repository
{
    public interface IToreRepository
    {
        Task<IEnumerable<Tore>> GetTore();
        Task<Tore> GetTor(int ToreId);
        Task<Tore> CreateTor(Tore ToreId);
        Task<Tore> UpdateTor(Tore ToreId);
        Task<Tore> DeleteTor(int ToreIdId);
    }
}
