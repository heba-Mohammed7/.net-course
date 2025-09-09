namespace studentCourse.Configurations;

public class LearnConfiguration : IEntityTypeConfiguration<LearnEntity>
{
    public void Configure(EntityTypeBuilder<LearnEntity> builder)
    {
        builder.HasKey(l => new { l.StudentId, l.CourseId });
        builder.HasOne(l => l.Student)
            .WithMany(s => s.Learns)
            .HasForeignKey(l => l.StudentId);
        builder.HasOne(l => l.Course)
            .WithMany(c => c.Learns)
            .HasForeignKey(l => l.CourseId);
    }
}