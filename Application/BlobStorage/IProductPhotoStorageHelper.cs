namespace Application.BlobStorage;

public interface IProductPhotoStorageHelper
{
    Task<List<string>> SaveProductPhotos(int productId, List<(byte[], string)> photos);
}