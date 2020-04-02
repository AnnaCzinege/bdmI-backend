using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.FetchModels
{
    public class FetchModelForMovieDetails
    {
        public int Id { get; set; }
        public int OriginalId { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public List<Genre> MovieGenres { get; set; }
        public List<Language> MovieLanguages { get; set; }
        public string ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }
        public string PosterPath { get; set; }
    }
}
