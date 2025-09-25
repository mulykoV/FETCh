using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Promotion
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор акції
        public string Title { get; set; }                  // Назва акції
        public string Code { get; set; }                   // Промокод для застосування знижки
        public decimal DiscountPercentage { get; set; }    // Відсоток знижки (наприклад 10%)
        public decimal? DiscountAmount { get; set; }       // Фіксована сума знижки (якщо застосовується)
        public DateTime StartDate { get; set; }            // Дата початку дії акції
        public DateTime EndDate { get; set; }              // Дата завершення акції
        public bool IsActive { get; set; }                 // Активна акція чи ні
        public ICollection<Course> Courses { get; set; }   // Курси, до яких застосовується акція
    }
}