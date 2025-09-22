namespace FETCh.Models
{
    public class CourseTag
    {
        public int CourseId { get; set; }                  // Посилання на курс (FK)
        public Course Course { get; set; }                 // Навігаційна властивість на курс
        public int TagId { get; set; }                     // Посилання на тег (FK)
        public Tag Tag { get; set; }                       // Навігаційна властивість на тег
    }
}