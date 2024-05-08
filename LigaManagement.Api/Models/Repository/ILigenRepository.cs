using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ILigenRepository
    {
        Task<IEnumerable<Liga>> GetLigen();
        Task<Liga> GetLiga(int ligaIdId);
        Task<Liga> AddLiga(Liga ligaId);
        Task<Liga> UpdateLiga(Liga ligaId);
        Task<Liga> DeleteLiga(int ligaIdId);
    }
}
