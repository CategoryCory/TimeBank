using Microsoft.AspNetCore.Identity;

namespace TimeBank.Repository.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Biography { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public bool IsApproved { get; set; }
    }
}
