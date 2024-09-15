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

        public async Task<PokalergebnisCL_EM_WMSpieltag> GetSpieltag(int id)
        {

            
            return await httpClient.GetJsonAsync<PokalergebnisCL_EM_WMSpieltag>($"api/SpieltageEMWM/{id}");

        }

        public async Task<IEnumerable<PokalergebnisCL_EM_WMSpieltag>> GetSpieltage()
        {
            try
            {
                return await httpClient.GetJsonAsync<PokalergebnisCL_EM_WMSpieltag[]>("api/SpieltageEMWM");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<PokalergebnisCL_EM_WMSpieltag>> GetPokalergebnisCLSpieltag()
        {
            try
            {

                return await httpClient.GetJsonAsync<PokalergebnisCL_EM_WMSpieltag[]>("api/SpieltageEMWM");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<PokalergebnisCL_EM_WMSpieltag> CreateSpieltag(PokalergebnisCL_EM_WMSpieltag spieltag)
        {
            try
            {
                return await httpClient.PostJsonAsync<PokalergebnisCL_EM_WMSpieltag>("api/SpieltageEMWM", spieltag);

            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<PokalergebnisCL_EM_WMSpieltag> UpdateSpieltag(PokalergebnisCL_EM_WMSpieltag updatedSpieltag)
        {

            return await httpClient.PutJsonAsync<PokalergebnisCL_EM_WMSpieltag>("api/SpieltageEMWM", updatedSpieltag);

        }

        public async Task DeleteSpieltag(int? id)
        {
            await httpClient.DeleteAsync($"api/SpieltageEMWM/{id}");

        }

        public async Task<IEnumerable<PokalergebnisCL_EM_WMSpieltag>> GetSpielergebnisse()
        {

            try
            {
                return await httpClient.GetJsonAsync<PokalergebnisCL_EM_WMSpieltag[]>("api/SpieltageEMWM");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }
    }


}
