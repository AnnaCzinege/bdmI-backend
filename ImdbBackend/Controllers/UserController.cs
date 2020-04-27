using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.RepositoryContainer;
using ImdbBackend.ViewModels;
using ImdbBackend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ImdbBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserDTOConverter _userDTOConverter = new UserDTOConverter();

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody]UserAuthentication userModel)
        {
            if (!await _unitOfWork.UserRepository.DoesUserExist(userModel.Email))
            {
                return await _unitOfWork.UserRepository.CreateNewUser(userModel.UserName, userModel.Email, userModel.Password);
            }
            return "Registration was unsuccessfull";
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserAuthentication userModel)
        {
            User user = await _unitOfWork.UserRepository.SignInUser(userModel.UserName, userModel.Password);
            if (user != null)
            {
                UserDTO userDTO = _userDTOConverter.ConvertUserObject(user);
                string token = _unitOfWork.UserRepository.GenerateTokenForUser(userDTO.Id, userDTO.UserName, userDTO.Email);
                userDTO.Token = token;
                return userDTO;
            }
            return new UserDTO() { ErrorMessage = "Username or password was invalid"};
        }

        [HttpPost]
        public async Task<ActionResult<string>> Logout([FromBody] UserDTO user)
        {
            if (ModelState.IsValid)
            {
                User userToLogOut = await _unitOfWork.UserRepository.GetUser(user.Email);

                await _unitOfWork.UserRepository.UpdateSecurityStamp(userToLogOut);

                await _unitOfWork.UserRepository.SignOut();
                return "You have been logged out";
            }

            return BadRequest("Unsuccesful logout");
        }

        [HttpPost]
        public async Task<ActionResult<List<Movie>>> GetWatchList([FromBody] UserDTO user)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await _unitOfWork.UserRepository.GetCurrentUser(user.Token);
                if (currentUser != null)
                {
                    return await _unitOfWork.WatchlistItemRepository.GetWatchListOfUser(currentUser.Id);
                }
            }
            return BadRequest("Unsuccesful get request of watchlist");
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddToWatchList([FromBody] WatchlistDTO watchlistDto)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await _unitOfWork.UserRepository.GetCurrentUser(watchlistDto.Token);
                if (currentUser != null)
                {
                    await _unitOfWork.WatchlistItemRepository.AddWatchListItem(watchlistDto.UserId, watchlistDto.MovieId);
                    return "Movie succesully added to watchlist!";
                }
            }
            return "Failed to add movie to watchlist!";
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteFromWatchList([FromBody] WatchlistDTO watchlistDto)
        {
            if (ModelState.IsValid)
            {

                await _unitOfWork.WatchlistItemRepository.DeleteWatchListItem(watchlistDto.UserId, watchlistDto.MovieId);
                return "Watchlist item succesully deleted";
            }

            return "Unsuccesful delete request of deleting item to watchlist";
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> GetCurrentUser([FromHeader] string jwtToken)
        {
            User currentUser = await _unitOfWork.UserRepository.GetCurrentUser(jwtToken);
            UserDTO currentUserDTO = _userDTOConverter.ConvertUserObject(currentUser);
            currentUserDTO.Token = jwtToken;
            return currentUserDTO;
        }
    }
}