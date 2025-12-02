using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Lecture
    {
        public int Id { get; set; }                        // Óí³êàëüíèé ³äåíòèô³êàòîð ëåêö³¿
        public int ModuleId { get; set; }                  // Ïîñèëàííÿ íà ìîäóëü (FK)
        public Module? Module { get; set; }                 // Íàâ³ãàö³éíà âëàñòèâ³ñòü íà ìîäóëü[
        [Required]
        public string? Title { get; set; }                  // Íàçâà ëåêö³¿
        public string? Content { get; set; }                // Îñíîâíèé òåêñò àáî HTML êîíòåíò ëåêö³¿
        public string? VideoUrl { get; set; }               // Ïîñèëàííÿ íà â³äåî (ÿêùî º)
        public TimeSpan? Duration { get; set; }            // Òðèâàë³ñòü ëåêö³¿
        //public int? Order { get; set; }                     // ??? Ïîðÿäîê â³äîáðàæåííÿ ëåêö³¿ ó ìîäóë³

        public ICollection<UserLectureProgress>? UserLectureProgresses { get; set; } = new List<UserLectureProgress>();
    }
}