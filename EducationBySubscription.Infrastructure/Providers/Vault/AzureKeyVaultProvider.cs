using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EducationBySubscription.Application.Providers.Vault;
using EducationBySubscription.Infrastructure.Providers.Vault.Options;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Providers.Vault;

public class AzureKeyVaultProvider : IVaultProvider
{
    private readonly AzureKeyVaultOptions _azureKeyVaultOptions;

    public AzureKeyVaultProvider(IOptions<AzureKeyVaultOptions> azureKeyVaultOptions)
    {
        _azureKeyVaultOptions = azureKeyVaultOptions.Value!;
    }
    
    public string GetSecret(string name)
    {
        var credential = new DefaultAzureCredential();
        var secretClient = new SecretClient(new Uri(_azureKeyVaultOptions.Uri), credential);
        var mySecret = secretClient.GetSecret(name);
        return mySecret.Value.Value;
    }
}