using FETChModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FETCh.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        // 1. Remote Validation
        // ErrorMessage тут — це ключ ресурсу, якщо сервер впаде або поверне false
        [Remote(action: "CheckTitle", controller: "AdminCourses", AdditionalFields = "Id", ErrorMessage = "TitleTaken")]
        [Required(ErrorMessage = "TitleRequired")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "TitleLengthError")]
        [Display(Name = "CourseTitle")] // Можна теж використовувати ключ, якщо налаштовано
        public string Title { get; set; }

        [Required(ErrorMessage = "SubtitleRequired")]
        [StringLength(200, ErrorMessage = "SubtitleLengthError")]
        public string Subtitle { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "LanguageRequired")]
        public string Language { get; set; }

        public bool IsFree { get; set; }

        // 2. Range Validation
        [Range(0, 100000, ErrorMessage = "PriceRangeError")]
        [Remote(action: "CheckPriceLogic", controller: "AdminCourses", AdditionalFields = "IsFree", ErrorMessage = "PriceLogicError")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public string? PriceCurrency { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? BannerImageUrl { get; set; }

        [Range(1, 500, ErrorMessage = "DurationRangeError")]
        [Display(Name = "Duration")]
        public int DurationHours { get; set; }

        // 3. Email Address
        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "EmailFormatError")]
        [Display(Name = "CuratorEmail")]
        public string ContactEmail { get; set; } = string.Empty;

        // 4. Compare Validation
        [Required(ErrorMessage = "ConfirmEmailRequired")]
        [Compare("ContactEmail", ErrorMessage = "EmailMismatchError")]
        [Display(Name = "ConfirmEmail")]
        public string ConfirmContactEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "CategoryRequired")]
        public int CategoryId { get; set; }

        public List<CourseCategory>? Categories { get; set; }
    }
}