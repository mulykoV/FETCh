using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Instructor
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор викладача
        public string FullName { get; set; }               // Повне ім'я викладача
        public string Bio { get; set; }                    // Біографія / коротка інформація про викладача
        public string AvatarUrl { get; set; }              // Посилання на фото/аватар викладача
        public string Email { get; set; }                  // Контактний email викладача
        public ICollection<Course> Courses { get; set; }   // Курси, які веде викладач
    }
}