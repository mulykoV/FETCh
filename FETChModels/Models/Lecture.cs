namespace FETCh.Models
{
    public class Lecture
    {
        public int Id { get; set; }                        // ��������� ������������� ������
        public int ModuleId { get; set; }                  // ��������� �� ������ (FK)
        public Module Module { get; set; }                 // ���������� ���������� �� ������
        public string Title { get; set; }                  // ����� ������
        public string Content { get; set; }                // �������� ����� ��� HTML ������� ������
        public string VideoUrl { get; set; }               // ��������� �� ���� (���� �)
        public TimeSpan? Duration { get; set; }            // ��������� ������
        public int Order { get; set; }                     // ������� ����������� ������ � �����
    }
}