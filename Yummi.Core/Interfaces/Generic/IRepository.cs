using Yummi.Core.Entities;

namespace Yummi.Core.Interfaces.Generic
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        Task DeleteAsync(int id);
        void RemoveRange(IEnumerable<T> entities);

    }
}
