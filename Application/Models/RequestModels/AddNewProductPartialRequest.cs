using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels;

public record AddNewProductPartialRequest(
    [Required] string ProductName,
    [Required] string ProductDescription,
    [Required] decimal Price,
    [Required] int InStockCount,
    [Required] int BrandId,
    [Required] int CategoryId
    );