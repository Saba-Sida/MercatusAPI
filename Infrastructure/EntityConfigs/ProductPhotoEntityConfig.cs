using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigs;

public class ProductPhotoEntityConfig : IEntityTypeConfiguration<ProductPhoto>
{
    public void Configure(EntityTypeBuilder<ProductPhoto> builder)
    {
        builder.HasKey(photo => photo.ProductPhotoId);
        
        builder.HasOne(photo => photo.Product)
            .WithMany(product => product.ProductPhotos)
            .HasForeignKey(photo => photo.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}