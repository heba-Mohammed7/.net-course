namespace studentCourse.Repositories.Implementations;

public class LearnRepository : GenericRepository<LearnEntity>, ILearnRepository
{
    public LearnRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsAsync(int studentId, int courseId)
    {
        return await _context.Set<LearnEntity>().AnyAsync(l => l.StudentId == studentId && l.CourseId == courseId);
    }

    public async Task AddAsync(LearnEntity entity)
    {
        await _context.Set<LearnEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(LearnEntity entity)
    {
        _context.Set<LearnEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}