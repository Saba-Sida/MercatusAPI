using Application.Models.RequestModels;
using Application.Services.Categories;
using MercatusAPI.ExternalMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercatusAPI.Controllers.Admin;


[Authorize(Policy = "AdminPolicy")]
[ApiController]
[Route("api/admin/[controller]")]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddNewCategory([FromBody] AddNewCategoryRequest request)
    {
        var newCategoryCreationResponse = await _categoryService.CreateNewCategory(request);

        return this.FormatResponse(newCategoryCreationResponse);
    }
}