using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class SpieltagBEService : ISpieltageBEService

    {
        private readonly HttpClient httpClient;
        public int TotalCount { get; set; }
        public SpieltagBEService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Spieltag> GetSpieltag(int id)
        {
<<<<<<< HEAD
            return await httpClient.GetJsonAsync<Spieltag>($"api/spieltageBE/{id}");
=======
            return await httpClient.GetJsonAsync<Spieltag>($"api/SpieltageBE/{id}");
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
        }

        public async Task<IEnumerable<Spieltag>> GetSpieltage()
        {
            try
            {
<<<<<<< HEAD
                return await httpClient.GetJsonAsync<Spieltag[]>("api/spieltageBE");
=======
                return await httpClient.GetJsonAsync<Spieltag[]>("api/SpieltageBE");
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
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
<<<<<<< HEAD
                return await httpClient.GetJsonAsync<Spielergebnisse[]>("api/spieltageBE");
=======
                return await httpClient.GetJsonAsync<Spielergebnisse[]>("api/SpieltageBE");
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
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
<<<<<<< HEAD
                return await httpClient.PostJsonAsync<Spieltag>("api/spieltageBE", spieltag);
=======
                return await httpClient.PostJsonAsync<Spieltag>("api/SpieltageBE", spieltag);
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<Spieltag> UpdateSpieltag(Spieltag updatedSpieltag)
        {
<<<<<<< HEAD
            return await httpClient.PutJsonAsync<Spieltag>("api/spieltageBE", updatedSpieltag);
=======
            return await httpClient.PutJsonAsync<Spieltag>("api/SpieltageBE", updatedSpieltag);
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
        }

        public async Task DeleteSpieltag(int? id)
        {
<<<<<<< HEAD
            await httpClient.DeleteAsync($"api/spieltageBE/{id}");
=======
            await httpClient.DeleteAsync($"api/SpieltageBE/{id}");
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
        }
     
    }


}
