using System;
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
    public class UpcomingController : ControllerBase
    {
        private readonly MovieContext _db;

        public UpcomingController(MovieContext db)
        {
            _db = db;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetUpcominMovies(int page)
        {
            long currentDate = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
            int moviesPerPage = 20;
            int from = moviesPerPage * page - 20; //in the db ids start w/1, not 0, but first we want to skip 0 items
            var movies = _db.Movies.Where(m => Convert.ToInt64(m.ReleaseDate.Replace("-", "")) > currentDate).OrderBy(m => Convert.ToInt64(m.ReleaseDate.Replace("-", ""))).Skip(from).Take(moviesPerPage);
            return await movies.ToListAsync();
        }
    }
}