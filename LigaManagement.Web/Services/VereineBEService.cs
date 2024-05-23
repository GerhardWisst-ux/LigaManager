using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
<<<<<<< HEAD
=======
using LigaManagerManagement.Models;
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class VereineBEService : IVereineBEService

    {
        private readonly HttpClient httpClient;

        public VereineBEService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<VereinAUS> CreateVerein(VereinAUS newVerein)
        {
            return await httpClient.PostJsonAsync<VereinAUS>("api/vereineBE", newVerein);
        }

<<<<<<< HEAD
        
=======

>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
        public async Task<List<VereineSaison>> CreateVereineSaison(List<VereineSaison> vereine)
        {
            try
            {
<<<<<<< HEAD
                return await httpClient.PostJsonAsync<List<VereineSaison>>("api/vereineBE", vereine);
=======
                return await httpClient.PostJsonAsync<List<VereineSaison>>("api/vereinesaison", vereine);
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
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
            return await httpClient.GetJsonAsync<VereinAUS>($"api/vereineBE/{Id}");
<<<<<<< HEAD
        }      

        public async Task<IEnumerable<VereinAUS>> GetVereine()
        {
            return await httpClient.GetJsonAsync<VereinAUS[]>("api/vereineBE");
        }              
=======
        }

        public async Task<IEnumerable<VereinAUS>> GetVereine()
        {
            try
            {
                return await httpClient.GetJsonAsync<VereinAUS[]>("api/vereineBE");
            }
            catch (Exception ex)
            {

                Debug.Print(ex.Message);
                return null;
            }
        }
>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc

        public async Task<IEnumerable<VereinAktSaisonAUS>> GetVereineSaison()
        {
            return await httpClient.GetJsonAsync<List<VereinAktSaisonAUS>>($"api/vereinesaison");
        }

        public async Task<VereinAUS> UpdateVerein(VereinAUS updatedVerein)
        {
            return await httpClient.PutJsonAsync<VereinAUS>("api/vereineBE", updatedVerein);
        }

<<<<<<< HEAD
      
=======

>>>>>>> 8a0e2d787e04b40732ddec05cdd3d89845d288fc
    }
}
