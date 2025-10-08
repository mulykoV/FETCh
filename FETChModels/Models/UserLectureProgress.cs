using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class UserLectureProgress
    {
        public string UserId { get; set; }                 // Ідентифікатор користувача (FK)
        public ApplicationUser User { get; set; }          // Навігаційна властивість на користувача

        public int LectureId { get; set; }                 // Ідентифікатор лекції (FK)
        public Lecture Lecture { get; set; }               // Навігаційна властивість на лекцію

        public bool Watched { get; set; }                  // Чи переглянута лекція
        public DateTime? WatchedDate { get; set; }         // Дата перегляду
        public double ProgressPercentage { get; set; }     // ??? Відсоток прогресу лекції
    }
}