using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record UserRegistrationDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50)]
        public string FirstName { get; init; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50)]
        public string LastName { get; init; }

        [Required(ErrorMessage = "Email address is required.")]
        [MaxLength(256)]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; init; }

        [Compare("Password", ErrorMessage = "The password and confirmation password must match.")]
        public string ConfirmPassword { get; init; }
    }
}
