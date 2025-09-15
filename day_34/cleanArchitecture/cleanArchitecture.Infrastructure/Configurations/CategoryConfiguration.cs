using cleanArchitecture.Domain.Models;
using cleanArchitecture.Domain.Models.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cleanArchitecture.Infrastructure.Configurations;

public class CategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(CategoryConstants.CategoryNameMaxLengthValue);

            
        builder
            .HasMany(s => s.Products)
            .WithOne(g => g.Category)
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