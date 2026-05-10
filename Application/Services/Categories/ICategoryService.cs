using Application.Models.Common;
using Application.Models.RequestModels;

namespace Application.Services.Categories;

public interface ICategoryService
{
    Task<OperationResponse<int>> CreateNewCategory(AddNewCategoryRequest newCategoryRequest);
}