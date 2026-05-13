namespace Application.Models.DTOs;

public record ProductViewingModel(
    int ProductId,
    string ProductName,
    string ProductDescription,
    decimal Price,
    int InStockCount,
    DateTime DateCreated,
    DateTime DateModified,
    int CategoryId,
    string CategoryName,
    int BrandId,
    string BrandName
    );