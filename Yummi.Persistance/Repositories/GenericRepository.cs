using Microsoft.EntityFrameworkCore;
using Yummi.Core.Entities;
using Yummi.Core.Interfaces.Generic;
using Yummi.Persistance.DataContext;

namespace Yummi.Persistance.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbSet<T> _entities;
        public GenericRepository(YummiDbContext context)
        {
            //_context = context;
            _entities = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            // _context.Set<T>().AddRange(entities);
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            _entities.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _entities.FindAsync(id);
            return entity;
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            // _context.Set<T>().RemoveRange(entities);
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
