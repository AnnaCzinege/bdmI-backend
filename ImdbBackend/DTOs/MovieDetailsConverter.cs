using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbBackend.DTOs
{
    public class MovieDetailsConverter : MovieDetails
    {
        public MovieDetails ConvertToMovieDetails(Movie movie, List<string> genres, List<string> languages)
        {
            return new MovieDetails()
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
        }
    }
}
