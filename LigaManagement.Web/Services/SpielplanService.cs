using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class SpielplanService : ISpielplanService
    {
        private string URL => "https://services.odata.org/Northwind/Northwind.svc/";

        private readonly HttpClient httpClient;
        public int TotalCount { get; set; }
        public SpielplanService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Spielplan> GetSpielplan(int id)
        {
            return await httpClient.GetJsonAsync<Spielplan>($"api/Spielplaene/{id}");
        }

        public async Task<Spielplan> GetSpielplanL3(int id)
        {
            return await httpClient.GetJsonAsync<Spielplan>($"api/SpielplaeneL3/{id}");
        }

        public async Task<IEnumerable<Spielplan>> GetSpielplaene()
        {
            try
            {
                return await httpClient.GetJsonAsync<Spielplan[]>("api/Spielplaene");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<Spielergebnisse>> GetSpielergebnisse()
        {
            try
            {
                return await httpClient.GetJsonAsync<Spielergebnisse[]>("api/Spielplaene");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<Spielplan> CreateSpielplan(Spielplan Spielplan)
        {
            try
            {
                return await httpClient.PostJsonAsync<Spielplan>("api/Spielplaene", Spielplan);
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<Spielplan> UpdateSpielplan(Spielplan updatedSpielplan)
        {
            return await httpClient.PutJsonAsync<Spielplan>("api/Spielplaene", updatedSpielplan);
        }

        public async Task DeleteSpielplan(int? id)
        {
            await httpClient.DeleteAsync($"api/Spielplaene/{id}/{Globals.LigaNummer}");
        }

        public async Task<IEnumerable<Spielplan>> GetSpielplaeneL3()
        {
            try
            {
                return await httpClient.GetJsonAsync<Spielplan[]>("api/SpielplaeneL3");
                
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<VereinAktSaison>> GetVereineL3()
        {
            try
            {
                return await httpClient.GetJsonAsync<VereinAktSaison[]>("api/VereineL3");

            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<Spielplan> UpdateSpielplanL3(Spielplan updatedSpielplan)
        {
            return await httpClient.PutJsonAsync<Spielplan>("api/SpielplaeneL3", updatedSpielplan);
        }

        public async Task<Spielplan> CreateSpielplanL3(Spielplan newSpielplan)
        {
            try
            {
                return await httpClient.PostJsonAsync<Spielplan>("api/SpielplaeneL3", newSpielplan);
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }
    }


}
