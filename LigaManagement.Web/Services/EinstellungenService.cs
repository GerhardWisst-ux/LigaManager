
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LigaManagement.Models;

namespace LigaManagerManagement.Web.Services
{
    public class EinstellungenService : IEinstellungenService
    {
        private readonly HttpClient httpClient;
        
        public EinstellungenService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<EinstellungenLM> GetEinstellungen()
        {
            return await httpClient.GetJsonAsync<EinstellungenLM>("api/einstellungen");
        }
                
        public async Task<EinstellungenLM> UpdateEinstellungen(EinstellungenLM updateEinstellungen)
        {
            return await httpClient.PutJsonAsync<EinstellungenLM>("api/einstellungen", updateEinstellungen);
        }
    }
}
