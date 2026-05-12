using Application.Repositories.ProductPhotos;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories.ProductPhotos;

public class ProductPhotoRepository : IProductPhotoRepository
{
    private readonly AppDbContext _dbContext;

    public ProductPhotoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddProductPhotosByProductId(int productId, List<string> photoAddresses)
    {
        var photoEntities = photoAddresses.Select(photo => new ProductPhoto
        {
            PhotoUri = photo,
            ProductId = productId
        });

        await _dbContext.ProductPhotos.AddRangeAsync(photoEntities);
        await _dbContext.SaveChangesAsync();
    }
}