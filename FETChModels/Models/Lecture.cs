using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Lecture
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор лекції
        public int ModuleId { get; set; }                  // Посилання на модуль (FK)
        public Module? Module { get; set; }                 // Навігаційна властивість на модуль
        public string? Title { get; set; }                  // Назва лекції
        public string? Content { get; set; }                // Основний текст або HTML контент лекції
        public string? VideoUrl { get; set; }               // Посилання на відео (якщо є)
        public TimeSpan? Duration { get; set; }            // Тривалість лекції
        public int Order { get; set; }                     // Порядок відображення лекції у модулі

        public ICollection<UserLectureProgress> UserLectureProgresses { get; set; } = new List<UserLectureProgress>();
    }
}