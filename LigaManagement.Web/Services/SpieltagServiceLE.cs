using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class SpieltagServiceLE : ISpieltagServiceLE
    {
        private string URL => "https://services.odata.org/Northwind/Northwind.svc/";

        private readonly HttpClient httpClient;
        public int TotalCount { get; set; }
        public SpieltagServiceLE(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }             

        public async Task<IEnumerable<Spieltag>> GetSpieltage()
        {
            try
            {
                return await httpClient.GetJsonAsync<Spieltag[]>("api/spieltageLE");
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.StackTrace);
                return null;
            }
        }
    }


}
