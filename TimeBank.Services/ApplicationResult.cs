namespace TimeBank.Services
{
    public class ApplicationResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static ApplicationResult Success() => new() { IsSuccess = true };
        public static ApplicationResult Failure(List<string> errors) => new() { IsSuccess = false, Errors = errors };
    }
}
