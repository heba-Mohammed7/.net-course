
using Ardalis.Specification.EntityFrameworkCore;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Infrastructure.Data;

namespace cleanArchitecture.Infrastructure.Repositories;

public class ReadRepository<TEntity>(ApplicationDbContext dbContext)
    : RepositoryBase<TEntity>(dbContext), IReadRepository<TEntity> where TEntity : class;
