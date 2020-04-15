using DataAccessLibrary.Models;
using DataAccessLibrary.RepositoryContainer;
using ImdbBackend.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ImdbBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("top-rated/{page}")]
        public async Task<ActionResult<List<Movie>>> GetTopRatedMovies(int page)
        {
            return await _unitOfWork.MovieRepository.GetTopRatedMovies(page);
        }


        [HttpGet("popular/{page}")]
        public async Task<ActionResult<List<Movie>>> GetPopularMovies(int page)
        {
            return await _unitOfWork.MovieRepository.GetPopularMovies(page);
        }


        [HttpGet("now-playing/{page}")]
        public async Task<ActionResult<List<Movie>>> GetNowPlayingMovies(int page)
        {
            return await _unitOfWork.MovieRepository.GetNowPlayingMovies(page);
        }


        [HttpGet("upcoming/{page}")]
        public async Task<ActionResult<List<Movie>>> GetUpcominMovies(int page)
        {
            return await _unitOfWork.MovieRepository.GetUpcomingMovies(page);
        }


        [HttpGet("moviedetails/{id}")]
        public async Task<ActionResult<MovieDetails>> GetMovieDetails(int id)
        {
            Movie movie = await _unitOfWork.MovieRepository.ExtendedFind(id);
            List<string> genres = await _unitOfWork.GenreRepository.GetGenres(movie.MovieGenres.Select(mg => mg.GenreId).ToList());
            List<string> languages = await _unitOfWork.LanguageRepository.GetLanguages(movie.MovieLanguages.Select(ml => ml.LanguageId).ToList());
            return new MovieDetailsConverter().ConvertToMovieDetails(movie, genres, languages);
        }


        [HttpGet("allmovies")]
        public async Task<ActionResult<List<AllMovies>>> GetAllMovies()
        {
            List<Movie> movies = await _unitOfWork.MovieRepository.GetAll();
            return new AllMoviesConverter().ConvertToAllMovies(movies);
        }
    }
}