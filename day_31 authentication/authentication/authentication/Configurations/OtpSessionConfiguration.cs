using authentication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace authentication.Configurations;

public class OtpSessionConfiguration : IEntityTypeConfiguration<OtpSession>
{
    public void Configure(EntityTypeBuilder<OtpSession> builder)
    {
        builder.HasKey(os => os.Id);
        builder.Property(os => os.UserId).IsRequired();
        builder.Property(os => os.OtpCode).IsRequired().HasMaxLength(6);
        builder.Property(os => os.SessionId).HasMaxLength(100);
        builder.Property(os => os.CreatedAt).IsRequired();
        builder.Property(os => os.ExpiresAt).IsRequired();
        builder.Property(os => os.IsUsed).IsRequired().HasDefaultValue(false);
        builder.Property(os => os.Purpose).IsRequired().HasMaxLength(50);

        builder.HasOne(os => os.User)
            .WithMany()
            .HasForeignKey(os => os.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}