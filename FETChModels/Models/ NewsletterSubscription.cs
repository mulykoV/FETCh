namespace FETCh.Models
{
    public class NewsletterSubscription
    {
        public int Id { get; set; }                        // ��������� ������������� �������
        public string Email { get; set; }                  // Email ���������
        public DateTime SubscribedDate { get; set; }       // ���� �������
        public bool IsConfirmed { get; set; }              // �� ����������� �������
    }
}