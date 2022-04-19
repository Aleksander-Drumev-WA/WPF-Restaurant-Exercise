using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace WPF_Restaurant.Resources.Converters
{
	public class ImageToSourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var result = string.Empty;
			if (value is string path)
			{
				var exists = File.Exists(path);
				if (exists)
				{
					result = path;
				}
				else
				{
					result = Environment.CurrentDirectory + "\\Resources\\Images\\not-found-image.jpg";
				}
			}
			else
			{
				result = Environment.CurrentDirectory + "\\Resources\\Images\\not-found-image.jpg";
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
