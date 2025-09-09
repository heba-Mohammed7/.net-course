namespace studentCourse.Repositories.Interfaces
{
    public interface IGenericRepository <TEntity> where TEntity : class
    {
        public Task Create(TEntity entity);

        public Task<List<TEntity>> GetAll();

        public Task<TEntity> GetById(int id);

        public Task Update(TEntity entity);

        public void Delete(TEntity entity);
        public Task DeleteAsync(TEntity entity);
    }
}
