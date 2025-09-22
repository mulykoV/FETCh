namespace FETCh.Models
{
    
        public class Module
        {
            public int Id { get; set; }                        // ��������� ������������� ������
            public int CourseId { get; set; }                  // ��������� �� ���� (FK)
            public Course Course { get; set; }                 // ���������� ���������� �� ����
            public string Title { get; set; }                  // ����� ������
            public string Description { get; set; }           // ���� ������
            public int Order { get; set; }                     // ������� ����������� ������ � ����
            public ICollection<Lecture> Lectures { get; set; } // ������ ������
        }
}