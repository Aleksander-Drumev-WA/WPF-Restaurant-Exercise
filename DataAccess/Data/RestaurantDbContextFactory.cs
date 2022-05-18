namespace WPF_Restaurant.DataAccess.Data;
public class RestaurantDbContextFactory
{
	private readonly string _connectionString;

	public RestaurantDbContextFactory(string connectionString)
	{
		_connectionString = connectionString;
	}

	public RestaurantDbContext CreateDbContext()
	{
		var options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

		return new RestaurantDbContext(options);
	}
}