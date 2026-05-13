using Application.Models.DTOs;
using Application.Repositories.Products;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddNewProduct(AddableProductModel product)
    {
        var productEntity = new Product
        {
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            Price = product.Price,
            InStockCount = product.InStockCount,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _dbContext.Products.Add(productEntity);
        await _dbContext.SaveChangesAsync();

        return productEntity.ProductId;
    }

    public async Task<List<ProductViewingModel>> GetListOfProductsPaginated(int pageNumber, int pageSize)
    {
        return await _dbContext
            .Products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(product => product.Category)
            .Include(product => product.Brand)
            .Include(product => product.ProductPhotos)
            .AsNoTracking()
            .Select(product => new ProductViewingModel
                (
                    product.ProductId,
                    product.ProductName,
                    product.ProductDescription,
                    product.Price,
                    product.InStockCount,
                    product.CreatedAt,
                    product.UpdatedAt,
                    product.CategoryId,
                    product.Category!.CategoryName,
                    product.BrandId,
                    product.Brand!.BrandName,
                    product.ProductPhotos.Select(photo => photo.PhotoUri).ToList()
                )
            ).ToListAsync();
    }
}