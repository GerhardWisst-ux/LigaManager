using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class SpieltagEMWMService : ISpieltageEMWMService

    {
        private readonly HttpClient httpClient;
        public int TotalCount { get; set; }
        public SpieltagEMWMService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PokalergebnisCLSpieltag> GetSpieltag(int id)
        {

            
            return await httpClient.GetJsonAsync<PokalergebnisCLSpieltag>($"api/SpieltageEMWM/{id}");

        }

        public async Task<IEnumerable<PokalergebnisCLSpieltag>> GetSpieltage()
        {
            try
            {
                return await httpClient.GetJsonAsync<PokalergebnisCLSpieltag[]>("api/SpieltageEMWM");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<PokalergebnisCLSpieltag>> GetPokalergebnisCLSpieltag()
        {
            try
            {

                return await httpClient.GetJsonAsync<PokalergebnisCLSpieltag[]>("api/SpieltageEMWM");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<PokalergebnisCLSpieltag> CreateSpieltag(PokalergebnisCLSpieltag spieltag)
        {
            try
            {
                return await httpClient.PostJsonAsync<PokalergebnisCLSpieltag>("api/SpieltageEMWM", spieltag);

            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<PokalergebnisCLSpieltag> UpdateSpieltag(PokalergebnisCLSpieltag updatedSpieltag)
        {

            return await httpClient.PutJsonAsync<PokalergebnisCLSpieltag>("api/SpieltageEMWM", updatedSpieltag);

        }

        public async Task DeleteSpieltag(int? id)
        {
            await httpClient.DeleteAsync($"api/SpieltageEMWM/{id}");

        }

        public async Task<IEnumerable<PokalergebnisCLSpieltag>> GetSpielergebnisse()
        {

            try
            {
                return await httpClient.GetJsonAsync<PokalergebnisCLSpieltag[]>("api/SpieltageEMWM");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }
    }


}
