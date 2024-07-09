
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LigaManagement.Models;
using LigaManagement.Api.Models;

namespace LigaManagerManagement.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<User> CreateUser(User Tor)
        {
            return await httpClient.PostJsonAsync<User>("api/User", Tor); ;
        }

        public Task DeleteUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUser(int id)
        {
            return await httpClient.GetJsonAsync<User>($"api/User/{id}");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await httpClient.GetJsonAsync<User[]>("api/User");
        }

        public async Task<User> UpdateUser(User Tor)
        {
            return await httpClient.PutJsonAsync<User>("api/User", Tor);
        }
    }
}
