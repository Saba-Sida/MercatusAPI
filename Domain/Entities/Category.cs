using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    [Key] 
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }
    
    public List<Product> Products { get; set; } = new();
}