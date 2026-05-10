using Application.ExtensionMethods;
using Application.Models.Common;
using Application.Models.RequestModels;
using Application.Repositories.Categories;
using Application.Services.Categories;

namespace Infrastructure.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResponse<int>> CreateNewCategory(AddNewCategoryRequest newCategoryRequest)
    {
        var newCategoryOperationResponse = new OperationResponse<int>();

        if (await _categoryRepository.CategoryNameExists(newCategoryRequest.NewCategoryName))
        {
            newCategoryOperationResponse.MakeConflictResponse("Such category name already exists!");
        }
        else
        {
            bool validParentId;
            if (newCategoryRequest.CategoryParentId != null)
                validParentId = await _categoryRepository.CategoryByIdExists((int)newCategoryRequest.CategoryParentId!);
            else validParentId = true;

            if (validParentId)
            {
                var newCategoryId = await _categoryRepository.AddNewCategory(newCategoryRequest.NewCategoryName,
                    newCategoryRequest.CategoryParentId);
                newCategoryOperationResponse.Data = newCategoryId;
            }
            else
                newCategoryOperationResponse.MakeGenerallyFailedResponse("No valid parent category id");
        }

        return newCategoryOperationResponse;
    }
}