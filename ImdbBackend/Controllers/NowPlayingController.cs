using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos;
using Microsoft.AspNetCore.Mvc;

namespace ImdbBackend.Controllers
{
    [Route("now-playing")]
    [ApiController]
    public class NowPlayingController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public NowPlayingController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<Movie>>> GetNowPlayingMovies(int page)
        {
            return await _movieRepository.GetNowPlayingMovies(page);
        }
    }
}