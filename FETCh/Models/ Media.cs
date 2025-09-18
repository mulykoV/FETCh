namespace FETCh.Models
{
    public class Media
    {
        public int Id { get; set; }                        // ��������� ������������� ���������
        public string FileName { get; set; }               // ��'� �����
        public string FileUrl { get; set; }                // ��������� �� ���� (URL ��� ���� �� ������)
        public string MediaType { get; set; }              // ��� ���� (��������� "image", "video")
        public int? CourseId { get; set; }                 // �������: ����, �� ����� ����'������ ����
        public Course Course { get; set; }                 // ���������� ���������� �� ����
    }
}