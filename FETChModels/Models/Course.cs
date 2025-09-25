using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Course
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор курсу
        public string Title { get; set; }                  // Назва курсу
        public string Subtitle { get; set; }               // Підзаголовок або короткий опис курсу
        public string Description { get; set; }           // Детальний опис курсу
        public string Language { get; set; }              // Мова курсу ("ua", "en" тощо)
        public bool IsFree { get; set; }                  // Чи безкоштовний курс
        public decimal Price { get; set; }                // Ціна курсу (якщо платний)
        public string PriceCurrency { get; set; }         // Валюта (UAH, USD)
        public DateTime? StartDate { get; set; }          // Дата початку курсу (якщо є)
        public DateTime? EndDate { get; set; }            // Дата завершення курсу (якщо є)
        public string ImageUrl { get; set; }              // Зображення/обкладинка курсу
        public string BannerImageUrl { get; set; }        // Банер курсу (для головної сторінки)
        public int DurationHours { get; set; }            // Орієнтовна тривалість курсу у годинах
        public CourseCategory Category { get; set; }      // Категорія курсу (зовнішня модель)
        public int CategoryId { get; set; }               // Ідентифікатор категорії (FK)
        public ICollection<Module> Modules { get; set; }  // Список модулів курсу
        public ICollection<Lecture> Lectures { get; set; }// Лекції, якщо вони не в модулях
        public ICollection<CourseTag> CourseTags { get; set; } // Теги курсу (для фільтрів/пошуку)
        public ICollection<UserCourse> Enrollments { get; set; } // Користувачі, записані на курс
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    }
}