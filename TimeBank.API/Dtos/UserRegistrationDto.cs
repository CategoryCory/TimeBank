using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
