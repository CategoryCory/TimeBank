using System.Collections.Generic;

namespace TimeBank.API.Services;

public class PhotoUploadResponse
{
    public bool IsSuccess { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhotoURI { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new List<string>();

}
