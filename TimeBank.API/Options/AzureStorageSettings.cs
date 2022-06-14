namespace TimeBank.API.Options;

public class AzureStorageSettings
{
    public const string AzureStorage = "AzureStorage";

    public string Connection { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
}
