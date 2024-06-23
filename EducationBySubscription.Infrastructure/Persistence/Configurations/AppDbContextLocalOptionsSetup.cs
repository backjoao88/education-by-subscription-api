using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Persistence.Configurations;

public class AppDbContextLocalOptionsSetup : IConfigureOptions<AppDbContextOptions>
{
    private readonly IConfiguration _configuration;
    private const string AppDbContextSectionName = "Database";
    
    public AppDbContextLocalOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(AppDbContextOptions options)
    {
        var connectionString = _configuration[AppDbContextSectionName] ?? "";
        options.ConnectionString = connectionString;
    }
}