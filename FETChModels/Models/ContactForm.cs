using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Models
{
    public class ContactForm
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор заявки
        public string Name { get; set; }                   // Ім'я користувача, який залишив заявку
        public string Email { get; set; }                  // Email користувача
        public string Subject { get; set; }                // Тема повідомлення
        public string Message { get; set; }                // Текст повідомлення
        public DateTime SubmittedDate { get; set; }        // Дата та час відправки заявки
        public bool IsProcessed { get; set; }             // Позначка, оброблена заявка чи н
    }
}