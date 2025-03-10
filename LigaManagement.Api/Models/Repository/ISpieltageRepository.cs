﻿using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ISpieltageRepository
    {
        Task<IEnumerable<Spieltag>> GetSpieltage();        
        Task<IEnumerable<Spieltag>> GetSpieltageL3();
               
        Task<Spieltag> GetSpieltag(int spieltagId);
        Task<Spieltag> GetSpieltagL3(int spieltagId);

        Task<Spieltag> AddSpieltag(Spieltag Spieltag);

        Task<Spieltag> AddSpieltagL3(Spieltag Spieltag);

        Task<Spieltag> UpdateSpieltag(Spieltag Spieltag);

        Task<Spieltag> UpdateSpieltagL3(Spieltag Spieltag);

        Task<Spieltag> DeleteSpieltag(int SpieltagId);

        Task<Spieltag> DeleteSpieltagL3(int SpieltagId);
    }
}
