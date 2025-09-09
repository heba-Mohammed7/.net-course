namespace studentCourse.Repositories.Interfaces;

public interface ICourseRepository : IGenericRepository<CourseEntity>
{
    Task<IQueryable<CourseEntity>> GetQueryableAsync(BaseSpecification<CourseEntity> spec = null);
}