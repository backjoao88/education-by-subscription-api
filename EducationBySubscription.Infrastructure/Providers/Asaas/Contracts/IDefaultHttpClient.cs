using System.Diagnostics.CodeAnalysis;

namespace EducationBySubscription.Infrastructure.Providers.Asaas.Contracts;

public interface IDefaultHttpClient
{
    Task<HttpResponseMessage> Post([StringSyntax(StringSyntaxAttribute.Uri)] string requestUri, HttpContent content);
    Task<HttpResponseMessage> Post(Uri requestUri, HttpContent content);
    Task<HttpResponseMessage> GetAsync([StringSyntax(StringSyntaxAttribute.Uri)] string requestUri);
    Task<HttpResponseMessage> GetAsync(Uri requestUri);
    
}