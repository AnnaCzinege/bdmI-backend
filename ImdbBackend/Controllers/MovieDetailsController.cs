using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.FetchModels;
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
        public async Task<ActionResult<FetchModelForMovieDetails>> GetMovieDetails(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            var movieGenreIds = _db.MovieGenres.Where(mg => mg.MovieId == id).Select(mg => mg.GenreId);
            var movieLanguageIds = _db.MovieLanguages.Where(ml => ml.MovieId == id).Select(ml => ml.LanguageId);
            List<Genre> genres = _db.Genres.Where(g => movieGenreIds.Contains(g.Id)).ToList();
            List<Language> languages = _db.Languages.Where(l => movieLanguageIds.Contains(l.Id)).ToList();
            FetchModelForMovieDetails MovieDetails = ConvertMovieObject(movie, genres, languages);
            return MovieDetails;
        }

        private FetchModelForMovieDetails ConvertMovieObject(Movie movie, List<Genre> genres, List<Language> languages)
        {
            FetchModelForMovieDetails movieDetails = new FetchModelForMovieDetails()
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