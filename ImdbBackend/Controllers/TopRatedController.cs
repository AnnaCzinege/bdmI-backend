using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ImdbBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopRatedController : ControllerBase
    {
        private readonly ILogger<TopRatedController> _logger;
        private readonly MovieContext _db;

        public TopRatedController(ILogger<TopRatedController> logger, MovieContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetTopRatedMovies(int page)
        {
            int from = 20 * page - 20; //in the db ids start w/1, not 0, but first we want to skip 0 items
            int to = 20 * page;
            var movies = _db.Movies.OrderByDescending(m => m.VoteCount).ThenByDescending(m => m.VoteAverage).Skip(from).Take(to);
            return await movies.ToListAsync();
        }
    }
}
