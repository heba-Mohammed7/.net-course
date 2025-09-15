using Ardalis.Specification;

namespace cleanArchitecture.Application.Abstractions.Repositories;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class;
