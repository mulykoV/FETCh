using FETChModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchData.Interfaces
{
    public interface IFETChRepository : IRepository
    {
        //Пошук користувача за email
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<List<CourseCategory>> GetAllCategoriesAsync();
        Task<CourseCategory?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CourseCategory category);
        Task UpdateCategoryAsync(CourseCategory category);
        Task DeleteCategoryAsync(CourseCategory category);
        Task<bool> CategoryHasCoursesAsync(int categoryId);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task EnrollUserAsync(int courseId, string userId);
        Task<bool> IsUserEnrolledAsync(int courseId, string userId);
        Task<Lecture?> GetLectureByIdAsync(int id);
        Task AddLectureAsync(Lecture lecture);
        Task UpdateLectureAsync(Lecture lecture);
        Task DeleteLectureAsync(int id);
        Task<Module?> GetModuleByIdAsync(int id);
        Task AddModuleAsync(Module module);
        Task UpdateModuleAsync(Module module);
        Task DeleteModuleAsync(int id);
        Task<Course?> GetCourseDetailsAsync(int id);
        Task<IEnumerable<UserCourse>> GetUserCoursesAsync(string userId);
        Task<UserLectureProgress?> GetUserLectureProgressAsync(string userId, int lectureId);
        Task AddUserLectureProgressAsync(UserLectureProgress progress);
        Task UpdateUserLectureProgressAsync(UserLectureProgress progress);
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId);
        Task<IEnumerable<UserLectureProgress>> GetLectureProgressForUserAsync(int lectureId, string userId);
    }
}
