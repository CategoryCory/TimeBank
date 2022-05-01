using System;
using System.Collections.Generic;
using TimeBank.Repository.Models;

namespace TimeBank.API.Dtos
{
    public class UserProfileResponseDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime Birthday { get; set; }
        public string Biography { get; set; }
        public bool IsApproved { get; set; }
        public List<UserSkillsDto> Skills { get; set; }
    }
}
