﻿using System;
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
            UserDTOConverter userDTOConverter = new UserDTOConverter();
            User user = await _unitOfWork.UserRepository.SignInUser(userModel.UserName, userModel.Password);
            if (user != null)
            {
                return userDTOConverter.ConvertUserObject(user);
            }
            return BadRequest(new { error = "Username or password is invalid!" });
        }

        [HttpPost]
        public async Task<ActionResult<string>> Logout([FromBody] UserDTO user)
        {
            if (this.ModelState.IsValid)
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
                
                return await _unitOfWork.WatchlistItemRepository.GetWatchListOfUser(user.Id);
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

    }
}