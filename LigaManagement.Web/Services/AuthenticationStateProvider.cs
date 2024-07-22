using LigaManagement.Api.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class LigaManagerAuthenticationStateProvider : AuthenticationStateProvider
    {
        [Inject]
        public static IUserService userService { get; set; }

        private readonly LigamanagerUserService _ligaManagerUserService;
        public User CurrentUser { get; private set; } = new();               

        public LigaManagerAuthenticationStateProvider(LigamanagerUserService LigaManagerUserService)
        {
            _ligaManagerUserService = LigaManagerUserService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();
            //var user = await _ligaManagerUserService.FetchUserFromBrowserAsync();

            var user = new { Username = "g.wisst", Password = "LMAdmin742" };

            if (user is not null)
            {
                var userInDatabase = _ligaManagerUserService.LookupUserInDatabase(user.Username, user.Password);

                if (userInDatabase is not null)
                {
                    principal = userInDatabase.ToClaimsPrincipal();
                    CurrentUser = userInDatabase;
                    Globals.CurrentRole = userInDatabase.Role;
                }
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));

            return new(principal);
        }



        public async Task LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = _ligaManagerUserService.LookupUserInDatabase(username, password);

            if (user is not null)
            {
                await _ligaManagerUserService.PersistUserToBrowserAsync(user);
                principal = user.ToClaimsPrincipal();
                CurrentUser = user;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
                       
        }

        public async Task LogoutAsync()
        {
            await _ligaManagerUserService.ClearBrowserUserDataAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }

    }

}
