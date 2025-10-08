using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FETChModels.Models
{
    public class Media //???
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор медіафайлу
        public string FileName { get; set; }               // Ім'я файлу
        public string FileUrl { get; set; }                // Посилання на файл (URL або шлях на сервері)
        public string MediaType { get; set; }              // Тип медіа (наприклад "image", "video")
        public int? CourseId { get; set; }                 // Опційно: курс, до якого прив'язаний файл
        public Course Course { get; set; }                 // Навігаційна властивість на курс
    }
}