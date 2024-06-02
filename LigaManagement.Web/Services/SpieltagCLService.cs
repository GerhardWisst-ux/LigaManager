using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class SpieltagCLService : ISpieltageCLService

    {
        private readonly HttpClient httpClient;
        public int TotalCount { get; set; }
        public SpieltagCLService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PokalergebnisCLSpieltag> GetSpieltag(int id)
        {

            
            return await httpClient.GetJsonAsync<PokalergebnisCLSpieltag>($"api/SpieltageCL/{id}");

        }

        public async Task<IEnumerable<PokalergebnisCLSpieltag>> GetSpieltage()
        {
            try
            {
                return await httpClient.GetJsonAsync<PokalergebnisCLSpieltag[]>("api/spieltageCL");
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

                return await httpClient.GetJsonAsync<PokalergebnisCLSpieltag[]>("api/SpieltageCL");
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
                return await httpClient.PostJsonAsync<PokalergebnisCLSpieltag>("api/spieltageCL", spieltag);

            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<PokalergebnisCLSpieltag> UpdateSpieltag(PokalergebnisCLSpieltag updatedSpieltag)
        {

            return await httpClient.PutJsonAsync<PokalergebnisCLSpieltag>("api/SpieltageCL", updatedSpieltag);

        }

        public async Task DeleteSpieltag(int? id)
        {
            await httpClient.DeleteAsync($"api/SpieltageCL/{id}");

        }

        public async Task<IEnumerable<PokalergebnisCLSpieltag>> GetSpielergebnisse()
        {

            try
            {
                return await httpClient.GetJsonAsync<PokalergebnisCLSpieltag[]>("api/spieltageCL");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }
    }


}
