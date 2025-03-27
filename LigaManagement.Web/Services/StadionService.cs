using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace StadionManagerManagement.Web.Services
{
    public class StadionService : IStadionService
    {
        private readonly HttpClient httpClient;

        public StadionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Stadion> CreateStadion(Stadion newStadion)
        {
            return await httpClient.PostJsonAsync<Stadion>("api/Stadion", newStadion);
        }

        public async Task DeleteStadion(int id)
        {
            await httpClient.DeleteAsync($"api/Stadion/{id}");
        }

        public async Task<Stadion> GetStadion(int id)
        {
            try
            {
                return await httpClient.GetJsonAsync<Stadion>($"api/Stadion/{id}");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Stadion>> GetStadien()
        {
            try
            {
                return await httpClient.GetJsonAsync<Stadion[]>("api/Stadion");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);
                throw;
            }
        }

        public async Task<Stadion> UpdateStadion(Stadion updateStadion)
        {
            return await httpClient.PutJsonAsync<Stadion>("api/Stadion", updateStadion);
        }
    }
}
