using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
