﻿using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface ISpieltagRepository
    {
        Task<IEnumerable<Spieltag>> GetSpieltage();
        Task<Spieltag> GetSpieltag(int spieltagId);
        Task<Spieltag> GetAktSpieltag();
        Task<Spieltag> AddSpieltag(Spieltag Spieltag);
        Task<Spieltag> UpdateSpieltag(Spieltag Spieltag);
        Task<Spieltag> DeleteSpieltag(int SpieltagId);
    }
}