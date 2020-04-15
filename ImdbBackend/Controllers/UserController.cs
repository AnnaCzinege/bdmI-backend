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

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody]UserAuthentication model)
        {
            if (!await _unitOfWork.UserRepository.DoesUserExist(model.Email))
            {
                await _unitOfWork.UserRepository.CreateNewUser(model.UserName, model.Email, model.Password);
                return "Registration was successful";
            }
            return BadRequest("User already exists");
        }


        [HttpPost]
        public async Task<ActionResult<string>> Logout([FromBody] UserDTO user)
        {
            if (this.ModelState.IsValid)
            {
                var userToLogOut = await _unitOfWork.UserRepository.GetUser(user.Email);

                await _unitOfWork.UserRepository.UpdateSecurityStamp(userToLogOut);

                await _unitOfWork.UserRepository.SignOut();
                return "You have been logged out";
            }

            return BadRequest("Unsuccesul logout");
        }

    }
}