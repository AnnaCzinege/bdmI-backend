using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.RepositoryContainer;
using ImdbBackend.DTOs;
using ImdbBackend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ImdbBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WatchlistController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return StatusCode(500);
        }

        [HttpPost]
        public async Task<ActionResult> AddToWatchList([FromBody] WatchlistViewModel watchlistDto)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await _unitOfWork.UserRepository.GetCurrentUser(watchlistDto.Token);
                if (currentUser != null)
                {
                    await _unitOfWork.WatchlistItemRepository.AddWatchListItem(watchlistDto.UserId, watchlistDto.MovieId);
                    return StatusCode(200);
                }
            }
            return StatusCode(500);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFromWatchList([FromBody] WatchlistViewModel watchlistDto)
        {
            if (ModelState.IsValid)
            {

                await _unitOfWork.WatchlistItemRepository.DeleteWatchListItem(watchlistDto.UserId, watchlistDto.MovieId);
                return StatusCode(200);
            }

            return StatusCode(500);
        }
    }
}