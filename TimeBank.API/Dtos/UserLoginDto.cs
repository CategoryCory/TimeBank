using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record UserLoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; init; }
    }
}
