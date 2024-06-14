using EducationBySubscription.Application.Providers.Vault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Providers.Authentication.Options;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IServiceProvider _provider;
    private const string IssuerNameSectionName = "JwtIssuer";
    private const string AudienceSectionName = "JwtAudience";
    private const string PrivateKeySectionName = "JwtPrivateKey";

    public JwtOptionsSetup(IServiceProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Bind the current JWT configurations from settings into the JWT options object.
    /// </summary>
    /// <param name="options"></param>
    public void Configure(JwtOptions options)
    {
        using (var scope = _provider.CreateScope())
        {
            var vaultProvider = (IVaultProvider)scope.ServiceProvider.GetService(typeof(IVaultProvider))!;
            var issuerName = vaultProvider.GetSecret(IssuerNameSectionName);
            var audienceName = vaultProvider.GetSecret(AudienceSectionName);
            var privateKey = vaultProvider.GetSecret(PrivateKeySectionName);
            options.Issuer = issuerName;
            options.PrivateKey = privateKey;
            options.Audience = audienceName;
        }
    }
}