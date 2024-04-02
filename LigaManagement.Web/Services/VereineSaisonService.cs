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
    public class VereineSaisonService : IVereineSaisonService

    {
        private readonly HttpClient httpClient;

        public VereineSaisonService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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
              
       
        public async Task<IEnumerable<VereineSaison>> GetVereineSaison()
        {
            return await httpClient.GetJsonAsync<List<VereineSaison>>($"api/vereinesaison");
        }
    }
}
