namespace FETCh.Models
{
    public class UserCourse
    {
        public int UserId { get; set; }                    // ������������� ����������� (FK)
        public User User { get; set; }                     // ���������� ���������� �� �����������
        public int CourseId { get; set; }                  // ������������� ����� (FK)
        public Course Course { get; set; }                 // ���������� ���������� �� ����
        public DateTime EnrolledDate { get; set; }         // ���� ������ �� ����
        public bool IsCompleted { get; set; }              // �� �������� ���� ����������
        public DateTime? CompletedDate { get; set; }       // ���� ���������� (���� ��������)
    }
}