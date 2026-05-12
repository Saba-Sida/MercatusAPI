

namespace Application.Models.RequestModels;

public class AddNewBrandRequest
{
    public required string Name { get; set; }
    public (byte[], string)? BrandPhoto { get; set; }
}