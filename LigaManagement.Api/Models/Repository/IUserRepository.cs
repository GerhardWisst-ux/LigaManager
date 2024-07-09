using LigaManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int UserId);
        Task<User> CreateUser(User UserId);
        Task<User> UpdateUser(User UserId);
        Task<User> DeleteUser(int UserId);
    }
}
