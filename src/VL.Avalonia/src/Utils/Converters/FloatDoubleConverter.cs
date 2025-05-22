namespace VL.Avalonia.Utils.Converters
{
    internal static class FloatToDoubleConverter
    {
        public static float ConvertToField(double value) => (float)value;
        public static double ConvertToProperty(float value) => (double)value;
    }
}
