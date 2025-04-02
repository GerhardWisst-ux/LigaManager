using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace TextManagerManagement.Web.Services
{
    public class InfoTexteService : IInfoTexteService
    {
        private readonly HttpClient httpClient;

        public InfoTexteService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<InfoText> CreateText(InfoText newText)
        {
            return await httpClient.PostJsonAsync<InfoText>("api/InfoTexte", newText);
        }

        public async Task DeleteText(int id)
        {
            await httpClient.DeleteAsync($"api/InfoTexte/{id}");
        }

        public async Task<InfoText> GetText(int id)
        {
            try
            {
                return await httpClient.GetJsonAsync<InfoText>($"api/InfoTexte/{id}");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<InfoText>> GetTexte()
        {
            try
            {
                return await httpClient.GetJsonAsync<InfoText[]>("api/InfoTexte");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);
                throw;
            }
        }

        public async Task<InfoText> UpdateText(InfoText updateText)
        {
            return await httpClient.PutJsonAsync<InfoText>("api/InfoTexte", updateText);
        }
    }
}
