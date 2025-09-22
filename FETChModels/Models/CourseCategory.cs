namespace FETCh.Models
{
	public class CourseCategory
	{
		public int Id { get; set; }                        // Унікальний ідентифікатор категорії
		public string Name { get; set; }                   // Назва категорії
		public string Slug { get; set; }                   // URL-дружній ідентифікатор (для роутів)
	}
}