using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface ISpieltagServiceLE
    {
        Task<IEnumerable<Spieltag>> GetSpieltage();        
    }
}
