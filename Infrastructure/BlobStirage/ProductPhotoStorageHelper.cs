using Application.BlobStorage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.BlobStirage;

public class ProductPhotoStorageHelper : IProductPhotoStorageHelper
{
    private readonly IGenericBlobStorageManager _genericBlobStorageManager;
    private readonly string _baseProductPhotosStorageAddress;
    
    public ProductPhotoStorageHelper(
        IGenericBlobStorageManager genericBlobStorageManager,
        IConfiguration configuration
    )
    {
        _genericBlobStorageManager = genericBlobStorageManager;
        var baseUri = configuration["BlobStorage:BaseUri"]!;
        var productPhotosMainSubDirectory = configuration["BlobStorage:ProductPhotosMainSubDirectory"]!;

        _baseProductPhotosStorageAddress =
            Path.Combine(baseUri, productPhotosMainSubDirectory); // not ends with slash
    }
    
    public async Task<List<string>> SaveProductPhotos(int productId, List<(byte[], string)> photos)
    {
        var productSpecificDirectory = $"{_baseProductPhotosStorageAddress}_{productId}";
        var createdPhotosAddressList = new List<string>();
        foreach (var photo in photos)
        {
            var createdPhotoAddress = await _genericBlobStorageManager
                .SaveFile(productSpecificDirectory, BuildProductPhotoName(productId, photo.Item2), photo.Item1);
            
            if(createdPhotoAddress != null) createdPhotosAddressList.Add(createdPhotoAddress);
        }

        return createdPhotosAddressList;
    }

    private string BuildProductPhotoName(int productId, string extension)
        => $"{productId}_{Guid.NewGuid()}{extension}";
}