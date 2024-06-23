namespace EducationBySubscription.Application.Providers.Storage;

public class SendStorageProviderResponse
{
    public SendStorageProviderResponse(string link)
    {
        Link = link;
    }
    public string Link { get; set; }
}