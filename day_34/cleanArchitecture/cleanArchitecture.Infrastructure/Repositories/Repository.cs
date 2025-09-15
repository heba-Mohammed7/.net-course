
using Ardalis.Specification.EntityFrameworkCore;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Infrastructure.Data;

namespace cleanArchitecture.Infrastructure.Repositories;

public class Repository<TEntity>(ApplicationDbContext dbContext)
    : RepositoryBase<TEntity>(dbContext), IRepository<TEntity> where TEntity : class;
