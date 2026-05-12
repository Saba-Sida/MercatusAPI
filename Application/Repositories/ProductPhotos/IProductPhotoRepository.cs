namespace Application.Repositories.ProductPhotos;

public interface IProductPhotoRepository
{
    Task AddProductPhotosByProductId(int productId, List<string> photoAddresses);
}