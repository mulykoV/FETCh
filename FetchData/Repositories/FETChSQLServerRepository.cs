using FetchData.Data;
using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchData.Repositories
{
    public class FETChSQLServerRepository : BaseSQLServrRepository<FETChDbContext>, IFETChRepository
    {
        public FETChSQLServerRepository(FETChDbContext db) : base(db)
        {
            Db = db;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await Db.Set<ApplicationUser>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<CourseCategory>> GetAllCategoriesAsync()
        {
            return await Db.CourseCategories
                .Include(c => c.Courses)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CourseCategory?> GetCategoryByIdAsync(int id)
        {
            return await Db.CourseCategories
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCategoryAsync(CourseCategory category)
        {
            await AddAsync(category);
        }

        public async Task UpdateCategoryAsync(CourseCategory category)
        {
            Db.CourseCategories.Update(category);
            await Db.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(CourseCategory category)
        {
            Db.CourseCategories.Remove(category);
            await Db.SaveChangesAsync();
        }

        public async Task<bool> CategoryHasCoursesAsync(int categoryId)
        {
            return await Db.Courses.AnyAsync(c => c.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await Db.Courses.Include(c => c.Category).ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await Db.Courses
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCourseAsync(Course course)
        {
            Db.Courses.Add(course);
            await Db.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            Db.Update(course);
            await Db.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await Db.Courses.FindAsync(id);
            if (course != null)
            {
                Db.Courses.Remove(course);
                await Db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await Db.Users.ToListAsync();
        }

        public async Task<bool> IsUserEnrolledAsync(int courseId, string userId)
        {
            return await Db.UserCourses
                .AnyAsync(uc => uc.CourseId == courseId && uc.UserId == userId);
        }

        public async Task EnrollUserAsync(int courseId, string userId)
        {
            Db.UserCourses.Add(new UserCourse
            {
                CourseId = courseId,
                UserId = userId,
                EnrolledDate = DateTime.UtcNow,
                IsCompleted = false
            });
            await Db.SaveChangesAsync();
        }

        public async Task<Lecture?> GetLectureByIdAsync(int id)
        {
            return await Db.Lectures
                .Include(l => l.Module)
                .ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddLectureAsync(Lecture lecture)
        {
            Db.Lectures.Add(lecture);
            await Db.SaveChangesAsync();
        }

        public async Task UpdateLectureAsync(Lecture lecture)
        {
            Db.Lectures.Update(lecture);
            await Db.SaveChangesAsync();
        }

        public async Task DeleteLectureAsync(int id)
        {
            var lecture = await Db.Lectures.FindAsync(id);
            if (lecture != null)
            {
                Db.Lectures.Remove(lecture);
                await Db.SaveChangesAsync();
            }
        }

        // --- МОДУЛІ ---
        public async Task<Module?> GetModuleByIdAsync(int id)
        {
            return await Db.Modules
                .Include(m => m.Course)
                .Include(m => m.Lectures)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddModuleAsync(Module module)
        {
            Db.Modules.Add(module);
            await Db.SaveChangesAsync();
        }

        public async Task UpdateModuleAsync(Module module)
        {
            Db.Modules.Update(module);
            await Db.SaveChangesAsync();
        }

        public async Task DeleteModuleAsync(int id)
        {
            var module = await Db.Modules.FindAsync(id);
            if (module != null)
            {
                Db.Modules.Remove(module);
                await Db.SaveChangesAsync();
            }
        }

        public async Task<Course?> GetCourseDetailsAsync(int courseId)
        {
            return await Db.Courses
                .Include(c => c.Modules)               // Підвантажуємо модулі курсу
                    .ThenInclude(m => m.Lectures)     // Підвантажуємо лекції кожного модуля
                .Include(c => c.Category)              // Підвантажуємо категорію курсу
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<IEnumerable<UserCourse>> GetUserCoursesAsync(string userId)
        {
            return await Db.UserCourses
                .Where(uc => uc.UserId == userId)
                .Include(uc => uc.Course)
                    .ThenInclude(c => c.Modules)
                .ToListAsync();
        }

        public async Task<UserLectureProgress?> GetUserLectureProgressAsync(string userId, int lectureId)
        {
            return await Db.UserLectureProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.LectureId == lectureId);
        }

        public async Task AddUserLectureProgressAsync(UserLectureProgress progress)
        {
            Db.UserLectureProgresses.Add(progress);
            await Db.SaveChangesAsync();
        }

        public async Task UpdateUserLectureProgressAsync(UserLectureProgress progress)
        {
            Db.UserLectureProgresses.Update(progress);
            await Db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            return await Db.Modules
                .Where(m => m.CourseId == courseId)
                .Include(m => m.Lectures)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserLectureProgress>> GetLectureProgressForUserAsync(int lectureId, string userId)
        {
            return await Db.UserLectureProgresses
                .Where(p => p.LectureId == lectureId && p.UserId == userId)
                .ToListAsync();
        }
    }
}
