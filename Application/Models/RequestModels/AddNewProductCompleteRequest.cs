namespace Application.Models.RequestModels;

public class AddNewProductCompleteRequest
{
    public string ProductName { get; set; } = String.Empty;
    public string ProductDescription { get; set; } = String.Empty;
    public decimal Price {get; set;}
    public int InStockCount {get; set;}
    public int BrandId {get; set;}
    public int CategoryId {get; set;}
    public List<(byte[], string)> Photos = new();
}