using System.Collections.Generic;
using System.Linq;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels.Common;

namespace WPF_Restaurant.ViewModels.Chef
{
    public class OrderItemViewModel : BaseViewModel
    {
        private readonly Dish _dish;
        private readonly IEnumerable<bool> _isCompletedCollection;
        private readonly int _quantity;
        private readonly int _orderNumber;

        public int Id => _dish.Id;

        public string Recipe => _dish.Recipe;

        public string Name => _dish.Name;

        public int Quantity => _quantity;

        public int OrderNumber => _orderNumber;

        public IEnumerable<bool> IsCompletedCollection => _isCompletedCollection;

        public int RenderCount => _isCompletedCollection.Count();

        public OrderItemViewModel(Dish dish, int quantity)
        {
            _dish = dish;
            _quantity = quantity;
        }

        public OrderItemViewModel(Dish dish, int quantity, int orderNumber, IEnumerable<bool> isCompletedCollection) : this(dish, quantity)
        {
            _orderNumber = orderNumber;
            _isCompletedCollection = isCompletedCollection;
        }
    }
}
