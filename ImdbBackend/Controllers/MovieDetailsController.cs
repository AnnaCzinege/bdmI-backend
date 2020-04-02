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
    public class MovieDetailsController : ControllerBase
    {
        private readonly ILogger<MovieDetailsController> _logger;
        private readonly MovieContext _db;

        public MovieDetailsController(ILogger<MovieDetailsController> logger, MovieContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieDetails(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            return movie;
        }
    }
}