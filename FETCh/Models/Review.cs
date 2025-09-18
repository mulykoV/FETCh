namespace FETCh.Models
{
    public class Review
    {
        public int Id { get; set; }                        // ��������� ������������� ������
        public int CourseId { get; set; }                  // ������������� ����� (FK)
        public Course Course { get; set; }                 // ���������� ���������� �� ����
        public int UserId { get; set; }                    // ������������� ����������� (FK)
        public User User { get; set; }                     // ���������� ���������� �� �����������
        public string Text { get; set; }                   // ����� ������
        public int Rating { get; set; }                    // ������ (1�5)
        public DateTime PostedDate { get; set; }           // ���� ��������� ������
    }
}