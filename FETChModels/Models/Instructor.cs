namespace FETCh.Models
{
    public class Instructor
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор викладача
        public string FullName { get; set; }               // Повне ім'я викладача
        public string Bio { get; set; }                    // Біографія / коротка інформація про викладача
        public string AvatarUrl { get; set; }              // Посилання на фото/аватар викладача
        public string Email { get; set; }                  // Контактний email викладача
        public ICollection<Course> Courses { get; set; }   // Курси, які веде викладач
    }
}