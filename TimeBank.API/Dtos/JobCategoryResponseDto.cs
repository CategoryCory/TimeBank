namespace TimeBank.API.Dtos
{
    public record JobCategoryResponseDto
    {
        public int JobCategoryId { get; init; }
        public string JobCategoryName { get; init; }
        public string JobCategorySlug { get; init; }
    }
}
