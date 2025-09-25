using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
	public class CourseCategory
	{
		public int Id { get; set; }                        // Унікальний ідентифікатор категорії
		public string Name { get; set; }                   // Назва категорії
		public string Slug { get; set; }                   // URL-дружній ідентифікатор (для роутів)

        public ICollection<Course> Courses { get; set; } = new List<Course>(); 
    }
}