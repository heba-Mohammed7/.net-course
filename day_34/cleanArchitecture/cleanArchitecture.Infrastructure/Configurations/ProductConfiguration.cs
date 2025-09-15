using cleanArchitecture.Domain.Models;
using cleanArchitecture.Domain.Models.Categories;
using cleanArchitecture.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cleanArchitecture.Infrastructure.Configurations;

public class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(ProductConstants.ProductNameMaxLengthValue);
            
        builder
            .Property(x => x.Price)
            .HasPrecision(18, 2)
            .IsRequired();
            
        builder
            .HasOne<Category>(s => s.Category)
            .WithMany(g => g.Products)
            .HasForeignKey(s => s.CategoryId);
        
        builder
            .Property(x=>x.CreatedAt)
            .HasDefaultValueSql("getdate()");
            
        builder
            .Property(x=>x.UpdatedAt)
            .HasDefaultValueSql("getdate()");
        builder
            .HasQueryFilter(p => !p.IsDeleted);
    }

}