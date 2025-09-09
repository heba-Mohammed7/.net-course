using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using studentCourse.Models;

namespace studentCourse.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasMany(s => s.Learns)
            .WithOne(l => l.Student)
            .HasForeignKey(l => l.StudentId);
    }
}