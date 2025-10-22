using FetchData.Data;
using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FetchData.Repositories
{
    public class BaseSQLServrRepository<TDbContext> : IRepository
   where TDbContext : DbContext
    {
        protected readonly TDbContext Db;

        public BaseSQLServrRepository(TDbContext db)
        {
            Db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<int> AddAsync<T>(T item) where T : class
        {
            await Db.Set<T>().AddAsync(item);
            return await Db.SaveChangesAsync();
        }

        public IQueryable<T> All<T>() where T : class
        {
            // просто повертаємо Db.Set<T>()
            return Db.Set<T>();
        }

        public IQueryable<T> ReadAll<T>() where T : class
        {
            return All<T>().AsNoTracking();
        }

        public async Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await ReadAll<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<T?> ReadSingleAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await ReadAll<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await ReadAll<T>().AnyAsync(expression);
        }

        public IQueryable<T> ReadWhere<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return ReadAll<T>().Where(expression);
        }

        public async Task<int> RemoveAsync<T>(T item) where T : class
        {
            Db.Set<T>().Remove(item);
            return await Db.SaveChangesAsync();
        }

        public async Task<T?> SingleAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await ReadAll<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<int> UpdateAsync<T>(T item) where T : class
        {
            var local = Db.Set<T>().Local.FirstOrDefault(entry => entry == item);

            if (local != null)
                Db.Entry(local).State = EntityState.Detached;

            Db.Update(item);
            return await Db.SaveChangesAsync();
        }
    }
}
