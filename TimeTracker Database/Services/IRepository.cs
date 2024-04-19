namespace TimeTracker.Database.Services;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync (T entity);

    Task<T?> GetByIDAsync (int id);

    List<T> Where (Func<T, bool> predicate);

    Task UpdateAsync(T entity);
}