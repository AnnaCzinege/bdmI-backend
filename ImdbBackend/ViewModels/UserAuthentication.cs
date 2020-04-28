using System.ComponentModel.DataAnnotations;

namespace ImdbBackend.ViewModels
{
    public class UserAuthentication
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z0-9]*$")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
