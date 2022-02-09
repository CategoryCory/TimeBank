namespace TimeBank.API.Dtos
{
    public class UserLoginResponseDto
    {
        public bool IsAuthenticationSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public string Token { get; set; }
    }
}
