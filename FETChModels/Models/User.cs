using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class User
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор користувача
        public string Email { get; set; }                  // Email користувача
        public string FullName { get; set; }               // Повне ім’я користувача
        public string PasswordHash { get; set; }           // Хеш пароля для безпеки
        public string AvatarUrl { get; set; }              // Аватар користувача
        public DateTime RegisteredDate { get; set; }       // Дата реєстрації
        public ICollection<UserCourse> Enrollments { get; set; } // Записані курси
        public ICollection<UserLectureProgress> LectureProgresses { get; set; } // Прогрес по лекціях
    }
}