using System;
using System.Collections.Generic;

namespace TimeBank.API.Dtos
{
    public record UserProfileResponseDto
    {
        public string Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string StreetAddress { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
        public DateTime Birthday { get; init; }
        public string Biography { get; init; }
        public bool IsApproved { get; init; }
        public List<UserSkillsDto> Skills { get; init; }
        public List<PhotoResponseDto> Photos { get; init; }
    }
}
