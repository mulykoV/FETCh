using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Models
{
    public class BlogPost
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор запису
        public string Title { get; set; }                  // Заголовок посту
        public string Content { get; set; }                // Основний контент / текст посту
        public DateTime PublishedDate { get; set; }        // Дата публікації
        public string AuthorName { get; set; }             // Ім'я автора посту
        public string ImageUrl { get; set; }               // Обкладинка або головне зображення
    }
}