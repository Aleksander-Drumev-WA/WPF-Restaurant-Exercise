namespace WPF_Restaurant.DataAccess.Entities;
public class OrderEntity
{
	public OrderEntity()
	{
		OrderItems = new HashSet<OrderItemEntity>();
		CreatedOn = DateTime.UtcNow;
	}

	[Key]
	public int Id { get; set; }

	public DateTime CreatedOn { get; set; }

	public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
}