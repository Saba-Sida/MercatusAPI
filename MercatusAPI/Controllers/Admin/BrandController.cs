using Application.Models.RequestModels;
using Application.Services.Brands;
using MercatusAPI.ExternalMethods;
using MercatusAPI.LayerSpecificHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercatusAPI.Controllers.Admin;

[Authorize(Policy = "AdminPolicy")]
[ApiController]
[Route("api/admin/[controller]")]
public class BrandController : ControllerBase
{
    private readonly PhotoConvertingHelper _photoConvertingHelper;
    private readonly IBrandService _brandService;

    public BrandController(
        PhotoConvertingHelper photoConvertingHelper,
        IBrandService brandService
    )
    {
        _photoConvertingHelper = photoConvertingHelper;
        _brandService = brandService;
    }

    [HttpPost]
    [Route("add")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddNewCategory([FromForm] string newBrandName, IFormFile? newBrandPhoto)
    {
        var request = new AddNewBrandRequest
        {
            Name = newBrandName,
            BrandPhoto = await _photoConvertingHelper.ConvertIFormFileToByteArrayAndFileExtension(newBrandPhoto)
        };

        var newCategoryCreationResponse = await _brandService.CreateNewBrand(request);

        return this.FormatResponse(newCategoryCreationResponse);
    }
}

