using Avalonia.Data.Converters;
using System.Globalization;

namespace VL.Avalonia.Helpers
{
    internal static class Converters
    {
        internal interface IConverter
        {
            public object Convert(object value);
            public object ConvertBack(object value);
        }

        internal class DoubleFloatConverter : IConverter
        {
            public object Convert(object value) => (float)value;
            public object ConvertBack(object value) => (double)value;

        }

        internal class DoubleFloatConverter2 : IValueConverter
        {
            public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => (double?)value;
            public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => (float?)value;
        }


    }


}
