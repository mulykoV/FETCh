namespace FETCh.Models
{
    public class UserLectureProgress
    {
        public int UserId { get; set; }                    // ������������� ����������� (FK)
        public User User { get; set; }                     // ���������� ���������� �� �����������
        public int LectureId { get; set; }                 // ������������� ������ (FK)
        public Lecture Lecture { get; set; }               // ���������� ���������� �� ������
        public bool Watched { get; set; }                  // �� ����������� ������
        public DateTime? WatchedDate { get; set; }         // ���� ���������
        public double ProgressPercentage { get; set; }     // ³������ �������� ������
    }
}