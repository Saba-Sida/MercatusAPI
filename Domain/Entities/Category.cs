using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Category
{
    [Key] 
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }
    
    public int? CategoryParentId { get; set; }
    [ForeignKey("CategoryParentId")]
    public Category? CategoryParent { get; set; }
    
    public List<Product> Products { get; set; } = new();
    public List<Category> SubCategories { get; set; } = new();
}