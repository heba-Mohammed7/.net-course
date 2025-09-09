namespace studentCourse.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.HasMany(c => c.Learns)
            .WithOne(l => l.Course)
            .HasForeignKey(l => l.CourseId);
    }
}