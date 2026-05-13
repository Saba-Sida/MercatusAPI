using Application.BlobStorage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.BlobStirage;

public class BrandPhotoStorageHelper : IBrandPhotoStorageHelper
{
    private readonly IGenericBlobStorageManager _genericBlobStorageManager;

    private readonly string _brandPhotoSubDirectoryName;

    public BrandPhotoStorageHelper(
        IGenericBlobStorageManager genericBlobStorageManager,
        IConfiguration configuration
    )
    {
        _genericBlobStorageManager = genericBlobStorageManager;
        _brandPhotoSubDirectoryName = configuration["BlobStorage:BrandPhotoSubDirectory"]!;
    }

    public async Task<string?> SaveBrandPhoto(byte[] bytes, string extension)
    {
        try
        {
            var fileName = $"BrandPhoto_{Guid.NewGuid()}{NormalizeExtension(extension)}";

            return await _genericBlobStorageManager.SaveFile(
                _brandPhotoSubDirectoryName,
                fileName,
                bytes
            );
        }
        catch
        {
            return null;
        }
    }

    private string NormalizeExtension(string extension)
        => extension.StartsWith(".") ? extension : "." + extension;
}