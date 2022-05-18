using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_Restaurant.Resources.Converters;

public class BooleanToVisibilityConverter : IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		var isCompletedCollection = (IEnumerable<bool>)values[0];

		if (!isCompletedCollection.Any())
		{
			return Visibility.Visible;
		}
		else
		{
			return Visibility.Hidden;
		}
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		return null;
	}
}