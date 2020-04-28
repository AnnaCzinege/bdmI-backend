using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmailConfirmationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace DataAccessLibrary.Repos.SQL
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailConfirmationSender _emailConfirmationSender;
        //private static readonly string SECRET_KEY = Environment.GetEnvironmentVariable("SECRET_KEY");
        public static readonly SymmetricSecurityKey SIGN_IN_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkeyforgeneratingjwttokenusingsymmetricsecuritykey"));

        public UserRepository(MovieContext context, UserManager<User> userManager, SignInManager<User> signInManager, IEmailConfirmationSender emailConfirmationSender) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailConfirmationSender = emailConfirmationSender;
        }

        public async Task<bool> DoesUserEmailExist(string userEmail)
        {
            User user = await _userManager.FindByEmailAsync(userEmail);

            return user == null ? false : true;
        }
        public async Task<bool> DoesUserNameExist(string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);

            return user == null ? false : true;
        }


        public async Task<User> CreateNewUser(string userName, string email, string password, IUrlHelper url, string scheme)
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
                string confirmationLink = url.Action("ConfirmEmail", "User", new { userEmail = newUser.Email, token }, scheme);
                string emailContent = _emailConfirmationSender.CreateEmailContent(newUser.UserName, confirmationLink);
                Message message = new Message(new string[] { newUser.Email }, "Confirmation letter - bdmI", emailContent);
                await _emailConfirmationSender.SendEmailAsync(message);
                return newUser;
            }
            return null;
    }

        public async Task<User> ConfirmEmail(string email, string token)
        {
            if (email == null || token == null)
            {
                return null;
            }
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            } else
            {
                await _userManager.ConfirmEmailAsync(user, token);
                return user;
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

        public async Task<User> GetCurrentUser(string token)
        {
            try
            {
                JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken result = jwtHandler.ReadToken(token) as JwtSecurityToken;
                string Id = result.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                return await _userManager.FindByIdAsync(Id);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is ArgumentException)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
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
