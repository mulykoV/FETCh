namespace FETCh.Models
{
    public class UserLectureProgress
    {
        public int UserId { get; set; }                    // Ідентифікатор користувача (FK)
        public User User { get; set; }                     // Навігаційна властивість на користувача
        public int LectureId { get; set; }                 // Ідентифікатор лекції (FK)
        public Lecture Lecture { get; set; }               // Навігаційна властивість на лекцію
        public bool Watched { get; set; }                  // Чи переглянута лекція
        public DateTime? WatchedDate { get; set; }         // Дата перегляду
        public double ProgressPercentage { get; set; }     // Відсоток прогресу лекції
    }
}