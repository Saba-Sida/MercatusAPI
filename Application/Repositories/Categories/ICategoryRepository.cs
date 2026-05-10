namespace Application.Repositories.Categories;

public interface ICategoryRepository
{
    Task<bool> CategoryNameExists(string name);
    Task<bool> CategoryByIdExists(int categoryId);
    Task<int> AddNewCategory(string categoryName, int? categoryParentId = null);
}