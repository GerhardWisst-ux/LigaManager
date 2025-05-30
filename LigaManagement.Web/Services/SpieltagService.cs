using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class SpieltagService : ISpieltagService
    {
        private string URL => "https://services.odata.org/Northwind/Northwind.svc/";

        private readonly HttpClient httpClient;
        public int TotalCount { get; set; }
        public SpieltagService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Spieltag> GetSpieltag(int id)
        {
            return await httpClient.GetJsonAsync<Spieltag>($"api/spieltage/{id}");
        }

        public async Task<Spieltag> GetSpieltagL3(int id)
        {
            return await httpClient.GetJsonAsync<Spieltag>($"api/spieltageL3/{id}");
        }

        public async Task<IEnumerable<Spieltag>> GetSpieltage()
        {
            try
            {
                return await httpClient.GetJsonAsync<Spieltag[]>("api/spieltage/GetSpieltage");
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
                return await httpClient.GetJsonAsync<Spielergebnisse[]>("api/spieltage");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<int> GetSpieltageCount()
        {
            try
            {
                return await httpClient.GetJsonAsync<int>("api/spieltage/GetSpieltageCount");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return 0;
            }
        }

        public async Task<Spieltag> CreateSpieltag(Spieltag spiel)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/spieltage", spiel);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Spieltag>();
            }
            catch (HttpRequestException ex)
            {
                
                Debug.Print($"HTTP Error: {ex.Message}");
                return null;
            }
            catch (System.Exception ex)
            {
                Debug.Print($"General Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Spieltag> UpdateSpieltag(Spieltag updatedSpieltag)
        {
            return await httpClient.PutJsonAsync<Spieltag>("api/spieltage", updatedSpieltag);
        }

        public async Task DeleteSpieltag(int? id)
        {
            await httpClient.DeleteAsync($"api/spieltage/{id}/{Globals.LigaNummer}");
        }

        public async Task<IEnumerable<Spieltag>> GetSpieltageL3()
        {
            try
            {
                return await httpClient.GetJsonAsync<Spieltag[]>("api/spieltageL3");
                
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<VereinAktSaison>> GetVereineL3()
        {
            try
            {
                return await httpClient.GetJsonAsync<VereinAktSaison[]>("api/VereineL3");

            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<Spieltag> UpdateSpieltagL3(Spieltag updatedSpieltag)
        {
            return await httpClient.PutJsonAsync<Spieltag>("api/spieltageL3", updatedSpieltag);
        }

        public async Task<Spieltag> CreateSpieltagL3(Spieltag newSpieltag)
        {
            try
            {
                return await httpClient.PostJsonAsync<Spieltag>("api/spieltageL3", newSpieltag);
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }
    }


}
