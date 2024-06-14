using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Providers.Vault.Options;

public class AzureKeyVaultOptionsSetup : IConfigureOptions<AzureKeyVaultOptions>
{
    private readonly IConfiguration _configuration;
    private const string VaultSectionName = "Vault:VaultUri";
    
    public AzureKeyVaultOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(AzureKeyVaultOptions options)
    {
        var value = _configuration[VaultSectionName] ?? "";
        options.Uri = value;
    }
}