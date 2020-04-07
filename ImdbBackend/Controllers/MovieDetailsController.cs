using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos;
using DataAccessLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImdbBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieDetailsController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        private readonly IMovieGenreRepository _movieGenreRepository;

        private readonly IMovieLanguageRepository _movieLanguageRepository;

        public MovieDetailsController(IMovieRepository movieRepository, IMovieGenreRepository movieGenreRepository, IMovieLanguageRepository movieLanguageRepository)
        {
            _movieRepository = movieRepository;
            _movieGenreRepository = movieGenreRepository;
            _movieLanguageRepository = movieLanguageRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDetails>> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetMovieDetails(id);
            var movieGenreIds = _movieGenreRepository.GetMovieGenreIds(id);
            var movieLanguageIds = _movieLanguageRepository.GetMovieLanguageIds(id);
            List<string> genres = _db.Genres.Where(g => movieGenreIds.Contains(g.Id)).Select(g => g.Name).ToList();
            List<string> languages = _db.Languages.Where(l => movieLanguageIds.Contains(l.Id)).Select(l => l.Name).ToList();
            MovieDetails MovieDetails = ConvertMovieObject(movie, genres, languages);
            return MovieDetails;
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
    }
}