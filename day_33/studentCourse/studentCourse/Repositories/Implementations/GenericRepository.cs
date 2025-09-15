using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace studentCourse.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IQueryable<TEntity>> GetQueryableAsync(BaseSpecification<TEntity>? spec = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (spec != null)
            {

                foreach (var criteria in spec.Criterias)
                {
                    query = query.Where(criteria);
                }

                foreach (var include in spec.Includes)
                {
                    query = query.Include(include);
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

            return await Task.FromResult(query);
        }

        public async Task<bool> ExistsAsync(int studentId, int courseId)
        {
            if (typeof(TEntity) == typeof(LearnEntity))
            {
                return await _context.Set<LearnEntity>()
                    .AnyAsync(l => l.StudentId == studentId && l.CourseId == courseId);
            }

            throw new InvalidOperationException("ExistsAsync with studentId and courseId is only supported for LearnEntity");
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}