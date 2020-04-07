using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImdbBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UpcomingController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public UpcomingController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<Movie>>> GetUpcominMovies(int page)
        {
            return await _movieRepository.GetUpcominMovies(page);
        }
    }
}