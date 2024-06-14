namespace EducationBySubscription.Application.Providers.Payment.Serialization;

public interface IDefaultHttpSerializer
{
    public string Serialize<TPaymentDto>(TPaymentDto toBeSerialized) where TPaymentDto : PaymentDto;
    public TPaymentDto? Deserialize<TPaymentDto>(string toBeDeserialized) where TPaymentDto : PaymentDto;
}