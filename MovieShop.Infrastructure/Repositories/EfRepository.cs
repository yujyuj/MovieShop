using Microsoft.EntityFrameworkCore;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly MovieShopDbContext _dbContext;
        public EfRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            // if a condition is passed in, get the count. Otherwise return the total count of the dataset.
            if(filter != null)
            {
                return await _dbContext.Set<T>().Where(filter).CountAsync();
            }
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _dbContext.Set<T>().Where(filter).AnyAsync();
            }
            return false;
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(); //make changes in SQL Server
            return entity;
        }

        public virtual async Task<bool> GetExistingAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _dbContext.Set<T>().Where(filter).AnyAsync();
            }
            return false;
        }
    }
}
