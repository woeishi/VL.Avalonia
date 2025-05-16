namespace VL.Avalonia.Attributes
{
    /// <summary>
    /// Implements two way Channel for a property.
    /// </summary>
    /// <typeparam name="T">Property Owner Class</typeparam>
    internal sealed class ImplementChannel<T> : Attribute
    {
        public string PropertyPath { get; }
        public ImplementChannel(string propertyPath)
        {
            PropertyPath = propertyPath;
        }
    }
}
