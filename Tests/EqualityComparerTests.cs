using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Tests
{
	public class EqualityComparerTests
	{
		private const int OrderNumber = 567;

		[SetUp]
		public void Setup()
		{
		}

		private IEnumerable<OrderItem1> orderItems = new List<OrderItem1>() {
			new OrderItem1 { DishId = 1, DishName = "Dish 1", DishRecipe= string.Empty, DishIngredients = string.Empty, IsCompleted = true },
			new OrderItem1 { DishId = 1, DishName = "Dish 1", DishRecipe= string.Empty, DishIngredients = string.Empty, IsCompleted = false },
			new OrderItem1 { DishId = 2, DishName = "Dish 2", DishRecipe= string.Empty, DishIngredients = string.Empty, IsCompleted = false },
			new OrderItem1 { DishId = 3, DishName = "Dish 3", DishRecipe= string.Empty, DishIngredients = string.Empty, IsCompleted = true },
		};

		[Test]
		public void TestPart1_WithoutEqualityComparer()
		{
			var result = orderItems.GroupBy(d => d.DishId)
						.Select(d => new OrderItemViewModel1(d.FirstOrDefault(x => x.DishId == d.Key),
															d.Count(),
															OrderNumber,
															d.Where(x => x.IsCompleted == false && x.DishId == d.Key).Select(x => x.IsCompleted).ToList()))
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

		[Test]
		public void TestPart1_WithEqualityComparer()
		{
			var result = orderItems.GroupBy(d => d, new OrderItem1EqualityComparer())
						.Select(d => new OrderItemViewModel1(d.Key,
															d.Count(),
															OrderNumber,
															d.Where(x => x.IsCompleted == false).Select(x => x.IsCompleted)
			// !!!!												.ToList()
															))
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

		public class OrderItem1EqualityComparer : IEqualityComparer<OrderItem1>
		{
			public bool Equals(OrderItem1? x, OrderItem1? y)
			{
				// TODO: Implement
				return false;
			}

			public int GetHashCode([DisallowNull] OrderItem1 obj)
			{
				// TODO: Implement
				return 0;
			}
		}
	}

	public class OrderItem1 {
		public int DishId { get; set; }

		public string DishName { get; set; }

		public string DishRecipe { get; set; }

		public string DishIngredients { get; set; }

		// public int Quantity { get; set; }

		public bool IsCompleted { get; set; }
	}

	public class OrderItemViewModel1 
	{
		private readonly OrderItem1 _dish;
		private readonly IEnumerable<bool> _isCompletedCollection;
		private readonly int _quantity;
		private readonly int _orderNumber;

		public int Id => _dish.DishId;

		public string Name => _dish.DishName;

		public int Quantity => _quantity;

		public int OrderNumber => _orderNumber;

		public IEnumerable<bool> IsCompletedCollection => _isCompletedCollection;

		public OrderItemViewModel1(OrderItem1 dish, int quantity)
		{
			_dish = dish;
			_quantity = quantity;
		}

		public OrderItemViewModel1(OrderItem1 dish, int quantity, int orderNumber, IEnumerable<bool> isCompletedCollection) : this(dish, quantity)
		{
			_orderNumber = orderNumber;
			_isCompletedCollection = isCompletedCollection;
		}
	}
}