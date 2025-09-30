using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FetchData.Interfaces
{
    public interface IRepository
    {
        IQueryable<T?> All<T>() where T : class;
        IQueryable<T?> ReadAll<T>() where T : class;

        IQueryable<T?> ReadWhere<T>(Expression<Func<T, bool>> expression) where T : class;

        Task<T?> FirstOrDefaultAsynk<T>(Expression<Func<T, bool>> expression) where T : class;

        Task<T?> SingleAsync<T>(Expression<Func<T, bool>> expression) where T : class;

        Task<T?> ReadSingleAsync<T>(Expression<Func<T, bool>> expression) where T : class;

        Task<int> AddAsync<T>(T item) where T : class;
        Task<int> UpdateAsync<T>(T item) where T : class;
        Task<int> RemoveAsync<T>(T item) where T : class;
    }
}
