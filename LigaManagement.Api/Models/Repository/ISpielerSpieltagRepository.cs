using LigaManagerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ISpielerSpieltagRepository
    {
        Task<IEnumerable<SpielerSpieltag>> GetAllSpieler();
        Task<SpielerSpieltag> GetSpieler(int Id);
        Task<SpielerSpieltag> AddSpieler(SpielerSpieltag Spieler);
        Task<SpielerSpieltag> UpdateSpieler(SpielerSpieltag Spieler);
        Task<SpielerSpieltag> DeleteSpieler(SpielerSpieltag SpielerId);
    }
}
