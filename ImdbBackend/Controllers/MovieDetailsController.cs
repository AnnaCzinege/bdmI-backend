using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.FetchModels;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackupProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieDetailsController : ControllerBase
    {
        private readonly MovieContext _db;

        public MovieDetailsController(MovieContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDetails>> GetMovieDetails(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            var movieGenreIds = _db.MovieGenres.Where(mg => mg.MovieId == id).Select(mg => mg.GenreId);
            var movieLanguageIds = _db.MovieLanguages.Where(ml => ml.MovieId == id).Select(ml => ml.LanguageId);
            List<Genre> genres = _db.Genres.Where(g => movieGenreIds.Contains(g.Id)).ToList();
            List<Language> languages = _db.Languages.Where(l => movieLanguageIds.Contains(l.Id)).ToList();
            MovieDetails MovieDetails = ConvertMovieObject(movie, genres, languages);
            return MovieDetails;
        }

        private MovieDetails ConvertMovieObject(Movie movie, List<Genre> genres, List<Language> languages)
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