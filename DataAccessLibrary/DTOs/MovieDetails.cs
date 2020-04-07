using System.Collections.Generic;

namespace DataAccessLibrary.DTOs
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public int OriginalId { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public List<string> MovieGenres { get; set; }
        public List<string> MovieLanguages { get; set; }
        public string ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }
        public string PosterPath { get; set; }
    }
}
