using System.Linq.Expressions;

namespace TimeTracker.Database.Services;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync (T entity);
}