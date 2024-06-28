using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ISpieltagRepositoryLE
    {
        Task<IEnumerable<Spieltag>> GetSpieltage();                
    }
}
