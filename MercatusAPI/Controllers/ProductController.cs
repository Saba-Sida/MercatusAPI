using Application.Models.RequestModels;
using Application.Services.Products;
using MercatusAPI.ExternalMethods;
using Microsoft.AspNetCore.Mvc;

namespace MercatusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> Products([FromQuery] GetProductPaginationRequest request)
    {
        var response = await _productService.GetPaginatedProducts(request);
        
        return this.FormatResponse(response);
    }
}