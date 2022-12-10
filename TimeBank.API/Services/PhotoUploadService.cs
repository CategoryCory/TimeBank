using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using TimeBank.API.Options;

namespace TimeBank.API.Services;

public sealed class PhotoUploadService : IPhotoUploadService
{
    private readonly IOptions<AzureStorageSettings> _options;

    public PhotoUploadService(IOptions<AzureStorageSettings> options)
    {
        _options = options;
    }

    public async ValueTask<PhotoUploadResponse> UploadPhoto(IFormFile photoToUpload)
    {
        var response = new PhotoUploadResponse();

        if (photoToUpload.Length > 5242880)
        {
            response.IsSuccess = false;
            response.Errors.Add("The photo must be less than 5 MB.");
            return response;
        }

        try
        {
            var storageConnString = _options.Value.Connection;
            var storageContainerName = _options.Value.ContainerName;

            var containerClient = new BlobContainerClient(storageConnString, storageContainerName);

            var photoExtension = Path.GetExtension(photoToUpload.FileName);
            var newFileName = $"{Guid.NewGuid()}{photoExtension}";

            using var fileStream = photoToUpload.OpenReadStream();

            var blobClient = containerClient.GetBlobClient(newFileName);

            var uploadResult = await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = photoToUpload.ContentType });

            response.IsSuccess = true;
            response.Name = newFileName;
            response.PhotoURI = blobClient.Uri.ToString();
            return response;
        }
        catch (Azure.RequestFailedException ex)
        {
            response.IsSuccess = false;
            response.Errors.Add($"An error occurred uploading the photo: {ex.Message}");
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Errors.Add($"An error occurred uploading the photo: {ex.Message}");
            return response;
        }
    }
}
