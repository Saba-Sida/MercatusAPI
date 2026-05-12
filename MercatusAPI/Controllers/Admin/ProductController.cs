using Application.Models.RequestModels;
using Application.Services.Products;
using MercatusAPI.ExternalMethods;
using MercatusAPI.LayerSpecificHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercatusAPI.Controllers.Admin;

[Authorize(Policy = "AdminPolicy")]
[ApiController]
[Route("api/admin/[controller]")]
public class ProductController : ControllerBase
{
    private readonly PhotoConvertingHelper _photoConvertingHelper;
    private readonly IProductService _productService;

    public ProductController(
        PhotoConvertingHelper photoConvertingHelper,
        IProductService productService)
    {
        _photoConvertingHelper = photoConvertingHelper;
        _productService = productService;
    }
    
    [HttpPost]
    [Route("add")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddNewProduct([FromForm] AddNewProductPartialRequest addNewProductPartialRequest, List<IFormFile> newProductPhotos)
    {
        var photos = await _photoConvertingHelper.ConvertIFormFileListToByteArrayAndFileExtensionList(newProductPhotos);

        var completeRequest = new AddNewProductCompleteRequest
        {
            ProductName = addNewProductPartialRequest.ProductName,
            ProductDescription = addNewProductPartialRequest.ProductDescription,
            Price = addNewProductPartialRequest.Price,
            InStockCount = addNewProductPartialRequest.InStockCount,
            BrandId = addNewProductPartialRequest.BrandId,
            CategoryId = addNewProductPartialRequest.CategoryId,
            Photos = photos
        };

        var response = await _productService.AddNewProduct(completeRequest);

        return this.FormatResponse(response);
    }
}