global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
global using DataAccess.Abstractions;
global using WPF_Restaurant.Models;


namespace WPF_Restaurant.DataAccess.Data;
public class Restaurant
{
	private readonly IDishProvider _dishProvider;
	private readonly IOrderCreator _databaseOrderCreator;
	private readonly IOrderProvider _databaseOrdersProvider;

	public string Name { get; }

	public IDishProvider DishProvider => _dishProvider;

	public IOrderCreator OrderCreator => _databaseOrderCreator;

	public IOrderProvider OrdersProvider => _databaseOrdersProvider;

	public Restaurant(
		string name,
		IDishProvider dishProvider,
		IOrderCreator databaseOrderCreator,
		IOrderProvider databaseOrdersProvider)
	{
		Name = name;
		_dishProvider = dishProvider;
		_databaseOrderCreator = databaseOrderCreator;
		_databaseOrdersProvider = databaseOrdersProvider;
	}
}