namespace Application.Models.DTOs;

public record AddableProductModel(
    string ProductName,
    string ProductDescription,
    decimal Price,
    int InStockCount,
    int BrandId,
    int CategoryId
);