using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Course
    {
        public int Id { get; set; }                        // Óí³êàëüíèé ³äåíòèô³êàòîð êóðñó
        public required string Title { get; set; }                  // Íàçâà êóðñó
        public required string Subtitle { get; set; }               // Ï³äçàãîëîâîê àáî êîðîòêèé îïèñ êóðñó
        public string? Description { get; set; }           // Äåòàëüíèé îïèñ êóðñó
        public required string Language { get; set; }              // Ìîâà êóðñó ("ua", "en" òîùî)
        public bool IsFree { get; set; }                  // ×è áåçêîøòîâíèé êóðñ
        [Precision(18, 2)] // ßêùî çíà÷åííÿ âèõîäèòü çà ðàìêè (íàïðèêëàä, á³ëüøå í³æ 18 öèôð àáî á³ëüøå 2 çíàê³â ï³ñëÿ êîìè)
        public decimal Price { get; set; }                // Ö³íà êóðñó (ÿêùî ïëàòíèé)
        public string? PriceCurrency { get; set; }         // Âàëþòà (UAH, USD)
        public DateTime? StartDate { get; set; }          // Äàòà ïî÷àòêó êóðñó (ÿêùî º)
        public DateTime? EndDate { get; set; }            // Äàòà çàâåðøåííÿ êóðñó (ÿêùî º)
        public string? ImageUrl { get; set; }              // Çîáðàæåííÿ/îáêëàäèíêà êóðñó
        public string? BannerImageUrl { get; set; }        // Áàíåð êóðñó (äëÿ ãîëîâíî¿ ñòîð³íêè)
        public int DurationHours { get; set; }            // Îð³ºíòîâíà òðèâàë³ñòü êóðñó ó ãîäèíàõ
        public required CourseCategory? Category { get; set; }      // Êàòåãîð³ÿ êóðñó (çîâí³øíÿ ìîäåëü)
        public int CategoryId { get; set; }               // ²äåíòèô³êàòîð êàòåãîð³¿ (FK)
        public ICollection<Module>? Modules { get; set; }  // Ñïèñîê ìîäóë³â êóðñó
        public ICollection<Lecture>? Lectures { get; set; }// Ëåêö³¿, ÿêùî âîíè íå â ìîäóëÿõ
        public ICollection<CourseTag>? CourseTags { get; set; } // Òåãè êóðñó (äëÿ ô³ëüòð³â/ïîøóêó)
        [NotMapped]
        public ICollection<UserCourse>? Enrollments { get; set; } // Êîðèñòóâà÷³, çàïèñàí³ íà êóðñ
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    }
}