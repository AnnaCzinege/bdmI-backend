using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImdbBackend.Controllers
{
    
    [ApiController]
    [Route("top-rated")]
    public class TopRatedController : ControllerBase
    {
        private IMovieRepository _movieRepository;

        public TopRatedController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetTopRatedMovies(int page)
        {
            return await _movieRepository.GetTopRatedMovies(page);
        }
    }
}
