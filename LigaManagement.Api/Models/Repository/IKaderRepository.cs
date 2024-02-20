using LigaManagerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface IKaderRepository
    {
        Task<IEnumerable<Kader>> GetAllSpieler();
        Task<Kader> GetSpieler(int Id);
        Task<Kader> AddSpieler(Kader Spieler);
        Task<Kader> UpdateSpieler(Kader Spieler);
        Task<Kader> DeleteSpieler(int SpielerId);
    }
}
