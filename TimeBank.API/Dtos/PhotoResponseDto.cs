using System;

namespace TimeBank.API.Dtos;

public record PhotoResponseDto
{
    public int PhotoId { get; init; }
    public string Name { get; init; }
    public string DisplayName { get; init; }
    public string URL { get; init; }
    public bool IsCurrent { get; init; }
    public DateTime UploadedOn { get; init; }
    public string UserId { get; init; }
}
