using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
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
                 return await httpClient.GetJsonAsync<Land[]>("api/laender");
             
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<Land> UpdateLand(Land updatedLand)
        {
            return await httpClient.PutJsonAsync<Land>("api/laender", updatedLand);
        }
    }
}
