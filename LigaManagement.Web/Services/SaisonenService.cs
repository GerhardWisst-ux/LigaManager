﻿using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
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
              
        public async Task<Saison> CreateSaison(Saison newsaison)
        {
            return await httpClient.PostJsonAsync<Saison>("api/saisonen", newsaison);
        }

        public async Task DeleteSaison(int id)
        {
            await httpClient.DeleteAsync($"api/saisonen/{id}");
        }

        public async Task<Saison> GetSaison(int id)
        {
            try
            {
                return await httpClient.GetJsonAsync<Saison>($"api/saisonen/{id}");
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<Saison> GetSaisonID(string saison)
        {
            return await httpClient.GetJsonAsync<Saison>($"api/saisonen/{saison}");
        }

        public async Task<IEnumerable<Saison>> GetSaisonen()
        {
            return await httpClient.GetJsonAsync<Saison[]>("api/saisonen");
        }

        public async Task<Saison> UpdateSaison(Saison updatedsaison)
        {
            return await httpClient.PutJsonAsync<Saison>("api/saisonen", updatedsaison);
        }

    }
}
