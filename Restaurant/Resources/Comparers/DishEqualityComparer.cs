using System.Diagnostics.CodeAnalysis;

namespace WPF_Restaurant.Resources.Comparers;

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
				 x.Ingredients == y.Ingredients)
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
		return HashCode.Combine(obj.Id, obj.Name, obj.Recipe, obj.Ingredients);
	}
}