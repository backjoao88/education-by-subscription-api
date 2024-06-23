using EducationBySubscription.Application.Providers.Vault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Persistence.Configurations;

/// <summary>
/// Sets up a <see cref="AppDbContextOptions"/>
/// </summary>
public class AppDbContextVaultOptionsSetup : IConfigureOptions<AppDbContextOptions>
{
    private readonly IServiceProvider _provider;
    private const string ConnectionStringSectionName = "EduSubscriptionDbConnectionString";

    public AppDbContextVaultOptionsSetup(IServiceProvider provider)
    {
        _provider = provider;
    }

    public void Configure(AppDbContextOptions options)
    {
        using (var scope = _provider.CreateScope())
        {
            var vaultProvider = (IVaultProvider) scope.ServiceProvider.GetService(typeof(IVaultProvider))!;
            var connectionString = vaultProvider.GetSecret(ConnectionStringSectionName);
            options.ConnectionString = connectionString;
        }
    }
}