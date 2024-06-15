using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class SaisonenCLService : ISaisonenCLService
    {
        private readonly HttpClient httpClient;

        public SaisonenCLService(HttpClient httpClient)
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
                return await httpClient.GetJsonAsync<Saison>($"api/saisonenCL/{id}");
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<Saison> GetSaisonID(string saison)
        {
            return await httpClient.GetJsonAsync<Saison>($"api/saisonenCL/{saison}");
        }

        public async Task<IEnumerable<Saison>> GetSaisonen()
        {
            return await httpClient.GetJsonAsync<Saison[]>("api/saisonenCL");
        }

        public async Task<Saison> UpdateSaison(Saison updatedsaison)
        {
            return await httpClient.PutJsonAsync<Saison>("api/saisonenCL", updatedsaison);
        }

        public async Task<Saison> GetSaisonWMEM(int id)
        {
            try
            {
                return await httpClient.GetJsonAsync<Saison>($"api/saisonenEMWM/{id}");
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return null;
            }
        }
    }
}
