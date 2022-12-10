using System;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models;

public class RefreshToken
{
    public int RefreshTokenId { get; set; }
    public string Token { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ExpiresOn { get; set; }
    public DateTime? RevokedOn { get; set; }
    public ApplicationUser User { get; set; }
    public bool IsExpired => DateTime.UtcNow > ExpiresOn;
    public bool IsRevoked => RevokedOn != null;
    public bool IsActive => !IsRevoked && !IsExpired;
}
