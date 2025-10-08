using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    
        public class Module
        {
            public int Id { get; set; }                        // Унікальний ідентифікатор модуля
            public int CourseId { get; set; }                  // Посилання на курс (FK)
            public Course? Course { get; set; }                 // Навігаційна властивість на курс
            public string? Title { get; set; }                  // Назва модуля
            public string? Description { get; set; }           // Опис модуля
            public int Order { get; set; }                     // Порядок відображення модуля у курсі
            public ICollection<Lecture>? Lectures { get; set; } // Лекції модуля
        }
}