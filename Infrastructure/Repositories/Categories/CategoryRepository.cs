using Application.Repositories.Categories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> CategoryNameExists(string name)
    {
        return await _dbContext.Categories
            .AnyAsync(category => category.CategoryName == name);
    }

    public async Task<bool> CategoryByIdExists(int categoryId)
    {
        return await _dbContext.Categories
            .AnyAsync(category => category.CategoryId == categoryId);
    }

    public async Task<int> AddNewCategory(string categoryName, int? categoryParentId = null)
    {
        var newCategory = new Category
        {
            CategoryName = categoryName,
            CategoryParentId = categoryParentId
        };

        _dbContext.Categories.Add(newCategory);
        await _dbContext.SaveChangesAsync();

        return newCategory.CategoryId;
    }
}