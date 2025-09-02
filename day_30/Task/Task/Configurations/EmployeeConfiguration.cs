using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task.Models;

namespace Task.Configurations;

public class EmployeeConfiguration: IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Gender).HasMaxLength(10);
        builder.Property(e => e.Address).HasMaxLength(200);
        builder.Property(e => e.Dob).IsRequired();
        builder.Property(e => e.Doj).IsRequired();
        builder.Property(e => e.ImagePath).HasMaxLength(200);
        builder.Property(e => e.WorksInSince).IsRequired();

        builder.HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}