using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class UserProfileUpdateDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name cannot be greater than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Last name cannot be greater than 50 characters.")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Please make sure this is a valid phone number.")]
        [MaxLength(25, ErrorMessage = "Phone number cannot be greater than 25 characters.")]
        public string PhoneNumber { get; set; }

        [MaxLength(100, ErrorMessage = "Street address cannot be greater than 100 characters.")]
        public string StreetAddress { get; set; }

        [MaxLength(50, ErrorMessage = "City cannot be greater than 50 characters.")]
        public string City { get; set; }

        [MaxLength(25, ErrorMessage = "State cannot be greater than 25 characters.")]
        public string State { get; set; }

        [MaxLength(25, ErrorMessage = "Zip code cannot be greater than 25 characters.")]
        public string ZipCode { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [MaxLength(500, ErrorMessage = "Biography cannot be greater than 500 characters.")]
        public string Biography { get; set; }

        public ICollection<UserSkillsDto> Skills { get; set; }
    }
}
