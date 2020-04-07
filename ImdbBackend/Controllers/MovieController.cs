using DataAccessLibrary.Models;
using DataAccessLibrary.RepositoryContainer;
using DataAccessLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            return await _unitOfWork.MovieRepository.GetUpcominMovies(page);
        }


        [HttpGet("moviedetails/{id}")]
        public async Task<ActionResult<MovieDetails>> GetMovieDetails(int id)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieDetails(id);
            List<int> movieGenreIds = await _unitOfWork.MovieGenreRepository.GetMovieGenreIds(id);
            var movieLanguageIds = await _unitOfWork.MovieLanguageRepository.GetMovieLanguageIds(id);
            List<string> genres = await _unitOfWork.GenreRepository.GetGenres(movieGenreIds);
            List<string> languages = await _unitOfWork.LanguageRepository.GetLanguages(movieLanguageIds);
            MovieDetails MovieDetails = ConvertMovieObject(movie, genres, languages);
            return MovieDetails;
        }


        [HttpGet("allmovies")]
        public async Task<ActionResult<List<AllMovies>>> GetAllMovies()
        {
            var movies = await _unitOfWork.MovieRepository.GetAllMovies();
            List<AllMovies> allMovies = ConvertMovieObjects(movies);
            return allMovies;
        }


        private MovieDetails ConvertMovieObject(Movie movie, List<string> genres, List<string> languages)
        {
            MovieDetails movieDetails = new MovieDetails()
            {
                Id = movie.Id,
                OriginalId = movie.OriginalId,
                OriginalTitle = movie.OriginalTitle,
                Overview = movie.Overview,
                MovieGenres = genres,
                MovieLanguages = languages,
                ReleaseDate = movie.ReleaseDate,
                Runtime = movie.Runtime,
                VoteCount = movie.VoteCount,
                VoteAverage = movie.VoteAverage,
                PosterPath = movie.PosterPath
            };
            return movieDetails;
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

                allMovies.Add(new AllMovies()
                {
                    Id = movie.Id,
                    OriginalId = movie.OriginalId,
                    OriginalTitle = $"{movie.OriginalTitle} {releaseYear}"
                });
            }
            return allMovies;
        }

    }
}