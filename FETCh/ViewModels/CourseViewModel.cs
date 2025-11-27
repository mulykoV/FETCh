using FETChModels.Models;
using Microsoft.AspNetCore.Mvc; // Не забудь цей using для [Remote]
using System.ComponentModel.DataAnnotations;

namespace FETCh.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        // 1. Remote Validation
        [Remote(action: "CheckTitle", controller: "AdminCourses", ErrorMessage = "Така назва курсу вже зайнята!")]
        [Required(ErrorMessage = "TitleRequired")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Назва має бути від 5 до 100 символів")]
        [Display(Name = "Назва курсу")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Підзаголовок обов'язковий")]
        [StringLength(200, ErrorMessage = "Максимум 200 символів")]
        public string Subtitle { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Вкажіть мову курсу")]
        public string Language { get; set; }

        public bool IsFree { get; set; }

        // 2. Range Validation
        [Range(0, 100000, ErrorMessage = "PriceRange")]
        [Remote(action: "CheckPriceLogic", controller: "AdminCourses", AdditionalFields = "IsFree", ErrorMessage = "Некоректна ціна для обраного статусу (Безкоштовний/Платний)")]
        [Display(Name = "Ціна курсу")]
        public decimal Price { get; set; }

        public string? PriceCurrency { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? BannerImageUrl { get; set; }

        [Range(1, 500, ErrorMessage = "Тривалість має бути від 1 до 500 годин")]
        [Display(Name = "Тривалість (год)")]
        public int DurationHours { get; set; }

        // 3. Email Address
        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "EmailFormat")]
        [Display(Name = "Email куратора")]
        public string ContactEmail { get; set; } = string.Empty;

        // 4. Compare Validation (Тільки у ViewModel!)
        [Required(ErrorMessage = "Підтвердіть Email")]
        [Compare("ContactEmail", ErrorMessage = "Email адреси не співпадають")]
        [Display(Name = "Підтвердження Email")]
        public string ConfirmContactEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Оберіть категорію")]
        public int CategoryId { get; set; }

        public List<CourseCategory>? Categories { get; set; }
    }
}