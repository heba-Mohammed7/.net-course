using Ardalis.Specification;

namespace cleanArchitecture.Application.Abstractions.Repositories;

public interface IReadRepository<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class;
