namespace studentCourse.Repositories.Interfaces;

public interface IStudentRepository : IGenericRepository<StudentEntity>
{
    Task<IQueryable<StudentEntity>> GetQueryableAsync(BaseSpecification<StudentEntity> spec = null);
}