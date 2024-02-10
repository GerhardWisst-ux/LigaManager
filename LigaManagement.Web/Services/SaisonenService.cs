using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class SaisonenService : ISaisonenService
    {
        private readonly HttpClient httpClient;

        public SaisonenService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
              
        public async Task<Saison> CreateSaison(Saison newSaison)
        {
            return await httpClient.PostJsonAsync<Saison>("api/saisonen", newSaison);
        }

        public Task DeleteSaison(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Saison> GetSaison(int id)
        {
            return await httpClient.GetJsonAsync<Saison>($"api/saisonen/{id}");
        }

        public async Task<IEnumerable<Saison>> GetSaisonen()
        {
            return await httpClient.GetJsonAsync<Saison[]>("api/saisonen");
        }

        public async Task<Saison> UpdateSaison(Saison updatedSaison)
        {
            return await httpClient.PutJsonAsync<Saison>("api/saisonen", updatedSaison);
        }

        



    }
}
