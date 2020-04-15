using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserRepository(MovieContext context, UserManager<User> userManager, SignInManager<User> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> DoesUserExist(string userEmail)
        {
            User user = await _userManager.FindByEmailAsync(userEmail);
            return user == null ? false : true;
        }

        public async Task CreateNewUser(string userName, string email, string password )
        {
            User newUser = new User
            {
                UserName = email,
                Email = email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                await _userManager.ConfirmEmailAsync(newUser, token);
            }
        }

        public async Task<User> GetUser(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task UpdateSecurityStamp(User user)
        {
            await _userManager.UpdateSecurityStampAsync(user);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }


    }
}
