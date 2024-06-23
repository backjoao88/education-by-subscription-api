using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Providers.Asaas.Options;

public class PaymentProviderLocalOptionsSetup : IConfigureOptions<PaymentProviderOptions>
{
    private readonly IConfiguration _configuration;
    private const string ApiKeySectioName = "AsaasApiKey";
    
    public PaymentProviderLocalOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(PaymentProviderOptions options)
    {
        var apiKey = _configuration[ApiKeySectioName] ?? "";
        options.ApiKey = apiKey;
    }
}