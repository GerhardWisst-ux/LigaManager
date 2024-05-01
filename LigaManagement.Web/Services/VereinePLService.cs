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
    public class VereineServicePL : IVereineServicePL

    {
        private readonly HttpClient httpClient;

        public VereineServicePL(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<VereinPL> CreateVerein(VereinPL newVerein)
        {
            return await httpClient.PostJsonAsync<VereinPL>("api/vereine", newVerein);
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

        public Task<List<VereinPL>> CreateVereineSaison(List<VereinPL> vereine)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVerein(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<VereinPL> GetVerein(int Id)
        {
            return await httpClient.GetJsonAsync<VereinPL>($"api/vereine/{Id}");
        }      

        public async Task<IEnumerable<VereinPL>> GetVereine()
        {
            return await httpClient.GetJsonAsync<VereinPL[]>("api/vereinePL");
        }              

        public async Task<IEnumerable<VereinAktSaisonPL>> GetVereineSaison()
        {
            return await httpClient.GetJsonAsync<List<VereinAktSaisonPL>>($"api/vereinesaison");
        }

        public async Task<VereinPL> UpdateVerein(VereinPL updatedVerein)
        {
            return await httpClient.PutJsonAsync<VereinPL>("api/vereine", updatedVerein);
        }

      
    }
}
