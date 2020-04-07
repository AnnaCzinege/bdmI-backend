using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos;
using Microsoft.AspNetCore.Mvc;

namespace ImdbBackend.Controllers
{
    
    [ApiController]
    [Route("top-rated")]
    public class TopRatedController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public TopRatedController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<Movie>>> GetTopRatedMovies(int page)
        {
            return await _movieRepository.GetTopRatedMovies(page);
        }
    }
}
