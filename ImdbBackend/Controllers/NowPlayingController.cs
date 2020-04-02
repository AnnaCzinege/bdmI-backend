using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class NowPlayingController : ControllerBase
    {
        private readonly MovieContext _db;

        public NowPlayingController( MovieContext db)
        {
            _db = db;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetNowPlayingMovies(int page)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            int moviesPerPage = 20;
            int from = page * moviesPerPage - 19; //in the db ids start w/1, not 0, but first we want to skip 0 items
            long currentDateOneMonthAgo = Convert.ToInt64(new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, 1).ToString("yyyyMMdd"));
            long currentDate = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
            return await _db.Movies.Where(movie => Convert.ToInt64(movie.ReleaseDate.Replace("-", "")) > currentDateOneMonthAgo && Convert.ToInt64(movie.ReleaseDate.Replace("-", "")) < currentDate).OrderByDescending(movie => Convert.ToInt64(movie.ReleaseDate.Replace("-", ""))).Skip(from).Take(moviesPerPage).ToListAsync();
        }
    }
}