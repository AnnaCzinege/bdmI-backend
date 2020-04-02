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

namespace BackupProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AllMoviesController : ControllerBase
    {
        private readonly ILogger<AllMoviesController> _logger;
        private readonly MovieContext _db;

        public AllMoviesController(ILogger<AllMoviesController> logger, MovieContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var movies = _db.Movies;
            return await movies.ToListAsync();
        }
    }
}