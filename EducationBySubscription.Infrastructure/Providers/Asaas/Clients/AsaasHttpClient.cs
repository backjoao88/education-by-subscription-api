using System.Diagnostics.CodeAnalysis;
using EducationBySubscription.Infrastructure.Providers.Asaas.Contracts;
using Microsoft.Extensions.Logging;

namespace EducationBySubscription.Infrastructure.Providers.Asaas.Clients;

public class AsaasHttpClient : IDefaultHttpClient
{
    private readonly HttpClient _client;
    private readonly ILogger<AsaasHttpClient> _logger;

    public AsaasHttpClient(HttpClient client, ILogger<AsaasHttpClient> logger)
    {
        _client = client;
        _logger = logger;
    }
    
    public Task<HttpResponseMessage> Post([StringSyntax(StringSyntaxAttribute.Uri)] string requestUri, HttpContent content) =>
        Post(CreateUri(requestUri), content);

    public Task<HttpResponseMessage> Post(Uri requestUri, HttpContent content)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.Content = content;
        return Send(request);
    }

    public Task<HttpResponseMessage> GetAsync([StringSyntax(StringSyntaxAttribute.Uri)] string requestUri) =>
        GetAsync(CreateUri(requestUri));

    public Task<HttpResponseMessage> GetAsync(Uri requestUri)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        return Send(request);
    }
    
    public async Task<HttpResponseMessage> Send(HttpRequestMessage request)
    {
        HttpResponseMessage? returnContent = null!;
        try
        {
            returnContent = await _client.SendAsync(request);
            ThrowIfNullResponse(returnContent);
            return returnContent;
        }
        catch (Exception e)
        {
            HandleFailure(e);
            throw;
        }
        finally
        {
            HandleFinish(returnContent);
        }
    }
    
    public void ThrowIfNullResponse(HttpResponseMessage? response)
    {
        if (response is null) throw new ArgumentNullException();
    }

    public void HandleFinish(HttpResponseMessage? content)
    {
        if (content is null) return;
        var strContent = content.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        _logger.LogDebug(strContent);
    }

    public void HandleFailure(Exception e)
    {
        _logger.LogDebug(e.Message);
        _logger.LogDebug(e.InnerException!.Message);
    }
    
    public Uri CreateUri(string requestUri)
    {
        ArgumentNullException.ThrowIfNull(requestUri);
        return new Uri(requestUri, UriKind.Relative);
    }
    
}