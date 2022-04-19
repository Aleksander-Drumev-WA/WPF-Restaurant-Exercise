using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace WPF_Restaurant.Resources.Converters
{
	public class ImageToSourceConverter : IValueConverter
	{
		private static readonly string defaultPath = Environment.CurrentDirectory + "\\Resources\\Images\\not-found-image.jpg";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string? result = null;
			if (value is string path)
			{
				var exists = File.Exists(path);
				if (exists)
				{
					result = path;
				}
			}

			return result ?? defaultPath;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
