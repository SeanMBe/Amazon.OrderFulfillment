using System;
using System.Windows.Data;

namespace Amazon.OrderFulfillment.WPF.Converter
{
    public class TypeToResourceKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var formatString = parameter as string;
            
            return string.Format(formatString, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}