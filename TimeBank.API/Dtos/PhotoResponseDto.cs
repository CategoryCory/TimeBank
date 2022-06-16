using System;

namespace TimeBank.API.Dtos;

public class PhotoResponseDto
{
    public int PhotoId { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string URL { get; set; }
    public bool IsCurrent { get; set; }
    public DateTime UploadedOn { get; set; }
    public string UserId { get; set; }
}
