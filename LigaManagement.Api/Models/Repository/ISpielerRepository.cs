using LigaManagerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ISpielerRepository
    {
        Task<IEnumerable<Spieler>> GetAllSpieler();
        Task<Spieler> GetSpieler(int Id);
        Task<Spieler> AddSpieler(Spieler Spieler);
        Task<Spieler> UpdateSpieler(Spieler Spieler);
        Task<Spieler> DeleteSpieler(int SpielerId);
    }
}
