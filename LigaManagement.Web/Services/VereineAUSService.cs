using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class VereineAUSService : IVereineAusService

    {
        private readonly HttpClient httpClient;

        public VereineAUSService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<VereinAUS> CreateVerein(VereinAUS newVerein)
        {
            return await httpClient.PostJsonAsync<VereinAUS>("api/VereineAus", newVerein);
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

        public Task<List<VereinAUS>> CreateVereineSaison(List<VereinAUS> vereine)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVerein(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<VereinAUS> GetVerein(int Id)
        {
            return await httpClient.GetJsonAsync<VereinAUS>($"api/VereineAus/{Id}");
        }

        public async Task<IEnumerable<VereinAUS>> GetVereineES()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereineES");
        }

        public async Task<IEnumerable<VereinAUS>> GetVereineFR()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereineFR");
        }

        public async Task<IEnumerable<VereinAUS>> GetVereineIT()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereineAus");
        }              

        public async Task<IEnumerable<VereinAktSaisonAUS>> GetVereineSaison()
        {
            return await httpClient.GetJsonAsync<List<VereinAktSaisonAUS>>($"api/vereinesaison");
        }

        public async Task<VereinAUS> UpdateVerein(VereinAUS updatedVerein)
        {
            return await httpClient.PutJsonAsync<VereinAUS>("api/VereineAus", updatedVerein);
        }

      
    }
}
