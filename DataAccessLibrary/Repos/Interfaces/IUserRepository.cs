using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> DoesUserExist(string email);
        Task CreateNewUser(string userName, string email, string password);
        Task<User> SignInUser(string userName, string password);
        Task<User> GetUser(string email);
        Task UpdateSecurityStamp(User user);
        Task SignOut();
        Task<User> GetCurrentUser(string token);
        string GenerateTokenForUser(string id, string userName, string email);
    }
}
