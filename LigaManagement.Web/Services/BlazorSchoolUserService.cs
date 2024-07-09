using LigaManagement.Api.Models;
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class BlazorSchoolUserService
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly string _blazorSchoolStorageKey = "blazorSchoolIdentity";

        [Inject]
        public static IUserService userService { get; set; }

        public BlazorSchoolUserService(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage;
        }

        public User? LookupUserInDatabase(string username, string password)
        {
            var usersFromDatabase = new List<User>()

        {
            new()
            {
                Username = "g.wisst",
                Password = "Rainer123!"
            }
        };

            
            var foundUser = usersFromDatabase.SingleOrDefault(u => u.Username == username && u.Password == password);

            return foundUser;
        }

        public async Task PersistUserToBrowserAsync(User user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            await _protectedLocalStorage.SetAsync(_blazorSchoolStorageKey, userJson);
        }

        public async Task<User?> FetchUserFromBrowserAsync()
        {
            try
            {
                var storedUserResult = await _protectedLocalStorage.GetAsync<string>(_blazorSchoolStorageKey);

                if (storedUserResult.Success && !string.IsNullOrEmpty(storedUserResult.Value))
                {
                    var user = JsonConvert.DeserializeObject<User>(storedUserResult.Value);

                    return user;
                }
            }
            catch (InvalidOperationException)
            {
            }

            return null;
        }

        public async Task ClearBrowserUserDataAsync() => await _protectedLocalStorage.DeleteAsync(_blazorSchoolStorageKey);        
        
    }
  
}
