using System.Linq.Expressions;

namespace WebServer.Data.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression);
        void  CreateAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
