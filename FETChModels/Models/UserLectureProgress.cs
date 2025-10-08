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
        public string? UserId { get; set; }                 // ²äåíòèô³êàòîð êîðèñòóâà÷à (FK)
        public ApplicationUser? User { get; set; }                     // Íàâ³ãàö³éíà âëàñòèâ³ñòü íà êîðèñòóâà÷à

        public int LectureId { get; set; }                 // ²äåíòèô³êàòîð ëåêö³¿ (FK)
        public Lecture? Lecture { get; set; }               // Íàâ³ãàö³éíà âëàñòèâ³ñòü íà ëåêö³þ

        public bool Watched { get; set; }                  // ×è ïåðåãëÿíóòà ëåêö³ÿ
        public DateTime? WatchedDate { get; set; }         // Äàòà ïåðåãëÿäó
        public double ProgressPercentage { get; set; }     // ??? Â³äñîòîê ïðîãðåñó ëåêö³¿
    }
}