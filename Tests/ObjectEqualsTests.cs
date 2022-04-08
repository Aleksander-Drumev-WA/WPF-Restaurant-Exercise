using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
	public class ObjectEqualsTests
	{
		private const int OrderNumber = 567;

		[SetUp]
		public void Setup()
		{
		}

		private IEnumerable<OrderItem> orderItems = new List<OrderItem>() {
			new OrderItem {
				Id = 1,
				OrderId = OrderNumber,
				Dish = new Dish { Id = 1, Name = "Dish 1" },
				IsCompleted = true,
			},
			new OrderItem {
				Id = 2,
				OrderId = OrderNumber,
				Dish = new Dish { Id = 1, Name = "Dish 1" }, // NOTE: New instance with Id = 1!!!
				IsCompleted = false,
			},
			new OrderItem {
				Id = 3,
				OrderId = OrderNumber,
				Dish = new Dish { Id = 2, Name = "Dish 2" },
				IsCompleted = false,
			},
			new OrderItem {
				Id = 4,
				OrderId = OrderNumber,
				Dish = new Dish { Id = 3, Name = "Dish 3" },
				IsCompleted = true,
			},
		};

		[Test]
		public void Test() {
			var result = orderItems.GroupBy(x => x.Dish)
				.Select(d => new OrderItemViewModel2(d.Key, d.Count(), OrderNumber, d.Where(x => x.IsCompleted == false).Select(x => x.IsCompleted)))
				.ToArray();

			Assert.That(result.SingleOrDefault(x => x.Id == 1), Is.Not.Null);
			Assert.That(result.SingleOrDefault(x => x.Id == 1).Quantity, Is.EqualTo(2));
			Assert.That(result.SingleOrDefault(x => x.Id == 1).IsCompletedCollection.Count(), Is.EqualTo(1));

			Assert.That(result.SingleOrDefault(x => x.Id == 2), Is.Not.Null);
			Assert.That(result.SingleOrDefault(x => x.Id == 2).Quantity, Is.EqualTo(1));
			Assert.That(result.SingleOrDefault(x => x.Id == 2).IsCompletedCollection.Count(), Is.EqualTo(1));

			Assert.That(result.SingleOrDefault(x => x.Id == 3), Is.Not.Null);
			Assert.That(result.SingleOrDefault(x => x.Id == 3).Quantity, Is.EqualTo(1));
			Assert.That(result.SingleOrDefault(x => x.Id == 3).IsCompletedCollection, Is.Empty);
		}
	}

	public class Dish {
		public int Id { get; set; }
		public string Name { get; set; }
		/*....*/

		public override bool Equals(object? obj)
		{
			// TODO: Implement
			return false;
		}

		public override int GetHashCode()
		{
			// TODO: Implement
			return 0;
		}
	}

	public class OrderItem {
		public int Id { get; set; }
		public int OrderId { get; set; }
		public Dish Dish { get; set; }
		public bool IsCompleted { get; set; }
	}

	public class OrderItemViewModel2
	{
		private readonly Dish _dish;
		private readonly IEnumerable<bool> _isCompletedCollection;
		private readonly int _quantity;
		private readonly int _orderNumber;

		public int Id => _dish.Id;

		public string Name => _dish.Name;

		public int Quantity => _quantity;

		public int OrderNumber => _orderNumber;

		public IEnumerable<bool> IsCompletedCollection => _isCompletedCollection;

		public OrderItemViewModel2(Dish dish, int quantity)
		{
			_dish = dish;
			_quantity = quantity;
		}

		public OrderItemViewModel2(Dish dish, int quantity, int orderNumber, IEnumerable<bool> isCompletedCollection) : this(dish, quantity)
		{
			_orderNumber = orderNumber;
			_isCompletedCollection = isCompletedCollection;
		}
	}
}
