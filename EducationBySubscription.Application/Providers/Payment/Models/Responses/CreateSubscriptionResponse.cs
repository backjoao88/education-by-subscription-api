namespace EducationBySubscription.Application.Providers.Payment.Models.Responses;

public record CreateSubscriptionResponse
{
    public CreateSubscriptionResponse(string id, string link)
    {
        Id = id;
        Link = link;
    }
    public string Id { get; set; }
    public string Link { get; set; }
}