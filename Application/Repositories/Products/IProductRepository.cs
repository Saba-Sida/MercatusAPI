using Application.Models.DTOs;

namespace Application.Repositories.Products;

public interface IProductRepository
{
    Task<int> AddNewProduct(AddableProductModel product);
    
    Task<List<ProductViewingModel>> GetListOfProductsPaginated(int pageNumber, int pageSize);
}