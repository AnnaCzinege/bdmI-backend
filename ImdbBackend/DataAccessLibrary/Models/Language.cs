using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Language
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}