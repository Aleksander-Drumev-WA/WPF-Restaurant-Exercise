using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.Resources.Comparers
{
    public class DishEqualityComparer : IEqualityComparer<Dish>
    {
        public bool Equals(Dish? x, Dish? y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else if (x.Id == y.Id &&
                     x.Name == y.Name &&
                     x.Recipe == y.Recipe &&
                     x.Ingredients == y.Ingredients &&
                     x.Quantity == y.Quantity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode([DisallowNull] Dish obj)
        {
            return HashCode.Combine(obj.Id, obj.Name, obj.Recipe, obj.Ingredients, obj.Quantity);
        }
    }
}
