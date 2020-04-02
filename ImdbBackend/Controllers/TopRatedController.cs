using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImdbBackend.Controllers
{
    
    [ApiController]
    [Route("top-rated")]
    public class TopRatedController : ControllerBase
    {
        private readonly MovieContext _db;

        public TopRatedController(MovieContext db)
        {
            _db = db;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetTopRatedMovies(int page)
        {
            int moviesPerPage = 20;
            int from = moviesPerPage * page - 20; //in the db ids start w/1, not 0, but first we want to skip 0 items
            var movies = _db.Movies.Where(m => m.VoteCount > 1000).OrderByDescending(m => m.VoteAverage).Skip(from).Take(moviesPerPage);
            return await movies.ToListAsync();
        }
    }
}
