using Application.Models.Common;
using Application.Models.RequestModels;

namespace Application.Services.Brands;

public interface IBrandService
{
    Task<OperationResponse<int>> CreateNewBrand(AddNewBrandRequest request);
}