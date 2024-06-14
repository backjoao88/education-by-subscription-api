using Azure.Storage;
using Azure.Storage.Blobs;
using EducationBySubscription.Application.Providers.Storage;
using EducationBySubscription.Infrastructure.Providers.Storage.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EducationBySubscription.Infrastructure.Providers.Storage;

public class AzureBlobStorageProvider : IStorageProvider
{
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;
    private readonly ILogger<AzureBlobStorageProvider> _logger;
    
    public AzureBlobStorageProvider(IOptions<AzureBlobStorageOptions> azureBlobStorageOptions, ILogger<AzureBlobStorageProvider> logger)
    {
        _logger = logger;
        _azureBlobStorageOptions = azureBlobStorageOptions.Value!;
    }

    public BlobClient CreateConnection(Uri blobUri)
    {
        var storageCredentials =
            new StorageSharedKeyCredential(_azureBlobStorageOptions.AccountName, _azureBlobStorageOptions.AccountKey);
        return new BlobClient(blobUri, storageCredentials);
    }

    public async Task Send(Guid idCourse, byte[] byteChunk)
    {
        var blobName = $"blob_{idCourse}.png";
        _logger.LogDebug(blobName);
        Uri blobUri = new Uri("https://" +
                              _azureBlobStorageOptions.AccountName +
                              ".blob.core.windows.net/" +
                              _azureBlobStorageOptions.ContainerName+
                              "/" + blobName);
        _logger.LogDebug(blobUri.ToString());
        var conn = CreateConnection(blobUri);
        _logger.LogDebug(conn.Name);
        var response = await conn.UploadAsync(new BinaryData(byteChunk));
        _logger.LogDebug(response.GetRawResponse().ToString());
    }

    public async Task<byte[]> Retrieve(Guid idCourse)
    {
        var blobName = $"blob_{idCourse}";
        Uri blobUri = new Uri("https://" +
                              _azureBlobStorageOptions.AccountName +
                              ".blob.core.windows.net/" +
                              _azureBlobStorageOptions.ContainerName+
                              "/" + blobName);
        var conn = CreateConnection(blobUri);
        var blobResult = await conn.DownloadAsync();
        var stream = blobResult.Value.Content;
        stream.Seek(0, SeekOrigin.Begin);
        var memStream = new MemoryStream();
        stream.CopyTo(memStream);
        return memStream.ToArray();
    }
}