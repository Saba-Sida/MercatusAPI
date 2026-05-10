using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Brand
{
    [Key]
    public int BrandId { get; set; }
    public required string BrandName { get; set; }

    public List<Product> Products { get; set; } = new();
}