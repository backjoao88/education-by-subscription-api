using EducationBySubscription.Application.Providers.Vault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Providers.Asaas.Options;

public class PaymentProviderVaultOptionsSetup : IConfigureOptions<PaymentProviderOptions>
{
    
    private readonly IServiceProvider _provider;
    private const string ApiKeySectioName = "AsaasApiKey";
    
    public PaymentProviderVaultOptionsSetup(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    public void Configure(PaymentProviderOptions options)
    {
        using (var scope = _provider.CreateScope())
        {
            var vaultProvider = (IVaultProvider) scope.ServiceProvider.GetService(typeof(IVaultProvider))!;
            var apiKey = vaultProvider.GetSecret(ApiKeySectioName);
            options.ApiKey = apiKey;
        }
    }
}