using System.ComponentModel.DataAnnotations;

namespace Library.DTOs.User
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required] [EmailAddress]

        public string Email { get; set; } = string.Empty;
        [Required] [MinLength(2)]

        public string Password { get; set; } = string.Empty;
    }
}
