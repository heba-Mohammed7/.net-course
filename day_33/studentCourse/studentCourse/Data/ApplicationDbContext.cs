using studentCourse.Configurations;

namespace studentCourse.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<LearnEntity> Learns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new LearnConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}