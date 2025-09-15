using cleanArchitecture.Domain.Models.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cleanArchitecture.Infrastructure.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.SessionId)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.TotalAmount)
            .HasColumnType("decimal(18,2)");
            
        builder.HasMany(c => c.CartItems)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(c => c.SessionId);
        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}
