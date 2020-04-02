using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using ImdbBackend.FetchModels;
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
        public async Task<ActionResult<List<AllMovies>>> GetAllMovies()
        {
            var movies = await _db.Movies.ToListAsync();
            List<AllMovies> allMovies = ConvertMovieObjects(movies);
            return allMovies;
        }

        private List<AllMovies> ConvertMovieObjects(List<Movie> movies)
        {
            List<AllMovies> allMovies = new List<AllMovies>();
            string releaseYear = "";
            foreach (var movie in movies)
            {
                string test = movie.ReleaseDate;
                if (movie.ReleaseDate.Length > 0)
                {
                    releaseYear = $"({Convert.ToString(movie.ReleaseDate).Substring(0, 4)})";
                }

                allMovies.Add(new AllMovies() { 
                    Id = movie.Id,
                    OriginalId = movie.OriginalId,
                    OriginalTitle = $"{movie.OriginalTitle} {releaseYear}" 
                });
            }
            return allMovies;
        }
    }
}