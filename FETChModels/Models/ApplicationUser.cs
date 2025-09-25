using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }    
        public string? FullName { get; set; }               // Повне ім’я користувача
        public string? AvatarUrl { get; set; }              // Аватар користувача
        public DateTime? RegisteredDate { get; set; }       // Дата реєстрації
        public ICollection<UserCourse>? Enrollments { get; set; } // Записані курси
        public ICollection<UserLectureProgress>? LectureProgresses { get; set; } // Прогрес по лекціях
    }
}