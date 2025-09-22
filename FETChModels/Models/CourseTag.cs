namespace FETCh.Models
{
    public class CourseTag
    {
        public int CourseId { get; set; }                  // ��������� �� ���� (FK)
        public Course Course { get; set; }                 // ���������� ���������� �� ����
        public int TagId { get; set; }                     // ��������� �� ��� (FK)
        public Tag Tag { get; set; }                       // ���������� ���������� �� ���
    }
}