using FETChModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FetchData.Data.Repositories
{
    // 🔹 Універсальний інтерфейс для репозиторію
    // TEntity – будь-яка сутність (Course, Module, Lecture тощо)
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(object id); // Отримати по ключу
        Task<IEnumerable<TEntity>> GetAllAsync(); // Всі записи
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate); // Пошук з умовою

        Task AddAsync(TEntity entity); // Додати
        void Update(TEntity entity);   // Оновити
        void Remove(TEntity entity);   // Видалити
    }

    // 🔹 Реалізація універсального репозиторію
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly FETChDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(FETChDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }

    // 🔹 Інтерфейс Unit of Work
    // Координує збереження та доступ до кількох репозиторіїв
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Course> Courses { get; }
        IRepository<Module> Modules { get; }
        IRepository<Lecture> Lectures { get; }
        IRepository<Review> Reviews { get; }
        IRepository<Tag> Tags { get; }
        IRepository<UserCourse> UserCourses { get; }
        IRepository<UserLectureProgress> UserLectureProgresses { get; }

        Task<int> SaveChangesAsync();
    }

    // 🔹 Реалізація Unit of Work
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FETChDbContext _context;

        public IRepository<Course> Courses { get; }
        public IRepository<Module> Modules { get; }
        public IRepository<Lecture> Lectures { get; }
        public IRepository<Review> Reviews { get; }
        public IRepository<Tag> Tags { get; }
        public IRepository<UserCourse> UserCourses { get; }
        public IRepository<UserLectureProgress> UserLectureProgresses { get; }

        public UnitOfWork(FETChDbContext context)
        {
            _context = context;

            // Ініціалізація універсальних репозиторіїв
            Courses = new Repository<Course>(_context);
            Modules = new Repository<Module>(_context);
            Lectures = new Repository<Lecture>(_context);
            Reviews = new Repository<Review>(_context);
            Tags = new Repository<Tag>(_context);
            UserCourses = new Repository<UserCourse>(_context);
            UserLectureProgresses = new Repository<UserLectureProgress>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
