using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private static readonly string SECRET_KEY = Environment.GetEnvironmentVariable("SECRET_KEY");
        private static readonly SymmetricSecurityKey SIGN_IN_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

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
                UserName = userName,
                Email = email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                await _userManager.ConfirmEmailAsync(newUser, token);
            }
        }

        public async Task<User> SignInUser(string userName, string password)
        {
            User loginUser = await _userManager.FindByNameAsync(userName);
            SignInResult result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
            if (result.Succeeded)
            {
                return loginUser;
            }
            return null;
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

        public string GenerateTokenForUser(string id, string userName, string email)
        {
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email, email)
                },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                signingCredentials: new SigningCredentials(SIGN_IN_KEY, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
