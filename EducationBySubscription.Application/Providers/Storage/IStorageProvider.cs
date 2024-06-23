namespace EducationBySubscription.Application.Providers.Storage;

public interface IStorageProvider
{
    public Task<SendStorageProviderResponse> Send(Guid idCourse, byte[] byteChunk);
    public Task<byte[]> Retrieve(Guid idCourse);
}