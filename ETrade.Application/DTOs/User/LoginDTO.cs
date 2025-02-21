using System.ComponentModel.DataAnnotations;

namespace ETrade.Application.DTOs.User
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 digits.")]
        public string Password { get; set; }
    }
}
