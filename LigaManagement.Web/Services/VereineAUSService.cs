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

        public async Task<IEnumerable<VereinAUS>> GetVereinePL()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereinePL");
        }


        public async Task<IEnumerable<VereinAUS>> GetVereineES()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereineES");
        }

        public async Task<IEnumerable<VereinAUS>> GetVereineNL()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereineNL");
        }

        public async Task<IEnumerable<VereinAUS>> GetVereinePT()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereinePT");
        }


        public async Task<IEnumerable<VereinAUS>> GetVereineTU()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereineTU");
        }

        public async Task<IEnumerable<VereinAUS>> GetVereineFR()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereineFR");
        }

        public async Task<IEnumerable<VereinAUS>> GetVereineIT()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereineIT");
        }
        public async Task<IEnumerable<VereinAUS>> GetVereineBE()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/VereineBE");
        }

        public async Task<IEnumerable<VereinAktSaisonAUS>> GetVereineSaison(int saisonid)
        {
            return await httpClient.GetJsonAsync<List<VereinAktSaisonAUS>>($"api/VereineSaisonAus/{saisonid}");
        }

        public async Task<VereinAUS> GetVereinES(int Id)
        {
            return await httpClient.GetJsonAsync<VereinAUS>($"api/VereineES/{Id}");
        }


        public async Task<VereinAUS> GetVereinFR(int Id)
        {
            return await httpClient.GetJsonAsync<VereinAUS>($"api/VereineFR/{Id}");
        }

        public async Task<VereinAUS> GetVereinIT(int Id)
        {
            return await httpClient.GetJsonAsync<VereinAUS>($"api/VereineIT/{Id}");
        }

        public async Task<VereinAUS> GetVereinPL(int Id)
        {
            return await httpClient.GetJsonAsync<VereinAUS>($"api/VereinePL/{Id}");
        }

        public async Task<VereinAUS> UpdateVerein(VereinAUS updatedVerein)
        {
            return await httpClient.PutJsonAsync<VereinAUS>("api/VereineAus", updatedVerein);
        }

        public async Task<VereinAUS> GetVereinNL(int id)
        {
            return await httpClient.GetJsonAsync<VereinAUS>($"api/VereineNL/{id}");
        }

        public async Task<VereinAUS> GetVereinPT(int id)
        {
            return await httpClient.GetJsonAsync<VereinAUS>($"api/VereinePT/{id}");
        }

        public async Task<VereinAUS> GetVereinTU(int id)
        {
            return await httpClient.GetJsonAsync<VereinAUS>($"api/VereineTU/{id}");
        }

        public async Task<VereinAUS> GetVereinBE(int id)
        {
            return await httpClient.GetJsonAsync<VereinAUS>($"api/VereineBE/{id}");
        }
    }
}
