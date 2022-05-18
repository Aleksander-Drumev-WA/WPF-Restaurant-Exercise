namespace WPF_Restaurant.DataAccess.Entities;
public class OrderItemEntity
{
	[Key]
	public int Id { get; set; }

	public bool IsCompleted { get; set; }

	public int DishId { get; set; }

	public virtual DishEntity Dish { get; set; }

	public int OrderId { get; set; }

	public virtual OrderEntity Order { get; set; }
}