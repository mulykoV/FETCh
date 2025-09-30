using FetchData.Data;
using FetchData.Interfaces;
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
     public class BaseSQLServrRepository<TDbContext> : IFETChRepository
        where TDbContext : FETChDbContext
    {
        private FETChDbContext db;

        public BaseSQLServrRepository(FETChDbContext db)
        {
            this.db = db;
        }

        protected TDbContext Db { get; set;}
        public async Task<int> AddAsync<T>(T item) where T : class
        {
            Db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return await Db.SaveChangesAsync();
        }

        public IQueryable<T?> All<T>() where T : class
        {
            return Db.Model.FindEntityType(typeof(T)) != null
                ? Db.Set<T>().AsQueryable()
                : new Collection<T>().AsQueryable();
        }

        public async Task<T?> FirstOrDefaultAsynk<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await All<T>().FirstOrDefaultAsync(expression);
        }

        public IQueryable<T?> ReadAll<T>() where T : class
        {
            return All<T>().AsNoTracking();
        }

        public async Task<T?> ReadSingleAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await ReadAll<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<IQueryable<T?>> ReadWhere<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return ReadAll<T>().Where(expression);
        }

        public async Task<int> RemoveAsync<T>(T item) where T : class
        {
            Db.Remove(item);

            return await Db.SaveChangesAsync();
        }

        public async Task<T?> SingleAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await All<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<int> UpdateAsync<T>(T item) where T : class
        {
            var local = Db.Set<T>()
                .Local
                .FirstOrDefault(entry => entry == item);

            if(local != null)
            {
                Db.Entry(local).State = EntityState.Detached;
            }
            else
            {
                Db.Entry(item).State = EntityState.Modified;
            }

            Db.Update(item);

            return await Db.SaveChangesAsync();
        }

        IQueryable<T?> IRepository.ReadWhere<T>(Expression<Func<T, bool>> expression) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
