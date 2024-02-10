using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class LigaService : ILigaService
    {
        private readonly HttpClient httpClient;

        public LigaService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Liga> CreateLiga(Liga newLiga)
        {
            return await httpClient.PostJsonAsync<Liga>("api/ligen", newLiga);
        }

        public async Task DeleteLiga(int id)
        {
            await httpClient.DeleteAsync($"api/Ligas/{id}");
        }

        public async Task<Liga> GetLiga(int id)
        {
            return await httpClient.GetJsonAsync<Liga>($"api/ligen/{id}");
        }

        public async Task<IEnumerable<Liga>> GetLigen()
        {
            return await httpClient.GetJsonAsync<Liga[]>("api/ligen");
        }

        public async Task<Liga> UpdateLiga(Liga updatedLiga)
        {
            return await httpClient.PutJsonAsync<Liga>("api/ligen", updatedLiga);
        }
    }
}
