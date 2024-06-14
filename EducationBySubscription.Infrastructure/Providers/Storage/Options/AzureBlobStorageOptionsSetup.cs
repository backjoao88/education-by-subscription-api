using EducationBySubscription.Application.Providers.Vault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Providers.Storage.Options;

public class AzureBlobStorageOptionsSetup : IConfigureOptions<AzureBlobStorageOptions>
{
    private readonly IServiceProvider _provider;
    private const string AccountNameSectioName = "EduSubscriptionStorageAccountName";
    private const string AccountKeySectioName = "EduSubscriptionStorageAccountKey";
    private const string ContainerNameSectioName = "EduSubscriptionStorageContainerName";
    
    public AzureBlobStorageOptionsSetup(IServiceProvider provider)
    {
        _provider = provider;
    }

    public void Configure(AzureBlobStorageOptions options)
    {
        using (var scope = _provider.CreateScope())
        {
            var vaultProvider = (IVaultProvider)scope.ServiceProvider.GetService(typeof(IVaultProvider))!;
            var accountName = vaultProvider.GetSecret(AccountNameSectioName);
            var accountKey = vaultProvider.GetSecret(AccountKeySectioName);
            var containerName = vaultProvider.GetSecret(ContainerNameSectioName);
            options.AccountName = accountName;
            options.AccountKey = accountKey;
            options.ContainerName = containerName;
        }
    }
}