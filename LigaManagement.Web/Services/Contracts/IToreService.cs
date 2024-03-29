using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IToreService
    {
        Task<IEnumerable<Tore>> GetTore();
        Task<Tore> GetTor(int id);        
        Task<Tore> Update(Tore updatedTor);
        Task<Tore> CreateTor(Tore newLiga);
        Task DeleteTor(int id);
        
    }
}
