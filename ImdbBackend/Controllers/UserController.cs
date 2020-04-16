using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.RepositoryContainer;
using ImdbBackend.ViewModels;
using ImdbBackend.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
                await _unitOfWork.UserRepository.CreateNewUser(userModel.UserName, userModel.Email, userModel.Password);
                return "Registration was successful";
            }
            return BadRequest(new {error = "User already exists!" });
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
            return BadRequest(new { error = "Username or password is invalid!" });
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

        [HttpGet]
        public async Task<ActionResult<List<int>>> GetWatchList([FromBody] UserDTO user)
        {
            if (ModelState.IsValid)
            {
                
                return await _unitOfWork.WatchlistItemRepository.GetWatchListByUser(user.Id);
            }

            return BadRequest("Unsuccesful get request of watchlist");
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddToWatchList([FromBody] WatchlistDTO watchlistDto)
        {
            if (ModelState.IsValid)
            {

                await _unitOfWork.WatchlistItemRepository.AddWatchListItem(watchlistDto.UserId, watchlistDto.MovieId);
                return "Watchlist item succesully added";
            }

            return BadRequest("Unsuccesful post request of adding item to watchlist");
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteFromWatchList([FromBody] WatchlistDTO watchlistDto)
        {
            if (ModelState.IsValid)
            {

                await _unitOfWork.WatchlistItemRepository.DeleteWatchListItem(watchlistDto.UserId, watchlistDto.MovieId);
                return "Watchlist item succesully deleted";
            }

            return BadRequest("Unsuccesful delete request of deleting item to watchlist");
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