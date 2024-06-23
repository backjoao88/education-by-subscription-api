using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Providers.Authentication.Options;

public class JwtOptionLocalSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;
    private const string IssuerNameSectionName = "JwtIssuer";
    private const string AudienceSectionName = "JwtAudience";
    private const string PrivateKeySectionName = "JwtPrivateKey";
    
    public JwtOptionLocalSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summa>
    /// Bind the current JWT configurations from settings into the JWT options object.
    /// </summary>
    /// <param name="options"></param>
    public void Configure(JwtOptions options)
    {
        var issuer= _configuration[IssuerNameSectionName] ?? "";
        var audience = _configuration[AudienceSectionName] ?? "";
        var privateKey = _configuration[PrivateKeySectionName] ?? "";
        options.Issuer = issuer;
        options.PrivateKey = privateKey;
        options.Audience = audience;
    }
}