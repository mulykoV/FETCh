namespace FETCh.Models
{
    public class Instructor
    {
        public int Id { get; set; }                        // ��������� ������������� ���������
        public string FullName { get; set; }               // ����� ��'� ���������
        public string Bio { get; set; }                    // ��������� / ������� ���������� ��� ���������
        public string AvatarUrl { get; set; }              // ��������� �� ����/������ ���������
        public string Email { get; set; }                  // ���������� email ���������
        public ICollection<Course> Courses { get; set; }   // �����, �� ���� ��������
    }
}