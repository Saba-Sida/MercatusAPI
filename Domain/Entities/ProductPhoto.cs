using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ProductPhoto
{
    [Key]
    public int ProductPhotoId { get; set; }
    public required string PhotoUri { get; set; }
    
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }
}