namespace WPF_Restaurant.DataAccess.Entities;
public class DishEntity
{
	[Key]
	public int Id { get; set; }

	public string Name { get; set; }

	public string ImagePath { get; set; }

	public string Recipe { get; set; }

	public string Ingredients { get; set; }
}