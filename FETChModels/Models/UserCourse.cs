namespace FETCh.Models
{
    public class UserCourse
    {
        public int UserId { get; set; }                    // Ідентифікатор користувача (FK)
        public User User { get; set; }                     // Навігаційна властивість на користувача
        public int CourseId { get; set; }                  // Ідентифікатор курсу (FK)
        public Course Course { get; set; }                 // Навігаційна властивість на курс
        public DateTime EnrolledDate { get; set; }         // Дата запису на курс
        public bool IsCompleted { get; set; }              // Чи завершив курс користувач
        public DateTime? CompletedDate { get; set; }       // Дата завершення (якщо завершив)
    }
}