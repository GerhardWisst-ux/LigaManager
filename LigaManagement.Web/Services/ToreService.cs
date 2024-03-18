
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LigaManagement.Models;

namespace ToreManagerManagement.Web.Services
{
    public class ToreService : IToreService
    {
        private readonly HttpClient httpClient;

        public ToreService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Tore> CreateTor(Tore newTore)
        {
            return await httpClient.PostJsonAsync<Tore>("api/tore", newTore); ;
        }

        public Task DeleteTor(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Tore> GetTor(int id)
        {
            return await httpClient.GetJsonAsync<Tore>($"api/tore/{id}");
        }

        public async Task<IEnumerable<Tore>> GetTore()
        {
            return await httpClient.GetJsonAsync<Tore[]>("api/tore");
        }

        public async Task<Tore> Update(Tore updatedTore)
        {
            return await httpClient.PutJsonAsync<Tore>("api/tore", updatedTore);
        }
    }
}
