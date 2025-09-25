using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FETChModels.Models;

namespace FetchData.Data
{
    public class FETChDbContext : IdentityDbContext
    {
        public FETChDbContext(DbContextOptions<FETChDbContext> options)
            : base(options)
        {
        }

        // Основні таблиці
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }

        // Користувачі та прогрес
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserLectureProgress> UserLectureProgresses { get; set; }

        // Додаткові сутності
        public DbSet<Review> Reviews { get; set; }
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }

        // Розширені моделі
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Course ↔ Tag (many-to-many)
            modelBuilder.Entity<CourseTag>()
                .HasKey(ct => new { ct.CourseId, ct.TagId });

            modelBuilder.Entity<CourseTag>()
                .HasOne(ct => ct.Course)
                .WithMany(c => c.CourseTags)
                .HasForeignKey(ct => ct.CourseId);

            modelBuilder.Entity<CourseTag>()
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.CourseTags)
                .HasForeignKey(ct => ct.TagId);

            // CourseCategory ↔ Course (one-to-many)
            modelBuilder.Entity<CourseCategory>()
                .HasMany(cc => cc.Courses)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            // Course ↔ Module (one-to-many)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Modules)
                .WithOne(m => m.Course)
                .HasForeignKey(m => m.CourseId);

            // Module ↔ Lecture (one-to-many)
            modelBuilder.Entity<Module>()
                .HasMany(m => m.Lectures)
                .WithOne(l => l.Module)
                .HasForeignKey(l => l.ModuleId);

            // Course ↔ Review (one-to-many)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);

            // Course ↔ UserCourse (one-to-many)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.UserCourses)
                .WithOne(uc => uc.Course)
                .HasForeignKey(uc => uc.CourseId);

            // Lecture ↔ UserLectureProgress (one-to-many)
            modelBuilder.Entity<Lecture>()
                .HasMany(l => l.UserLectureProgresses)
                .WithOne(ulp => ulp.Lecture)
                .HasForeignKey(ulp => ulp.LectureId);
        }
    }
}
