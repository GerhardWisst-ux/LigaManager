using System.Collections.Generic;
using System.Threading.Tasks;
using LigaManagement.Models;

namespace ToremanagerManagement.Api.Models.Repository
{
    public interface IToreRepository
    {
        Task<IEnumerable<Tore>> GetTore();
        Task<Tore> GetTor(int ToreId);
        Task<Tore> AddTore(Tore ToreId);
        Task<Tore> UpdateTore(Tore ToreId);
        Task<Tore> DeleteTor(int ToreIdId);
    }
}
