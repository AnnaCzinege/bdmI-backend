using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> DoesUserEmailExist(string email);
        Task<bool> DoesUserNameExist(string userName);
        Task<string> CreateNewUser(string userName, string email, string password, IUrlHelper url, string scheme);
        Task<User> SignInUser(string userName, string password);
        Task<User> GetUser(string email);
        Task UpdateSecurityStamp(User user);
        Task SignOut();
        Task<User> GetCurrentUser(string token);
        Task<User> ConfirmEmail(string email, string token);
        string GenerateTokenForUser(string id, string userName, string email);
    }
}
