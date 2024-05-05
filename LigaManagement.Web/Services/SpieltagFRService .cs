using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class SpieltagFRService : ISpieltagFRService

    {
        private readonly HttpClient httpClient;
        public int TotalCount { get; set; }
        public SpieltagFRService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Spieltag> GetSpieltag(int id)
        {
            return await httpClient.GetJsonAsync<Spieltag>($"api/spieltageFR/{id}");
        }

        public async Task<IEnumerable<Spieltag>> GetSpieltage()
        {
            try
            {
                return await httpClient.GetJsonAsync<Spieltag[]>("api/spieltageFR");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<Spielergebnisse>> GetSpielergebnisse()
        {
            try
            {
                return await httpClient.GetJsonAsync<Spielergebnisse[]>("api/spieltageFR");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<Spieltag> CreateSpieltag(Spieltag spieltag)
        {
            try
            {
                return await httpClient.PostJsonAsync<Spieltag>("api/spieltageFR", spieltag);
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<Spieltag> UpdateSpieltag(Spieltag updatedSpieltag)
        {
            return await httpClient.PutJsonAsync<Spieltag>("api/spieltageFR", updatedSpieltag);
        }

        public async Task DeleteSpieltag(int? id)
        {
            await httpClient.DeleteAsync($"api/spieltageFR/{id}");
        }
     
    }


}
