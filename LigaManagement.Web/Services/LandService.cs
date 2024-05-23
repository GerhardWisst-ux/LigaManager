using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class LandService : ILandService
    {
        private readonly HttpClient httpClient;

        public LandService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Land> CreateLand(Land newLand)
        {
            return await httpClient.PostJsonAsync<Land>("api/laender", newLand);
        }

        public async Task DeleteLand(int id)
        {
            await httpClient.DeleteAsync($"api/laender/{id}");
        }

        public async Task<Land> GetLand(int id)
        {
            return await httpClient.GetJsonAsync<Land>($"api/laender/{id}");
        }

        public async Task<IEnumerable<Land>> GetLaender()
        {
            try
            {
<<<<<<< HEAD
                return await httpClient.GetJsonAsync<Land[]>("api/laender");
=======
                try
                {
                    return await httpClient.GetJsonAsync<Land[]>("api/laender");
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);
                return null;
            }
        }

        public async Task<Land> UpdateLand(Land updatedLand)
        {
            return await httpClient.PutJsonAsync<Land>("api/laender", updatedLand);
        }
    }
}
