using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace authentication.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "ProductCreator",
                NormalizedName = "PRODUCTCREATOR"
            }
        );
    }
}