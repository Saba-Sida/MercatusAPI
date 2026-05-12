using Application.Repositories.Brands;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Brands;

public class BrandRepository: IBrandRepository
{
    private readonly AppDbContext _dbContext;

    public BrandRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> BrandNameExists(string name)
    {
        return await _dbContext.Brands
            .AnyAsync(brand => brand.BrandName == name);
    }

    public async Task<int> AddNewBrandName(string name)
    {
        var newBrand = new Brand
        {
            BrandName = name
        };
        
        _dbContext.Brands.Add(newBrand);
        await _dbContext.SaveChangesAsync();
        
        return newBrand.BrandId;
    }

    public async Task UpdateBrandPhotoAddress(int brandId, string brandPhotoAddress)
    {
        await _dbContext.Brands
        .Where(b => b.BrandId == brandId)
        .ExecuteUpdateAsync(setters => setters
            .SetProperty(b => b.BrandPhoto, brandPhotoAddress)
        );
    }

    public async Task<bool> BrandByIdExists(int brandId)
    {
        return await _dbContext.Brands
            .AnyAsync(brand => brand.BrandId == brandId);
    }
}