using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF_Restaurant.Resources.Converters
{
    public class MyMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var dishes = new List<StackPanel>();

            if (values[0] is int renderCount && values[1] is StackPanel content)
            {
                for (int i = 0; i < renderCount; i++)
                {
                    dishes.Add(content);
                }
            }
            return dishes.Select(x => x.ToString());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
