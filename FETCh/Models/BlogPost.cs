namespace FETCh.Models
{
    public class BlogPost
    {
        public int Id { get; set; }                        // ��������� ������������� ������
        public string Title { get; set; }                  // ��������� �����
        public string Content { get; set; }                // �������� ������� / ����� �����
        public DateTime PublishedDate { get; set; }        // ���� ���������
        public string AuthorName { get; set; }             // ��'� ������ �����
        public string ImageUrl { get; set; }               // ���������� ��� ������� ����������
    }
}