using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FETCh.FETChModels.Models;

namespace FetchData.Data
{
    public class FETChDbContext : IdentityDbContext
    {
        public FETChDbContext(DbContextOptions<FETChDbContext> options)
        : base(options)
        {
        }
        //основні таблиці
        public DbSet<Course> Courses { get; set; }              // Курси
        public DbSet<Module> Modules { get; set; }              // Модулі курсу
        public DbSet<Lecture> Lectures { get; set; }            // Лекції
        public DbSet<CourseCategory> CourseCategories { get; set; }  // Категорії курсів
        public DbSet<Tag> Tags { get; set; }                    // Теги
        public DbSet<CourseTag> CourseTags { get; set; }        // Зв’язок «Курс–Тег»
        //Користувачі та прогрес
        public DbSet<UserCourse> UserCourses { get; set; }      // Записи користувачів на курси
        public DbSet<UserLectureProgress> UserLectureProgresses { get; set; } // Прогрес по лекціях
        //Додаткові сутності
        public DbSet<Review> Reviews { get; set; }              // Відгуки про курси
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; } // Підписки на розсилку
        //Розширені моделі
        public DbSet<Instructor> Instructors { get; set; }      // Викладачі
        public DbSet<Promotion> Promotions { get; set; }        // Акції та знижки
        public DbSet<Media> Media { get; set; }                 // Медіафайли
        public DbSet<BlogPost> BlogPosts { get; set; }          // Пости у блозі
        public DbSet<ContactForm> ContactForms { get; set; }    // Заявки від користувачів

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Course ↔ Tag (many-to-many)
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
            //CourseCategory ↔ Course (one-to-many)
            modelBuilder.Entity<CourseCategory>()
                .HasMany(cc => cc.Courses)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);
            //Course ↔ Module (one-to-many)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Modules)
                .WithOne(m => m.Course)
                .HasForeignKey(m => m.CourseId);
            //Module ↔ Lecture (one-to-many)
            modelBuilder.Entity<Module>()
                .HasMany(m => m.Lectures)
                .WithOne(l => l.Module)
                .HasForeignKey(l => l.ModuleId);
            //Course ↔ Review (one-to-many)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);
            //Course ↔ UserCourse (one-to-many)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.UserCourses)
                .WithOne(uc => uc.Course)
                .HasForeignKey(uc => uc.CourseId);
            //Lecture ↔ UserLectureProgress (one-to-many)
            modelBuilder.Entity<Lecture>()
                .HasMany(l => l.UserLectureProgresses)
                .WithOne(ulp => ulp.Lecture)
                .HasForeignKey(ulp => ulp.LectureId);
            //Course ↔ Instructor (many-to-many)
            modelBuilder.Entity<CourseInstructor>()
                .HasKey(ci => new { ci.CourseId, ci.InstructorId });
            modelBuilder.Entity<CourseInstructor>()
                .HasOne(ci => ci.Course)
                .WithMany(c => c.CourseInstructors)
                .HasForeignKey(ci => ci.CourseId);
            modelBuilder.Entity<CourseInstructor>()
                .HasOne(ci => ci.Instructor)
                .WithMany(i => i.CourseInstructors)
                .HasForeignKey(ci => ci.InstructorId);
            //Course ↔ Promotion (many-to-many)
            modelBuilder.Entity<CoursePromotion>()
                .HasKey(cp => new { cp.CourseId, cp.PromotionId });
            modelBuilder.Entity<CoursePromotion>()
                .HasOne(cp => cp.Course)
                .WithMany(c => c.CoursePromotions)
                .HasForeignKey(cp => cp.CourseId);
            modelBuilder.Entity<CoursePromotion>()
                .HasOne(cp => cp.Promotion)
                .WithMany(p => p.CoursePromotions)
                .HasForeignKey(cp => cp.PromotionId);
        }

    }
}
