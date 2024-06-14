using EducationBySubscription.Application.Providers.Payment.Serialization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EducationBySubscription.Infrastructure.Providers.Asaas.Serialization;

public class AsaasHttpSerializer : IDefaultHttpSerializer
{
    private JsonSerializer _serializer = new();
    private ILogger<AsaasHttpSerializer> _logger;
    
    public AsaasHttpSerializer(ILogger<AsaasHttpSerializer> logger)
    {
        _logger = logger;
        _serializer.NullValueHandling = NullValueHandling.Ignore;
    }

    public string Serialize<TPaymentDto>(TPaymentDto toBeSerialized) where TPaymentDto : PaymentDto
    {
        using var memString = new StringWriter();
        using var jsonWriter = new JsonTextWriter(memString);
        _serializer.Serialize(jsonWriter, toBeSerialized);
        _logger.LogDebug(memString.ToString());
        return memString.ToString();
    }

    public TPaymentDto? Deserialize<TPaymentDto>(string toBeDeserialized) where TPaymentDto : PaymentDto
    {
        using var memString = new StringReader(toBeDeserialized);
        using var jsonReader = new JsonTextReader(memString);
        _logger.LogDebug(memString.ToString());
        return _serializer.Deserialize<TPaymentDto>(jsonReader);
    }
}