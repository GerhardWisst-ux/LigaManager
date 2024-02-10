using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
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

        public Task DeleteVerein(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Verein> GetVerein(int Id)
        {
            return await httpClient.GetJsonAsync<Verein>($"api/vereine/{Id}");
        }

        public async Task<IEnumerable<Verein>> GetVereine()
        {
            return await httpClient.GetJsonAsync<Verein[]>("api/vereine");
        }

        public async Task<Verein> UpdateVerein(Verein updatedVerein)
        {
            return await httpClient.PutJsonAsync<Verein>("api/vereine", updatedVerein);
        }


    }
}
