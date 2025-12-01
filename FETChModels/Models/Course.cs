using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва курсу є обов'язковою")]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Назва курсу")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Підзаголовок обов'язковий")]
        [StringLength(200)]
        public string Subtitle { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Вкажіть мову курсу")]
        public string Language { get; set; }

        public bool IsFree { get; set; }

        [Range(0, 100000)]
        [Precision(18, 2)]
        [Display(Name = "Ціна курсу")]
        public decimal Price { get; set; }

        public string? PriceCurrency { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? BannerImageUrl { get; set; }

        [Range(1, 500)]
        public int DurationHours { get; set; }

        [Required(ErrorMessage = "Вкажіть Email куратора")]
        [EmailAddress]
        [Display(Name = "Email куратора")]
        public string ContactEmail { get; set; } = string.Empty;


        public int CategoryId { get; set; }
        public CourseCategory? Category { get; set; }

        public ICollection<Module>? Modules { get; set; }
        public ICollection<Lecture>? Lectures { get; set; }
        public ICollection<CourseTag>? CourseTags { get; set; }

        [NotMapped]
        public ICollection<UserCourse>? Enrollments { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

        public string AuthorId { get; set; } = string.Empty;
    }
}