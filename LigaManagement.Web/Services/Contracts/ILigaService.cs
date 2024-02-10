using LigaManagement.Models;
using LigaManagerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ILigaService
    {
        Task<IEnumerable<Liga>> GetLigen();
        Task<Liga> GetLiga(int id);        
        Task<Liga> UpdateLiga(Liga updatedLiga);
        Task<Liga> CreateLiga(Liga newLiga);
        Task DeleteLiga(int id);
        
    }
}
