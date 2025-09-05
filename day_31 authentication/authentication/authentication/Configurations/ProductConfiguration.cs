using authentication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace authentication.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(p => p.Description)
            .HasMaxLength(500);
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Property(p => p.ImagePath)
            .HasMaxLength(200);
        builder.Property(p => p.IsApproved)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(p => p.Creator)
            .WithMany()
            .HasForeignKey(p => p.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}