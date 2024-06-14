namespace EducationBySubscription.Infrastructure.Providers.Vault.Options;

public class AzureKeyVaultOptions
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
}