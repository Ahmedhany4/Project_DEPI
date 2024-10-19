using ContextFile;
using Microsoft.EntityFrameworkCore;
using CoffeeManagementSystem.Repositories.Interfaces;
using System.Linq.Expressions;

namespace CoffeeManagementSystem.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly MyDbContext _db;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(MyDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task<T> AddItem(T item)
        {
            await _dbSet.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _db.SaveChangesAsync();
            }
            else
            {
                // Handle the case when the entity is not found
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> criteria = null, string[] includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include).AsSplitQuery();
                }
            }

            if (criteria != null)
            {
                return await query.Where(criteria).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateItem(T item)
        {
            _dbSet.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
