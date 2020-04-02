using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackupProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AllMoviesController : ControllerBase
    {
        private readonly MovieContext _db;

        public AllMoviesController(MovieContext db)
        {
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