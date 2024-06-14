namespace EducationBySubscription.Infrastructure.Persistence.Configurations;

/// <summary>
/// Represents a set of configurations for <see cref="AppDbContext"/>
/// </summary>
public class AppDbContextOptions
{
    public string ConnectionString { get; set; } = string.Empty;
}