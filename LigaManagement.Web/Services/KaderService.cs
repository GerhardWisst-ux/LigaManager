using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using LigaManagement.Web.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class KaderService : IKaderService
    {
        private readonly HttpClient httpClient;

        public KaderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
              
        public async Task<Kader> CreateSpieler(Kader newSpieler)
        {
            return await httpClient.PostJsonAsync<Kader>("api/Kader", newSpieler);
        }

        public async Task DeleteSpieler(int id)
        {
            await httpClient.DeleteAsync($"api/Kader/{id}"); ;
        }

        public async Task<IEnumerable<Kader>> GetAllSpieler()
        {
            return await httpClient.GetJsonAsync<Kader[]>("api/Kader");
        }

        public async Task<Kader> GetSpieler(int id)
        {       
            return await httpClient.GetJsonAsync<Kader>($"api/Kader/{id}");
        }

        public async Task<Kader> UpdateSpieler(Kader updatedSpieler)
        {
            return await httpClient.PutJsonAsync<Kader>("api/Kader", updatedSpieler);
        }

        



    }
}
