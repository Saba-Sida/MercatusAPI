using Application.BlobStorage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.BlobStirage;

public class BrandPhotoStorageHelper : IBrandPhotoStorageHelper
{
    private readonly IGenericBlobStorageManager _genericBlobStorageManager;
    private readonly string _baseBrandPhotosStorageAddress;
    
    public BrandPhotoStorageHelper(
        IGenericBlobStorageManager genericBlobStorageManager,
        IConfiguration configuration
    )
    {
        _genericBlobStorageManager = genericBlobStorageManager;
        var baseUri = configuration["BlobStorage:BaseUri"]!;
        var brandPhotoSubDirectory = configuration["BlobStorage:BrandPhotoSubDirectory"]!;

        _baseBrandPhotosStorageAddress =
            Path.Combine(baseUri, brandPhotoSubDirectory); // not ends with slash
    }

    public async Task<string?> SaveBrandPhoto(byte[] bytes, string extension)
    {
        try
        {
            var fileName = BuildBrandPhotoName(extension);

            return await _genericBlobStorageManager.SaveFile(
                _baseBrandPhotosStorageAddress,
                fileName,
                bytes
            );
        }
        catch
        {
            return null;
        }
    }

    string BuildBrandPhotoName(string extension) => $"BrandPhoto_{Guid.NewGuid()}{extension}";
}