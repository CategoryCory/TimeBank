using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models;

public class Photo
{
    public int PhotoId { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
