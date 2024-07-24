namespace CineMagic.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
