using Application.Models.Common;
using Application.Models.RequestModels;

namespace Application.Services.Products;

public interface IProductService
{
    Task<OperationResponse> AddNewProduct(AddNewProductCompleteRequest request);
}