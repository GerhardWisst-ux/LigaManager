using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using LigaManagement.Web.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LigaManagement.Models;

namespace LigaManagerManagement.Web.Services
{
    public class SpielerSpieltagService : ISpielerSpieltagService
    {
        private readonly HttpClient httpClient;

        public SpielerSpieltagService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SpielerSpieltag> CreateSpieler(SpielerSpieltag newSpieler)
        {
            return await httpClient.PostJsonAsync<SpielerSpieltag>("api/spielerspieltag", newSpieler);
        }

        public Task DeleteSpieler(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SpielerSpieltag>> GetAllSpieler()
        {
            throw new NotImplementedException();
        }

        public Task<SpielerSpieltag> GetSpieler(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SpielerSpieltag> UpdateSpieler(SpielerSpieltag updatedSpieler)
        {
            return await httpClient.PutJsonAsync<SpielerSpieltag>("api/spielerspieltag", updatedSpieler);
        }
    }
}
