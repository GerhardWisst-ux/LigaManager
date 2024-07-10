using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LigaManagement.Api.Models
{
    public class User
    {        
        [Required(ErrorMessage = "Benutzername muß angegeben werden.")]
        public string Username { get; set; } = "";
        
        [Required(ErrorMessage = "Passwort muß angegeben werden.")]
        //[StringLength(20, ErrorMessage = "Passwortlänge darf nicht länger als 20 sein.")]

        public string Password { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string Location { get; set; } = "";

        //[Required(ErrorMessage = "Mail muß angegeben werden.")]
        
        public string Mail { get; set; } = "";
        public string Role { get; set; } = "";

        public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.Name, Username),
            new (ClaimTypes.Hash, Password),
        }, "Ligamanager"));

        public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            Username = principal.FindFirstValue(ClaimTypes.Name),
            Password = principal.FindFirstValue(ClaimTypes.Hash)
        };
    }


}
