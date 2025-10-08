using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Review
    {
        public int Id { get; set; }                        // Óí³êàëüíèé ³äåíòèô³êàòîð â³äãóêó
        public int CourseId { get; set; }                  // ²äåíòèô³êàòîð êóðñó (FK)
        public Course? Course { get; set; }                 // Íàâ³ãàö³éíà âëàñòèâ³ñòü íà êóðñ

        public string? UserId { get; set; }                 // ²äåíòèô³êàòîð êîðèñòóâà÷à (FK, string äëÿ Identity)
        public ApplicationUser? User { get; set; }                     // Íàâ³ãàö³éíà âëàñòèâ³ñòü íà êîðèñòóâà÷à

        public string? Text { get; set; }                   // Òåêñò â³äãóêó
        public int Rating { get; set; }                    // Îö³íêà (1–5)
        public DateTime PostedDate { get; set; }
    }
}