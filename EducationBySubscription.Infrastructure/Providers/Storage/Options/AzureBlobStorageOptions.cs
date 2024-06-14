namespace EducationBySubscription.Infrastructure.Providers.Storage.Options;

public class AzureBlobStorageOptions
{
    public string AccountKey { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
}