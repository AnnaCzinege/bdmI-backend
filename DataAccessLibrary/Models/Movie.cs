using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Movie
    {

        public int Id { get; set; }
        [Required]
        public int OriginalId { get; set; }
        [Required]
        [MaxLength(200)]
        public string OriginalTitle { get; set; }
        [Required]
        public string Overview { get; set; }
        public IList<MovieGenre> MovieGenres { get; set; }
        public IList<MovieLanguage> MovieLanguages { get; set; }
        [Required]
        [MaxLength(200)]
        public string ReleaseDate { get; set; }
        [Required]
        public int Runtime { get; set; }
        [Required]
        public double VoteAverage { get; set; }
        [Required]
        public int VoteCount { get; set; }
        [Required]
        public double Popularity { get; set; }
        [Required]
        [MaxLength(500)]
        public string PosterPath { get; set; }
    }
}
