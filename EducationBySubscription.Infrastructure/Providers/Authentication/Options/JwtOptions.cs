namespace EducationBySubscription.Infrastructure.Providers.Authentication.Options;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
}