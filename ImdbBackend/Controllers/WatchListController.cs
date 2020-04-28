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
        public async Task<ActionResult<List<Movie>>> GetWatchlist([FromBody] UserDTO user)
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
        public async Task<ActionResult> AddToWatchlist([FromBody] WatchlistViewModel watchlistViewModel)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await _unitOfWork.UserRepository.GetCurrentUser(watchlistViewModel.Token);
                if (currentUser != null)
                {
                    await _unitOfWork.WatchlistItemRepository.AddWatchListItem(watchlistViewModel.UserId, watchlistViewModel.MovieId);
                    return StatusCode(200);
                }
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteFromWatchlist(string userId, int movieId)
        {
            if (ModelState.IsValid)
            {

                await _unitOfWork.WatchlistItemRepository.DeleteWatchListItem(userId, movieId);
                return StatusCode(200);
            }

            return StatusCode(500);
        }
    }
}