namespace studentCourse.Repositories.Interfaces;

public interface ILearnRepository : IGenericRepository<LearnEntity>
{
    Task<bool> ExistsAsync(int studentId, int courseId);
    Task AddAsync(LearnEntity entity);
    Task RemoveAsync(LearnEntity entity);
}