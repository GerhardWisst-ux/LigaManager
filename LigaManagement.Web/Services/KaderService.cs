using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services
{
    public class KaderService : IKaderService
    {
        private readonly HttpClient httpClient;

        public KaderService(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<Kader> CreateSpieler(Kader newSpieler)
        {
            return await httpClient.PostJsonAsync<Kader>("api/Kader", newSpieler);
        }

        public async Task DeleteSpieler(int id)
        {
            await httpClient.DeleteAsync($"api/Kader/{id}"); ;
        }

        public async Task<IEnumerable<Kader>> GetAllSpieler()
        {
            return await httpClient.GetJsonAsync<Kader[]>("api/Kader");
        }

        public async Task<IEnumerable<Kader>> GetSpielerKopiere(int saisonid, int saisonidvorher, int vereinid)
        {
            try
            {
                return await httpClient.GetJsonAsync<Kader[]>($"api/Kader/kopiere/{saisonid}/{saisonidvorher}/{vereinid}");
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Kader> GetSpieler(int id)
        {       
            return await httpClient.GetJsonAsync<Kader>($"api/Kader/{id}");
        }

        public async Task<Kader> UpdateSpieler(Kader updatedSpieler)
        {
            return await httpClient.PutJsonAsync<Kader>("api/Kader", updatedSpieler);
        }

        



    }
}
