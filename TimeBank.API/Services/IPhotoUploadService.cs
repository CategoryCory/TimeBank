using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TimeBank.API.Services;
public interface IPhotoUploadService
{
    ValueTask<PhotoUploadResponse> UploadPhoto(IFormFile photoToUpload);
}