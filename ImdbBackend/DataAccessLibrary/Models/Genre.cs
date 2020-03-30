using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}