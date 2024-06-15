using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class VereineService : IVereineService

    {
        private readonly HttpClient httpClient;

        public VereineService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Verein> CreateVerein(Verein newVerein)
        {
            return await httpClient.PostJsonAsync<Verein>("api/vereine", newVerein);
        }

        
        public async Task<List<VereineSaison>> CreateVereineSaison(List<VereineSaison> vereine)
        {
            try
            {
                return await httpClient.PostJsonAsync<List<VereineSaison>>("api/vereinesaison", vereine);
            }
            catch (Exception ex)
            {

                Debug.Print(ex.Message);
                return null;
            }
        }

        public Task<List<Verein>> CreateVereineSaison(List<Verein> vereine)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVerein(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Verein> GetVerein(int Id)
        {
            return await httpClient.GetJsonAsync<Verein>($"api/vereine/{Id}");
        }

        public async Task<VereinAktSaison> GetVereinCL(int Id)
        {
            return await httpClient.GetJsonAsync<VereinAktSaison>($"api/vereineCL/{Id}");
        }

        public async Task<VereinAktSaison> GetVereinEMWM(int Id)
        {
            return await httpClient.GetJsonAsync<VereinAktSaison>($"api/vereineEMWM/{Id}");
        }

        public async Task<IEnumerable<Verein>> GetVereine()
        {
            return await httpClient.GetJsonAsync<Verein[]>("api/vereine");
        }

        public async Task<IEnumerable<VereinAktSaison>> GetVereineCL()
        {
            return await httpClient.GetJsonAsync<VereinAktSaison[]>("api/vereineCL");
        }

        public async Task<IEnumerable<VereinAktSaison>> GetVereineEMWM()
        {
            return await httpClient.GetJsonAsync<VereinAktSaison[]>("api/vereineEMWM");
        }
        public async Task<IEnumerable<Verein>> GetVereinePL()
        {
            return await httpClient.GetJsonAsync<Verein[]>("api/vereinePL");
        }

        public async Task<IEnumerable<VereinAktSaison>> GetVereineSaison()
        {
            return await httpClient.GetJsonAsync<List<VereinAktSaison>>($"api/vereinesaison");
        }

        public async Task<VereinAktSaison> GetVereinL3(int Id)
        {
            return await httpClient.GetJsonAsync<VereinAktSaison>($"api/vereineL3/{Id}");
        }

        public async Task<Verein> UpdateVerein(Verein updatedVerein)
        {
            return await httpClient.PutJsonAsync<Verein>("api/vereine", updatedVerein);
        }

        
    }
}
