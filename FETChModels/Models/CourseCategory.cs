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
		public int Id { get; set; }                        // Óí³êàëüíèé ³äåíòèô³êàòîð êàòåãîð³¿
		public string? Name { get; set; }                   // Íàçâà êàòåãîð³¿
		public string? Slug { get; set; }                   // URL-äðóæí³é ³äåíòèô³êàòîð (äëÿ ðîóò³â)

        public ICollection<Course> Courses { get; set; } = new List<Course>(); 
    }
}