using FetchData.Data;
using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchData.Repositories
{
    public class FETChSQLServerRepository : BaseSQLServrRepository<FETChDbContext>, IFETChRepository
    {
        public FETChSQLServerRepository(FETChDbContext db) : base(db) { }

        // --- КОРИСТУВАЧІ ---
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await ReadAll<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await ReadAll<ApplicationUser>().ToListAsync();
        }

        // --- КАТЕГОРІЇ ---
        public async Task<List<CourseCategory>> GetAllCategoriesAsync()
        {
            return await ReadAll<CourseCategory>()
                .Include(c => c.Courses)
                .ToListAsync();
        }

        public async Task<CourseCategory?> GetCategoryByIdAsync(int id)
        {
            return await ReadAll<CourseCategory>()
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCategoryAsync(CourseCategory category)
        {
            await AddAsync(category);
        }

        public async Task UpdateCategoryAsync(CourseCategory category)
        {
            await UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(CourseCategory category)
        {
            await RemoveAsync(category);
        }

        public async Task<bool> CategoryHasCoursesAsync(int categoryId)
        {
            return await ExistsAsync<Course>(c => c.CategoryId == categoryId);
        }

        // --- КУРСИ ---
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await ReadAll<Course>()
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await ReadAll<Course>()
                .Include(c => c.Modules)
                    .ThenInclude(m => m.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCourseAsync(Course course)
        {
            await AddAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await FirstOrDefaultAsync<Course>(c => c.Id == id);
            if (course != null)
                await RemoveAsync(course);
        }

        public async Task<Course?> GetCourseDetailsAsync(int courseId)
        {
            return await ReadAll<Course>()
                .Include(c => c.Modules)
                    .ThenInclude(m => m.Lectures)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        // --- МОДУЛІ ---
        public async Task<Module?> GetModuleByIdAsync(int id)
        {
            return await ReadAll<Module>()
                .Include(m => m.Course)
                .Include(m => m.Lectures)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddModuleAsync(Module module)
        {
            await AddAsync(module);
        }

        public async Task UpdateModuleAsync(Module module)
        {
            await UpdateAsync(module);
        }

        public async Task DeleteModuleAsync(int id)
        {
            var module = await FirstOrDefaultAsync<Module>(m => m.Id == id);
            if (module != null)
                await RemoveAsync(module);
        }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            return await ReadWhere<Module>(m => m.CourseId == courseId)
                .Include(m => m.Lectures)
                .ToListAsync();
        }

        // --- ЛЕКЦІЇ ---
        public async Task<Lecture?> GetLectureByIdAsync(int id)
        {
            return await ReadAll<Lecture>()
                .Include(l => l.Module)
                    .ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddLectureAsync(Lecture lecture)
        {
            await AddAsync(lecture);
        }

        public async Task UpdateLectureAsync(Lecture lecture)
        {
            await UpdateAsync(lecture);
        }

        public async Task DeleteLectureAsync(int id)
        {
            var lecture = await FirstOrDefaultAsync<Lecture>(l => l.Id == id);
            if (lecture != null)
                await RemoveAsync(lecture);
        }

        // --- КУРСИ КОРИСТУВАЧІВ ---
        public async Task<bool> IsUserEnrolledAsync(int courseId, string userId)
        {
            return await ExistsAsync<UserCourse>(uc => uc.CourseId == courseId && uc.UserId == userId);
        }

        public async Task EnrollUserAsync(int courseId, string userId)
        {
            await AddAsync(new UserCourse
            {
                CourseId = courseId,
                UserId = userId,
                EnrolledDate = DateTime.UtcNow,
                IsCompleted = false
            });
        }

        public async Task<IEnumerable<UserCourse>> GetUserCoursesAsync(string userId)
        {
            return await ReadWhere<UserCourse>(uc => uc.UserId == userId)
                .Include(uc => uc.Course)
                    .ThenInclude(c => c.Modules)
                .ToListAsync();
        }
        public async Task MarkCourseAsCompletedAsync(string userId, int courseId)
        {
            // Шукаємо запис про запис користувача на курс
            var userCourse = await ReadAll<UserCourse>()
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);

            // Якщо запис знайдено, оновлюємо статус
            if (userCourse != null)
            {
                userCourse.IsCompleted = true;
                userCourse.CompletedDate = DateTime.UtcNow; // Використовуємо UtcNow, як і в методі EnrollUserAsync

                await UpdateAsync(userCourse);
            }
        }

        // --- ПРОГРЕС ЛЕКЦІЙ ---
        public async Task<UserLectureProgress?> GetUserLectureProgressAsync(string userId, int lectureId)
        {
            return await FirstOrDefaultAsync<UserLectureProgress>(
                p => p.UserId == userId && p.LectureId == lectureId);
        }

        public async Task AddUserLectureProgressAsync(UserLectureProgress progress)
        {
            await AddAsync(progress);
        }

        public async Task UpdateUserLectureProgressAsync(UserLectureProgress progress)
        {
            await UpdateAsync(progress);
        }

        public async Task<IEnumerable<UserLectureProgress>> GetLectureProgressForUserAsync(int lectureId, string userId)
        {
            return await ReadWhere<UserLectureProgress>(p => p.LectureId == lectureId && p.UserId == userId)
                .ToListAsync();
        }
    }
}
