namespace studentCourse.Repositories.Implementations;

public class StudentRepository : GenericRepository<StudentEntity>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IQueryable<StudentEntity>> GetQueryableAsync(BaseSpecification<StudentEntity> spec = null)
    {
        var query = _context.Set<StudentEntity>().AsQueryable();
        if (spec != null)
        {
            foreach (var criteria in spec.Criterias)
            {
                query = query.Where(criteria);
            }

            foreach (var include in spec.Includes)
            {
                query = query.Include(include);
                if (include.ToString().Contains("Learns"))
                {
                    query = query.Include(x => x.Learns).ThenInclude(l => l.Course);
                }
            }

            foreach (var includeString in spec.IncludeStrings)
            {
                query = query.Include(includeString);
            }

            if (spec.OrderByAsc != null)
            {
                query = query.OrderBy(spec.OrderByAsc);
            }
            else if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            if (spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
        }
        return query; 
    }
}