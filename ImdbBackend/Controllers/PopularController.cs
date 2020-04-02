using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ImdbBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PopularController : ControllerBase
    {
        private readonly ILogger<PopularController> _logger;
        private readonly MovieContext _db;

        public PopularController(ILogger<PopularController> logger, MovieContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetPopularMovies(int page)
        {
            int moviesPerPage = 20;
            int from = page * moviesPerPage - 19;
            return await _db.Movies.OrderByDescending(movie => movie.Popularity).Skip(from).Take(moviesPerPage).Select(movie => movie).ToListAsync();
        }
    }
}