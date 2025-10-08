using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    
        public class Module
        {
            public int Id { get; set; }                        // Óí³êàëüíèé ³äåíòèô³êàòîð ìîäóëÿ
            public int CourseId { get; set; }                  // Ïîñèëàííÿ íà êóðñ (FK)
            public Course? Course { get; set; }                 // Íàâ³ãàö³éíà âëàñòèâ³ñòü íà êóðñ
            public string? Title { get; set; }                  // Íàçâà ìîäóëÿ
            public string? Description { get; set; }           // Îïèñ ìîäóëÿ
            public int Order { get; set; }                     // Ïîðÿäîê â³äîáðàæåííÿ ìîäóëÿ ó êóðñ³
            public ICollection<Lecture>? Lectures { get; set; } // Ëåêö³¿ ìîäóëÿ
        }
}