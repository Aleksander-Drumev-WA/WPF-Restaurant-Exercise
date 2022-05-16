using Microsoft.EntityFrameworkCore.Design;

namespace WPF_Restaurant.DataAccess.Data;
public class RestaurantDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RestaurantDbContext>
{
	public RestaurantDbContext CreateDbContext(string[] args)
	{
		var options = new DbContextOptionsBuilder().UseSqlite("Data Source=restaurant.db").Options;

		return new RestaurantDbContext(options);
	}
}