using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Tag //???
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор тегу
        public string Name { get; set; }                   // Назва тегу
        public string Slug { get; set; }                   // URL-дружнє ім’я тегу
        public ICollection<CourseTag> CourseTags { get; set; } // Курси, які мають цей тег
    }
}