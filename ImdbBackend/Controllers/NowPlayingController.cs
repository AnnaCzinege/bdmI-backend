using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImdbBackend.Controllers
{
    [Route("now-playing")]
    [ApiController]
    public class NowPlayingController : ControllerBase
    {
        private IMovieRepository _movieRepository;

        public NowPlayingController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<Movie>>> GetNowPlayingMovies(int page)
        {
            return await _movieRepository.GetNowPlayingMovies(page);
        }
    }
}