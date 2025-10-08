using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class CourseTag //???
    {
        public int CourseId { get; set; }                  // Посилання на курс (FK)
        public Course? Course { get; set; }                 // Навігаційна властивість на курс
        public int TagId { get; set; }                     // Посилання на тег (FK)
        public Tag? Tag { get; set; }                       // Навігаційна властивість на тег
    }
}