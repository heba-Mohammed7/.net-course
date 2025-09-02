namespace Task.interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    Task<TEntity?> GetByIdAsync(int id);
    IQueryable<TEntity> GetQueryable();
}