using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts;
public interface IPhotoService
{
    Task<Photo> GetCurrentPhotoByUserIdAsync(string userId);
    Task<ApplicationResult> AddPhotoAsync(Photo photo);
}