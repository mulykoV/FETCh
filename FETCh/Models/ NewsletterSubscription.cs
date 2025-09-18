namespace FETCh.Models
{
    public class NewsletterSubscription
    {
        public int Id { get; set; }                        // Унікальний ідентифікатор підписки
        public string Email { get; set; }                  // Email підписника
        public DateTime SubscribedDate { get; set; }       // Дата підписки
        public bool IsConfirmed { get; set; }              // Чи підтверджена підписка
    }
}