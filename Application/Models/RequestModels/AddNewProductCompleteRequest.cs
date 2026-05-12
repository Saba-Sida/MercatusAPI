namespace Application.Models.RequestModels;

public record AddNewProductCompleteRequest(
    string ProductName,
    string ProductDescription,
    decimal Price,
    int InStockCount,
    int BrandId,
    int CategoryId,
    List<(byte[], string)> Photos
);