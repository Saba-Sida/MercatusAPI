using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigs;

public class CategoryEntityConfig: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.CategoryId);

        builder.HasMany(category => category.Products)
            .WithOne(product => product.Category)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(c => c.CategoryParent)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.CategoryParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}