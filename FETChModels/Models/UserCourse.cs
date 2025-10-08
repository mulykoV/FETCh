using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class UserCourse
    {
        public string? UserId { get; set; }                 // ²äåíòèô³êàòîð êîðèñòóâà÷à (FK)
        public ApplicationUser? User { get; set; }                     // Íàâ³ãàö³éíà âëàñòèâ³ñòü íà êîðèñòóâà÷à

        public int CourseId { get; set; }                  // ²äåíòèô³êàòîð êóðñó (FK)
        public Course? Course { get; set; }                 // Íàâ³ãàö³éíà âëàñòèâ³ñòü íà êóðñ

        public DateTime EnrolledDate { get; set; }         // Äàòà çàïèñó íà êóðñ
        public bool IsCompleted { get; set; }              // ×è çàâåðøèâ êóðñ êîðèñòóâà÷
        public DateTime? CompletedDate { get; set; }       // Äàòà çàâåðøåííÿ (ÿêùî çàâåðøèâ)
    }
}