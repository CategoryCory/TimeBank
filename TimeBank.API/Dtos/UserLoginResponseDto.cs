using System.Collections.Generic;

namespace TimeBank.API.Dtos
{
    public record UserLoginResponseDto
    {
        public bool IsAuthenticationSuccessful { get; init; }
        public string ErrorMessage { get; init; }
        public string UserId { get; init; }
        public string UserName { get; init; }
        public string DisplayName { get; init; }
        public string Email { get; init; }
        public string ProfileImageUrl { get; init; }
        public IList<string> Roles { get; init; }
        public bool IsApproved { get; init; }
        //public string Token { get; init; }
    }
}
