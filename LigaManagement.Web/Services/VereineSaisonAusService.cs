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
    public class VereineSaisonAusService : IVereineSaisonAusService

    {
        private readonly HttpClient httpClient;

        public VereineSaisonAusService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }      
        
        public async Task<List<VereineSaisonAus>> CreateVereineSaisonAus(List<VereineSaisonAus> vereine)
        {
            try
            {
                return await httpClient.PostJsonAsync<List<VereineSaisonAus>>("api/VereineSaisonAus", vereine);
            }
            catch (Exception ex)
            {

                Debug.Print(ex.Message);
                return null;
            }
        }
              
       
        public async Task<IEnumerable<VereineSaisonAus>> GetVereineSaisonAus()
        {
            return await httpClient.GetJsonAsync<List<VereineSaisonAus>>($"api/VereineSaisonAus");
        }
    }
}
