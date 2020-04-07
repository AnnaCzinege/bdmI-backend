using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImdbBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PopularController : ControllerBase
    {
        private IMovieRepository _movieRepository;

        public PopularController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetPopularMovies(int page)
        {

            return await _movieRepository.GetPopularMovies(page);
        }
    }
}