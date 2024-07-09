using LigaManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> UpdateUser(User updatedUser);
        Task<User> CreateUser(User newUser);
        Task DeleteUser(int id);

    }
}
