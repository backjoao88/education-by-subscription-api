using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Providers.Storage.Options;

public class AzureBlobStorageLocalOptionsSetup : IConfigureOptions<AzureBlobStorageOptions>
{
    private readonly IConfiguration _configuration;
    private const string StorageSectionName = "Storage";
    
    public AzureBlobStorageLocalOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(AzureBlobStorageOptions options)
    {
        _configuration.GetSection(StorageSectionName).Bind(options);
    }
}