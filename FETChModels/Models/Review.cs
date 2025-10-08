using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Review
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор відгуку
        public int CourseId { get; set; }                  // Ідентифікатор курсу (FK)
        public Course? Course { get; set; }                 // Навігаційна властивість на курс

        public string? UserId { get; set; }                 // Ідентифікатор користувача (FK, string для Identity)
        public ApplicationUser? User { get; set; }                     // Навігаційна властивість на користувача

        public string? Text { get; set; }                   // Текст відгуку
        public int Rating { get; set; }                    // Оцінка (1–5)
        public DateTime PostedDate { get; set; }
    }
}