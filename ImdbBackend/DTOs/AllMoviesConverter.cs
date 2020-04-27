using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;

namespace ImdbBackend.DTOs
{
    public class AllMoviesConverter : AllMovies
    {
        public List<AllMovies> ConvertToAllMovies(List<Movie> movies)
        {
            List<AllMovies> allMovies = new List<AllMovies>();
            string releaseYear = "";
            foreach (var movie in movies)
            {
                if (movie.ReleaseDate.Length > 0)
                {
                    releaseYear = $"({Convert.ToString(movie.ReleaseDate)[0..4]})";
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
