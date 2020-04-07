using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos;
using DataAccessLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ImdbBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AllMoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public AllMoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<AllMovies>>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMovies();
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