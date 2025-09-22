namespace FETCh.Models
{
    public class Review
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор відгуку
        public int CourseId { get; set; }                  // Ідентифікатор курсу (FK)
        public Course Course { get; set; }                 // Навігаційна властивість на курс
        public int UserId { get; set; }                    // Ідентифікатор користувача (FK)
        public User User { get; set; }                     // Навігаційна властивість на користувача
        public string Text { get; set; }                   // Текст відгуку
        public int Rating { get; set; }                    // Оцінка (1–5)
        public DateTime PostedDate { get; set; }           // Дата публікації відгуку
    }
}