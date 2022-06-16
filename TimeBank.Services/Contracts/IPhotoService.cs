using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts;
public interface IPhotoService
{
    Task<ApplicationResult> AddPhotoAsync(Photo photo);
}