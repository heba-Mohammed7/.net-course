using cleanArchitecture.Domain.Models.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cleanArchitecture.Infrastructure.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.Id);
        
        builder.Property(ci => ci.UnitPrice)
            .HasColumnType("decimal(18,2)");
            
        builder.Property(ci => ci.TotalPrice)
            .HasColumnType("decimal(18,2)");
            
        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasIndex(ci => new { ci.CartId, ci.ProductId });
        builder.HasQueryFilter(c => !c.IsDeleted);

    }
}
