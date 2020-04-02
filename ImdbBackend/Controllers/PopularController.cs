using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImdbBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PopularController : ControllerBase
    {
        private readonly MovieContext _db;

        public PopularController(MovieContext db)
        {
            _db = db;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetPopularMovies(int page)
        {
            int moviesPerPage = 20;
            int from = page * moviesPerPage - 20;
            return await _db.Movies.OrderByDescending(movie => movie.Popularity).Skip(from).Take(moviesPerPage).Select(movie => movie).ToListAsync();
        }
    }
}