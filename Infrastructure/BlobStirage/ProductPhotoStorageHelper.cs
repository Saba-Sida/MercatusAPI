using Application.BlobStorage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.BlobStirage;

public class ProductPhotoStorageHelper : IProductPhotoStorageHelper
{
    private readonly IGenericBlobStorageManager _genericBlobStorageManager;
    private readonly string _productPhotosMainSubDirectoryName;

    public ProductPhotoStorageHelper(
        IGenericBlobStorageManager genericBlobStorageManager,
        IConfiguration configuration)
    {
        _genericBlobStorageManager = genericBlobStorageManager;
        _productPhotosMainSubDirectoryName = configuration["BlobStorage:ProductPhotosMainSubDirectory"]!;
    }

    public async Task<List<string>> SaveProductPhotos(int productId, List<(byte[], string)> photos)
    {
        var result = new List<string>();

        var productFolder = $"{_productPhotosMainSubDirectoryName}/ProductPhotos_{productId}";

        foreach (var photo in photos)
        {
            var fileName =
                $"{productId}_{Guid.NewGuid()}{NormalizeExtension(photo.Item2)}";

            var savedPath = await _genericBlobStorageManager.SaveFile(
                productFolder,
                fileName,
                photo.Item1
            );

            if (savedPath != null)
                result.Add(savedPath);
        }

        return result;
    }

    private string NormalizeExtension(string extension)
        => extension.StartsWith(".") ? extension : "." + extension;
}