namespace FETCh.Models
{
    public class Tag
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор тегу
        public string Name { get; set; }                   // Назва тегу
        public string Slug { get; set; }                   // URL-дружнє ім’я тегу
        public ICollection<CourseTag> CourseTags { get; set; } // Курси, які мають цей тег
    }
}