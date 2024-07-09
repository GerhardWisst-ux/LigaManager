using LigaManagement.Api.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class BlazorSchoolAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly BlazorSchoolUserService _blazorSchoolUserService;
        public User CurrentUser { get; private set; } = new();

        public BlazorSchoolAuthenticationStateProvider(BlazorSchoolUserService blazorSchoolUserService)
        {
            _blazorSchoolUserService = blazorSchoolUserService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();
            var user = await _blazorSchoolUserService.FetchUserFromBrowserAsync();

            if (user is not null)
            {
                var userInDatabase = _blazorSchoolUserService.LookupUserInDatabase(user.Username, user.Password);

                if (userInDatabase is not null)
                {
                    principal = userInDatabase.ToClaimsPrincipal();
                    CurrentUser = userInDatabase;
                }
            }

            return new(principal);
        }



        public async Task LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = _blazorSchoolUserService.LookupUserInDatabase(username, password);

            if (user is not null)
            {
                await _blazorSchoolUserService.PersistUserToBrowserAsync(user);
                principal = user.ToClaimsPrincipal();
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
                       
        }

        public async Task LogoutAsync()
        {
            await _blazorSchoolUserService.ClearBrowserUserDataAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }

    }

}
